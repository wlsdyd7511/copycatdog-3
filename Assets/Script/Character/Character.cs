using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Character : MonoBehaviour, IWallBoom
{
    enum Direction { Left, Up, Right, Down }; // 0 = 왼쪽, 1 = 위, 2 = 오른쪽, 3 = 아래,
    [SerializeField]
    private GameObject waterBalloonPrefab;
    [SerializeField]
    private FrontChecker frontChecker;
    private GameObject frontCheckerObject;
    public int waterBalloonPower;
    public int waterBalloonMaxCount = 1;
    public int currentWaterBalloons = 0;
    public float moveSpeed = 5f;
    private bool isTrapped = false;
    private Waterballoon tempWaterBalloon;
    private Vector3 waterBalloonPos;
    private Vector3 moveDirect;
    private float preMoveSpeed;
    public bool canPush = false;
    public bool canThrow = false;
    public bool haveWaterBalloon = false;
    Direction playerDir = Direction.Left;
    public float pushTime = 0;


    void Start()
    {
        frontCheckerObject = frontChecker.gameObject;
        preMoveSpeed = moveSpeed;
    }

    void FixedUpdate()
    {
        transform.Translate(moveDirect * Time.deltaTime);
        frontCheckerObject.transform.rotation = Quaternion.Euler(0,0,-90 * (int)playerDir);
    }

    void Update()
    {
        if (preMoveSpeed != moveSpeed)
        {
            moveDirect = moveDirect.normalized * moveSpeed;
        }
        preMoveSpeed = moveSpeed;

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (haveWaterBalloon)
            {
                ThrowWaterBalloon();
                haveWaterBalloon = false;
            }
            else
            {
                SpawnWaterBalloon();
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveDirect = Vector3.left * moveSpeed;
            playerDir = Direction.Left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveDirect = Vector3.right * moveSpeed;
            playerDir = Direction.Right;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            moveDirect = Vector3.up * moveSpeed;
            playerDir = Direction.Up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            moveDirect = Vector3.down * moveSpeed;
            playerDir = Direction.Down;
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


    }

    void OnTriggerEnter2D(Collider2D obj)
    {

        if (obj.tag == "Attack")
        {
            WaterBalloonBoom();
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
            pushTime += Time.deltaTime;
            if (pushTime > .5f)
            {
                //물풍선 밀기
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
            if (pushTime > .5f)
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

        waterBalloonPos = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), 0);
        if (Map.instance.mapArr[7 - (int)waterBalloonPos.y, (int)waterBalloonPos.x + 7] == 3)
        {
            if (canThrow)
            {
                haveWaterBalloon = true;
                Destroy(tempWaterBalloon.gameObject);
                Map.instance.mapArr[7 - (int)waterBalloonPos.y, (int)waterBalloonPos.x + 7] = 0;
            }
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



    public void WaterBalloonBoom()
    {
        Debug.Log("맞았다");
        if (!isTrapped)
        {
            isTrapped = true;
        }
    }

    public void KeyPushCheck()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveDirect = Vector3.left * moveSpeed;
            playerDir = Direction.Left;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            moveDirect = Vector3.right * moveSpeed;
            playerDir = Direction.Right;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            moveDirect = Vector3.up * moveSpeed;
            playerDir = Direction.Up;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            moveDirect = Vector3.down * moveSpeed;
            playerDir = Direction.Down;
        }
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

}
