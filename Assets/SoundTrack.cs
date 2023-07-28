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
 
    
}
