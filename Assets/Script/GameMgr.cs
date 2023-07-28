using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 
public class GameMgr : MonoBehaviour
{
    // 싱글톤 인스턴스를 저장할 변수
    private static GameMgr instance;

    // 외부에서 접근 가능한 싱글톤 인스턴스
    public static GameMgr Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<GameMgr>();
            }
            return instance;
        }
    }

    //선택한 맵과 모드 설정 변수들
    private string selectedMap;
    private string selectedMode;

    //선택한 맵과 모드를 인게임 씬에서 사용할 수 있도록 전달하는 함수
    public void SetSelectedMapAndMode(string map, string mode)
    {
        selectedMap = map;
        selectedMode = mode;
    }

    //선택한 맵과 모드에 따른 인게임 씬 로드
    public void LoadInGame()
    {
        string InGameName = DetermineInGameName(selectedMap, selectedMode);

        if(!string.IsNullOrEmpty(InGameName))
        {
            SceneManager.LoadScene(InGameName);
        }
        else
        {
            Debug.LogError("Could not determine the In-game to load.");
        }
    }

    private string DetermineInGameName(string map, string mode)
    {
        string InGameName = map + "_" + mode + "_Scene";
        return InGameName;
    }

    private void Awake()
    {
        // 인스턴스가 이미 존재하는지 확인하고, 존재하면 중복 생성 방지
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject); // 다른 씬으로 이동해도 오브젝트 유지
    }
 
}
