using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RideOwlItem : MonoBehaviour, IRideable
{
    private Character rider;
    private SpriteRenderer owlSpriteRenderer;
    [SerializeField]
    private Sprite[] owlSprite;

    private void Start()
    {
        owlSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Ride(Character player)
    {
        // 캐릭터를 탑승 처리하고, 캐릭터 속도를 조정
        rider = player;
        rider.ApplyRideableItem(this);

        // 부엉이 아이템을 캐릭터의 자식으로 설정
        transform.SetParent(player.transform);

        // 부엉이 아이템을 아래로 조금 내려갈 위치로 설정
        Vector3 offset = new Vector3(0f, -0.5f, 0f); // 예시로 아래로 0.5만큼 내려가도록 설정
        transform.localPosition = offset;

    }

    private void Update()
    {
        if (rider == null)
        {
            return;
        }

        owlSpriteRenderer.sprite = owlSprite[(int)rider.playerDir];
    }
}
