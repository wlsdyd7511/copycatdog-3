using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class GameMgr : MonoBehaviour
{
    // 싱글톤 인스턴스를 저장할 변수
    private static GameMgr instance;
    public int mapNum = 0;
    public int modNum = 0;
    // 외부에서 접근 가능한 싱글톤 인스턴스
    public static GameMgr Instance
    {
        get { return instance; }
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

    public void GameStart() 
    {
        Map.instance.CreateMap(mapNum);
    }
 
}
