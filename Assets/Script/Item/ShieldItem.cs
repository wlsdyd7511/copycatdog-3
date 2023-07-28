using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldItem : MonoBehaviour, IItem
{

    //방패 아이템을 캐릭터에게 적용하는 함수
    public void Get(Character Player)
    {
        Player.EquipShieldItem();
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

    //물풍선에 맞아서 사라지는 방패 아이템
    public void WaterBalloonBoom()
    {
        Destroy(this.gameObject);
    }
}
