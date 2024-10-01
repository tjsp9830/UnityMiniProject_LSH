using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;



public class Mini_MarioCtrl : MonoBehaviour
{
    // I tried to make a "Player Hierarchy-Finite-State-Machine".



    // ------------------------------------------------------------------------------------------
    // ----------------------------------------- �ʵ� -------------------------------------------
    // ------------------------------------------------------------------------------------------



    //������ ���� �ӽ������� hp�� �ο�
    [SerializeField] public int playerHp = 100;


    //�������� (HFSM)
    public enum State { Idle, Run, Jump, GrowUp, Die, Size }
    [SerializeField] State curState;
    private Mini_BaseState[] states = new Mini_BaseState[(int)State.Size];
    Vector2 startPos;
    //[SerializeField] bool isHit; //F�ȸ��� T�Ѵ����
    [SerializeField] bool isDead; //F������� T�׾���
    

    //����
    [SerializeField] Mini_UICtrl ui;
    [SerializeField] Mini_Coin coin;


    //������Ʈ
    [SerializeField] SpriteRenderer render;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] Animator animator;
    [SerializeField] LayerMask groundMask; //�������� ����ؾߵż� ���� �ʱ�� ��

    //�̵�
    float posX;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float maxMoveSpeed = 10f;

    //����
    Vector2 lay;
    [SerializeField] int jumpPower = 5;
    [SerializeField] float maxFallSpeed = 10f;
    [SerializeField] bool isGrounded;

    //�̺�Ʈ
    public UnityAction OnTouchDoor; //Ŭ���� �̺�Ʈ
    public UnityAction OnPlayerDead; //���ӿ��� �̺�Ʈ
    public UnityAction OnEatRedMushroom; //�������� �̺�Ʈ(������ 1�ܰ� ��ȭ)
    public UnityAction OnPickUpCoin; //���� ȹ�� �̺�Ʈ


    ////������ ��ȭ�ܰ� (0, 1, 2)
    //    [SerializeField] int curLevel;
    bool isGrowUp;

    // �ִϸ��̼� �ؽ� (�ؽ����̺� �װ�)
    private int CheckAniHash;
    private int curAniHash;
    private static int idleHash;
    private static int runHash;
    private static int jumpHash;
    private static int growUpHash;
    private static int dieHash;


    // ------------------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------------------
    // ---------------------------------- ����Ƽ �޽��� �Լ� -------------------------------------
    // ------------------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------------------



    private void Awake()
    {
        playerHp = 100;
        isDead = false;
        isGrowUp = false;
        //isHit = false;
        //curLevel = 0;
        curState = State.Idle;

        //�ִϸ�����
        idleHash = Animator.StringToHash("P0_Idle");
        runHash = Animator.StringToHash("P0_Run");
        jumpHash = Animator.StringToHash("P0_Jump");
        growUpHash = Animator.StringToHash("P0_GrowUp");
        dieHash = Animator.StringToHash("P0_DIe");

        //���¸ӽ� HFSM ���鼭 �Ѱ�
        states[(int)State.Idle] = new IdleState(this);
        states[(int)State.Run] = new RunState(this);
        states[(int)State.Jump] = new JumpState(this);
        states[(int)State.GrowUp] = new GrowUpState(this);
        states[(int)State.Die] = new DieState(this);

    }


    private void Start()
    {
        //���¸ӽ� HFSM ���鼭 �Ѱ�
        startPos = transform.position;

        states[(int)curState].Enter();
    }

    private void OnDestroy()
    {
        //���¸ӽ� HFSM ���鼭 �Ѱ�
        states[(int)curState].Exit();

    }

    private void FixedUpdate()
    {
        if (curState != State.Run) //Run�� �ƴҶ����߸� �ϴ���, �ƴϸ� ���� ��ü�� ���־� �Ǵ��� ������
        {
            posX = Input.GetAxisRaw("Horizontal"); //GetAxisRaw(����)�� GetAxis(�Ǽ�)�� ���̰� �ȴ����� �Ф�
            PlayerMove();

        }

    }


    private void Update()
    {

        Debug.DrawRay(transform.position, Vector2.down * 0.2f, Color.red, 0.1f);


        if (Input.GetKeyDown(KeyCode.K)) //Kill
        {
            playerHp = 0;
            ChangeState(State.Die); 
        }

        if (Input.GetKeyDown(KeyCode.G)) //GrowUp
        {
            isGrowUp = true;
            ChangeState(State.GrowUp);
        }




        if (Input.GetKeyDown(KeyCode.Space))
        {
            //PlayerJump();
            GroundCheck();
            
        }


        //Debug.Log("switch�� ���ư�, �� ���ư��� �ϴµ� �̰� �����ϰ� �� ���������� �𸣰���");
        switch (curState)
        {
            case State.Idle:
                states[(int)State.Idle].Update();
                break;

            case State.Run:
                states[(int)State.Run].Update();
                break;

            case State.Jump:
                states[(int)State.Jump].Update();
                break;

            // (���������� �߰��ϱ�)
            //case State.GrowUp:
            //    states[(int)State.GrowUp].Update();
            //    break;

            case State.Die:
                states[(int)State.Die].Update();
                break;

        }

        AnyState(); //�� ����ġ���� �ƿ� ���ְų�, �ִϽ�����Ʈ�� ���ĺ��� �����

        AnimatorPlay();


    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Door")
        {
            OnTouchDoor?.Invoke();
        }
        if (collision.name == "Coin")
        {
            ui.coinCount += 1;

        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log($"{collision.gameObject.name}�� �ε���");
    }








    // ------------------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------------------
    // -------------------------- ����� Ŀ���� �Լ�, �̵�&����&��üũ ----------------------------
    // ------------------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------------------



    /// <summary>
    /// �÷��̾� �̵� �Լ�
    /// </summary>
    private void PlayerMove()
    {
        if (isDead == true)
            return;

        //ChangeState(State.Run);


        rigid.AddForce(Vector2.right * posX * moveSpeed, ForceMode2D.Force);

        //������ �̵�
        if (rigid.velocity.x > maxMoveSpeed)
        {
            rigid.velocity = new Vector2(maxMoveSpeed, rigid.velocity.y);
        }
        //���� �̵�
        else if (rigid.velocity.x < -maxMoveSpeed)
        {
            rigid.velocity = new Vector2(-maxMoveSpeed, rigid.velocity.y);
        }

        //�Ʒ��� �̵�
        if (rigid.velocity.y < -maxFallSpeed)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, -maxFallSpeed);
        }

        //�̹��� �ø�
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
    /// �÷��̾� ���� �Լ�
    /// </summary>
    private void PlayerJump()
    {
        if (isDead == true)
            return;

        if (isGrounded == false)
            return;

        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        //isGrounded = false;

    }


    /// <summary>
    /// ���� �� üũ �Լ�
    /// </summary>
    private void GroundCheck()
    {
        //RaycastHit2D hit08 = Physics2D.Raycast(transform.position, Vector2.down, 0.1f,  8); //��
        //RaycastHit2D hit11 = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, 11); //������
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, groundMask);

        // ������ �� groundMask �ȿ����� collider�� üũ�ϰ� �ǹǷ� ���� ������ �����Ѵ�.

        //if (hit08.collider != null || hit11.collider != null)
        //if (hit.collider != null)
        //{            
        //    isGrounded = true;

        //}
        if (hit.collider != null)
        {
            isGrounded = true;
            ChangeState(State.Jump);
        }
        else
        {
            isGrounded = false;
        }

    }



    private void PlayerGrowUp()
    {
        isGrowUp = true;
    }


    private void PlayerDie()
    {
        isDead = true;
        OnPlayerDead?.Invoke();

        //�������� ���� �� ���ٰ� ����ɰ�,
        //���� ���� �������� �׸��� �� �Ʒ��� �������� �ִϸ��̼��� ���� ����ϱ�.

    }







    // ------------------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------------------
    // ---------- ����� Ŀ���� �Լ�,  ���¸ӽ� HFSM ���鼭 �Ѱ� & �ִϸ��̼� ���¸ӽ� -------------
    // ------------------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------------------



    private void AnyState()
    {
        // ������ Ư�� ���� ������, Ư�� ���·� ��ȯ

        if (playerHp <= 0)
        {
            ChangeState(State.Die);
        }

        if (isGrowUp == true)
        {
            ChangeState(State.GrowUp);
        }


    }



    // ##### ���¸ӽ� HFSM

    public void ChangeState(State state)
    {
        states[(int)curState].Exit();
        CheckAniHash = curAniHash;
        states[(int)state].Enter();

    }

    public void ChangeStateForSomeMoment() //---> �Ķ���͵� �ٲ�ߵɵ�
    {
        //���� �ൿ���� ��� ��������
        states[(int)curState].Exit();

        //GrowUp�� ~~~�� �����ϰ�
        states[(int)State.GrowUp].Update();

        //���� �ൿ���� �ٽ� ������
        states[(int)curState].Enter();

    }



    private class MarioState : Mini_BaseState
    {
        //�������̺� �� �� �ֵ��� ��ũ��Ʈ �ֻ����� �ڱ��ڽ��� �ӽ� Ŭ������ ��������
        public Mini_MarioCtrl marioBro;

        //������
        public MarioState(Mini_MarioCtrl mario)
        {
            //���� TextRPG������ ������ ������ ������ ������������, ������ ���鵵 ������ �����ֵ��� �߾���
            //���⼭�� State�� ĳ���͸�, ĳ���͵� State�� ���� �����Ҽ� �ֵ��� (������ ��������) ������
            //�׷��� ĳ������ ��ǥ, �̼��� Idle���³� Trace���� �� ���پ��� �ְԵ�. �̰� ���ؼ� ���� �����ϴ°�
            //�׷��� �� ���� ���� �迭�� ������ Awake�� ������ǵ�(����), ���� �� �ȿ� this ������ �ϰ� �ò���
            //������ �ϱ� ���ؼ� ������ State�鿡�� �Ȱ��� ������ �ο�

            this.marioBro = mario;
        }





    }



    private class IdleState : MarioState
    {
        public IdleState(Mini_MarioCtrl mario) : base(mario)
        {
        }

        public override void Enter() 
        { 
            marioBro.curState = State.Idle; 
            marioBro.curAniHash = idleHash;
        } 

        public override void Update()
        {
            // Idle �ൿ�� �ϱ�
            //���~ ���������� �������������ϴ°ž�

            // �ٸ� ���·� ��ȯ
            if (marioBro.playerHp <= 0)
            {
                marioBro.ChangeState(State.Die);
            }
            else if (marioBro.isGrounded == false && marioBro.rigid.velocity.y > 0.05f)
            {
                marioBro.ChangeState(State.Jump);

            }
            else if (marioBro.isGrounded == true && marioBro.rigid.velocity.y < 0.05f && marioBro.posX != 0)
            {
                marioBro.ChangeState(State.Run);
            }
            else { }

        }

        public override void Exit() { }


    }



    private class RunState : MarioState
    {
        public RunState(Mini_MarioCtrl mario) : base(mario)
        {
        }


        public override void Enter() 
        { 
            marioBro.curState = State.Run; 
            marioBro.curAniHash = runHash;
        }

        public override void Update()
        {
            // Run �ൿ (����: rigid.velocity.sqrMagnitude > 0.05f)
            marioBro.PlayerMove();

            // �ٸ� ���·� ��ȯ
            if (marioBro.playerHp <= 0)
            {
                marioBro.ChangeState(State.Die);
            }
            else if (marioBro.isGrounded == false && marioBro.rigid.velocity.y > 0.05f)
            {
                marioBro.ChangeState(State.Jump);
            }
            else if (marioBro.isGrounded == true && marioBro.rigid.velocity.sqrMagnitude < 0.05f) //isGrounded == true && rigid.velocity.y < 0.05f
            {
                marioBro.ChangeState(State.Idle);
            }
            else { }

        }

        public override void Exit() { }


    }



    private class JumpState : MarioState
    {
        public JumpState(Mini_MarioCtrl mario) : base(mario)
        {
        }


        public override void Enter() 
        { 
            marioBro.curState = State.Jump;
            marioBro.curAniHash = jumpHash;
        }

        public override void Update()
        {
            // Jump �ൿ (����: rigid.velocity.y > 0.05f)
            if (Input.GetKeyDown(KeyCode.Space))
                marioBro.PlayerJump();

            // �ٸ� ���·� ��ȯ
            if (marioBro.playerHp <= 0)
            {
                marioBro.ChangeState(State.Die);
            }
            else if (marioBro.rigid.velocity.y < 0.05f)
            {
                marioBro.ChangeState(State.Idle);
            }
            else { }

            //���� ���� ����
        }

        public override void Exit() { }


    }



    private class GrowUpState : MarioState
    {

        //State rememberCurState;

        public GrowUpState(Mini_MarioCtrl mario) : base(mario)
        {
        }


        public override void Enter()
        {
            //Change ���Խ� �� �ൿ
            Debug.Log("GrowUp ����");
            marioBro.curAniHash = growUpHash;
            marioBro.isGrowUp = false;

            //rememberCurState = marioBro.curState;
            //timer = 0f;
        }

        public override void Update()
        {
            //Change ���϶� �� �ൿ
            Debug.Log("GrowUp ���");
            //timer += Time.deltaTime;

            //if (timer > 2f)
            //{
            //    return;
            //}

            // Change �ൿ ����: ������ ���� (isGrowUp==true�� ��ü)
            //marioBro.PlayerGrowUp();

            // �ϴ� ��ȯ���� ���� (���߿� 1�ܰ�, 2�ܰ� �ִϸ��̼� �߰��Ҷ� �ٽ� Ȯ��)
        }

        public override void Exit()
        {
            //Change ������ �� �ൿ
            Debug.Log("GrowUp Ż��");

            //marioBro.curState = rememberCurState;
        }

    }



    private class DieState : MarioState
    {
        public DieState(Mini_MarioCtrl mario) : base(mario)
        {
        }


        public override void Enter() 
        { 
            marioBro.curState = State.Die;
            marioBro.curAniHash = dieHash;
        }

        public override void Update()
        {
            // Die �ൿ (����: hp <= 0)
            marioBro.PlayerDie();

            // �ٸ� ���·� ��ȯ���� ����
        }

        public override void Exit() { }


    }





    // ##### �ִϸ����� �÷��� �Լ�

    private void AnimatorPlay()
    {

        //// 2D�� ���尡 �ȵǱ� ������, ��ν�Ƽ�� ũ�Ⱑ �۾����� ��ȯ�� �־� �ڿ������� ����
        //// float�� Ư¡��, velocity���� ��Ȯ�� 0�� �ƴҼ� �־ �ִ������ ������ ���ֱ� ����




        ////���� (��ȸ��, �ڷ�ƾ �̿��غ��°� ��õ����)
        //if (curState == State.GrowUp)
        //{            
        //    //CheckAniHash = growUpHash;
        //    //isGrowUp = false;
        //}

        ////���
        //if (curState == State.Die)
        //{
        //    //CheckAniHash = dieHash;
        //}

        ////����
        //else if (curState == State.Jump)
        //{
        //    //CheckAniHash = jumpHash;
        //}

        ////���� or �̵�
        //else if (curState == State.Idle)
        //{
        //    //CheckAniHash = idleHash;
        //}
        //else
        //{
        //    //CheckAniHash = runHash;
        //}


        ////�ִϰ� ������ �ٸ����� ����, �����Ӹ��� ��� ȣ������ �ʰԵ� (�ִ� �ߺ���� ���°�)
        if (curAniHash != CheckAniHash)
        {
            //curAniHash = CheckAniHash;
            animator.Play(curAniHash);
        }


    }


}

