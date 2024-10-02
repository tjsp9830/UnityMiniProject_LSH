using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Block Parent Script
// ��ϵ��� �θ� ��ũ��Ʈ
// ��ϵ��� hit ����� �׿����� ���� �뵵�� ����� ��

// ����:  (������=�÷��̾ �ָ����� ġ�� �� ���� �ö󰬴ٰ� �������� �ִ�), ������ �����ϱ�, ��������Ʈ �ٲ�, �����


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
    //        Debug.Log($"��: {collision.gameObject.name}");
    //        OnTouched?.Invoke();
    //    }
    //}


    // [����] ������: �Ϲ�, ����, ����
    protected void ReAct()
    {
        // ���� ���� ����̴� �ִϸ��̼� ���
        Debug.Log("���� ����δ�");
        
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



    // [�߻�]������: ����, ����

    protected abstract void CreateItem();
    //{
    //// ���� or ����1 or ����2 or ��Ÿ�� Ƣ���
    //// Debug.Log("�������� ���´�");
    //// �۵� Ƣ��ö� ���� �������� Ƣ��;� ��
    //// ������ �� ���� �����
    //// ����1�� �� ���� ���������� �̵��� (y�� �������)
    //// ����2�� ��Ÿ�� �� ���� ��� �� ��������
    //}



    // �����: �Ϲ�
    protected void BlockDespawn()
    {
        // ��� ����� 
        Debug.Log("���� �������");
        this.gameObject.SetActive(false);
    }


    // ��������Ʈ �ٲ�: ����, ����
    protected void ChangeSprite()
    {
        render.sprite = blockOff;

    }

    




}



/*
����� 3�����̰� (�Ϲݺ�, ������, �����ۺ�)
�������� 4������ (����, ��������, �ʷϹ���, ��)

���⼭ (������=�÷��̾ �ָ����� ġ�� �� ���� �ö󰬴ٰ� �������� �ִ�)
�Ϲݺ�: ������, �����
������: ������, ������, ��������Ʈ �ٲ�, �����
���ۺ�: ������, ������, ��������Ʈ �ٲ�, ����� �̰�,

�������� (�⺻�ִ�=�����ɶ� Ƣ����� �ִ�, ������=���������� �̵�)
��������: ������, �⺻�ִ�, ������, �����, UI �ڵ� �ǵ帲(����)
��������: ������, �⺻�ִ�, ������, �����, �÷��̾� �ڵ� �ǵ帲(ũ��)
�ʷϹ���: ������, �⺻�ִ�, ������, �����, UI �ڵ� �ǵ帲(���)
��������: ������, �⺻�ִ�, ������, �����, UI �ڵ� �ǵ帲(����)
 */