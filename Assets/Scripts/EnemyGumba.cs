using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGumba : MonoBehaviour
{

    // 공통된 필드
    [SerializeField] SpriteRenderer render;
    [SerializeField] CircleCollider2D circleCollider2D;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] Animator ani;
    [SerializeField] private float moveSpeed;

    //벽 인지하기
    [SerializeField] bool isDead = false; //T죽음 F생존
    [SerializeField] bool isLeft = false; //T왼쪽이동 F오른쪽이동 
    [SerializeField] LayerMask groundMask; //11번 Pipe로

    //콜라이더 말고 스크립트 형식으로 가져왔고, 콜라이더는 자식 콜리더에 스크립트를 따로 직접 추가했음
    [SerializeField] EnemyGumbaHit hitArea;



    //일단 MonoBehaviour로 작동됨, 이후 리팩토링 필요    

    private void Start()
    {
        hitArea.OnHitGumba += EnemyHit;
    }

    private void FixedUpdate()
    {
        GumbaMove();
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, Vector2.right * 0.2f, Color.red, 0.1f);

    }

    private void GumbaMove()
    {
        if (isDead == true)
            return;


        if (isLeft == true)
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            render.flipX = false;

        }
        else if (isLeft == false)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            render.flipX = true;

        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (isDead == true)
            return;


        if (collision.gameObject.layer == 11) //11번 Pipe
        {
            //벽이랑 만남
            isLeft = !isLeft;

        }

        if (collision.gameObject.layer == 3) //3번 Player
        {
            //GumbaAttack()
            Debug.Log("굼바의 몸통박치기");
        }


    }


    public void EnemyHit()
    {
        isDead = true;
        ani.SetTrigger("isTreaded");

        // 이건 마리오가 죽을때로 바꾸던지 하자
        //collider.enabled = false;
        //hitArea.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        // 그리고 마리오가 얘네 죽일때 마리오 강제점프 시키면 됨

        StartCoroutine(WaitForItG());
    }

    //얘 머리 콜라이더랑 플레이어 발 콜라이더가 부딪히면
    //이동 멈추고, isDead 처리하고, 납작 애니메이션, 이후 없애기, 점수 +100

    IEnumerator WaitForItG()
    {
        
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }









    //  [리팩토링 필요 구간]


    //public EnemyGumba()
    //{
    //}



    //// 1.이동 (x축 이동, 방향은 오른쪽. 프로그램 실행시 시작)
    //public override void EnemyMove()
    //{

    //}

    //// 2.감지 (벽 감지, 벽이랑 부딪히면 진행방향 반전)
    //public override void EnemyDetect()
    //{

    //}

    //// 3.애니 (걷는 애니메이션 실행, 죽는 애니로 전환 필요)
    //public override void EnemyAnimePlay()
    //{

    //}



}