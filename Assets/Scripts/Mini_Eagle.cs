using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ����: �ɿ� ����,  �������� ��ü: �÷��̾ ����,  ��Ƽ� óġ�ϴ°�... �ϼ̾���..?

public class Mini_Eagle : MonoBehaviour
{ 
    // �������� ���� 1. ����üũ�� ��������
    // ���� ���¸� if�� �Ƶ��� �ϸ鼭 ���� üũüũ.. �� �ʿ䰡 ������! 
    // ������ ���¿��� �ٸ� ���·��� ��ȯ(�ʿ�ÿ���)�� �������ָ��

    public enum State { Idle, Trace, Return, Attack, Dead }
    [SerializeField] public State curState; //������Ʈ�� �������߿��� �� �ϳ��� ���¸��� ����

    //[SerializeField] GameObject player;
    [SerializeField] Mini_MarioCtrl player;
    [SerializeField] Mini_UICtrl ui;
    [SerializeField] float traceRange = 5f;
    [SerializeField] float attackRange = 2f;
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] Vector2 startPos;
    [SerializeField] public int EagleHp = 100;
    [SerializeField] public bool isDead;   //T���� F����

    [Header("�� ���� 0 �̻��� ������ �ٲٽðų�, KŰ �����ø� �÷��̾� �׾��")]
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
        // Idle �ൿ�� ����
        // ������ �ֱ�

        // �ٸ� ���·� ��ȯ
        if (EagleHp <= 0 || isDead)
            return;

        if (Vector2.Distance(transform.position, player.transform.position) < traceRange)
        {
            curState = State.Trace;
        }

    }

    private void Trace()
    {
        // Trace �ൿ�� ����
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);

        // �ٸ� ���·� ��ȯ
        if (EagleHp <= 0 || isDead)
            return;

        if (Vector2.Distance(transform.position, player.transform.position) > traceRange)
        {
            curState = State.Return; //���⼭ �ڵ����ʿ�X ���¸� �ٲ��ָ� ���⼭ �˾Ƽ� ������. ���� ��ü����!!
        }
        else if (Vector2.Distance(transform.position, player.transform.position) < attackRange)
        {
            curState = State.Attack;
        }

    }

    private void Return()
    {
        // Return �ൿ�� ����
        transform.position = Vector2.MoveTowards(transform.position, startPos, moveSpeed * Time.deltaTime);

        // �ٸ� ���·� ��ȯ
        if (EagleHp <= 0 || isDead)
            return;

        if (Vector2.Distance(transform.position, startPos) < 0.01f)
        {
            curState = State.Idle;
        }

    }

    private void Attack()
    {
        // �ٸ� ���·� ��ȯ
        if (EagleHp <= 0 || isDead || ui.isTouchGoal)
            return;

        if (Vector2.Distance(transform.position, player.transform.position) > attackRange)
        {
            curState = State.Trace;
        }

        // Attack �ൿ�� ����
        if(player.playerHp <= 0)
            return;

        player.playerHp -= attackPower;

    }

    private void Dead()
    {
        // Dead �ൿ�� ����
        Debug.Log("������ ����");

        // �ٸ� ���·� ��ȯ
        

    }

}
