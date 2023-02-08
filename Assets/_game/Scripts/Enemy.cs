using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : Character
{
    // Start is called before the first frame update
    [SerializeField] private float attackRange;
    [SerializeField] private float  moveSpeed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] GameObject attackArea;
    private IState currentState;
    public bool isRight => transform.rotation.y == 0;
    private Character target;
    public Character Target => target;

    void Start()
    {

        OnInit();
    }

    // Update is called once per frame
    void FixedUpdate()
    { 
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
        if (IsDead)
        {
            ChangeState(new IDeathState());
            return;
        }
    }

    public override void OnInit()
    {
        base.OnInit();
        ChangeState(new IdleState());
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        ResetAttackArea();
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        Destroy(gameObject);
    }

    public override void OnDeath()
    {
        base.OnDeath();
        rb.velocity = Vector3.zero;
        ChangeAnim("die");
        Invoke(nameof(OnDespawn), 2f);
    }



    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }



    public void Moving()
    {
        ChangeAnim("run");
        rb.velocity = transform.right * moveSpeed;
    }

    public void StopMoving()
    {
        ChangeAnim("idle");
        rb.velocity = Vector2.zero;
    }

    public void Attack()
    {
        ChangeAnim("attack");
        Invoke(nameof(SetAttackArea), 0.2f);
        Invoke(nameof(ResetAttackArea), 0.5f);
    }

    internal void SetTarget(Character character)
    {
        this.target = character;
        if (IsTargetInRange())
        {
            ChangeState(new AttackState());
        }
        else
        {
            if (Target != null)
            {
                ChangeState(new PatrolState());
            }
            else
            {
                ChangeState(new IdleState());
            }
        }

    }

    public bool IsTargetInRange()
    {
        if (Target == null) return false;
        else
        {
            if (Mathf.Abs(transform.position.x - Target.transform.position.x) < attackRange)
            {
                return true;
            }
            else return false;
        }
        
    }

    public void ChangeDirection() {
    
        if (isRight)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }
    public void ChangeDirectionToPlayer()
    {
        if (Target != null)
        {
            if (transform.position.x < Target.transform.position.x)
            {
                //quayphai
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            else
            {
                //quaytrai
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemyWall")
        {
            ChangeDirection();
        }
    }

    private void SetAttackArea()
    {
        attackArea.SetActive(true);
    }

    public void ResetAttackArea()
    {
        attackArea.SetActive(false);
    }
}
