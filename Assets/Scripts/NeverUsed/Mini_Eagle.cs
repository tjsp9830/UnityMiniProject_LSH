using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 추적: 꽃에 적용,  상태패턴 자체: 플레이어에 적용,  밟아서 처치하는거... 하셨었네..?

public class Mini_Eagle : MonoBehaviour
{ 
    // 상태패턴 장점 1. 조건체크가 없어진다
    // 현재 상태를 if문 아도겐 하면서 조건 체크체크.. 할 필요가 없어짐! 
    // 각각의 상태에서 다른 상태로의 전환(필요시에만)을 진행해주면됨

    public enum State { Idle, Trace, Return, Attack, Dead }
    [SerializeField] public State curState; //오브젝트는 열거형중에서 딱 하나의 상태만을 가짐

    //[SerializeField] GameObject player;
    [SerializeField] Mini_MarioCtrl player;
    [SerializeField] Mini_UICtrl ui;
    [SerializeField] float traceRange = 5f;
    [SerializeField] float attackRange = 2f;
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] Vector2 startPos;
    [SerializeField] public int EagleHp = 100;
    [SerializeField] public bool isDead;   //T죽음 F살음

    [Header("이 값을 0 이상의 정수로 바꾸시거나, K키 누르시면 플레이어 죽어요")]
    [SerializeField] int attackPower = 5;


    private void Start()
    {
        startPos = transform.position;
        //player = GameObject.FindGameObjectWithTag("Player");
        isDead = false;
        EagleHp = 100;
    }


    private void Update()
    {
        switch (curState)
        {
            case State.Idle:
                Idle();
                break;

            case State.Trace:
                Trace();
                break;

            case State.Return:
                Return();
                break;

            case State.Attack:
                Attack();
                break;

            case State.Dead:
                Dead();
                break;

        }

        AnyState();

    }


    


    private void AnyState()
    {
        if (EagleHp <= 0)
        {
            isDead = true;
            curState = State.Dead;
        }
    }

    private void Idle()
    {
        // Idle 행동만 구현
        // 가만히 있기

        // 다른 상태로 전환
        if (EagleHp <= 0 || isDead)
            return;

        if (Vector2.Distance(transform.position, player.transform.position) < traceRange)
        {
            curState = State.Trace;
        }

    }

    private void Trace()
    {
        // Trace 행동만 구현
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);

        // 다른 상태로 전환
        if (EagleHp <= 0 || isDead)
            return;

        if (Vector2.Distance(transform.position, player.transform.position) > traceRange)
        {
            curState = State.Return; //여기서 코딩할필요X 상태를 바꿔주면 저기서 알아서 실행함. 완전 객체지향!!
        }
        else if (Vector2.Distance(transform.position, player.transform.position) < attackRange)
        {
            curState = State.Attack;
        }

    }

    private void Return()
    {
        // Return 행동만 구현
        transform.position = Vector2.MoveTowards(transform.position, startPos, moveSpeed * Time.deltaTime);

        // 다른 상태로 전환
        if (EagleHp <= 0 || isDead)
            return;

        if (Vector2.Distance(transform.position, startPos) < 0.01f)
        {
            curState = State.Idle;
        }

    }

    private void Attack()
    {
        // 다른 상태로 전환
        if (EagleHp <= 0 || isDead || ui.isTouchGoal)
            return;

        if (Vector2.Distance(transform.position, player.transform.position) > attackRange)
        {
            curState = State.Trace;
        }

        // Attack 행동만 구현
        if(player.playerHp <= 0)
            return;

        player.playerHp -= attackPower;

    }

    private void Dead()
    {
        // Dead 행동만 구현
        Debug.Log("독수리 죽음");

        // 다른 상태로 전환
        

    }

}
