using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontChecker : MonoBehaviour
{
    public GameObject FrontWaterBalloon;
    // Start is called before the first frame update
    void Start()
    {
        FrontWaterBalloon = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "WaterBalloon")
        {
            FrontWaterBalloon = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject == FrontWaterBalloon)
        {
            FrontWaterBalloon = null;
        }
    }
}
