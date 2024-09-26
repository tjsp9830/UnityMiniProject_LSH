using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlower : MonoBehaviour
{
    [SerializeField] Animation ani;

    void Start()
    {
        //작업 후반부, 플레이어가 근처에 가야지만 실행되게 고치기
        flowerMove();
    }

    private void flowerMove()
    {
        ani.Play();
    }

}
