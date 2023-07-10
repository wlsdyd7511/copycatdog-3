using UnityEngine;

public class Waterballoon : MonoBehaviour
{
    public int Power; // 물풍선 범위 변수

    private void Start()// 물풍선 생성
    {
        StartCoroutine(ExplodeAfterDelay(5f));
    }

    private System.Collections.IEnumerator ExplodeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Explode();
    }

    private void Explode()
    {
        Debug.Log("물풍선이 터졌습니다!"); // 맵 구현 후, 터지는 범위까지 구현
    }
}

