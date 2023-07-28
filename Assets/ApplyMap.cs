using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingRoom : MonoBehaviour
{
    public Image ConfirmedMap; // 대기실에 띄울 맵 이미지를 보여줄 UI Image
    private Sprite previousMap; // 이전에 선택된 맵 이미지를 저장할 변수

    void Start()
    {
        // ConfirmedMap = GetComponent<Image>();
        // 이전에 저장된 맵 이미지 초기화
        previousMap = null;
    }

    void Update()
    {
        // 대기실 시작 시 선택한 맵 이미지를 불러와서 대기실에 표시
        Sprite selectedMap = MapSelect.GetSelectedMap();
        // 이전에 선택한 맵과 현재 선택한 맵이 다른 경우에만 새로운 맵 이미지를 대기실에 표시
        if (selectedMap != null && selectedMap != previousMap)
        {
            ConfirmedMap.sprite = selectedMap;
            // 현재 선택한 맵을 이전에 저장된 맵으로 갱신
            previousMap = selectedMap;
        }
    }
}
 




