using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class selfMadePlayer : Character
{
    // Start is called before the first frame update

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Kunai kunaiPrefab;
    [SerializeField] private Transform kunaiSpawner;


    private bool isJumping = false;
    private bool isAttacking = false;
    private bool isDeath = false;
    private Vector3 savePoint;
    private float horizontal;
    public int coinCount = 0;
    public LayerMask groundLayer;
    public bool isGrounded = false;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        SavePoint();
        OnInit();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        isGrounded = CheckGrounded();
        horizontal = Input.GetAxisRaw("Horizontal");

        if (isDeath)    
        { 
            return;
        }
        if (isAttacking)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        //nhay
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                Jump();
                return;
            }
            if (Input.GetKeyDown(KeyCode.C) && isGrounded)
            {
                Attack();
                return;
            }
            if (Input.GetKeyDown(KeyCode.V) && isGrounded)
            {
                Throw();
                return;
            }
        }
        //fall
        if(!isGrounded && rb.velocity.y < 0)
        {
            ChangeAnim("fall");
        }

        //di chuyen trai phai
        if (Mathf.Abs(horizontal) > 0.1f)
        {
            //move
            rb.velocity = new Vector2(speed * Time.fixedDeltaTime * horizontal, rb.velocity.y);
            transform.rotation = Quaternion.Euler(0, horizontal > 0 ? 0 : 180, 0);
            if(isGrounded) ChangeAnim("run");
        }

        else if (isGrounded)// dung yen o mat dat
        {
            rb.velocity = Vector2.zero;
            ChangeAnim("idle");
        }
        else if (!isGrounded)// dung yen o tren khong
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    public override void OnInit()
    {
        base.OnInit();
        isGrounded = false;
        isJumping = false;
        isAttacking = false;
        isDeath = false;
        transform.position = savePoint;
        ChangeAnim("idle");
    }
    public override void OnDeath()
    {
        ChangeAnim("die");
        isDeath = true;
        rb.velocity = Vector3.zero;
        Invoke(nameof(OnInit), 1f);
    }
    void SavePoint()
    {
        savePoint = transform.position;
    }

    private bool CheckGrounded()
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);
        return hit.collider != null;
    }

    void Jump()
    {
        isJumping = true;
        ChangeAnim("jump");
        rb.AddForce(jumpForce * Vector2.up);
    }

    void Attack()
    {
        isAttacking = true;
        ChangeAnim("attack");
        Invoke(nameof(ResetAttack), 0.5f);
    }

    void Throw()
    {
        isAttacking = true;
        ChangeAnim("throw");
        Invoke(nameof(ResetAttack), 0.5f);
        Instantiate(kunaiPrefab, kunaiSpawner.transform.position, kunaiSpawner.rotation);
    }

    void ResetAttack()
    {
        isAttacking = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coin")
        {
            coinCount++;
            Destroy(collision.gameObject);
        }
        else if(collision.tag == "SavePoint")
        {
            SavePoint();
        }
        else if(collision.tag == "DeathZone")
        {
            hp = 0;
            ChangeAnim("die");
            isDeath = true;
            Invoke(nameof(OnInit), 1f);
        }
    }
}
