using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    // 애니메이션 해싱
    protected static int P0_Die_Hash; //P0의 Hit은 바로 사망이기 때문에 Die를 넣음
    protected static int P0_Idle_Hash;
    protected static int P0_Run_Hash;
    protected static int P0_Jump_Hash;
    protected static int P0_Change_Hash;

    protected static int P1_Hit_Hash;
    protected static int P1_Idle_Hash;
    protected static int P1_Run_Hash;
    protected static int P1_Jump_Hash;
    protected static int P1_Change_Hash;

    protected static int P2_Hit_Hash;
    protected static int P2_Idle_Hash;
    protected static int P2_Run_Hash;
    protected static int P2_Jump_Hash;
    protected static int P2_Fire_Idle_Hash; //P2는 Change가없고 Fire의 상태 두개만 있음
    protected static int P2_Fire_Walk_Hash; //P2는 Change가없고 Fire의 상태 두개만 있음


    //public void AnimatorReady()
    private void Awake()    
    {
        P0_Die_Hash = Animator.StringToHash("P0_Die");
        P0_Idle_Hash = Animator.StringToHash("P0_Idle");
        P0_Run_Hash = Animator.StringToHash("P0_Run");
        P0_Jump_Hash = Animator.StringToHash("P0_Jump");
        P0_Change_Hash = Animator.StringToHash("P0_Change");

        P1_Hit_Hash = Animator.StringToHash("P1_Hit");
        P1_Idle_Hash = Animator.StringToHash("P1_Idle");
        P1_Run_Hash = Animator.StringToHash("P1_Run");
        P1_Jump_Hash = Animator.StringToHash("P1_Jump");
        P1_Change_Hash = Animator.StringToHash("P1_Change");

        P2_Hit_Hash = Animator.StringToHash("P2_Hit");
        P2_Idle_Hash = Animator.StringToHash("P2_Idle");
        P2_Run_Hash = Animator.StringToHash("P2_Run");
        P2_Jump_Hash = Animator.StringToHash("P2_Jump");
        P2_Fire_Idle_Hash = Animator.StringToHash("P2_Fire_Idle");
        P2_Fire_Walk_Hash = Animator.StringToHash("P2_Fire_Walk");
    }


}
