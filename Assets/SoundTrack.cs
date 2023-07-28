using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrack : MonoBehaviour
{
    // 싱글톤 인스턴스를 저장할 변수
    public SoundTrack instance;
 
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

    // 게임 상태와 로직 관련 변수와 함수들을 추가하여 게임을 관리할 수 있음
    // 예시: 게임 시작, 게임 종료, 점수 관리, 플레이어 상태 등
}
