using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject[] walls = new GameObject[2];// 0 = 부숴지지 않는벽, 1 = 부숴지는벽
    public int[,] mapArr; // 0 = 빈공간, 1 = 부숴지지 않는벽, 2 = 부숴지는벽
    Vector3 wallPos;
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateMap(int mapSize)
    {
        mapArr = new int[mapSize, mapSize];
        for (int i = 0; i < mapArr.GetLength(0); i++)
        {
            for (int j = 0; j < mapArr.GetLength(1); j++)
            {
                if (mapArr[i, j] != 0)
                {
                    //wallPos = new Vector3(첫번째 벽 x좌표 + j * 움직여야하는 x좌표 크기 , 첫번째 벽 y좌표 + i * 움직여야하는 y좌표 크기)
                    //Instantiate(walls[mapArr[i, j]], wallPos);
                }
            }
        }
    }
}
