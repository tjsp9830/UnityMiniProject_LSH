using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class EnemyGumba : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] bool isDead = false; //T���� F����
    [SerializeField] bool isWall = false; //F�������̵� T�����̵�
    [SerializeField] LayerMask groundMask;


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

    //private void ColliderCheck()
    //{
    //    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 0.1f, groundMask);


    //    if (hit.collider != null)
    //    {
    //        Debug.Log($"{hit}");
    //        Debug.Log($"{hit.collider}"); //���̸��� �� �δ�ġ�� �ڵ��°ɷ� �ٲ㺸��
    //        //���̶� ����
    //        isWall = !isWall;

    //    }

    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9) //9���� Pipe
        {
            Debug.Log($"{collision}");
            Debug.Log($"{collision.gameObject}");
            //���̶� ����
            isWall = !isWall;

        }

    }


    //�� �Ӹ� �ݶ��̴��� �÷��̾� �� �ݶ��̴��� �ε�����
    //�̵� ���߰�, isDead ó���ϰ�, ���� �ִϸ��̼�, ���� ���ֱ�, �� +100

}
