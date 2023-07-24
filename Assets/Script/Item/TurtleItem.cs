using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//거북이 아이템 속도를 나타내는 열거형(enum)
public enum TurtleSpeed
{
    Fast,
    Slow
}

public class TurtleItem : MonoBehaviour,IItem
{
    private bool isDefending; // 거북이 아이템의 방어 기능
    private TurtleSpeed speed; //거북이 아이템 스피드

    //거북이 아이템의 속도를 결정하는 랜덤 함수
    private TurtleSpeed GetRandomSpeed()
    {
        System.Random r = new System.Random();
        int randomValue = Random.Range(0, 10);

        return randomValue < 2 ? TurtleSpeed.Fast : TurtleSpeed.Slow;
    } 

    //캐릭터가 거북이 아이템을 획득한 경우
    public void Get(Character Player)
    {
        Player.ApplyTurtleItemEffects(speed);
        Destroy(this.gameObject);
    }

    //물풍선에 맞아서 사라지는 거북이 아이템
    public void WaterBalloonBoom()
    {
        Destroy(this.gameObject);
    }

}
