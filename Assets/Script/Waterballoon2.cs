using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBalloon 
{
    public int Power;

    float TimeSpan;  //경과 시간을 갖는 변수
    float CheckTime;  // 특정 시간을 갖는 변수

    public int WaterballoonBoom(Power)
    {
        
    }

    void Start()
    {
        TimeSpan = 0.0f; // 경과시간 초기화
        CheckTime = 5.0f;  // 특정시간을 5초로 지정
    }

    void Update()
    {
        TimeSpan += Time.deltaTime;  // 경과 시간을 계속 등록
        if (TimeSpan == CheckTime)  // 경과 시간이 5초인 경우
        {
            WaterballoonBoom(Power);//물풍선이 범위만큼 터짐
            TimeSpan = 0;
        }
    }
}


