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
    public int waterBalloonPower;
    public int waterBalloonMaxCount = 1;
    public int currentWaterBalloons = 0;
    public float moveSpeed = 5f;
    private bool isTrapped = false;
    private Waterballoon tempWaterBalloon;
    private Vector3 waterBalloonPos;
    private Vector3 moveDirect;
    private float preMoveSpeed;
    private bool canPush = false;
    public bool canThrow = false;
    public bool haveWaterBalloon = false;
    Direction playerDir = Direction.Left;

    void Start()
    {

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
                currentWaterBalloons++;
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
                currentWaterBalloons--;
                return;
            }
            else
            {
                currentWaterBalloons--;
                return;
            }

        }
        if (currentWaterBalloons >= waterBalloonMaxCount)
        {
            currentWaterBalloons--;
            return;
        }
        //   Debug.Log(waterBalloonPos);
        tempWaterBalloon = Instantiate(waterBalloonPrefab, waterBalloonPos, Quaternion.identity).GetComponent<Waterballoon>();
        tempWaterBalloon.Power = waterBalloonPower;
        tempWaterBalloon.Player = this;
        tempWaterBalloon.Position = new int[2] { (int)waterBalloonPos.x + 7, 7 - (int)waterBalloonPos.y };
        Map.instance.mapArr[7 - (int)waterBalloonPos.y, (int)waterBalloonPos.x + 7] = 3;
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
                        tempWaterBalloon.Move((int)playerDir);
                        tempWaterBalloon.targetPos = new Vector3(-7 + (y - i + 15), 7 - x);
                        break;
                    }
                }
                else if (Map.instance.mapArr[x, y - i] == 0) // 장애물을 만남
                {
                    
                    tempWaterBalloon = Instantiate(waterBalloonPrefab, waterBalloonPos, Quaternion.identity).GetComponent<Waterballoon>();
                    tempWaterBalloon.Move((int)playerDir);
                    tempWaterBalloon.targetPos = new Vector3(-7 + (y - i), 7 - x);
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
                        tempWaterBalloon.Move((int)playerDir);
                        tempWaterBalloon.targetPos = new Vector3(-7 + y, 7 - (x - i + 15));
                        break;
                    }
                }
                else if (Map.instance.mapArr[x - i, y] != 0) // 장애물 만났을때 안부숴지는 벽
                {
                    tempWaterBalloon = Instantiate(waterBalloonPrefab, waterBalloonPos, Quaternion.identity).GetComponent<Waterballoon>();
                    tempWaterBalloon.Move((int)playerDir);
                    tempWaterBalloon.targetPos = new Vector3(-7 + y, 7 - (x - i));
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
                        tempWaterBalloon.Move((int)playerDir);
                        tempWaterBalloon.targetPos = new Vector3(-7 + y, 7 - (x + i - 15));
                        break;
                    }
                }
                else if (Map.instance.mapArr[x + i, y] != 0) // 장애물 만났을때 안부숴지는 벽
                {
                    tempWaterBalloon = Instantiate(waterBalloonPrefab, waterBalloonPos, Quaternion.identity).GetComponent<Waterballoon>();
                    tempWaterBalloon.Move((int)playerDir);
                    tempWaterBalloon.targetPos = new Vector3(-7 + y, 7 - (x + i));
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
                        tempWaterBalloon.Move((int)playerDir);
                        tempWaterBalloon.targetPos = new Vector3(-7 + (y + i - 15), 7 - x);
                        break;
                    }
                }
                else if (Map.instance.mapArr[x, y + i] != 0) // 장애물 만났을때 안부숴지는 벽
                {
                    tempWaterBalloon = Instantiate(waterBalloonPrefab, waterBalloonPos, Quaternion.identity).GetComponent<Waterballoon>();
                    tempWaterBalloon.Move((int)playerDir);
                    tempWaterBalloon.targetPos = new Vector3(-7 + (y + i), 7 - x);
                    break;
                }
            }
        }
        tempWaterBalloon.Power = waterBalloonPower;
        tempWaterBalloon.Player = this;
        tempWaterBalloon.Position = new int[2] { (int)tempWaterBalloon.targetPos.x + 7, 7 - (int)tempWaterBalloon.targetPos.y };
        Map.instance.mapArr[7 - (int)tempWaterBalloon.targetPos.y, (int)tempWaterBalloon.targetPos.x + 7] = 3;
    }

}
