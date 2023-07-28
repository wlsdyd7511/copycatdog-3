using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveTest : MonoBehaviour
{
    public GameObject MapSet;

    void Start()
    {
        MapSet.SetActive(false);
    }
}
