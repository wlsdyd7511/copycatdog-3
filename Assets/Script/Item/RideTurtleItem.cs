using System.Collections;
using System.Collections.Generic;
using JetBrains.Rider.Unity.Editor;
using UnityEngine;


public class RideTurtleItem : MonoBehaviour, IRideable
{
    private Character rider;
    public SpriteRenderer turtleSpriteRenderer;

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
    private void Update()
    {
        if (rider == null)
        {
            return;
        }

        // 캐릭터의 방향을 확인하여 거북이 스프라이트 방향 설정
        if (rider.playerDir == Character.Direction.Left)
        {
            turtleSpriteRenderer.flipX = false;
        }
        else if (rider.playerDir == Character.Direction.Right)
        {
            turtleSpriteRenderer.flipX = true;
        }
        // 앞, 뒤 방향에 따라 flipY를 설정해 거북이 이미지를 뒤집습니다.
        else if (rider.playerDir == Character.Direction.Up)
        {
            turtleSpriteRenderer.flipY = false;
        }
        else if (rider.playerDir == Character.Direction.Down)
        {
            turtleSpriteRenderer.flipY = true;
        }
    }

}
