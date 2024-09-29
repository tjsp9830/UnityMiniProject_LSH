using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlower : MonoBehaviour
{

    //// 공통된 필드
    //[SerializeField] Animation ani;
    //[SerializeField] float moveSpeed = 1f;



    ////일단 MonoBehaviour로 작동됨, 이후 리팩토링 필요

    //void Start()
    //{
    //    // [리팩토링] 플레이어가 근처에 가야지만 실행되게 고치기
    //    FlowerMove();
    //}


    //private void FlowerMove()
    //{
    //    // [리팩토링] 애니 실행을 부모함수 오버라이드로 고치기
    //    ani.Play();
    //}










    ////  [리팩토링 필요 구간]

    //public EnemyFlower()
    //{
    //    ani = GetComponent<Animation>();
    //}



    //// 1.감지 (플레이어 감지, 캐릭터가 근처에 오면 이동)
    //public override void EnemyDetect()
    //{

    //}

    //// 2.이동 (y축 이동, 방향은 위쪽. 이후 플레이어 멀어지면 아래로 이동함)
    //public override void EnemyMove()
    //{

    //}

    //// 3.애니 (입 콰작콰작 애니메이션 실행. 죽는 애니로 전환 없음)
    //public override void EnemyAnimePlay()
    //{

    //}



}