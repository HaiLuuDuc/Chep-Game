using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] HealthBar healthBar;
    [SerializeField] private Vector3 offset;
    [SerializeField] CombatText combatTextPrefab;
    private string currentAnimName;
    protected float hp;
    public bool IsDead => hp <= 0;


    private void Update()
    {
        healthBar.transform.position = transform.position + offset;
        healthBar.SetNewHp(hp);
    }

    public virtual void OnInit()
    {
        hp = 100;
        healthBar.OnInit(100);
    }

    public virtual void OnDespawn()
    {
        Destroy(gameObject);
        Destroy(healthBar.gameObject);
    }

    public virtual void OnDeath()
    {
        
        ChangeAnim("die");
        Invoke(nameof(OnDespawn), 2f);
    }

    protected void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }

    public void OnHit(float damage)
    {
        Debug.Log("Hit");
        if (!IsDead)
        {
            hp -= damage;
            Instantiate(combatTextPrefab, transform.position + Vector3.up, Quaternion.identity).OnInit(damage);
            if (IsDead)
            {
                OnDeath();
            }
        }
    }

}