using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
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
    private Vector3 moveDirect;
    private float preMoveSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        preMoveSpeed = moveSpeed;
    }

    void FixedUpdate()
    {
        transform.Translate(moveDirect * Time.deltaTime);
    }

    void Update()
    {
        if(preMoveSpeed != moveSpeed)
        {
            moveDirect = moveDirect.normalized * moveSpeed;
        }
        preMoveSpeed = moveSpeed;

        if (Input.GetKeyDown(KeyCode.Z) && currentWaterBalloons < waterBalloonMaxCount)
        {
            //  Debug.Log("press");
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
        if(Input.GetKey(KeyCode.LeftArrow))
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
