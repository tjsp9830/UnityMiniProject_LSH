using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class EnemyGumba : MonoBehaviour
{
    //벽 돌진
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] bool isDead = false; //T죽음 F생존
    [SerializeField] bool isWall = false; //F오른쪽이동 T왼쪽이동
    [SerializeField] LayerMask groundMask;

    //공격
    [SerializeField] GameObject hitArea; //머리 부분만 피격, 제외한 전체는 공격

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

        if(isWall == false)
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        else if(isWall == true)
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9) //9번이 Pipe
        {
            //벽이랑 만남
            isWall = !isWall;

        }

    }


    //얘 머리 콜라이더랑 플레이어 발 콜라이더가 부딪히면
    //이동 멈추고, isDead 처리하고, 납작 애니메이션, 이후 없애기, 돈 +100

    public void GumbaHit()
    {
        Debug.Log("굼바 맞음");
    }

    public void GumbaAttack()
    {

    }

}
