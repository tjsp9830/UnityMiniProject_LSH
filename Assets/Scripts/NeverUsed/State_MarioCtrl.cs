using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.iOS;


public class State_MarioCtrl : MonoBehaviour
{

}

//    // I tried to make a "Player Hierarchy-Finite-State-Machine".



//    // ------------------------------------------------------------------------------------------
//    // ----------------------------------------- 필드 -------------------------------------------
//    // ------------------------------------------------------------------------------------------



//    //상태패턴 (HFSM)
//    public enum State { Idle, Run, Jump, GrowUp, Die, Size }
//    [SerializeField] State curState;
//    private State_Base[] states = new State_Base[(int)State.Size];
//    Vector2 startPos;

//    //과제를 위해 임시적으로 hp를 부여
//    [SerializeField] public int playerHp = 100;
//    //[SerializeField] bool isHit; //F안맞음 T한대맞음
//    [SerializeField] bool isDead; //F살아있음 T죽었음
//    [SerializeField] State_Eagle mob;
//    [SerializeField] GameObject foots;


//    //코인
//    [SerializeField] State_UICtrl ui;
//    [SerializeField] State_Coin coin;


//    //컴포넌트
//    [SerializeField] SpriteRenderer render;
//    [SerializeField] Rigidbody2D rigid;
//    [SerializeField] Animator animator;
//    [SerializeField] LayerMask groundMask;

//    //이동
//    float posX;
//    [SerializeField] float moveSpeed = 30f;
//    [SerializeField] float maxMoveSpeed = 10f;

//    //점프
//    Vector2 lay;
//    [SerializeField] float jumpPower = 10f;
//    [SerializeField] float maxFallSpeed = 150f;
//    [SerializeField] bool isGrounded;

//    //이벤트
//    public UnityAction OnTouchDoor; //클리어 이벤트
//    public UnityAction OnPlayerDead; //게임오버 이벤트
//    public UnityAction OnEatRedMushroom; //빨간버섯 이벤트(먹으면 1단계 진화)
//    public UnityAction OnPickUpCoin; //코인 획득 이벤트


//    ////마리오 진화단계 (0, 1, 2)
//    //    [SerializeField] int curLevel;
//    bool isGrowUp;

//    // 애니메이션 해싱 (해시테이블 그거)
//    private int curAniHash;
//    private static int idleHash;
//    private static int runHash;
//    private static int jumpHash;
//    private static int growUpHash;
//    private static int dieHash;


//    // ------------------------------------------------------------------------------------------
//    // ------------------------------------------------------------------------------------------
//    // ---------------------------------- 유니티 메시지 함수 -------------------------------------
//    // ------------------------------------------------------------------------------------------
//    // ------------------------------------------------------------------------------------------



//    private void Awake()
//    {
//        playerHp = 100;
//        isDead = false;
//        isGrowUp = false;
//        //isHit = false;
//        //curLevel = 0;
//        curState = State.Idle;

//        //애니메이터
//        idleHash = Animator.StringToHash("P0_Idle");
//        runHash = Animator.StringToHash("P0_Run");
//        jumpHash = Animator.StringToHash("P0_Jump");
//        growUpHash = Animator.StringToHash("P0_GrowUp");
//        dieHash = Animator.StringToHash("P0_DIe");

//        //상태머신 HFSM 배우면서 한거
//        states[(int)State.Idle] = new IdleState(this);
//        states[(int)State.Run] = new RunState(this);
//        states[(int)State.Jump] = new JumpState(this);
//        states[(int)State.GrowUp] = new GrowUpState(this);
//        states[(int)State.Die] = new DieState(this);

//    }


//    private void Start()
//    {
//        //상태머신 HFSM 배우면서 한거
//        startPos = transform.position;

//        states[(int)curState].Enter();
//    }

//    private void OnDestroy()
//    {
//        //상태머신 HFSM 배우면서 한거
//        states[(int)curState].Exit();

//    }

//    private void FixedUpdate()
//    {
//        PlayerMove();

//    }


//    private void Update()
//    {
//        Debug.DrawRay(transform.position, Vector2.down * 0.2f, Color.red, 0.1f);

//        if (Input.GetKeyDown(KeyCode.K)) //Kill
//        {
//            playerHp = 0;
//            ChangeState(State.Die); //명시 O? X? 
//        }

