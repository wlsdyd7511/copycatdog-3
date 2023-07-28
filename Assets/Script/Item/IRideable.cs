using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRideable : IItem
{
    void Ride(Character player);
    GameObject gameObject { get; }

}
