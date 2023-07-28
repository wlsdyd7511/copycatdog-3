using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRideable
{
    void Ride(Character player);
    GameObject gameObject { get; } // GameObject 속성 추가
}
