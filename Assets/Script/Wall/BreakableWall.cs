using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour, IWallBoom
{
    public GameObject[] itemArr;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WaterBalloonBoom()
    {
        //item 소환확률 30%
        float temp = Random.Range(0f, 1f);
        if (temp <= 0.5f)
        {
            int tempItemNum = Random.Range(0, itemArr.Length);
            GameObject temp2 = Instantiate(itemArr[tempItemNum], gameObject.transform.position, Quaternion.identity);
        }
        Destroy(this.gameObject);
    }
}
