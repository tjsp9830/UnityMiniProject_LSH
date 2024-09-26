using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    //피격
    [SerializeField] GameObject hitArea; //발부분을 뺀 플레이어 그 자체


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8) //8번 Monster
        {
            MarioHit();
        }



    }

    public void MarioHit()
    {
        Debug.Log("플레이어가 부딪혔다");
        //플레이어 피격: 점프해서 몬스터 위로 착지하면 실행
        // 이벤트 주고받기, 게임오버 UI,
        // 이후에는 커지고 줄어드는거 개발하면 애니메이션도 추가,
        // 코루틴으로 게임 잠깐 멈추던가 하기, 사망 물리처리,

    }





}
