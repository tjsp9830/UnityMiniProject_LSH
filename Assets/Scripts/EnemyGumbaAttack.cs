using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGumbaAttack : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {        
        if (collision.gameObject.layer == 3) //8�����̾� Monster
        {
            GumbaAttack();
        }



    }

    public void GumbaAttack()
    {
        Debug.Log("������ �����ġ��");
    }


}
