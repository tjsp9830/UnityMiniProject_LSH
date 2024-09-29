using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurtle : MonoBehaviour
{

    // ����� �ʵ�
    [SerializeField] SpriteRenderer render;
    [SerializeField] CapsuleCollider2D collider;
    [SerializeField] Animator ani;
    [SerializeField] private float moveSpeed;

    //�� �����ϱ�
    [SerializeField] bool isDead = false; //T���� F����
    [SerializeField] bool isLeft = false; //T�����̵� F�������̵� 
    [SerializeField] LayerMask groundMask; //11�� Pipe��

    //�ݶ��̴� ���� ��ũ��Ʈ �������� �����԰�, �ݶ��̴��� �ڽ� �ݸ����� ��ũ��Ʈ�� ���� ���� �߰�����
    [SerializeField] EnemyTurtleHit hitArea;



    //�ϴ� MonoBehaviour�� �۵���, ���� �����丵 �ʿ�
    private void Awake()
    {
        moveSpeed = 1f;
    }

    private void Start()
    {
        hitArea.OnHitTurtle += EnemyHit;
    }

    private void FixedUpdate()
    {
        TurtleMove();
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, Vector2.right * 0.2f, Color.red, 0.1f);

    }

    private void TurtleMove()
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


        if (collision.gameObject.layer == 11) //11�� Pipe
        {
            //���̶� ����
            isLeft = !isLeft;           

        }

        if (collision.gameObject.layer == 3) //3�� Player
        {
            //TurtleAttack()
            Debug.Log("�ź��� �����ġ��");
        }


    }


    public void EnemyHit()
    {
        isDead = true;
        ani.SetTrigger("isTurtleZziboo");

        StartCoroutine(WaitForItT());
    }

    //�� �Ӹ� �ݶ��̴��� �÷��̾� �� �ݶ��̴��� �ε�����
    //�̵� ���߰�, isDead ó���ϰ�, ���� �ִϸ��̼�, ���� ���ֱ�, ���� +100

    IEnumerator WaitForItT()
    {
        collider.enabled = false;
        hitArea.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }











    //  [�����丵 �ʿ� ����]


    //public EnemyTurtle()
    //{
    //}



    //// 1.�̵� (x�� �̵�, ������ ������. ���α׷� ����� ����)
    //public override void EnemyMove()
    //{

    //}

    //// 2.���� (�� ����, ���̶� �ε����� ������� ����)
    //public override void EnemyDetect()
    //{

    //}

    //// 3.�ִ� (�ȴ� �ִϸ��̼� ����, �״� �ִϷ� ��ȯ �ʿ�)
    //public override void EnemyAnimePlay()
    //{

    //}



}