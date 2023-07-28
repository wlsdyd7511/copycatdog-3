using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using System.Runtime.CompilerServices;
using System.Linq;
using Unity.VisualScripting;

public class Character : MonoBehaviour, IWallBoom
{
    public enum Direction { Left, Up, Right, Down }; // 0 = 왼쪽, 1 = 위, 2 = 오른쪽, 3 = 아래,
    [SerializeField]
    private GameObject waterBalloonPrefab;
    [SerializeField]
    private FrontChecker frontChecker;
    [SerializeField]
    private GameObject shieldAnimation;
    private GameObject frontCheckerObject;
    public int waterBalloonPower;
    public int waterBalloonMaxCount = 1;
    public int currentWaterBalloons = 0;
    public float moveSpeed = 5f;
    public float inWaterSpeed = 1f; // 물풍선에 갇혔을때 속도
    private bool isTrapped = false;
    private Waterballoon tempWaterBalloon;
    private Vector3 waterBalloonPos;
    private Vector3 moveDirect;
    private float preMoveSpeed;
    public bool canPush = false;
    public bool canThrow = false;
    public Direction playerDir = Direction.Left;
    public float pushTime = 0;
    public bool infinitymod = false;
    public int limitCount = 6;
    public int limitPower = 7;
    public int limitSpeed = 8;

    public KeyCode[] playerKey; // 0 = 왼쪽, 1 = 위, 2 = 오른쪽, 3 = 아래, 4 = 물풍선설치, 5 = 아이템 사용

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public int playerNum;//1p 2p 구별용
    private Ending ending;

    //아이템 유무와 관련된 변수들    private 변경
    public int countNeedleItem = 0;//바늘 아이템 개수
    public int countShieldItem = 0;//방패 아이템 개수

    //바늘 아이템 사용 여부와 관련된 변수
    public bool canEscape = false;

    //방패 보호 상태와 관련된 변수들
    public bool isShieldItem = false; //방패 아이템이 켜져 있는가
    private float shieldProtectionTime = 5f;
    private float shieldProtectionTimer = 0f;

    //캐릭터가 물풍선에 갇혀있는시간
    public float timer = 0f;

    //탑승 아이템 관련 변수들
    public bool isRidingItem = false;
    private IRideable currentRideable; // 현재 탑승 중인 아이템
    private IRideable realCurrentRideable;

    //탑승 아이템 속도 관련 변수
    public float ridingSpeed;

    //캐릭터 기본 속도
    public float characterSpeed = 5f;

    //캐릭터 공격당함 여부
    public bool isAttacked = false;

    void Start()
    {
        frontCheckerObject = frontChecker.gameObject;
        preMoveSpeed = moveSpeed;
        animator = this.GetComponent<Animator>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        if(GameMgr.Instance.modNum == 1)
        {
            limitCount = 100;
            limitPower = 100;
            limitSpeed = 100;
            waterBalloonMaxCount = 100;
            waterBalloonPower = 100;
            moveSpeed = 10;
        }
        ending = GameObject.FindWithTag("InGameUI").GetComponent<Ending>();
    }

    void FixedUpdate()
    {
        frontCheckerObject.transform.rotation = Quaternion.Euler(0,0,-90 * (int)playerDir);
        if (isTrapped)
        {
            transform.Translate(moveDirect / moveSpeed * Time.deltaTime * inWaterSpeed);
            spriteRenderer.sortingOrder = 8 - (int)Mathf.Round(transform.position.y);
            timer += Time.deltaTime;
            if(timer >= 7f)
            {
                animator.SetBool("Die", true);
                GameOver(playerNum);
            }
        }
        else
        {
            transform.Translate(moveDirect * Time.deltaTime);
            spriteRenderer.sortingOrder = 8 - (int)Mathf.Round(transform.position.y);
        }

        if (isShieldItem)
        {
            shieldProtectionTimer += Time.deltaTime;
            if(shieldProtectionTimer >= shieldProtectionTime)
            {
                shieldAnimation.SetActive(false);
                isShieldItem = false;
            }
        }


    }

