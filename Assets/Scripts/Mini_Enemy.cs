using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mini_Enemy : MonoBehaviour
{
    
    // 공통된 필드
    //[SerializeField] Animation ani;
    //[SerializeField] float moveSpeed = 1f;


    //생성자에서 애니 초기화 (필수인지 생각해보기)
    public Mini_Enemy()
    {
        //ani = GetComponent<Animation>();
    }



    // 메서드를 FSM쓴 Eagle클래스 참고해서 3개를 서로 연결해줘야 할듯


    //이동 함수
    //굼바, 거북: x축을 따라서 이동, 프로그램 실행시 시작, 방향은 오른쪽 (이후 왼쪽으로 반전됨)
    // 꽃:  y축을 따라서 이동, 플레이어 감지시 시작, 방향은 위쪽 (이후 플레이어 멀어지면 아래로 이동함)
    public abstract void EnemyMove();

    //감지하는 함수
    //굼바, 거북: 벽 감지하는 함수. 벽이랑 부딪히면 진행방향 반전
    // 꽃:  플레이어 감지하는 함수. 캐릭터가 근처에 오면 이동
    public abstract void EnemyDetect();

    //애니메이션 플레이 함수
    //굼바, 거북: 걷는 애니메이션 실행
    // 꽃:  입 콰작콰작 애니메이션 실행
    public abstract void EnemyAnimePlay();


}
