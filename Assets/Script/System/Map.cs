using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject[] walls = new GameObject[2];// 0 = 바닥, 1 = 안부숴지는벽, 2 = 부숴지는거
    public GameObject[] players = new GameObject[2];
    public Character[] InGameplayers = new Character[2];
    public GameObject[,] mapObject;
    public Sprite[] mapSprites;
    public Sprite[] groundSprites; // 0 기본 1~ 가로 횡단보도, 세로 횡단보도, 가로선, 세로선, 도로
    public int[,] mapArr; // 0 = 빈공간, 1 = 부숴지지 않는벽, 2 = 부숴지는벽, 3 = 물풍선, 4 = 1p, 5 = 2p
    private int[,] mapSpriteArr; //  0 = x, 1 ~ 블럭 빨, 노, 파, 박스, 집 빨, 파
    private int[,] groundSpriteArr;
    private List<string> mapList = new List<string>();
    Vector3 wallPos;

    public static Map instance = null;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        DirectoryInfo di = new DirectoryInfo(Application.dataPath + "/Map");

        foreach (FileInfo file in di.GetFiles("*.txt"))
        {

            Debug.Log("파일명 : " + file.FullName);
            mapList.Add(file.FullName);
        }
    }

    public void CreateMap(int num)
    {

        GameObject tempObject;
        ReadMapFile(num);
        for (int i = 0; i < mapArr.GetLength(0); i++)
        {
            for (int j = 0; j < mapArr.GetLength(1); j++)
            {
                wallPos = new Vector3(-7 + j * 1, 7 - i * 1);
                if (mapArr[i, j] != 0 && mapArr[i, j] < 3)
                {
                    mapObject[i, j] = Instantiate(walls[mapArr[i, j]], wallPos, Quaternion.identity);
                    mapObject[i, j].GetComponent<SpriteRenderer>().sprite = mapSprites[mapSpriteArr[i, j] - 1];
                    mapObject[i, j].GetComponent<SpriteRenderer>().sortingOrder = i + 1;
                }
                else if (mapArr[i, j] >= 4)
                {
                    InGameplayers[mapArr[i, j] - 4] = Instantiate(players[mapArr[i, j] - 4], new Vector3(wallPos.x, wallPos.y, -2), Quaternion.identity).GetComponent<Character>();
                    mapArr[i, j] = 0;
                }
                if (groundSpriteArr[i, j] != 0)
                {
                    tempObject = Instantiate(walls[0], wallPos, Quaternion.identity);
                    tempObject.GetComponent<SpriteRenderer>().sprite = groundSprites[groundSpriteArr[i, j] - 1];
                }
                else
                {
                    Instantiate(walls[0], wallPos, Quaternion.identity);
                }
            }
        }
    }

    void ReadMapFile(int num)
    {
        string[] contents = System.IO.File.ReadAllLines(mapList[num]);
        string[] txtArr = contents[0].Split(',');

        Debug.Log(txtArr.Length);
        mapArr = new int[txtArr.Length, txtArr.Length];
        mapSpriteArr = new int[txtArr.Length, txtArr.Length];
        groundSpriteArr = new int[txtArr.Length, txtArr.Length];
        mapObject = new GameObject[txtArr.Length, txtArr.Length];
        int cnt = 0;
        if (contents.Length > 0)
        {
            for (int i = 0; i < txtArr.Length; i++)
            {
                txtArr = contents[i].Split(',');
                int[] numArr = new int[txtArr.Length];
                for (int j = 0; j < numArr.Length; j++)
                {
                    mapArr[cnt, j] = int.Parse(txtArr[j]);
                }
                cnt++;
            }
            cnt = 0;
            for (int i = txtArr.Length + 1; i < txtArr.Length * 2 + 1; i++)
            {
                txtArr = contents[i].Split(',');
                int[] numArr = new int[txtArr.Length];
                for (int j = 0; j < numArr.Length; j++)
                {
                    mapSpriteArr[cnt, j] = int.Parse(txtArr[j]);
                }
                cnt++;
            }
            cnt = 0;
            for (int i = txtArr.Length * 2 + 2; i < contents.Length; i++)
            {
                txtArr = contents[i].Split(',');

                int[] numArr = new int[txtArr.Length];
                for (int j = 0; j < numArr.Length; j++)
                {
                    groundSpriteArr[cnt, j] = int.Parse(txtArr[j]);
                }
                cnt++;
            }
        }
    }
}
