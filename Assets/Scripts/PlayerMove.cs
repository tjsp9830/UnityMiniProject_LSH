using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] GameObject deadLineBox;

    public UnityAction OnClear;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Door")
        {
            Debug.Log(1);
            OnClear?.Invoke();
        }


    }



}
