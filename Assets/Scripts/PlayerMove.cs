using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMove : MonoBehaviour
{

    [SerializeField] Rigidbody2D rigid;
    [SerializeField] SpriteRenderer render;
    [SerializeField] Animator animator;

    //이동
    float posX;
    [SerializeField] float moveSpeed = 30f;
    [SerializeField] float maxMoveSpeed = 5f;

    //점프
    Vector2 lay;
    [SerializeField] bool isGrounded;
    [SerializeField] int jumpCount;
    [SerializeField] float jumpPower = 6f;
    [SerializeField] float maxFallSpeed = 6f;

    //클리어 이벤트
    //[SerializeField] GameObject deadLineBox;
    public UnityAction OnClear;



    private void Start()
    {
        //transform.position = new Vector3(-6, -3, 0); --> 원래 이게 필요했는데 또 필요 없어진 이유가 ???
        lay = new Vector2(transform.position.x, transform.position.y - 1f);
        jumpCount = 1;
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


    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log($"콜리전엔터: {collision}");
    //    Debug.Log($"콜리전엔터: {collision.gameObject}");
    //    Debug.Log($"콜리전엔터: {collision.gameObject.tag}");

    //    if (collision.gameObject.tag == "Clear")
    //    {
    //        OnClear?.Invoke();
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        Debug.Log($"트리거엔터: {_collision}");
        Debug.Log($"트리거엔터: {_collision.gameObject}");
        Debug.Log($"트리거엔터: {_collision.gameObject.tag}");

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
        //posX = Input.GetAxis("Horizontal") * Time.deltaTime;
        //transform.position += Vector3.right * posX * moveSpeed;
        
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
            //animator.SetBool("isFalling", true);
        }

        //if (rigid.velocity.y > 0)
        //{
        //    isGrounded = false;
        //}
        //else
        //    isGrounded = true;


        if (rigid.velocity.sqrMagnitude < 0.01f)
        {
            //animator.SetBool("isRunning", false);
        }
        else
        {
            //animator.SetBool("isRunning", true);
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
        if (isGrounded == false || jumpCount < 1)
            return;



        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        isGrounded = false;
        jumpCount = 0;
        //animator.SetBool("isJumping", true);

    }

    [SerializeField] LayerMask groundMask;

    private void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, groundMask);


        if (hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject.layer);
            isGrounded = true;
            jumpCount = 1;
            //animator.SetBool("isJumping", false);
            //animator.SetBool("isFalling", false);

        }
        else
        {            
            isGrounded = false;
        }

    }






}
