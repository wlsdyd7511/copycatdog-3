using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBalloonEffect : MonoBehaviour
{
    [SerializeField]
    private Sprite[] effectSpriteArr = new Sprite[5]; // 0 = 왼쪽, 1 = 위, 2 = 오른쪽, 3 = 아래, 4 = 가운데
    [SerializeField]
    private SpriteRenderer thisSpriteRender;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeSprite(int num)
    {
        thisSpriteRender.sprite = effectSpriteArr[num];
    }
}
