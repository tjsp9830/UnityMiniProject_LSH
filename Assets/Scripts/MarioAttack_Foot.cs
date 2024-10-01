using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MarioAttack_Foot : MonoBehaviour
{

    //이벤트
    public UnityAction OnFootAttack; //충돌시 공격하는 이벤트

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log($"{collision.gameObject.name}랑 부딪힘");

        if (collision.gameObject.layer == 14) //14번 HitArea
        {
            OnFootAttack?.Invoke();
        }

    }

}
