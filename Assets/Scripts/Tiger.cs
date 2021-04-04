using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiger : MonoBehaviour
{
    public Game gameCtrl;
    public float speed = 1f;
    private GameObject nearestBull = null;
    private float sleepTime = 0;

    [Range(0, 10f)] public float ChaseStrenth;

    private Vector2 Lerp_Dir;
    void Start()
    {

    }

    void Update()
    {
        if (sleepTime > 0)
        {
            sleepTime -= Time.deltaTime;
            return;
        }
        if (nearestBull == null)
        {
            nearestBull = gameCtrl.getNearestBull(transform.position);
        }
        if (nearestBull == null)
        {
            return;
        }
        Vector2 v2 = nearestBull.transform.position - transform.position;
        v2.Normalize();
        // GetComponent<Rigidbody2D>().velocity = speed * v2;

        // Lerp插值寻找牛
        Lerp_Dir = Vector2.Lerp(Lerp_Dir, v2, ChaseStrenth / 100);
        Lerp_Dir.Normalize();
        GetComponent<Rigidbody2D>().velocity = speed * Lerp_Dir;
        transform.rotation = v2.x < 0 ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, -180f, 0);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bull")
        {
            collision.gameObject.SendMessage("capture");
            sleepTime = 2;
        }
    }
}
