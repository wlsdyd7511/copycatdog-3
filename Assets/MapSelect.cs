using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelect : MonoBehaviour
{
    public Image CheckMark; // 체크 표시 이미지 UI
    public GameObject MapSet;

    private bool isSelected = false; // 맵 선택 여부

    // 맵 이미지 클릭 시 호출되는 함수
    public void OnMapImageClicked()
    {
        // 현재 선택된 맵 이미지와 다른 맵 이미지들의 체크 표시를 모두 해제
        MapSelect[] mapSelects = FindObjectsOfType<MapSelect>();
        foreach (MapSelect mapSelect in mapSelects)
        {
            mapSelect.SetSelected(false);
        }

        // 현재 선택된 맵 이미지에 체크 표시 활성화
        SetSelected(true);
    }

    // 다른 스크립트에서 현재 맵 이미지를 선택 상태로 설정하기 위한 함수
    public void SetSelected(bool selected)
    {
        isSelected = selected;
        CheckMark.gameObject.SetActive(selected);
        
    }

    // 맵 선택 확인 버튼 클릭 시 호출되는 함수
    public void OnCheckButtonClicked()
    {
        // 선택한 맵 이미지를 대기실에 표시
        if (isSelected)
        {
            // 여기서 GameMgr 스크립트에 있는 SetSelectedMap 함수를 호출하여 선택한 맵 이미지를 설정

            // 맵 선택창 닫기
            MapSet.SetActive(false);
        }
    }
}
