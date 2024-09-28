using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMove : PlayerAnimation
{
    //컴포넌트
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] SpriteRenderer render;
    [SerializeField] Animator animator;
    [SerializeField] LayerMask groundMask;

    //이동
    float posX;
    [SerializeField] float moveSpeed = 30f;
    [SerializeField] float maxMoveSpeed = 10f;

    //점프
    Vector2 lay;
    [SerializeField] bool isGrounded;
    [SerializeField] float jumpPower = 10f;
    [SerializeField] float maxFallSpeed = 150f;

    //이벤트
    public UnityAction OnClear; //클리어 이벤트
    public UnityAction OnEatRedMushroom; //빨간버섯 이벤트(먹으면 1단계 진화)

    //마리오 진화단계 (0, 1, 2)
    [SerializeField] int curLevel;

    //애니메이션 해싱관련
    private int CheckAniHash;
    private int curAniHash;



    private void Awake()
    {
        curLevel = 0;
    }


    private void FixedUpdate()
    {
        PlayerMoving();        
                
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, Vector2.down * 0.2f, Color.red, 0.1f);


        posX = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerJump();
        }
        GroundCheck();

    }



    private void OnTriggerEnter2D(Collider2D _collision)
    {

        if (_collision.gameObject.layer == 7)
        {
            OnClear?.Invoke();
        }
        else if (_collision.gameObject.tag == "Clear")
        {
            OnClear?.Invoke();

        }
    }



    private void PlayerMoving()
    {
        
        rigid.AddForce(Vector2.right * posX * moveSpeed, ForceMode2D.Force);

        if (rigid.velocity.x > maxMoveSpeed)
        {
            rigid.velocity = new Vector2(maxMoveSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < -maxMoveSpeed)
        {
            rigid.velocity = new Vector2(-maxMoveSpeed, rigid.velocity.y);
        }


        if (rigid.velocity.y < -maxFallSpeed)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, -maxFallSpeed);
        }


        if (posX < 0)
        {
            render.flipX = true;
        }
        else if (posX > 0)
        {
            render.flipX = false;
        }


    }


    private void PlayerJump()
    {
        if (isGrounded == false)
            return;

        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        isGrounded = false;

    }

    

    private void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, groundMask);


        if (hit.collider != null)
        {            
            isGrounded = true;

        }
        else
        {            
            isGrounded = false;
        }
        
    
    }

    

    public void AnimatorPlay()
    {

        // float의 특징상, velocity값이 정확히 0이 아닐수 있어서 애니재생의 오류를 없애기 위함

        //점프 (뛰거나 떨어지기)
        if (rigid.velocity.y > 0.01f)
        {
            CheckAniHash = P0_Jump_Hash;
                    
        }
        else if (rigid.velocity.y < -0.01f)
        {
            
            CheckAniHash = P0_Jump_Hash;
                    
        }

        //이동 (걷거나 뛰기)
        else if (rigid.velocity.sqrMagnitude < 0.01f)
        {
            
            CheckAniHash = P0_Idle_Hash;
                    

        }
        else
        {
            
            CheckAniHash = P0_Run_Hash;
                    

        }
        

        //애니가 기존과 다를때만 실행, 프레임마다 계속 호출하지 않게됨 (애니 중복재생 막기)
        if (curAniHash != CheckAniHash)
        {
            curAniHash = CheckAniHash;
            animator.Play(curAniHash);
        }

    }


}
