using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    //�ǰ�
    [SerializeField] GameObject hitArea; //�ߺκ��� �� �÷��̾� �� ��ü
        
    Coroutine timeStoper;
    [SerializeField] float repeatTime;

    private void Start()
    {
        //timeStoper = StartCoroutine(TimeStopRoutine());

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8) //8�� Monster
        {
            MarioHit();
        }


    }

    //���Ŵܰ谡 1,2���ٸ�
    public void MarioHit()
    {
        Debug.Log("�÷��̾ �ε�����");
        // �ڷ�ƾ ������ ���� 2�ʰ� ���߰�
        timeStoper = StartCoroutine(TimeStopRoutine());
        // �پ��� �ִϸ��̼� ����,
        // ���Ŵܰ� ���߱�,

    }


    // ���Ŵܰ谡 0�̾��ٸ�
    public void MarioDie()
    {
        //���� ������ �ʰ�
        //�������� ���缭
        //�ִϸ��̼� P0_Die�� �ٲ��ְ� 
        //�ǵ��Ѵ�� ƨ�ܼ� ��������


    }


    IEnumerator TimeStopRoutine()
    {
        Debug.Log("�ڷ�ƾ ����");
        WaitForSecondsRealtime delay = new WaitForSecondsRealtime(repeatTime);

        Time.timeScale = 0f;
        yield return delay;
        Time.timeScale = 1f;
        Debug.Log("�ڷ�ƾ ����");
    }

}
