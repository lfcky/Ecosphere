using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameInterface : MonoBehaviour
{
    // 右上角的UI 数量统计
    public TMP_Text GrassCountText;
    public TMP_Text CowCountText;
    public TMP_Text TigerCountText;
    public static GameInterface Instance;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

    }
    public void UpdateAnimalsAmount(int grassCount, int cowCount, int tigerCount)
    {
        this.GrassCountText.text = grassCount.ToString();
        this.CowCountText.text = cowCount.ToString();
        this.TigerCountText.text = tigerCount.ToString();
    }
    // Update is called once per frame
    void Update()
    {

    }
}
