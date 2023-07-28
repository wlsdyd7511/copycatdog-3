using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RideTurtleItem : MonoBehaviour, IRideable
{
    [SerializeField]
    private GameObject rideablePrefab; // 탑승 아이템 프리팹


    public void Ride(Character player)
    {
        // 캐릭터를 탑승 처리하고, 캐릭터 속도를 조정
        player.ApplyRideableItem(this);


        // 라이드터틀아이템을 캐릭터의 자식으로 설정
        transform.SetParent(player.transform);

    }

    public GameObject GetRideablePrefab()
    {
        return rideablePrefab.gameObject; // 자기 자신의 프리팹 반환
    }
}
