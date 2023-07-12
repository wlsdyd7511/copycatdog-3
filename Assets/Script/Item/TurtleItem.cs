using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurtleSpeed
{
    Fast,
    Slow
}

public class TurtleItem : IItem
{
    private bool isDefending;
    private TurtleSpeed speed;

    private TurtleSpeed GetRandomSpeed()//거북이 아이템의 속도를 결정하는 랜덤 함수
    {
        System.Random r = new System.Random();
        int randomValue = Random.Range(0, 10);

        return randomValue < 2 ? TurtleSpeed.Fast : TurtleSpeed.Slow;
    } 

    public void Get(Character player)
    {
        if (speed == TurtleSpeed.Fast)
        {
            //캐릭터가 빠른 거북이 아이템을 획득
        }
        else
        {
            //캐릭터가 느린 거북이 아이템을 획득
        }
    }   

    //물풍선 맞을 때 함수 구현해야함

}
