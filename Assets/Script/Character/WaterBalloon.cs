using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

public class Waterballoon : MonoBehaviour
{

    public int Power; // 물풍선 범위 변수
    public int[] Position = new int[2];
    private Vector3 temapWallPos;
    public GameObject waterBalloonEffect;
    public BoxCollider2D waterBalloonBlock;
    public CircleCollider2D waterBalloonCollider;
    public Character Player;
    private bool isExplode = false;
    public bool moving = false;
    public Vector3 targetPos;
    public Vector3 movingDir;
    private Vector3 nowPos;
    private float timer = 0;

    void Start()// 물풍선 생성
    {
        StartCoroutine(ExplodeAfterDelay(5f));
        Debug.Log(Position[0] + "," + Position[1]);
        waterBalloonCollider=this.gameObject.gameObject.GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        nowPos = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), 0);
        if (moving)
        {
            
            waterBalloonBlock.enabled = false;
            waterBalloonCollider.enabled = false;
            transform.Translate(movingDir * Time.deltaTime);
            if(nowPos == targetPos)
            {
                waterBalloonBlock.enabled = true;
                waterBalloonCollider.enabled = true;
                moving = false;
                transform.position = nowPos;
            }
            if(transform.position.x < -8)
            {
                transform.position = new Vector3(7.5f, transform.position.y);
            }
            else if (transform.position.x > 8)
            {
                transform.position = new Vector3(-7.5f, transform.position.y);
            }
            else if (transform.position.y < -8)
            {
                transform.position = new Vector3(transform.position.x, 7.5f);
            }
            else if (transform.position.y > 8)
            {
                transform.position = new Vector3(transform.position.x, -7.5f);
            }
        }
    }
    private System.Collections.IEnumerator ExplodeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        yield return new WaitForSeconds(timer);
        Explode();
    }

    public void Move(int dir, Vector3 tempPos) // 0 = 왼쪽, 1 = 위, 2 = 오른쪽, 3 = 아래,
    {
        timer += 1;
        targetPos = tempPos;
        if (dir == 0)
        {
            movingDir = Vector3.left * 10f;
        }
        else if (dir == 1)
        {
            movingDir = Vector3.up * 10f;
        }
        else if (dir == 2)
        {
            movingDir = Vector3.right * 10f;
        }
        else if (dir == 3)
        {
            movingDir = Vector3.down * 10f;
        }
        moving = true;
        Position = new int[2] { (int)targetPos.x + 7, 7 - (int)targetPos.y };
        Map.instance.mapArr[7 - (int)targetPos.y, (int)targetPos.x + 7] = 3;
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
            else if (Map.instance.mapArr[x, y - i] == 1) // 장애물 만났을때 안부숴지는 벽
                break;
            else if (Map.instance.mapArr[x, y - i] == 2) // 장애물 만났을때 부숴지는 벽
            {
                Map.instance.mapObject[x, y - i].GetComponent<BreakableWall>().WaterBalloonBoom();
                Map.instance.mapArr[x, y - i] = 0;
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
            else if (Map.instance.mapArr[x - i, y] == 1) // 장애물 만났을때 안부숴지는 벽
                break;
            else if (Map.instance.mapArr[x - i, y] == 2) // 장애물 만났을때 부숴지는 벽
            {
                Map.instance.mapObject[x - i, y].GetComponent<BreakableWall>().WaterBalloonBoom();
                Map.instance.mapArr[x - i, y] = 0;
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
            else if (Map.instance.mapArr[x + i, y] == 1) // 장애물 만났을때 안부숴지는 벽
                break;
            else if (Map.instance.mapArr[x + i, y] == 2) // 장애물 만났을때 부숴지는 벽
            {
                Map.instance.mapObject[x + i, y].GetComponent<BreakableWall>().WaterBalloonBoom();
                Map.instance.mapArr[x + i, y] = 0;
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
            else if (Map.instance.mapArr[x, y + i] == 1) // 장애물 만났을때 안부숴지는 벽
                break;
            else if (Map.instance.mapArr[x, y + i] == 2) // 장애물 만났을때 부숴지는 벽
            {
                Map.instance.mapObject[x, y + i].GetComponent<BreakableWall>().WaterBalloonBoom();
                Map.instance.mapArr[x, y + i] = 0;
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
        Map.instance.mapArr[Position[1],Position[0]] = 0;
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
            waterBalloonBlock.enabled = true;
        }
    }
}