    void Update()
    {
        if (preMoveSpeed != moveSpeed)
        {
            moveDirect = moveDirect.normalized * moveSpeed;
        }
        preMoveSpeed = moveSpeed;

        if (Input.GetKeyDown(playerKey[4]))
        {
            SpawnWaterBalloon();
        }

        if (Input.GetKeyDown(playerKey[0]))
        {
            moveDirect = Vector3.left * moveSpeed;
            playerDir = Direction.Left;
            editAnimator(true);
        }
        else if (Input.GetKeyDown(playerKey[2]))
        {
            moveDirect = Vector3.right * moveSpeed;
            playerDir = Direction.Right;
            editAnimator(true);
        }
        else if (Input.GetKeyDown(playerKey[1]))
        {
            moveDirect = Vector3.up * moveSpeed;
            playerDir = Direction.Up;
            editAnimator(true);
        }
        else if (Input.GetKeyDown(playerKey[3]))
        {
            moveDirect = Vector3.down * moveSpeed;
            playerDir = Direction.Down;
            editAnimator(true);
        }

        if (Input.GetKeyUp(playerKey[0]) && moveDirect == Vector3.left * moveSpeed)
        {
            moveDirect = Vector3.zero;
            KeyPushCheck();
        }
        else if (Input.GetKeyUp(playerKey[2]) && moveDirect == Vector3.right * moveSpeed)
        {
            moveDirect = Vector3.zero;
            KeyPushCheck();
        }
        else if (Input.GetKeyUp(playerKey[1]) && moveDirect == Vector3.up * moveSpeed)
        {
            moveDirect = Vector3.zero;
            KeyPushCheck();
        }
        else if (Input.GetKeyUp(playerKey[3]) && moveDirect == Vector3.down * moveSpeed)
        {
            moveDirect = Vector3.zero;
            KeyPushCheck();
        }


  
        if (Input.GetKeyDown(playerKey[5]))
        {
            if (isTrapped && countNeedleItem > 0)
            {
                UseNeedleItem();
            }
            else if (!isTrapped && countShieldItem > 0)
            {
                Debug.Log("shield");
                UseShieldItem();
            }
        }
    }

    void LateUpdate()
    {
        isAttacked = false;
    }

    void OnTriggerEnter2D(Collider2D obj)
    {

        if (obj.tag == "Attack" && !isTrapped && !isAttacked)
        {
            WaterBalloonBoom();
            isAttacked = true;
        }

        else if(obj.tag == "PlayerAttack")
        {
            Character enemy = obj.GetComponentInParent<Character>();
            if (enemy.isTrapped)
            {
                Debug.Log("player hit");
                enemy.timer += 10;
            }
        }
    }

