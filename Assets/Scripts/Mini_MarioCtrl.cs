using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public class Mini_MarioCtrl : MonoBehaviour
{
    // I tried to make a "Player Finite-State-Machine".

    // ------------------------------------------------------------------------------------------
    // ----------------------------------------- �ʵ� -------------------------------------------
    // ------------------------------------------------------------------------------------------


    
    //��������
    public enum State { Idle, Run, Jump, Change, Die }
    [SerializeField] State curState;

    //������ ���� �ӽ������� hp�� �ο�
    [SerializeField] public int playerHp = 100;
    //[SerializeField] bool isHit; //F�ȸ��� T�Ѵ����
    [SerializeField] bool isDead; //F������� T�׾���
    [SerializeField] Mini_Eagle mob;
    [SerializeField] GameObject foots;


    //����
    [SerializeField] Mini_UICtrl ui;
    [SerializeField] Mini_Coin coin;


    //������Ʈ
    [SerializeField] SpriteRenderer render;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] Animator animator;
    //[SerializeField] LayerMask groundMask; 8, 11

    //�̵�
    float posX;
    [SerializeField] float moveSpeed = 30f;
    [SerializeField] float maxMoveSpeed = 10f;

    //����
    Vector2 lay;
    [SerializeField] float jumpPower = 10f;
    [SerializeField] float maxFallSpeed = 150f;
    [SerializeField] bool isGrounded;

    //�̺�Ʈ
    public UnityAction OnTouchDoor; //Ŭ���� �̺�Ʈ
    public UnityAction OnPlayerDead; //���ӿ��� �̺�Ʈ
    public UnityAction OnEatRedMushroom; //�������� �̺�Ʈ(������ 1�ܰ� ��ȭ)
    public UnityAction OnPickUpCoin; //���� ȹ�� �̺�Ʈ


    ////������ ��ȭ�ܰ� (0, 1, 2)
    //    [SerializeField] int curLevel;
    bool isChange;

    // �ִϸ��̼� �ؽ� (�ؽ����̺� �װ�)
    private int curAniHash;
    private static int idleHash;
    private static int runHash;
    private static int jumpHash;
    private static int changeHash;
    private static int dieHash;





    // ------------------------------------------------------------------------------------------
    // ---------------------------------- ����Ƽ �޽��� �Լ� -------------------------------------
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

            // (���������� �߰��ϱ�)
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
        if (collision.gameObject.layer == 10) //10 ����
        {
            ui.coinCount += 1;           

        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
    }



    // ------------------------------------------------------------------------------------------
    // -------------------------- ����� Ŀ���� �Լ�, �̵�&����&��üũ ----------------------------
    // ------------------------------------------------------------------------------------------



    /// <summary>
    /// �÷��̾� �̵� �Լ�
    /// </summary>
    private void PlayerMove()
    {
        if (isDead == true)
            return;


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

    }


    /// <summary>
    /// ���� �� üũ �Լ�
    /// </summary>
    private void GroundCheck()
    {
        
        RaycastHit2D hit08 = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, 8);  // 08 Ground
        RaycastHit2D hit11 = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, 11); // 11 Pipe

        // ������ �� groundMask �ȿ����� collider�� üũ�ϰ� �ǹǷ� ���� ������ �����Ѵ�.

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
        Debug.Log("����4");
        isChange = true;
    }


    private void PlayerDie()
    {
        isDead = true;
        Debug.Log("������ �׾���");
        OnPlayerDead?.Invoke();

        //�������� ���� �� ���ٰ� ����ɰ�,
        //���� ���� �������� �׸��� �� �Ʒ��� �������� �ִϸ��̼��� ���� ����ϱ�.

    }





    // ------------------------------------------------------------------------------------------
    // ------------------------- ����� Ŀ���� �Լ�, �ִϸ��̼� ���¸ӽ� ---------------------------
    // ------------------------------------------------------------------------------------------



    private void AnyState()
    {
        // ������ Ư�� ���� ������, Ư�� ���·� ��ȯ

        if (playerHp <= 0)
        {
            curState = State.Die;
        }

        if (isChange == true)
        {
            Debug.Log("����1");
            curState = State.Change;
        }

        //// (���������� �߰� �����̰�,  Loop���� �ʴ� �ִ��� ��� ó���� �������  �ּ�ó����)
        //else if (��������)
        //{
        //    curState = State.Change;
        //}

    }

    private void Idle()
    {
        // Idle �ൿ (����: rigid.velocity.sqrMagnitude < 0.01f)

        // �ٸ� ���·� ��ȯ
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
        // Run �ൿ (����: rigid.velocity.sqrMagnitude > 0.01f)
        PlayerMove();

        // �ٸ� ���·� ��ȯ
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
        // Jump �ൿ (����: rigid.velocity.y > 0.01f)
        if (Input.GetKeyDown(KeyCode.Space))
            PlayerJump();

        // �ٸ� ���·� ��ȯ
        if (playerHp <= 0)
        {
            curState = State.Die;
        }
        else if (rigid.velocity.y < 0.01f)
        {
            curState = State.Idle;
        }
        else { }

        //���� ���� ����


    }

    // (���������� �߰� �����̰�,  Loop���� �ʴ� �ִ��� ��� ó���� �������  �ּ�ó����)
    private void Change()
    {
        Debug.Log("����3");
        // Change �ൿ ����: ������ ���� (isChange==true�� ��ü)
        PlayerGrowUp();

        // �ϴ� ��ȯ���� ���� (���߿� 1�ܰ�, 2�ܰ� �ִϸ��̼� �߰��Ҷ� �ٽ� Ȯ��)
        

    }

    private void Die()
    {
        // Die �ൿ (����: hp <= 0)
        PlayerDie();

        // �ٸ� ���·� ��ȯ���� ����

    }



    /// <summary>
    /// �ִϸ����� �÷��� �Լ�
    /// </summary>
    private void AnimatorPlay()
    {

        // 2D�� ���尡 �ȵǱ� ������, ��ν�Ƽ�� ũ�Ⱑ �۾����� ��ȯ�� �־� �ڿ������� ����
        // float�� Ư¡��, velocity���� ��Ȯ�� 0�� �ƴҼ� �־ �ִ������ ������ ���ֱ� ����

        int CheckAniHash;


        //����
        if (curState == State.Change)
        {
            Debug.Log("����2");
            CheckAniHash = changeHash;
            isChange = false;
            Debug.Log("����222222222");
        }

        //���
        if (curState == State.Die)
        {
            CheckAniHash = dieHash;
        }

        //����
        else if (curState == State.Jump)
        {
            CheckAniHash = jumpHash;
        }

        //���� or �̵�
        else if (curState == State.Idle)
        {
            CheckAniHash = idleHash;
        }
        else
        {
            CheckAniHash = runHash;
        }


        //�ִϰ� ������ �ٸ����� ����, �����Ӹ��� ��� ȣ������ �ʰԵ� (�����ִ� ����� ��û�����°� �Ƚ�)
        if (curAniHash != CheckAniHash)
        {
            Debug.Log("����7");
            curAniHash = CheckAniHash;
            Debug.Log("����8");
            animator.Play(curAniHash);
            Debug.Log("����9");
        }


    }


}
