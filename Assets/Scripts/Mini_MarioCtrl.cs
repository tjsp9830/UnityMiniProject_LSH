using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public class Mini_MarioCtrl : MonoBehaviour
{
    // I tried to make a "Player Finite-State-Machine".

    // ------------------------------------------------------------------------------------------
    // ----------------------------------------- 필드 -------------------------------------------
    // ------------------------------------------------------------------------------------------


    
    //상태패턴
    public enum State { Idle, Run, Jump, Change, Die }
    [SerializeField] State curState;

    //과제를 위해 임시적으로 hp를 부여
    [SerializeField] public int playerHp = 100;
    //[SerializeField] bool isHit; //F안맞음 T한대맞음
    [SerializeField] bool isDead; //F살아있음 T죽었음
    [SerializeField] Mini_Eagle mob;
    [SerializeField] GameObject foots;


    //코인
    [SerializeField] Mini_UICtrl ui;
    [SerializeField] Mini_Coin coin;


    //컴포넌트
    [SerializeField] SpriteRenderer render;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] Animator animator;
    //[SerializeField] LayerMask groundMask; 8, 11

    //이동
    float posX;
    [SerializeField] float moveSpeed = 30f;
    [SerializeField] float maxMoveSpeed = 10f;

    //점프
    Vector2 lay;
    [SerializeField] float jumpPower = 10f;
    [SerializeField] float maxFallSpeed = 150f;
    [SerializeField] bool isGrounded;

    //이벤트
    public UnityAction OnTouchDoor; //클리어 이벤트
    public UnityAction OnPlayerDead; //게임오버 이벤트
    public UnityAction OnEatRedMushroom; //빨간버섯 이벤트(먹으면 1단계 진화)
    public UnityAction OnPickUpCoin; //코인 획득 이벤트


    ////마리오 진화단계 (0, 1, 2)
    //    [SerializeField] int curLevel;
    bool isChange;

    // 애니메이션 해싱 (해시테이블 그거)
    private int curAniHash;
    private static int idleHash;
    private static int runHash;
    private static int jumpHash;
    private static int changeHash;
    private static int dieHash;





    // ------------------------------------------------------------------------------------------
    // ---------------------------------- 유니티 메시지 함수 -------------------------------------
    // ------------------------------------------------------------------------------------------



    private void Awake()
    {
        playerHp = 100;
        isDead = false;
        isChange = false;
        //isHit = false;
        curState = State.Idle;
        //curLevel = 0;
        idleHash = Animator.StringToHash("P0_Idle");
        runHash = Animator.StringToHash("P0_Run");
        jumpHash = Animator.StringToHash("P0_Jump");
        changeHash = Animator.StringToHash("P0_Change");
        dieHash = Animator.StringToHash("P0_DIe");
    }


    private void Start()
    {

    }


    private void FixedUpdate()
    {
        PlayerMove();

    }


    private void Update()
    {
        Debug.DrawRay(transform.position, Vector2.down * 0.2f, Color.red, 0.1f);

        if (Input.GetKeyDown(KeyCode.K)) //Kill
        {
            playerHp = 0;
        }

        if (Input.GetKeyDown(KeyCode.C)) //Change
        {
            PlayerGrowUp();
        }


        posX = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerJump();
        }
        GroundCheck();
        AnimatorPlay();

        switch (curState)
        {
            case State.Idle:
                Idle();
                break;

            case State.Run:
                Run();
                break;

            case State.Jump:
                Jump();
                break;

            // (버섯아이템 추가하기)
            case State.Change:
                Change();
                break;

            case State.Die:
                Die();
                break;

        }

        AnyState();


    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Door")
        {
            OnTouchDoor?.Invoke();
        }
        if (collision.gameObject.layer == 10) //10 코인
        {
            ui.coinCount += 1;           

        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
    }



    // ------------------------------------------------------------------------------------------
    // -------------------------- 사용자 커스텀 함수, 이동&점프&땅체크 ----------------------------
    // ------------------------------------------------------------------------------------------



    /// <summary>
    /// 플레이어 이동 함수
    /// </summary>
    private void PlayerMove()
    {
        if (isDead == true)
            return;


        rigid.AddForce(Vector2.right * posX * moveSpeed, ForceMode2D.Force);

        //오른쪽 이동
        if (rigid.velocity.x > maxMoveSpeed)
        {
            rigid.velocity = new Vector2(maxMoveSpeed, rigid.velocity.y);
        }
        //왼쪽 이동
        else if (rigid.velocity.x < -maxMoveSpeed)
        {
            rigid.velocity = new Vector2(-maxMoveSpeed, rigid.velocity.y);
        }

        //아래로 이동
        if (rigid.velocity.y < -maxFallSpeed)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, -maxFallSpeed);
        }

        //이미지 플립
        if (posX < 0)
        {
            render.flipX = true;
        }
        else if (posX > 0)
        {
            render.flipX = false;
        }


    }


    /// <summary>
    /// 플레이어 점프 함수
    /// </summary>
    private void PlayerJump()
    {
        if (isDead == true)
            return;

        if (isGrounded == false)
            return;

        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

    }


    /// <summary>
    /// 점프 땅 체크 함수
    /// </summary>
    private void GroundCheck()
    {
        
        RaycastHit2D hit08 = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, 8);  // 08 Ground
        RaycastHit2D hit11 = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, 11); // 11 Pipe

        // 지정해 둔 groundMask 안에서만 collider를 체크하게 되므로 다음 로직이 성립한다.

        if (hit08.collider != null || hit11.collider != null)
        {
            isGrounded = true;

        }
        else
        {
            isGrounded = false;
        }

    }



    private void PlayerGrowUp()
    {
        Debug.Log("변신4");
        isChange = true;
    }


    private void PlayerDie()
    {
        isDead = true;
        Debug.Log("마리오 죽었슈");
        OnPlayerDead?.Invoke();

        //마리오가 위로 뿅 떴다가 가라앉고,
        //왼쪽 위로 포물선을 그리며 땅 아래로 떨어지는 애니메이션을 만들어서 출력하기.

    }





    // ------------------------------------------------------------------------------------------
    // ------------------------- 사용자 커스텀 함수, 애니메이션 상태머신 ---------------------------
    // ------------------------------------------------------------------------------------------



    private void AnyState()
    {
        // 언제든 특정 조건 성립시, 특정 상태로 전환

        if (playerHp <= 0)
        {
            curState = State.Die;
        }

        if (isChange == true)
        {
            Debug.Log("변신1");
            curState = State.Change;
        }

        //// (버섯아이템 추가 이전이고,  Loop하지 않는 애니의 경우 처리가 어려워서  주석처리함)
        //else if (버섯먹음)
        //{
        //    curState = State.Change;
        //}

    }

    private void Idle()
    {
        // Idle 행동 (조건: rigid.velocity.sqrMagnitude < 0.01f)

        // 다른 상태로 전환
        if (playerHp <= 0)
        {
            curState = State.Die;
        }
        else if (isGrounded == false && rigid.velocity.y > 0.01f)
        {

            curState = State.Jump;

        }
        else if(isGrounded == true && rigid.velocity.y < 0.01f && posX != 0)
        {
            curState = State.Run;
        }
        else { }
        

    }




    private void Run()
    {
        // Run 행동 (조건: rigid.velocity.sqrMagnitude > 0.01f)
        PlayerMove();

        // 다른 상태로 전환
        if (playerHp <= 0)
        {
            curState = State.Die;
        }
        else if (isGrounded == false && rigid.velocity.y > 0.01f)
        {
            curState = State.Jump;
        }
        else if (isGrounded == true && rigid.velocity.sqrMagnitude < 0.01f) //isGrounded == true && rigid.velocity.y < 0.01f
        {
            curState = State.Idle;

        }
        else { }


    }

    private void Jump()
    {
        // Jump 행동 (조건: rigid.velocity.y > 0.01f)
        if (Input.GetKeyDown(KeyCode.Space))
            PlayerJump();

        // 다른 상태로 전환
        if (playerHp <= 0)
        {
            curState = State.Die;
        }
        else if (rigid.velocity.y < 0.01f)
        {
            curState = State.Idle;
        }
        else { }

        //버섯 업글 예정


    }

    // (버섯아이템 추가 이전이고,  Loop하지 않는 애니의 경우 처리가 어려워서  주석처리함)
    private void Change()
    {
        Debug.Log("변신3");
        // Change 행동 조건: 버섯을 먹음 (isChange==true로 대체)
        PlayerGrowUp();

        // 일단 전환하지 않음 (나중에 1단계, 2단계 애니메이션 추가할때 다시 확인)
        

    }

    private void Die()
    {
        // Die 행동 (조건: hp <= 0)
        PlayerDie();

        // 다른 상태로 전환하지 않음

    }



    /// <summary>
    /// 애니메이터 플레이 함수
    /// </summary>
    private void AnimatorPlay()
    {

        // 2D는 블렌드가 안되기 때문에, 밸로시티값 크기가 작아지면 전환을 주어 자연스럽게 해줌
        // float의 특징상, velocity값이 정확히 0이 아닐수 있어서 애니재생의 오류를 없애기 위함

        int CheckAniHash;


        //변신
        if (curState == State.Change)
        {
            Debug.Log("변신2");
            CheckAniHash = changeHash;
            isChange = false;
            Debug.Log("변신222222222");
        }

        //사망
        if (curState == State.Die)
        {
            CheckAniHash = dieHash;
        }

        //점프
        else if (curState == State.Jump)
        {
            CheckAniHash = jumpHash;
        }

        //멈춤 or 이동
        else if (curState == State.Idle)
        {
            CheckAniHash = idleHash;
        }
        else
        {
            CheckAniHash = runHash;
        }


        //애니가 기존과 다를때만 실행, 프레임마다 계속 호출하지 않게됨 (점프애니 재생이 엄청나지는것 픽스)
        if (curAniHash != CheckAniHash)
        {
            Debug.Log("변신7");
            curAniHash = CheckAniHash;
            Debug.Log("변신8");
            animator.Play(curAniHash);
            Debug.Log("변신9");
        }


    }


}
