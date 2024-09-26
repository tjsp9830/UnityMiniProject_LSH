using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAttack : MonoBehaviour
{
    //공격
    [SerializeField] GameObject attackArea; //발 부분만 공격, 제외한 전체는 피격
    public UnityAction OnPlayerAttack;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.name == "HitArea") //8번레이어 Monster
        if (collision.gameObject.layer == 8) //8번레이어 Monster
        {
            //collision.gameObject.GetComponent<EnemyGumba>().GumbaHit();

            MarioAttack();
        }



    }

    public void MarioAttack()
    {
        Debug.Log("플레이어가 굼바를 밟았다!!!!!");

        OnPlayerAttack?.Invoke();

    }

}
