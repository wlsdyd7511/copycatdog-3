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

    void OnTriggerEnter2D(Collider2D gameobject)
    {
        if (gameObject.tag == "Player")
        {
            Get(gameObject.GetComponent<Character>());
        }
    }
}
