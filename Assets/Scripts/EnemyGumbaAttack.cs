using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGumbaAttack : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {        
        if (collision.gameObject.layer == 3) //8번레이어 Monster
        {
            GumbaAttack();
        }



    }

    public void GumbaAttack()
    {
        Debug.Log("굼바의 몸통박치기");
    }


}
