using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemyTurtleHit : MonoBehaviour
{

    //�������̽��� Ȯ��Ŭ����??�� ����� �; I~���̹� �Ͽ�����
    //�������̽��� Animator�� �޾ƿü� ���°Ű��Ƽ� �ϴ��� �ð��� ����

    [SerializeField] GameObject turtleHitArea;
    public UnityAction OnHitTurtle;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            Debug.Log("�ź� ��εŽ�");
            OnHitTurtle?.Invoke();
        }


    }



}
