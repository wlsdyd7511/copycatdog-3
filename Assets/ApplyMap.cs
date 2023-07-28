using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingRoom : MonoBehaviour
{
    public Image ConfirmedMap; // 대기실에 띄울 맵 이미지를 보여줄 UI Image

    void Start()
    {
        // 대기실 시작 시 선택한 맵 이미지를 불러와서 대기실에 표시
        Sprite selectedMap = MapSelect.GetSelectedMap();
        if (selectedMap != null)
        {
            ConfirmedMap.sprite = selectedMap;
        }
    }
}