//        if (Input.GetKeyDown(KeyCode.G)) //GrowUp
//        {
//            isGrowUp = true;
//            ChangeState(State.GrowUp); //명시 O? X? 
//        }


//        posX = Input.GetAxisRaw("Horizontal");

//        if (Input.GetKeyDown(KeyCode.Space))
//        {
//            PlayerJump();
//        }
//        GroundCheck();
//        AnimatorPlay();


//        Debug.Log("switch문 돌아감");
//        switch (curState)
//        {
//            case State.Idle:
//                //ChangeState(State.Idle);
//                states[(int)State.Idle].Update();
//                break;

//            case State.Run:
//                //ChangeState(State.Run);
//                states[(int)State.Run].Update();
//                break;

//            case State.Jump:
//                //ChangeState(State.Jump);
//                states[(int)State.Jump].Update();
//                break;

//            // (버섯아이템 추가하기)
//            //case State.GrowUp:
//            //    //ChangeState(State.GrowUp);
//            //    states[(int)State.GrowUp].Update();
//            //    break;

//            case State.Die:
//                //ChangeState(State.Die);
//                states[(int)State.Die].Update();
//                break;

//        }

//        AnyState();

//    }




//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.tag == "Door")
//        {
//            OnTouchDoor?.Invoke();
//        }
//        if (collision.name == "Coin")
//        {
//            ui.coinCount += 1;

//        }

//    }


//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        Debug.Log(collision.gameObject.name);
//    }







//    // ------------------------------------------------------------------------------------------
//    // ------------------------------------------------------------------------------------------
//    // -------------------------- 사용자 커스텀 함수, 이동&점프&땅체크 ----------------------------
//    // ------------------------------------------------------------------------------------------
//    // ------------------------------------------------------------------------------------------



//    /// <summary>
//    /// 플레이어 이동 함수
//    /// </summary>
//    private void PlayerMove()
//    {
//        if (isDead == true)
//            return;


//        rigid.AddForce(Vector2.right * posX * moveSpeed, ForceMode2D.Force);

//        //오른쪽 이동
//        if (rigid.velocity.x > maxMoveSpeed)
//        {
//            rigid.velocity = new Vector2(maxMoveSpeed, rigid.velocity.y);
//        }
//        //왼쪽 이동
//        else if (rigid.velocity.x < -maxMoveSpeed)
//        {
//            rigid.velocity = new Vector2(-maxMoveSpeed, rigid.velocity.y);
//        }

//        //아래로 이동
//        if (rigid.velocity.y < -maxFallSpeed)
//        {
//            rigid.velocity = new Vector2(rigid.velocity.x, -maxFallSpeed);
//        }

//        //이미지 플립
//        if (posX < 0)
//        {
//            render.flipX = true;
//        }
//        else if (posX > 0)
//        {
//            render.flipX = false;
//        }


//    }


//    /// <summary>
//    /// 플레이어 점프 함수
//    /// </summary>
//    private void PlayerJump()
//    {
//        if (isDead == true)
//            return;

//        if (isGrounded == false)
//            return;

//        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

//    }


//    /// <summary>
//    /// 점프 땅 체크 함수
//    /// </summary>
//    private void GroundCheck()
//    {
//        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, groundMask);

//        // 지정해 둔 groundMask 안에서만 collider를 체크하게 되므로 다음 로직이 성립한다.

//        if (hit.collider != null)
//        {
//            isGrounded = true;

//        }
//        else
//        {
//            isGrounded = false;
//        }

//    }



//    private void PlayerGrowUp()
//    {
//        isGrowUp = true;
//    }


//    private void PlayerDie()
//    {
//        isDead = true;
//        OnPlayerDead?.Invoke();

//        //마리오가 위로 뿅 떴다가 가라앉고,
//        //왼쪽 위로 포물선을 그리며 땅 아래로 떨어지는 애니메이션을 만들어서 출력하기.

//    }







//    // ------------------------------------------------------------------------------------------
//    // ------------------------------------------------------------------------------------------
//    // ---------- 사용자 커스텀 함수,  상태머신 HFSM 배우면서 한거 & 애니메이션 상태머신 -------------
//    // ------------------------------------------------------------------------------------------
//    // ------------------------------------------------------------------------------------------



