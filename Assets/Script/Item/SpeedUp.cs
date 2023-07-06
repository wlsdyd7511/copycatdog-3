using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour, IItem
{
    public float speed;


    public void Get(GameObject Player)
    {
        Player.GetComponent<Character>().moveSpeed += speed;
    }
}
