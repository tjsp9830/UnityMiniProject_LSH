using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Block_Normal : Block
{

    //private void Start()
    //{
    //    OnTouched += base.ReAct;
    //}

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.layer == 15)
    //    {
    //        Debug.Log($"��: {collision.gameObject.name}");
    //        base.ReAct();
    //        OnTouched?.Invoke();
    //    }
    //}



    // �Ϲݺ�: ReAct() --> CreateItem() --> BlockDespawn()
    // Ư����: ReAct() --> CreateItem() --> ChangeSprite()
    protected override void CreateItem()
    {
        if (OnTouched == null)
            return;

        Debug.Log("abstract�Լ� \'CreateItem\' ����");
        Invoke("base.BlockDespawn", 1f);
    }





}
