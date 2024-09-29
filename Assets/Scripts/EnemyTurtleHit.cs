using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemyTurtleHit : MonoBehaviour
{

    //인터페이스나 확장클래스??로 만들고 싶어서 I~네이밍 하였으나
    //인터페이스는 Animator를 받아올수 없는거같아서 일단은 시간상 포기

    [SerializeField] GameObject turtleHitArea;
    public UnityAction OnHitTurtle;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            Debug.Log("거북 찌부돼써");
            OnHitTurtle?.Invoke();
        }


    }



}
