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
    int countNeedleItem = 0;//바늘 아이템 개수
    public bool isShieldItem = false;//방패 아이템 유무 여부

    //WaterBalloonBoom 함수와 관련된 변수들
    private float needleItemDelay = 5f;
    private float deathDelay = 10f;
    private float timer = 0f;


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

    //캐릭터가 바늘 아이템 획득하는 함수
    public void EquipNeedleItem()
    {
        countNeedleItem++;
    }

    //캐릭터에 방패 아이템 효과 적용 함수
    public void ApplyShieldItemEffects()
    {
        isShieldItem = true;
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
            //거북이를 탄 경우
            if (isTurtleItem)
            {
                isTurtleItem = false;
            }

            //방패 아이템이 있는 경우
            else if (isShieldItem)
            {

                isShieldItem = false;
            }

            //그 외의 경우에는 물풍선에 갇힘
            else
            {
                isTrapped = true;
                timer += Time.deltaTime;

                //바늘 아이템이 있는 경우
                if (countNeedleItem > 0 && timer >= needleItemDelay)
                {
                    Debug.Log("바늘 아이템을 이용해 물풍선 탈출!");

                    //물풍선을 탈출함
                    isTrapped = false;
                    countNeedleItem--;
                    timer = 0f;
                }

                //바늘 아이템이 없는 경우
                else if (countNeedleItem <= 0 && timer >= deathDelay)
                {
                    Debug.Log("GameOver");
                }
            }

        }

    }

}
