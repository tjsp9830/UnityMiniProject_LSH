using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGumbaHit : MonoBehaviour
{
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            Debug.Log("±À¹Ù ÂîºÎµÅ½á");
        }


    }




}
