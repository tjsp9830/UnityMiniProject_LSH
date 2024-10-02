using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Block Parent Script
// 블록들의 부모 스크립트
// 블록들의 hit 에어리어 겜옵젝에 넣을 용도로 만드는 중

// 구성:  (반응함=플레이어가 주먹으로 치면 블럭 위로 올라갔다가 내려오는 애니), 아이템 생성하기, 스프라이트 바뀜, 사라짐


public abstract class Block : MonoBehaviour
{
    
    [SerializeField] protected SpriteRenderer render;
    [SerializeField] protected Sprite blockOff;

    [SerializeField] protected float movingSpeed;
    Vector3 curPos;
    Vector3 reActPos;

    protected UnityAction OnTouched;

    private void Start()
    {
        movingSpeed *= 0.1f;
        curPos = transform.position;
        reActPos = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
    }


    //protected void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.layer == 15)
    //    {
    //        Debug.Log($"블럭: {collision.gameObject.name}");
    //        OnTouched?.Invoke();
    //    }
    //}


    // [가상] 반응함: 일반, 동전, 아템
    protected void ReAct()
    {
        // 블럭이 위로 들썩이는 애니메이션 출력
        Debug.Log("블럭이 들썩인다");
        
        Invoke("Went", 0f);
        Invoke("CreateItem", 0.5f);
        Invoke("Back", 1f);

    }

    private void Went()
    {
        transform.Translate(reActPos * movingSpeed);        
    }

    private void Back()
    {
        transform.Translate(curPos * movingSpeed);
    }



    // [추상]아이템: 동전, 아템

    protected abstract void CreateItem();
    //{
    //// 코인 or 버섯1 or 버섯2 or 스타가 튀어나옴
    //// Debug.Log("아이템이 나온다");
    //// 템도 튀어나올때 위쪽 방향으로 튀어나와야 함
    //// 코인은 그 이후 사라짐
    //// 버섯1은 그 이후 오른쪽으로 이동함 (y축 상관없이)
    //// 버섯2와 스타는 그 이후 계속 블럭 위에있음
    //}



    // 사라짐: 일반
    protected void BlockDespawn()
    {
        // 블록 사라짐 
        Debug.Log("블럭이 사라진다");
        this.gameObject.SetActive(false);
    }


    // 스프라이트 바뀜: 동전, 아템
    protected void ChangeSprite()
    {
        render.sprite = blockOff;

    }

    




}



/*
블록은 3가지이고 (일반블럭, 동전블럭, 아이템블럭)
아이템은 4가지임 (동전, 빨간버섯, 초록버섯, 별)

여기서 (반응함=플레이어가 주먹으로 치면 블럭 위로 올라갔다가 내려오는 애니)
일반블럭: 반응함, 사라짐
동전블럭: 반응함, 아이템, 스프라이트 바뀜, 사라짐
아템블럭: 반응함, 아이템, 스프라이트 바뀜, 사라짐 이고,

아이템은 (기본애니=생성될때 튀어나오는 애니, 움직임=오른쪽으로 이동)
동전코인: 생성됨, 기본애니, 움직임, 사라짐, UI 코드 건드림(갯수)
빨간버섯: 생성됨, 기본애니, 움직임, 사라짐, 플레이어 코드 건드림(크기)
초록버섯: 생성됨, 기본애니, 고정됨, 사라짐, UI 코드 건드림(목숨)
별별별별: 생성됨, 기본애니, 고정됨, 사라짐, UI 코드 건드림(점수)
 */