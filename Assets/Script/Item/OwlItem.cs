using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlItem : MonoBehaviour, IItem
{
    //캐릭터가 부엉이 아이템을 획득한 경우
    public void Get(Character Player)
    {
        Player.ApplyOwlItemEffects();
        Destroy(this.gameObject);
    }

    //물풍선에 맞아서 사라지는 부엉이 아이템
    public void WaterBalloonBoom()
    {
        Destroy(this.gameObject);
    }

}
