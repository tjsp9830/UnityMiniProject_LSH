using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    //�ǰ�
    [SerializeField] GameObject hitArea; //�ߺκ��� �� �÷��̾� �� ��ü


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8) //8�� Monster
        {
            MarioHit();
        }



    }

    public void MarioHit()
    {
        Debug.Log("�÷��̾ �ε�����");
        //�÷��̾� �ǰ�: �����ؼ� ���� ���� �����ϸ� ����
        // �̺�Ʈ �ְ�ޱ�, ���ӿ��� UI,
        // ���Ŀ��� Ŀ���� �پ��°� �����ϸ� �ִϸ��̼ǵ� �߰�,
        // �ڷ�ƾ���� ���� ��� ���ߴ��� �ϱ�, ��� ����ó��,

    }





}
