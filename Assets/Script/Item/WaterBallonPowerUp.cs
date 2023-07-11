using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBallonPowerUp : MonoBehaviour, IItem
{
    public void Get(Character Player)
    {
        Player.waterBalloonPower++;
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
