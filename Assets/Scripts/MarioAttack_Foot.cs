using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MarioAttack_Foot : MonoBehaviour
{

    //�̺�Ʈ
    public UnityAction OnFootAttack; //�浹�� �����ϴ� �̺�Ʈ

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log($"{collision.gameObject.name}�� �ε���");

        if (collision.gameObject.layer == 14) //14�� HitArea
        {
            OnFootAttack?.Invoke();
        }

    }

}
