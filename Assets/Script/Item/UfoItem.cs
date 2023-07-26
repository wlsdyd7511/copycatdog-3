using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoItem : MonoBehaviour, IItem
{
    //캐릭터가 Ufo 아이템을 획득한 경우
    public void Get(Character Player)
    {
        Player.ApplyUfoItemEffects();
        Destroy(this.gameObject);
    }
    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.tag == "Player")
        {
            Character player = obj.GetComponent<Character>();
            if (player != null)
            {
                Get(player);
            }
        }
        else if (obj.tag == "Attack")
        {
            WaterBalloonBoom();
        }
    }

    //물풍선에 맞아서 사라지는 Ufo 아이템
    public void WaterBalloonBoom()
    {
        Destroy(this.gameObject);
    }
}