//    private void AnyState()
//    {
//        // 언제든 특정 조건 성립시, 특정 상태로 전환

//        if (playerHp <= 0)
//        {
//            ChangeState(State.Die);
//        }

//        if (isGrowUp == true)
//        {
//            ChangeState(State.GrowUp);
//        }


//    }



//    // ##### 상태머신 HFSM

//    public void ChangeState(State state)
//    {
//        states[(int)curState].Exit();
//        curState = state;
//        states[(int)state].Enter();

//    }

//    public void ChangeStateForSomeMoment() //---> 파라미터도 바꿔야될듯
//    {
//        //이전 행동에서 잠깐 빠져나옴
//        states[(int)curState].Exit();

//        //GrowUp의 ~~~를 진행하고
//        states[(int)State.GrowUp].Update();

//        //이전 행동으로 다시 진입함
//        states[(int)curState].Enter();

//    }



//    private class MarioState : State_Base
//    {
//        //모노비헤이비어를 쓸 수 있도록 스크립트 최상위의 자기자신을 임시 클래스에 가져오기
//        public State_MarioCtrl marioBro;

//        //생성자
//        public MarioState(State_MarioCtrl mario)
//        {
//            //기존 TextRPG에서는 게임이 각각의 씬들을 가지고있지만, 각각의 씬들도 게임을 갖고있도록 했었음
//            //여기서도 State가 캐릭터를, 캐릭터도 State를 서로 참조할수 있도록 (참조와 역참조를) 구성함
//            //그러면 캐릭터의 좌표, 이속을 Idle상태나 Trace상태 등등도 갖다쓸수 있게됨. 이걸 위해서 서로 참조하는거
//            //그래서 맨 위에 상태 배열의 내용을 Awake에 써줬던건데(참조), 이제 그 안에 this 역참조 하고 올꺼임
//            //역참조 하기 위해서 각각의 State들에게 똑같은 생성자 부여

//            this.marioBro = mario;
//        }





//    }



//    private class IdleState : MarioState
//    {
//        public IdleState(State_MarioCtrl mario) : base(mario)
//        {
//        }

//        public override void Enter() { Debug.Log("Idle 진입"); marioBro.curState = State.Idle; } //curstate를 idle로 바꾸기

//        public override void Update()
//        {
//            Debug.Log("Idle 업데이트"); //------>밥먹고와서 이거 먼저 해보고 안되면 위에꺼 해봐
//            // Idle 행동을 하기
//            //우와~ 가만히있자 내가제일좋아하는거야

//            // 다른 상태로 전환
//            if (marioBro.playerHp <= 0)
//            {
//                Debug.Log("죽은거같애");
//                marioBro.ChangeState(State.Die);
//            }
//            else if (marioBro.isGrounded == false && marioBro.rigid.velocity.y > 0.01f)
//            {
//                Debug.Log("점프한거같애");
//                marioBro.ChangeState(State.Jump);

//            }
//            else if (marioBro.isGrounded == true && marioBro.rigid.velocity.y < 0.01f && marioBro.posX != 0)
//            {
//                Debug.Log("달리는거같애");
//                marioBro.ChangeState(State.Run);
//            }
//            else { Debug.Log("여전히 가만히 있는거같애"); }

//        }

//        public override void Exit() { Debug.Log("Idle 탈출"); }


//    }



//    private class RunState : MarioState
//    {
//        public RunState(State_MarioCtrl mario) : base(mario)
//        {
//        }


//        public override void Enter() { Debug.Log("Run 진입"); marioBro.curState = State.Run; }

//        public override void Update()
//        {
//            // Run 행동 (조건: rigid.velocity.sqrMagnitude > 0.01f)
//            marioBro.PlayerMove();

//            // 다른 상태로 전환
//            if (marioBro.playerHp <= 0)
//            {
//                //marioBro.curState = State.Die;
//                marioBro.ChangeState(State.Die);
//            }
//            else if (marioBro.isGrounded == false && marioBro.rigid.velocity.y > 0.01f)
//            {
//                //marioBro.curState = State.Jump;
//                marioBro.ChangeState(State.Jump);
//            }
//            else if (marioBro.isGrounded == true && marioBro.rigid.velocity.sqrMagnitude < 0.01f) //isGrounded == true && rigid.velocity.y < 0.01f
//            {
//                //marioBro.curState = State.Idle;
//                marioBro.ChangeState(State.Idle);
//            }
//            else { }
//        }

