using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RideTurtleItem : MonoBehaviour, IRideable
{
    [SerializeField]
    private GameObject rideTurtleFast; // 빠른 거북이 아이템 프리팹
    [SerializeField]
    private GameObject rideTurtleSlow; // 느린 거북이 아이템 프리팹


    public void Ride(Character player)
    {
        // 캐릭터를 탑승 처리하고, 캐릭터 속도를 조정
        player.ApplyRideableItem(this);

        // 거북이 아이템을 캐릭터의 자식으로 설정
        transform.SetParent(player.transform);

        // 거북이 아이템을 아래로 조금 내려갈 위치로 설정
        Vector3 offset = new Vector3(0f, -0.5f, 0f); // 예시로 아래로 0.5만큼 내려가도록 설정
        transform.localPosition = offset;

    }

    public GameObject GetRideablePrefab(Character player)
    {
        // 캐릭터의 현재 속도에 따라 적절한 프리팹 반환
        if (player.ridingSpeed == 9f)
        {
            return rideTurtleFast;
        }

        else
        {
            return rideTurtleSlow;
        }
    }
}
