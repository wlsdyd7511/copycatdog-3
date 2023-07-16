using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldItem : MonoBehaviour, IItem
{

    //방패 아이템을 캐릭터에게 적용하는 함수
    public void Get(Character Player)
    {
        Player.ApplyShieldItemEffects();
        Destroy(this.gameObject);

    }

    //물풍선에 맞아서 사라지는 방패 아이템
    public void WaterBalloonBoom()
    {
        Destroy(this.gameObject);
    }
}
