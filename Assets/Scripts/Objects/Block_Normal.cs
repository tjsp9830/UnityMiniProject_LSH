//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Block_Normal : Block
//{

//    //private void Start()
//    //{
//    //    OnTouched += base.ReAct;
//    //}

//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        Debug.Log($"��: {collision.gameObject.layer}��, {collision.gameObject.name}");

//        if (collision.gameObject.tag == "Player")
//        {
            
//            base.ReAct();
//            OnTouched?.Invoke();
//        }
//    }



//    // �Ϲݺ�: ReAct() --> CreateItem() --> BlockDespawn()
//    // Ư����: ReAct() --> CreateItem() --> ChangeSprite()
//    protected override void CreateItem()
//    {
//        if (OnTouched == null)
//            return;

//        Debug.Log("abstract�Լ� \'CreateItem\' ����");
//        Invoke("base.BlockDespawn", 1f);
//    }





//}
