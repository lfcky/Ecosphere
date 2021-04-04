using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public int Grass_Init_Amount = 60;
    public int Cow_Init_Amount = 10;
    public int Tiger_Init_Amount = 2;
    public GameObject bullObj;
    public GameObject tigerObj;
    public GameObject[] grassObjList;
    public GameObject ground;
    public GameInterface gameInterface;

    private List<GameObject> cowInstanceList = new List<GameObject>();
    private List<GameObject> tigerInstanceList = new List<GameObject>();
    private List<GameObject> grassInstanceList = new List<GameObject>();
    float randomY()
    {
        return Random.Range(-7f, 7f);
    }
    float randomX()
    {
        return Random.Range(-9f, 9f);
    }
    float getDirection()
    {
        return Random.Range(0, 2) * 180;
    }
    GameObject getRandomGrassObject()
    {
        return grassObjList[Random.Range(0, 15)];
    }
    void Start()
    {
        // 生成草
        for (int i = 0; i < Grass_Init_Amount; i++)
        {
            GameObject grass = Instantiate(getRandomGrassObject(), new Vector3(ground.transform.position.x + randomX(), ground.transform.position.y + randomY(), 0), Quaternion.Euler(0, getDirection(), 0), ground.transform);
            grass.AddComponent<Grass>();
            grass.GetComponent<Grass>().gameCtrl = this;
            grassInstanceList.Add(grass);
        }
        // 生成牛
        for (int i = 0; i < Cow_Init_Amount; i++)
        {
            GameObject bull = Instantiate(bullObj, new Vector3(ground.transform.position.x + randomX(), ground.transform.position.y + randomY(), 0), Quaternion.Euler(0, getDirection(), 0), ground.transform);
            bull.GetComponent<Bull>().gameCtrl = this;
            cowInstanceList.Add(bull);
        }
        // 生成老虎
        for (int i = 0; i < Tiger_Init_Amount; i++)
        {
            GameObject tiger = Instantiate(tigerObj, new Vector3(ground.transform.position.x + randomX(), ground.transform.position.y + randomY(), 0), Quaternion.Euler(0, getDirection(), 0), ground.transform);
            tiger.GetComponent<Tiger>().gameCtrl = this;
            tigerInstanceList.Add(tiger);
        }
        updateAnimalsCount();
    }

    void Update()
    {
    }

    private void updateAnimalsCount()
    {
        // 设置全局动物的数量
        gameInterface.UpdateAnimalsAmount(grassInstanceList.Count, cowInstanceList.Count, tigerInstanceList.Count);
    }

    public GameObject getNearestBull(Vector2 v2)
    {
        if (cowInstanceList.Count == 0)
            return null;
        float distance = Vector2.Distance(cowInstanceList[0].transform.position, v2);
        GameObject minObj = cowInstanceList[0];
        foreach (GameObject bull in cowInstanceList)
        {
            float currentDis = Vector2.Distance(bull.transform.position, v2);
            if (currentDis < distance)
            {
                distance = currentDis;
                minObj = bull;
            }
        }
        return minObj;
    }

    public void removeBull(GameObject bull)
    {
        cowInstanceList.Remove(bull);
        updateAnimalsCount();
    }
}
