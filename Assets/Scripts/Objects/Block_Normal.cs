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
//        Debug.Log($"블럭: {collision.gameObject.layer}번, {collision.gameObject.name}");

//        if (collision.gameObject.tag == "Player")
//        {
            
//            base.ReAct();
//            OnTouched?.Invoke();
//        }
//    }



//    // 일반블럭: ReAct() --> CreateItem() --> BlockDespawn()
//    // 특수블럭: ReAct() --> CreateItem() --> ChangeSprite()
//    protected override void CreateItem()
//    {
//        if (OnTouched == null)
//            return;

//        Debug.Log("abstract함수 \'CreateItem\' 진입");
//        Invoke("base.BlockDespawn", 1f);
//    }





//}
