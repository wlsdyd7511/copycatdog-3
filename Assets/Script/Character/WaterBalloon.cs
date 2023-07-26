using System.Runtime.CompilerServices;
using UnityEngine;

public class Waterballoon : MonoBehaviour
{

    public int Power; // 물풍선 범위 변수
    public int[] Position = new int[2];
    public Map map;
    private Vector3 temapWallPos;
    public GameObject waterBalloonEffect;
    public GameObject waterBalloonBlock;
    public Character Player;
    private bool isExplode = false;


    void Start()// 물풍선 생성
    {
        map = GameObject.FindGameObjectWithTag("Manager").GetComponent<Map>();
        StartCoroutine(ExplodeAfterDelay(5f));
    }

    private System.Collections.IEnumerator ExplodeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Explode();
    }

    private void Explode()
    {
        isExplode = true;
        WaterBalloonEffect tempEffect;
        int x = Position[1];
        int y = Position[0];
        for (int i = 1; i <= Power; i++)
        {
            if (y - i < 0) // 맵 밖
                break;
            else if (map.mapArr[x, y - i] == 1) // 장애물 만났을때 안부숴지는 벽
                break;
            else if (map.mapArr[x, y - i] == 2) // 장애물 만났을때 부숴지는 벽
            {
                map.mapObject[x, y - i].GetComponent<BreakableWall>().WaterBalloonBoom();
                map.mapArr[x, y - i] = 0;
                break;
            }
            else
            {
                temapWallPos = new Vector3(-7 + y - i, 7 - x);
                tempEffect = Instantiate(waterBalloonEffect, temapWallPos, Quaternion.identity).GetComponent<WaterBalloonEffect>();
                tempEffect.ChangeSprite(0);
            }
        }
        for (int i = 1; i <= Power; i++)
        {
            if (x - i < 0) // 맵 밖
                break;
            else if (map.mapArr[x - i, y] == 1) // 장애물 만났을때 안부숴지는 벽
                break;
            else if (map.mapArr[x - i, y] == 2) // 장애물 만났을때 부숴지는 벽
            {
                map.mapObject[x - i, y].GetComponent<BreakableWall>().WaterBalloonBoom();
                map.mapArr[x - i, y] = 0;
                break;
            }
            else
            {
                temapWallPos = new Vector3(-7 + y, 7 - (x - i));
                tempEffect = Instantiate(waterBalloonEffect, temapWallPos, Quaternion.identity).GetComponent<WaterBalloonEffect>();
                tempEffect.ChangeSprite(1);
            }
        }
        for (int i = 1; i <= Power; i++)
        {
            if (x + i >= 15) // 맵 밖
                break;
            else if (map.mapArr[x + i, y] == 1) // 장애물 만났을때 안부숴지는 벽
                break;
            else if (map.mapArr[x + i, y] == 2) // 장애물 만났을때 부숴지는 벽
            {
                map.mapObject[x + i, y].GetComponent<BreakableWall>().WaterBalloonBoom();
                map.mapArr[x + i, y] = 0;
                break;
            }
            else
            {
                temapWallPos = new Vector3(-7 + y, 7 - (x + i));
                tempEffect = Instantiate(waterBalloonEffect, temapWallPos, Quaternion.identity).GetComponent<WaterBalloonEffect>();
                tempEffect.ChangeSprite(3);
            }
        }
        for (int i = 1; i <= Power; i++)
        {
            if (y + i >= 15) // 맵 밖
                break;
            else if (map.mapArr[x, y + i] == 1) // 장애물 만났을때 안부숴지는 벽
                break;
            else if (map.mapArr[x, y + i] == 2) // 장애물 만났을때 부숴지는 벽
            {
                map.mapObject[x, y + i].GetComponent<BreakableWall>().WaterBalloonBoom();
                map.mapArr[x, y + i] = 0;
                break;
            }
            else
            {
                temapWallPos = new Vector3(-7 + (y + i), 7 - x);
                tempEffect = Instantiate(waterBalloonEffect, temapWallPos, Quaternion.identity).GetComponent<WaterBalloonEffect>();
                tempEffect.ChangeSprite(2);
            }
        }
        temapWallPos = new Vector3(-7 + y, 7 - x);
        tempEffect = Instantiate(waterBalloonEffect, temapWallPos, Quaternion.identity).GetComponent<WaterBalloonEffect>();
        tempEffect.ChangeSprite(4);
        Player.WaterBalloonExploded();
        map.mapArr[Position[0], Position[1]] = 0;
        // 맵 구현 후, 터지는 범위까지 구현
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D obj)
    {

        if (obj.tag == "Attack" && !isExplode)
        {
            this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            Explode();
        }
    }

    void OnTriggerExit2D(Collider2D obj)
    {

        if (obj.tag == "Player")
        {
            waterBalloonBlock.SetActive(true);
        }
    }

}

