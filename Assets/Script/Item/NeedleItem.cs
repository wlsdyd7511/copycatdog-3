using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NeedleItem : MonoBehaviour, IItem
{
    //캐릭터가 바늘 아이템을 획득하는 함수
    public void Get(Character Player)
    {
        Player.EquipNeedleItem(); // 바늘 아이템을 캐릭터에게 적용
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
    private void Start()
    {
        // BoxCollider2D 컴포넌트 추가
        BoxCollider2D boxCollider = gameObject.AddComponent<BoxCollider2D>();
        // Is Trigger 옵션을 체크
        boxCollider.isTrigger = true;
    }

    //물풍선이 터져서 바늘 아이템이 사라지는 경우
    public void WaterBalloonBoom()
    {
        Destroy(this.gameObject);
    }

}