    void OnTriggerExit2D(Collider2D obj)
    {

        if (obj.tag == "Waterballoon")
        {
            if (obj.gameObject != frontChecker.FrontWaterBalloon)
            {
                return;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D obj)
    {
        if (obj.gameObject.tag == "WaterBalloon" && canPush)
        {
            if (obj.gameObject != frontChecker.FrontWaterBalloon || moveDirect == Vector3.zero)
            {
                pushTime = 0;
                return;
            }
            pushTime += Time.deltaTime;
            if (pushTime > .25f)
            {
                pushTime = 0;
                PushWaterBalloon();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D obj)
    {
        if (obj.gameObject.tag == "WaterBalloon")
        {
            pushTime = 0;
        }
    }


    void SpawnWaterBalloon()
    {
        if (isTrapped)
        {
            return;
        }
        waterBalloonPos = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), 0);
        if (Map.instance.mapArr[7 - (int)waterBalloonPos.y, (int)waterBalloonPos.x + 7] == 3)
        {
            if (canThrow)
            {
                Destroy(tempWaterBalloon.gameObject);
                Map.instance.mapArr[7 - (int)waterBalloonPos.y, (int)waterBalloonPos.x + 7] = 0;
                ThrowWaterBalloon();
            }
            return;
        }else if (Map.instance.mapArr[7 - (int)waterBalloonPos.y, (int)waterBalloonPos.x + 7] != 0)
        {
            return;
        }
        if (currentWaterBalloons >= waterBalloonMaxCount)
        {
            return;
        }
        currentWaterBalloons++;
        tempWaterBalloon = Instantiate(waterBalloonPrefab, waterBalloonPos, Quaternion.identity).GetComponent<Waterballoon>();
        tempWaterBalloon.Power = waterBalloonPower;
        tempWaterBalloon.Player = this;
        tempWaterBalloon.Position = new int[2] { (int)waterBalloonPos.x + 7, 7 - (int)waterBalloonPos.y };
        Map.instance.mapArr[7 - (int)waterBalloonPos.y, (int)waterBalloonPos.x + 7] = 3;
        tempWaterBalloon.GetComponent<SpriteRenderer>().sortingOrder = 8 - (int)waterBalloonPos.y;
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
        //바늘아이템을 사용했을때로 이전
        Debug.Log("바늘 아이템을 이용해 물풍선 탈출!");
        isTrapped = false;
        animator.SetBool("Live", true);
        animator.SetBool("InWater", false);
        timer = 0f;
        StartCoroutine(LiveAnimation(0.7f));
    }

    private System.Collections.IEnumerator LiveAnimation(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool("Live", false);
        KeyPushCheck();
    }

    //캐릭터가 방패 아이템 획득하는 함수
    public void EquipShieldItem()
    {
        countShieldItem++;
    }
    //캐릭터에 방패 아이템 효과 적용 함수
    public void UseShieldItem()
    {
        shieldAnimation.SetActive(true);
        countShieldItem--;
        isShieldItem = true;
        shieldProtectionTimer = 0;
    }


    //캐릭터에 거북이 아이템 효과 적용 함수
    public void ApplyTurtleItemEffects(TurtleSpeed speed)
    {
        if (!isRidingItem)
        {
            //거북이 아이템 속도 지정
            if (speed == TurtleSpeed.Fast)
            {
                ridingSpeed = 9f;
            }

            else
            {
                ridingSpeed = 1f;
            }
        }
    }

    //캐릭터에 Ufo 아이템 효과 적용 함수
    public void ApplyUfoItemEffects()
    {
        if (!isRidingItem)
        {
            gameObject.layer = 8;
            ridingSpeed = 10f;
        }
    }

    //캐릭터에 부엉이 아이템 효과 적용 함수
    public void ApplyOwlItemEffects()
    {
        if (!isRidingItem) 
        {
            ridingSpeed = 5f;
        }
    }

    // 탑승 아이템을 획득하면 호출되는 함수
    public void ApplyRideableItem(IRideable rideableItem)
    {
        if (isRidingItem)
        {
            currentRideable = realCurrentRideable;
            currentRideable = rideableItem;
            Destroy(currentRideable.gameObject);
            currentRideable = realCurrentRideable;
        }

        currentRideable = rideableItem;

        isRidingItem = true;
        moveSpeed = ridingSpeed;
    }



    //캐릭터가 물풍선에 맞은 경우를 구현한 함수
    public void WaterBalloonBoom()
    {

        //탈 것을 탄 경우
        if(isRidingItem)
        {
            gameObject.layer = 6;
            isRidingItem = false;            
            moveSpeed = characterSpeed;
            Destroy(currentRideable.gameObject);

            return;
        }

        else if (isShieldItem)// 실드가 켜져 있다면
        {
            return;//맞지 않는다
        }

        //그 외의 경우에는 물풍선에 갇힘
        else
        {
            isTrapped = true;
            animator.SetBool("InWater",true);
        }

        animator.SetBool("Live", false);

    }

    public void KeyPushCheck()
    {
        if (Input.GetKey(playerKey[0]))
        {
            moveDirect = Vector3.left * moveSpeed;
            playerDir = Direction.Left;
        }
        else if (Input.GetKey(playerKey[2]))
        {
            moveDirect = Vector3.right * moveSpeed;
            playerDir = Direction.Right;
        }
        else if (Input.GetKey(playerKey[1]))
        {
            moveDirect = Vector3.up * moveSpeed;
            playerDir = Direction.Up;
        }
        else if (Input.GetKey(playerKey[3]))
        {
            moveDirect = Vector3.down * moveSpeed;
            playerDir = Direction.Down;
        }
        else
        {
            editAnimator(false);
            return;
        }
        editAnimator(true);
    }

    public void PushWaterBalloon()
    {
        waterBalloonPos = new Vector3(Mathf.Round(frontChecker.FrontWaterBalloon.transform.position.x), Mathf.Round(frontChecker.FrontWaterBalloon.transform.position.y), 0);
        int x = 7 - (int)waterBalloonPos.y;
        int y = (int)waterBalloonPos.x + 7;
        Map.instance.mapArr[x, y] = 0;
        if (playerDir == Direction.Left)
        {
            if (y - 1 < 0)
            {
                return;//밀수 없는 경우
            }
            if (Map.instance.mapArr[x, y - 1] != 0)
            {
                return;//밀수 없는 경우
            }
            for (int i = 2; ; i++)//왼
            {
                if (y - i < 0) // 맵 밖 14 = -1
                {
                    tempWaterBalloon = frontChecker.FrontWaterBalloon.GetComponent<Waterballoon>();
                    tempWaterBalloon.Move((int)playerDir, new Vector3(-7 + (y - i + 1), 7 - x));
                    break;
                }
                else if (Map.instance.mapArr[x, y - i] != 0) // 장애물을 만남
                {

                    tempWaterBalloon = frontChecker.FrontWaterBalloon.GetComponent<Waterballoon>();
                    tempWaterBalloon.Move((int)playerDir, new Vector3(-7 + (y - i + 1), 7 - x));
                    break;
                }
            }
        }
        else if (playerDir == Direction.Up)
        {
            if (x - 1 < 0)
            {
                return;
            }
            if (Map.instance.mapArr[x - 1, y] != 0)
            {
                return;
            }
            for (int i = 2; ; i++)//위
            {
                if (x - i < 0) // 맵 밖
                {
                    tempWaterBalloon = frontChecker.FrontWaterBalloon.GetComponent<Waterballoon>();
                    tempWaterBalloon.Move((int)playerDir, new Vector3(-7 + y, 7 - (x - i + 1)));
                    break;
                }
                else if (Map.instance.mapArr[x - i, y] != 0) 
                {
                    tempWaterBalloon = frontChecker.FrontWaterBalloon.GetComponent<Waterballoon>();
                    tempWaterBalloon.Move((int)playerDir, new Vector3(-7 + y, 7 - (x - i + 1)));
                    break;
                }
            }
        }
        else if (playerDir == Direction.Down)
        {
            if (x + 1 >= 15)
            {
                return;
            }
            if (Map.instance.mapArr[x + 1, y] != 0)
            {
                return;
            }
            for (int i = 2; ; i++)//아
            {
                if (x + i >= 15) // 맵 밖
                {
                    tempWaterBalloon = frontChecker.FrontWaterBalloon.GetComponent<Waterballoon>();
                    tempWaterBalloon.Move((int)playerDir, new Vector3(-7 + y, 7 - (x + i - 1)));
                    break;
                }
                else if (Map.instance.mapArr[x + i, y] != 0) 
                {
                    tempWaterBalloon = frontChecker.FrontWaterBalloon.GetComponent<Waterballoon>();
                    tempWaterBalloon.Move((int)playerDir, new Vector3(-7 + y, 7 - (x + i - 1)));
                    break;
                }
            }
        }
        else if (playerDir == Direction.Right)
        {
            if (y + 1 >= 15)
            {
                return;
            }
            if (Map.instance.mapArr[x, y + 1] != 0)
            {
                return;
            }
            for (int i = 2; ; i++)// 오
            {
                if (y + i >= 15) // 맵 밖
                {
                    tempWaterBalloon = frontChecker.FrontWaterBalloon.GetComponent<Waterballoon>();
                    tempWaterBalloon.Move((int)playerDir, new Vector3(-7 + (y + i - 1), 7 - x));
                    break;
                }
                else if (Map.instance.mapArr[x, y + i] != 0) 
                {
                    tempWaterBalloon = frontChecker.FrontWaterBalloon.GetComponent<Waterballoon>();
                    tempWaterBalloon.Move((int)playerDir, new Vector3(-7 + (y + i - 1), 7 - x));
                    break;
                }
            }
        }
        tempWaterBalloon.Power = waterBalloonPower;
        tempWaterBalloon.Player = this;
    }

    public void ThrowWaterBalloon()
    {
        waterBalloonPos = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), 0);
        int x = 7 - (int)Mathf.Round(transform.position.y);
        int y = (int)Mathf.Round(transform.position.x) + 7;
        if (playerDir == Direction.Left)
        {
            for (int i = 7; ; i++)//왼
            {
                if (y - i < 0) // 맵 밖 14 = -1
                {
                    if (Map.instance.mapArr[x, y - i + 15] == 0)
                    {
                        tempWaterBalloon = Instantiate(waterBalloonPrefab, waterBalloonPos, Quaternion.identity).GetComponent<Waterballoon>();
                        tempWaterBalloon.Move((int)playerDir, new Vector3(-7 + (y - i + 15), 7 - x));
                        break;
                    }
                }
                else if (Map.instance.mapArr[x, y - i] == 0) // 장애물을 만남
                {
                    
                    tempWaterBalloon = Instantiate(waterBalloonPrefab, waterBalloonPos, Quaternion.identity).GetComponent<Waterballoon>();
                    tempWaterBalloon.Move((int)playerDir, new Vector3(-7 + (y - i), 7 - x));
                    break;
                }
            }
        }
        else if (playerDir == Direction.Up)
        {
            for (int i = 7; ; i++)//위
            {
                if (x - i < 0) // 맵 밖
                {
                    if (Map.instance.mapArr[x - i + 15, y] == 0)
                    {
                        tempWaterBalloon = Instantiate(waterBalloonPrefab, waterBalloonPos, Quaternion.identity).GetComponent<Waterballoon>();
                        tempWaterBalloon.Move((int)playerDir, new Vector3(-7 + y, 7 - (x - i + 15)));
                        break;
                    }
                }
                else if (Map.instance.mapArr[x - i, y] == 0) // 장애물 만났을때 안부숴지는 벽
                {
                    tempWaterBalloon = Instantiate(waterBalloonPrefab, waterBalloonPos, Quaternion.identity).GetComponent<Waterballoon>();
                    tempWaterBalloon.Move((int)playerDir, new Vector3(-7 + y, 7 - (x - i)));
                    break;
                }
            }
        }
        else if (playerDir == Direction.Down)
        {
            for (int i = 7; ; i++)//아
            {
                if (x + i >= 15) // 맵 밖
                {
                    if (Map.instance.mapArr[x + i - 15, y] == 0)
                    {
                        tempWaterBalloon = Instantiate(waterBalloonPrefab, waterBalloonPos, Quaternion.identity).GetComponent<Waterballoon>();
                        tempWaterBalloon.Move((int)playerDir, new Vector3(-7 + y, 7 - (x + i - 15)));
                        break;
                    }
                }
                else if (Map.instance.mapArr[x + i, y] == 0) // 장애물 만났을때 안부숴지는 벽
                { 
                    tempWaterBalloon = Instantiate(waterBalloonPrefab, waterBalloonPos, Quaternion.identity).GetComponent<Waterballoon>();
                    tempWaterBalloon.Move((int)playerDir, new Vector3(-7 + y, 7 - (x + i)));
                    break;
                }
            }
        }
        else if (playerDir == Direction.Right)
        {
            for (int i = 7; ; i++)// 오
            {
                if (y + i >= 15) // 맵 밖
                {
                    if (Map.instance.mapArr[x, y+i-15] == 0)
                    {
                        tempWaterBalloon = Instantiate(waterBalloonPrefab, waterBalloonPos, Quaternion.identity).GetComponent<Waterballoon>();
                        tempWaterBalloon.Move((int)playerDir, new Vector3(-7 + (y + i - 15), 7 - x));
                        break;
                    }
                }
                else if (Map.instance.mapArr[x, y + i] == 0) // 장애물 만났을때 안부숴지는 벽
                {
                    tempWaterBalloon = Instantiate(waterBalloonPrefab, waterBalloonPos, Quaternion.identity).GetComponent<Waterballoon>();
                    tempWaterBalloon.Move((int)playerDir, new Vector3(-7 + (y + i), 7 - x));
                    break;
                }
            }
        }
        tempWaterBalloon.Power = waterBalloonPower;
        tempWaterBalloon.Player = this;
    }

    private void editAnimator(bool move)
    {
        animator.SetInteger("Direction", (int)playerDir);
        animator.SetBool("Moving",move);
    }

    private void GameOver(int num)
    {
        ending.DisplayEnding(num);
        for(int i = 0; i < playerKey.Length; i++)
        {
            playerKey[i] = KeyCode.F11;
        }
        Debug.Log("게임오버");
    }
}
