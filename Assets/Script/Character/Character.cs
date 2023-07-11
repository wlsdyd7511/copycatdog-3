using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IWallBoom
{
    [SerializeField]
    private GameObject waterBalloonPrefab;
    public int waterBalloonPower;
    public int waterBalloonMaxCount = 1;
    private int currentWaterBalloons = 0;
    public float moveSpeed = 5f;

    private bool isTrapped = false;

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0, 0, moveSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0, 0, -moveSpeed * Time.deltaTime);
        }
        
        if (Input.GetKeyDown(KeyCode.Z) && currentWaterBalloons < waterBalloonMaxCount)
        {
            SpawnWaterBalloon();
            currentWaterBalloons++;
        }

    }

    void SpawnWaterBalloon()
    {
        Instantiate(waterBalloonPrefab, transform.position, Quaternion.identity);
    }

    public void WaterBalloonExploded()
    {
        currentWaterBalloons--;
    }



    public void WaterBalloonBoom()
    {
        if(!isTrapped)
        {
            isTrapped = true;
        }
    }

}
