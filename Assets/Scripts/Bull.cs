using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bull : MonoBehaviour
{
    public Game gameCtrl;
    public float speed = 0.5f;
    public GameObject collectedEffect;

    public GameObject shadowEffect;
    private SpriteRenderer _spriteRenderer;

    private BoxCollider2D _boxCollider2D;

    private bool isMoving = true;
    private bool isEscaping = false;
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void capture()
    {
        // 取消外层显示
        _spriteRenderer.enabled = false;
        _boxCollider2D.enabled = false;
        shadowEffect.GetComponent<SpriteRenderer>().enabled = false;
        // 播放动画
        collectedEffect.SetActive(true);
        isMoving = false;
        // 销毁牛对象
        gameCtrl.removeBull(gameObject);
        Destroy(gameObject, 0.35f); // 延迟以便播放动画
    }
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "tiger" && !collider.isTrigger)
        {
            isEscaping = true;
            Vector2 v2 = transform.position - collider.transform.position;
            v2.Normalize();
            if (!isMoving) v2 = new Vector2(0, 0);  // collected动画不需要移动
            GetComponent<Rigidbody2D>().velocity = speed * v2;
            transform.rotation = v2.x < 0 ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, -180f, 0);
        }
        if (collider.gameObject.tag == "grass" && !isEscaping && !collider.isTrigger)
        {
            collider.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            // 接下来要销毁草 并且草的数量减一
        }
    }
}
