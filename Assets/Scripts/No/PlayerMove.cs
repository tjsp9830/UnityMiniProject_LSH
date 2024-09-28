using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMove : PlayerAnimation
{
    //������Ʈ
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] SpriteRenderer render;
    [SerializeField] Animator animator;
    [SerializeField] LayerMask groundMask;

    //�̵�
    float posX;
    [SerializeField] float moveSpeed = 30f;
    [SerializeField] float maxMoveSpeed = 10f;

    //����
    Vector2 lay;
    [SerializeField] bool isGrounded;
    [SerializeField] float jumpPower = 10f;
    [SerializeField] float maxFallSpeed = 150f;

    //�̺�Ʈ
    public UnityAction OnClear; //Ŭ���� �̺�Ʈ
    public UnityAction OnEatRedMushroom; //�������� �̺�Ʈ(������ 1�ܰ� ��ȭ)

    //������ ��ȭ�ܰ� (0, 1, 2)
    [SerializeField] int curLevel;

    //�ִϸ��̼� �ؽ̰���
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

        // float�� Ư¡��, velocity���� ��Ȯ�� 0�� �ƴҼ� �־ �ִ������ ������ ���ֱ� ����

        //���� (�ٰų� ��������)
        if (rigid.velocity.y > 0.01f)
        {
            CheckAniHash = P0_Jump_Hash;
                    
        }
        else if (rigid.velocity.y < -0.01f)
        {
            
            CheckAniHash = P0_Jump_Hash;
                    
        }

        //�̵� (�Ȱų� �ٱ�)
        else if (rigid.velocity.sqrMagnitude < 0.01f)
        {
            
            CheckAniHash = P0_Idle_Hash;
                    

        }
        else
        {
            
            CheckAniHash = P0_Run_Hash;
                    

        }
        

        //�ִϰ� ������ �ٸ����� ����, �����Ӹ��� ��� ȣ������ �ʰԵ� (�ִ� �ߺ���� ����)
        if (curAniHash != CheckAniHash)
        {
            curAniHash = CheckAniHash;
            animator.Play(curAniHash);
        }

    }


}
