using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RideTurtleItem : MonoBehaviour, IRideable
{
    public void Ride(Character player)
    {
        // 캐릭터를 탑승 아이템의 위치로 이동시킴
        player.transform.position = transform.position;
        // 캐릭터를 탑승 아이템의 자식 오브젝트로 설정하여 아이템의 움직임에 따라 함께 움직이도록 함
        player.transform.parent = transform;
        // 캐릭터의 탑승 상태 변수를 true로 설정
        player.isRidingItem = true;
    }

    public void Get(Character player)
    {
        Ride(player);

    }

    public GameObject GetRideablePrefab()
    {
        return gameObject; // 자기 자신의 프리팹 반환
    }
}
