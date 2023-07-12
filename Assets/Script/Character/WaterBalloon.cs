using UnityEngine;

public class Waterballoon : MonoBehaviour
{
    public int Power; // 물풍선 범위 변수
    public int[] Position = new int[2];
    public Map map;
    private Vector3 temapWallPos;
    public GameObject waterBalloonEffect;
    public Character Player;

    private void Start()// 물풍선 생성
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
                temapWallPos = new Vector3(-7 + y-i, 7 - x);
                Instantiate(waterBalloonEffect, temapWallPos, Quaternion.identity);
            }
        }
        for (int i = 1; i <= Power; i++)
        {
            if (x - i < 0) // 맵 밖
                break;
            else if (map.mapArr[x-i, y] == 1) // 장애물 만났을때 안부숴지는 벽
                break;
            else if (map.mapArr[x-i,y] == 2) // 장애물 만났을때 부숴지는 벽
            {
                map.mapObject[x - i, y].GetComponent<BreakableWall>().WaterBalloonBoom();
                map.mapArr[x - i, y] = 0;
                break;
            }
            else
            {
                temapWallPos = new Vector3(-7 + y , 7 - (x-i));
                Instantiate(waterBalloonEffect, temapWallPos, Quaternion.identity);
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
                Instantiate(waterBalloonEffect, temapWallPos, Quaternion.identity);
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
                Instantiate(waterBalloonEffect, temapWallPos, Quaternion.identity);
            }
        }
        temapWallPos = new Vector3(-7 + y, 7 - x);
        Instantiate(waterBalloonEffect, temapWallPos, Quaternion.identity);
        Player.WaterBalloonExploded();
       // Debug.Log("물풍선이 터졌습니다!"); // 맵 구현 후, 터지는 범위까지 구현
        Destroy(this.gameObject);
    }
}

