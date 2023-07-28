using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoItem : MonoBehaviour, IItem
{

    [SerializeField]
    private GameObject RideUfoItem;
    
    //캐릭터가 Ufo 아이템을 획득한 경우
    public void Get(Character Player)
    {
        if (!Player.isRidingItem)// 플래이어가 타고있지 않을때
        {
            Player.ApplyUfoItemEffects();
            //탑승 처리
            RideUfoItem rideUfoItem = Instantiate(RideUfoItem).GetComponent<RideUfoItem>();
            rideUfoItem.Ride(Player);
        }

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

    //물풍선에 맞아서 사라지는 Ufo 아이템
    public void WaterBalloonBoom()
    {
        Destroy(this.gameObject);
    }
}
