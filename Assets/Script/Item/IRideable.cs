using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRideable
{
    void Ride(Character player);
    GameObject GetRideablePrefab(); // 탑승 아이템 프리팹 반환 함수
    GameObject gameObject { get; } // GameObject 속성 추가
}
