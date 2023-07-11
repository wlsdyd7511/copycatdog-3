using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour, IItem
{
    public float speed;

    public void Get(Character Player)
    {
        Player.moveSpeed += speed;
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D gameobject)
    {
        if(gameObject.tag == "Player")
        {
            Get(gameObject.GetComponent<Character>());
        }
    }

    public void WaterBalloonBoom()
    {
        Destroy(this.gameObject);
    }
}
