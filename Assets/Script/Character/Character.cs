using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

public class Character : MonoBehaviour, IWallBoom
{
    [SerializeField]
    private GameObject waterBalloonPrefab;
    public int waterBalloonPower;
    public int waterBalloonMaxCount = 1;
    public int currentWaterBalloons = 0;
    public float moveSpeed = 5f;
    private bool isTrapped = false;
    private Waterballoon tempWaterBalloon;
    private Vector3 waterBalloonPos;
    private Vector3 moveDirect;
    private float preMoveSpeed;
    private Map map;

    //아이템 유무와 관련된 변수들    
    int countNeedleItem = 0;//바늘 아이템 개수
    public bool isShieldItem = false;//방패 아이템 유무 여부
    public bool isTurtleItem = false; //거북이 아이템 유무 여부
    public bool isUfoItem = false; //Ufo 아이템 유무 여부
    public bool isOwlItem = false; //부엉이 아이템 유무 여부

    //바늘 아이템 사용 여부와 관련된 변수
    public bool canEscape = false;

    //방패 보호 상태와 관련된 변수들
    private bool isShieldProtected = false;
    private float shieldProtectionTime = 5f;
    private float shieldProtectionTimer = 0f;

    //WaterBalloonBoom 함수와 관련된 변수들
    private float needleItemDelay = 5f;
    private float deathDelay = 10f;
    private float timer = 0f;


    void Start()
    {
        map = GameObject.FindGameObjectWithTag("Manager").GetComponent<Map>();
        preMoveSpeed = moveSpeed;
    }

    void FixedUpdate()
    {
        transform.Translate(moveDirect * Time.deltaTime);
    }

    void Update()
    {
        if (preMoveSpeed != moveSpeed)
        {
            moveDirect = moveDirect.normalized * moveSpeed;
        }
        preMoveSpeed = moveSpeed;

        if (Input.GetKeyDown(KeyCode.Z) && currentWaterBalloons < waterBalloonMaxCount)
        {
            SpawnWaterBalloon();
            currentWaterBalloons++;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveDirect = Vector3.left * moveSpeed;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveDirect = Vector3.right * moveSpeed;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            moveDirect = Vector3.up * moveSpeed;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            moveDirect = Vector3.down * moveSpeed;
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) && moveDirect == Vector3.left * moveSpeed)
        {
            moveDirect = Vector3.zero;
            KeyPushCheck();
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow) && moveDirect == Vector3.right * moveSpeed)
        {
            moveDirect = Vector3.zero;
            KeyPushCheck();
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow) && moveDirect == Vector3.up * moveSpeed)
        {
            moveDirect = Vector3.zero;
            KeyPushCheck();
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow) && moveDirect == Vector3.down * moveSpeed)
        {
            moveDirect = Vector3.zero;
            KeyPushCheck();
        }

        if (Input.GetKeyDown(KeyCode.X) && countNeedleItem > 0)
        {
            UseNeedleItem();
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
        if (map.mapArr[(int)waterBalloonPos.x + 7, 7 - (int)waterBalloonPos.y] == 3)
        {
            currentWaterBalloons--;
            return;
        }
        //   Debug.Log(waterBalloonPos);
        tempWaterBalloon = Instantiate(waterBalloonPrefab, waterBalloonPos, Quaternion.identity).GetComponent<Waterballoon>();
        tempWaterBalloon.Power = waterBalloonPower;
        tempWaterBalloon.Player = this;
        tempWaterBalloon.Position = new int[2] { (int)waterBalloonPos.x + 7, 7 - (int)waterBalloonPos.y };
        map.mapArr[(int)waterBalloonPos.x + 7, 7 - (int)waterBalloonPos.y] = 3;
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

    //바늘 아이템 사용하는 함수
    void UseNeedleItem()
    {
        countNeedleItem--;
        canEscape = true;
    }

    //캐릭터에 방패 아이템 효과 적용 함수
    public void ApplyShieldItemEffects()
    {
        isShieldItem = true;

    }


    //캐릭터에 거북이 아이템 효과 적용 함수
    public void ApplyTurtleItemEffects()
    {

        isTurtleItem = true;


    }

    //캐릭터에 Ufo 아이템 효과 적용 함수
    public void ApplyUfoItemEffects()
    {
        isUfoItem = true;

    }

    //캐릭터에 부엉이 아이템 효과 적용 함수
    public void ApplyOwlItemEffects()
    {
        isOwlItem = true;
    }

    //캐릭터가 물풍선에 맞은 경우를 구현한 함수

    public void WaterBalloonBoom()
    {
        Debug.Log("맞았다");


        //거북이를 탄 경우
        if (isTurtleItem)
        {
            isTurtleItem = false;
        }

        //방패 아이템이 있는 경우
        else if (isShieldItem)
        {
            //방패를 보호 상태로 변경하고 타이머 시작
            isShieldProtected = true;
            shieldProtectionTimer = 0f;

            //5초가 지나면 보호 상태가 아님
            shieldProtectionTimer += Time.deltaTime;
            if (shieldProtectionTimer >= shieldProtectionTime)
            {
                isShieldProtected = false;
            }

            //방패 아이템 삭제
            isShieldItem = false;
        }

        //그 외의 경우에는 물풍선에 갇힘
        else
        {
            isTrapped = true;
            timer += Time.deltaTime;

            //바늘 아이템이 있는 경우
            if (countNeedleItem > 0 && timer >= needleItemDelay && canEscape)
            {
                Debug.Log("바늘 아이템을 이용해 물풍선 탈출!");

                //물풍선을 탈출함
                isTrapped = false;
                timer = 0f;
                canEscape = false;
            }

            //바늘 아이템이 없는 경우
            else
            {
                Debug.Log("GameOver");
            }
        }



    }

    public void KeyPushCheck()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveDirect = Vector3.left * moveSpeed;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            moveDirect = Vector3.right * moveSpeed;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            moveDirect = Vector3.up * moveSpeed;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            moveDirect = Vector3.down * moveSpeed;
        }
    }

}
