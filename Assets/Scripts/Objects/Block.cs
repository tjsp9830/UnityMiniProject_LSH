using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Block Parent Script
// ��ϵ��� �θ� ��ũ��Ʈ
// ��ϵ��� hit ����� �׿����� ���� �뵵�� ����� ��

// ����:  (������=�÷��̾ �ָ����� ġ�� �� ���� �ö󰬴ٰ� �������� �ִ�), ������ �����ϱ�, ��������Ʈ �ٲ�, �����


public class Block : MonoBehaviour
{
    [SerializeField] GameObject rigidObject;
    [SerializeField] protected float movingRange;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"��: {collision.gameObject.layer}��, {collision.gameObject.name}");

        if (collision.gameObject.tag == "Player")
        {
            Invoke("ReAct", 0f);
            Invoke("BlockDespawn", 1f);

        }
    }



    protected void ReAct()
    {
        // ���� ���� ����̴� �ִϸ��̼� ���

        //Invoke("Went", 0f);
        //Invoke("Back", 0.5f);
        //Invoke("CreateItem", 0.05f);
        rigidObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * movingRange, ForceMode2D.Impulse);

        Debug.Log("���� ���ŷȴ�");
    }


    // �����: �Ϲ�
    protected void BlockDespawn()
    {
        // ��� ����� 
        Debug.Log("���� �������");
        this.gameObject.SetActive(false);        
    }



}



/*
 * �θ� ��ũ��Ʈ ����
public abstract class Block : MonoBehaviour
{
    
    [SerializeField] protected SpriteRenderer render;
    [SerializeField] protected Sprite blockOff;
    [SerializeField] GameObject rigidObject;

    [SerializeField] protected float movingRange;
    Vector3 curPos;
    Vector3 reActPos;

    protected UnityAction OnTouched;

    private void Start()
    {
        curPos = transform.position;
        reActPos = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
    }



    // [����] ������: �Ϲ�, ����, ����
    protected void ReAct()
    {
        // ���� ���� ����̴� �ִϸ��̼� ���

        //Invoke("Went", 0f);
        //Invoke("Back", 0.5f);
        //Invoke("CreateItem", 0.05f);
        rigidObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * movingRange, ForceMode2D.Impulse);

        Debug.Log("���� ���ŷȴ�");
    }

    protected void Went()
    {
        transform.Translate(reActPos * movingRange);        
    }

    protected void Back()
    {
        transform.Translate(curPos * movingRange);
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
*/



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