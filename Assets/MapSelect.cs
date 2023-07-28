using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelect : MonoBehaviour
{
    public Image CheckMark; // 체크 표시 이미지 UI
    public GameObject MapSet;
    public int mapnum;// 맵 번호
 

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
        GameMgr.Instance.mapNum = mapnum;
        SetSelected(true);
    }


    private static Sprite selectedMapImage;
    
    // 다른 스크립트에서 현재 맵 이미지를 선택 상태로 설정하기 위한 함수
    public void SetSelected(bool selected)
    {
        isSelected = selected;
        CheckMark.gameObject.SetActive(selected);

        //선택한 맵 이미지 저장
        if (selected)
        {
            var sprite = GetComponent<Image>().sprite;
            selectedMapImage = sprite;
        }

        Debug.Log(gameObject.name + selected);

    }

    public static Sprite GetSelectedMap()
    {
        return selectedMapImage;
    }

}