//        public override void Exit() { Debug.Log("Run 탈출"); }


//    }



//    private class JumpState : MarioState
//    {
//        public JumpState(State_MarioCtrl mario) : base(mario)
//        {
//        }


//        public override void Enter() { Debug.Log("Jump 진입"); marioBro.curState = State.Jump; }

//        public override void Update()
//        {
//            // Jump 행동 (조건: rigid.velocity.y > 0.01f)
//            if (Input.GetKeyDown(KeyCode.Space))
//                marioBro.PlayerJump();

//            // 다른 상태로 전환
//            if (marioBro.playerHp <= 0)
//            {
//                //marioBro.curState = State.Die;
//                marioBro.ChangeState(State.Die);
//            }
//            else if (marioBro.rigid.velocity.y < 0.01f)
//            {
//                //marioBro.curState = State.Idle;
//                marioBro.ChangeState(State.Idle);
//            }
//            else { }

//            //버섯 업글 예정
//        }

//        public override void Exit() { Debug.Log("Jump 탈출"); }


//    }



//    private class GrowUpState : MarioState
//    {

//        //State rememberCurState;

//        public GrowUpState(State_MarioCtrl mario) : base(mario)
//        {
//        }


//        public override void Enter()
//        {
//            //Change 진입시 할 행동
//            Debug.Log("GrowUp 진입");

//            //rememberCurState = marioBro.curState;
//            //timer = 0f;
//        }

//        public override void Update()
//        {
//            //Change 중일때 할 행동
//            Debug.Log("GrowUp 계속");
//            //timer += Time.deltaTime;

//            //if (timer > 2f)
//            //{
//            //    return;
//            //}

//            // Change 행동 조건: 버섯을 먹음 (isGrowUp==true로 대체)
//            //marioBro.PlayerGrowUp();

//            // 일단 전환하지 않음 (나중에 1단계, 2단계 애니메이션 추가할때 다시 확인)
//        }

//        public override void Exit()
//        {
//            //Change 끝날때 할 행동
//            Debug.Log("GrowUp 탈출");

//            //marioBro.curState = rememberCurState;
//        }

//    }



//    private class DieState : MarioState
//    {
//        public DieState(State_MarioCtrl mario) : base(mario)
//        {
//        }


//        public override void Enter() { Debug.Log("Die 진입"); marioBro.curState = State.Die; }

//        public override void Update()
//        {
//            // Die 행동 (조건: hp <= 0)
//            marioBro.PlayerDie();

//            // 다른 상태로 전환하지 않음
//        }

//        public override void Exit() { Debug.Log("Die 탈출"); }


//    }





//    // ##### 애니메이터 플레이 함수

//    private void AnimatorPlay()
//    {
//        Debug.Log("들어오니?");

//        // 2D는 블렌드가 안되기 때문에, 밸로시티값 크기가 작아지면 전환을 주어 자연스럽게 해줌
//        // float의 특징상, velocity값이 정확히 0이 아닐수 있어서 애니재생의 오류를 없애기 위함

//        int CheckAniHash;


//        //변신
//        if (curState == State.GrowUp)
//        {

//            Debug.Log("들어오니?");
//            CheckAniHash = growUpHash;
//            isGrowUp = false;
//        }

//        //사망
//        if (curState == State.Die)
//        {
//            CheckAniHash = dieHash;
//        }

//        //점프
//        else if (curState == State.Jump)
//        {
//            CheckAniHash = jumpHash;
//        }

//        //멈춤 or 이동
//        else if (curState == State.Idle)
//        {
//            CheckAniHash = idleHash;
//        }
//        else
//        {
//            CheckAniHash = runHash;
//        }


//        //애니가 기존과 다를때만 실행, 프레임마다 계속 호출하지 않게됨 (애니 중복재생 막는것)
//        if (curAniHash != CheckAniHash)
//        {
//            curAniHash = CheckAniHash;
//            animator.Play(curAniHash);
//        }


//    }


//}