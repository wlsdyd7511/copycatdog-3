using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

public class Character : MonoBehaviour, IWallBoom
{
    [SerializeField]
    private GameObject waterBalloonPrefab;
    public int waterBalloonPower;
    public int waterBalloonMaxCount = 1;
    private int currentWaterBalloons = 0;
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private bool isTrapped = false;
    private Waterballoon tempWaterBalloon;
    private Vector3 waterBalloonPos;
    public bool isTurtleItem = false; //캐릭터의 거북이 아이템 유무 여부


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveX, moveY).normalized; //대각선 벡터 제한

        rb.velocity = movement * moveSpeed;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && currentWaterBalloons < waterBalloonMaxCount)
        {
            //  Debug.Log("press");
            SpawnWaterBalloon();
            currentWaterBalloons++;
        }
    }

    void OnTriggerEnter2D(Collider2D obj)
    {

        if (obj.tag == "Attack")
        {
            WaterBalloonBoom();
        }
    }


    void SpawnWaterBalloon()
    {

        waterBalloonPos = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), 0);
        //   Debug.Log(waterBalloonPos);
        tempWaterBalloon = Instantiate(waterBalloonPrefab, waterBalloonPos, Quaternion.identity).GetComponent<Waterballoon>();
        tempWaterBalloon.Power = waterBalloonPower;
        tempWaterBalloon.Player = this;
        tempWaterBalloon.Position = new int[2] { (int)waterBalloonPos.x + 7, 7 - (int)waterBalloonPos.y };
    }

    public void WaterBalloonExploded()
    {
        currentWaterBalloons--;
    }

    //캐릭터에 거북이 아이템 효과 적용 함수
    public void ApplyTurtleItemEffects(TurtleSpeed speed)
    {
        isTurtleItem = true;

        if (speed == TurtleSpeed.Fast)
        {
            // 빠른 거북이 아이템의 효과를 적용
        }
        else
        {
            // 느린 거북이 아이템의 효과를 적용
        }
    }

    //캐릭터가 물풍선에 맞은 경우를 구현한 함수

    public void WaterBalloonBoom()
    {
        Debug.Log("맞았다");
        if (!isTrapped)
        {
            isTrapped = true;
        }

        //거북이를 탄 경우
        if (isTurtleItem)
        {

        }

        //바늘 아이템을 가진 경우

        else
        {
            //캐릭터 삭제
            //질문: 일정 시간이 경과후 캐릭터가 삭제되도록 할까요??
            Destroy(this.gameObject);
        }

    }

}
