using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCnt : MonoBehaviour
{
    public Text[] ItemCntText = new Text[4];// 0 red needle 1 red shield 2 blue needle 3 blue shield
    void Update()
    {
        if (Map.instance.InGameplayers != null) {
            ItemCntText[0].text = Map.instance.InGameplayers[0].countNeedleItem + "";
            ItemCntText[1].text = Map.instance.InGameplayers[0].countShieldItem + "";
            ItemCntText[2].text = Map.instance.InGameplayers[1].countNeedleItem + "";
            ItemCntText[3].text = Map.instance.InGameplayers[1].countShieldItem + "";
        }
    }
}
