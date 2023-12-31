using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBalloonPush: MonoBehaviour, IItem
{
    public void Get(Character Player)
    {
        Player.canPush = true;
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.tag == "Player")
        {
            Get(obj.GetComponent<Character>());
        }
        else if (obj.tag == "Attack")
        {
            WaterBalloonBoom();
        }
    }

    public void WaterBalloonBoom()
    {
        Destroy(this.gameObject);
    }
}
