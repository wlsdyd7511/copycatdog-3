using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour, IWallBoom
{
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
        //맵 2차원 배열 구현후 수정
        //아이템생성
        Destroy(this.gameObject);
    }
}
