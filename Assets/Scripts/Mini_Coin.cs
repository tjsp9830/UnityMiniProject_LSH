using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Mini_Coin : MonoBehaviour
{
    

    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
        }

    }

    //private void OnDisable()
    //{
    //    uiScript.PickUpCoin();
    //}

    
}
