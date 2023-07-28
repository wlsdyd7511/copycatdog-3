using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RideUfoItem : MonoBehaviour, IRideable
{
    [SerializeField]
    private GameObject rideUfoItem;
    public void Ride(Character player)
    {
        // 캐릭터를 탑승 처리하고, 캐릭터 속도를 조정
        player.ApplyRideableItem(this);

        // Ufo 아이템을 캐릭터의 자식으로 설정
        transform.SetParent(player.transform);

        // Ufo 아이템을 아래로 조금 내려갈 위치로 설정
        Vector3 offset = new Vector3(0f, -0.5f, 0f); // 예시로 아래로 0.5만큼 내려가도록 설정
        transform.localPosition = offset;

    }
    public GameObject GetRideablePrefab()
    {
        return rideUfoItem;
    }
}
