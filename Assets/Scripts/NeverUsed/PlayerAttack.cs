using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAttack : MonoBehaviour
{
    //����
    [SerializeField] GameObject attackArea; //�� �κи� ����, ������ ��ü�� �ǰ�
    public UnityAction OnPlayerAttack;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.name == "HitArea") //8�����̾� Monster
        if (collision.gameObject.layer == 8) //8�����̾� Monster
        {
            //collision.gameObject.GetComponent<EnemyGumba>().GumbaHit();

            MarioAttack();
        }



    }

    public void MarioAttack()
    {
        Debug.Log("�÷��̾ ���ٸ� ��Ҵ�!!!!!");

        OnPlayerAttack?.Invoke();

    }

}
