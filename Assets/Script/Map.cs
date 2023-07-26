using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject[] walls = new GameObject[2];// 0 = 부숴지지 않는벽, 1 = 부숴지는벽
    public GameObject[,] mapObject;
    public int[,] mapArr; // 0 = 빈공간, 1 = 부숴지지 않는벽, 2 = 부숴지는벽, 3 = 물풍선
    private List<string> mapList = new List<string>();
    Vector3 wallPos;

    private static Map instance = null;

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
        CreateMap();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateMap()
    {
        ReadMapFile();
        for (int i = 0; i < mapArr.GetLength(0); i++)
        {
            for (int j = 0; j < mapArr.GetLength(1); j++)
            {
                if (mapArr[i, j] != 0)
                {
                    wallPos = new Vector3(-7 + j * 1, 7 - i * 1);
                    mapObject[i,j] = Instantiate(walls[mapArr[i, j]-1], wallPos, Quaternion.identity);
                }
            }
        }
    }

   void ReadMapFile()
    {
        
        string[] contents = System.IO.File.ReadAllLines(mapList[Random.Range(0,mapList.Count)]);
        Debug.Log(contents.Length);
        mapArr = new int[contents.Length, contents.Length];
        mapObject = new GameObject[contents.Length, contents.Length];
        int cnt = 0;
        if (contents.Length > 0)
        {
            for (int i = 0; i < contents.Length; i++)
            {
                string[] txtArr = contents[i].Split(',');

                int[] numArr = new int[txtArr.Length];
                for (int j = 0; j < numArr.Length; j++)
                {
                    mapArr[cnt,j] = int.Parse(txtArr[j]);
                }
                cnt++;
            }
        } 
    }
}
