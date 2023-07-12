using System.Collections;
using System.Collections.Generic;
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

    private bool isTrapped = false;
    private Waterballoon tempWaterBalloon;
    private Vector3 waterBalloonPos;


    void FixedUpdate()
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
            transform.Translate(0, moveSpeed * Time.deltaTime, 0);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0, -moveSpeed * Time.deltaTime, 0 );
        }
        
       

    }

    private void Update()
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

        waterBalloonPos = new Vector3 (Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), 0);
     //   Debug.Log(waterBalloonPos);
        tempWaterBalloon = Instantiate(waterBalloonPrefab, waterBalloonPos, Quaternion.identity).GetComponent<Waterballoon>();
        tempWaterBalloon.Power = waterBalloonPower;
        tempWaterBalloon.Player = this;
        tempWaterBalloon.Position = new int[2] { (int)waterBalloonPos.x + 7 , 7 - (int)waterBalloonPos.y };
    }

    public void WaterBalloonExploded()
    {
        currentWaterBalloons--;
    }



    public void WaterBalloonBoom()
    {
        Debug.Log("맞았다");
        if(!isTrapped)
        {
            isTrapped = true;
        }
    }

}
