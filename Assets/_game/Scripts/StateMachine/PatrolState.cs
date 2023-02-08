using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    float timer;
    float randomTime = 2f;
    public void OnEnter(Enemy enemy)
    {
        enemy.Moving();
        timer = 0;
    }
    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;
        if (enemy.Target != null)
        {
            enemy.ChangeDirectionToPlayer();
            enemy.Moving();
 
            if (enemy.IsTargetInRange())
            {
                enemy.ChangeState(new AttackState());
            }
        }
        else
        {
            if (timer > randomTime)
            {
                enemy.ChangeState(new IdleState());
            }
            else enemy.Moving();
        }


    }
    public void OnExit(Enemy enemy)
    {

    }
}
