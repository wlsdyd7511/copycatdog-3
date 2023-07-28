using System.Collections;
using System.Collections.Generic;
using JetBrains.Rider.Unity.Editor;
using UnityEngine;


public class RideTurtleItem : MonoBehaviour, IRideable
{
    private Character rider;  

    public void Ride(Character player)
    {
        // 캐릭터를 탑승 처리하고, 캐릭터 속도를 조정
        rider = player;
        rider.ApplyRideableItem(this);        

        // 거북이 아이템을 캐릭터의 자식으로 설정
        transform.SetParent(player.transform);

        // 거북이 아이템을 아래로 조금 내려갈 위치로 설정
        Vector3 offset = new Vector3(0f, -0.5f, 0f); // 예시로 아래로 0.5만큼 내려가도록 설정
        transform.localPosition = offset;

    }

    private void LateUpdate()
    {
        // 거북이를 탄 경우, 캐릭터와 같은 방향으로 회전
        if (rider != null)
        {
            // 캐릭터의 방향을 읽어와서 거북이 방향을 업데이트
            transform.rotation = Quaternion.Euler(0, 0, -(int)rider.playerDir * 90);
        }
    }

    public void Dismount()
    {
        rider = null;
    }

}
