using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    private GameObject waterBalloonPrefab;
    public int waterBalloonPower;
    public int waterBalloonMaxCount = 1;
    private int currentWaterBalloons = 0;
    public float moveSpeed = 5f;
    public bool isTurtleItem = false; //캐릭터의 거북이 아이템 유무 여부
    public bool WaterBalloonHit = false; //캐릭터가 물풍선에 맞음 여부

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
    public void HitByWaterBalloon(bool WaterBalloonHit)
    {
        //물풍선에 맞은 경우
        if (WaterBalloonHit)
        {
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


}
