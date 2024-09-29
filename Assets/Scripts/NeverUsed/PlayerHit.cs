using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    //피격
    [SerializeField] GameObject hitArea; //발부분을 뺀 플레이어 그 자체
        
    Coroutine timeStoper;
    [SerializeField] float repeatTime;

    private void Start()
    {
        //timeStoper = StartCoroutine(TimeStopRoutine());

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8) //8번 Monster
        {
            MarioHit();
        }


    }

    //변신단계가 1,2였다면
    public void MarioHit()
    {
        Debug.Log("플레이어가 부딪혔다");
        // 코루틴 등으로 게임 2초간 멈추고
        timeStoper = StartCoroutine(TimeStopRoutine());
        // 줄어드는 애니메이션 실행,
        // 변신단계 낮추기,

    }


    // 변신단계가 0이었다면
    public void MarioDie()
    {
        //게임 멈추지 않고
        //마리오만 멈춰서
        //애니메이션 P0_Die로 바꿔주고 
        //의도한대로 튕겨서 떨어지기


    }


    IEnumerator TimeStopRoutine()
    {
        Debug.Log("코루틴 진입");
        WaitForSecondsRealtime delay = new WaitForSecondsRealtime(repeatTime);

        Time.timeScale = 0f;
        yield return delay;
        Time.timeScale = 1f;
        Debug.Log("코루틴 끝남");
    }

}
