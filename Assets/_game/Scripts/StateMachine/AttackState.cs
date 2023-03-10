using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AttackState : IState
{
    float timer;
    public void OnEnter(Enemy enemy)
    {
        if (enemy.Target != null)
        {
            enemy.ChangeDirectionToPlayer();
            enemy.StopMoving();
            enemy.Attack();
        }
        timer = 0;
    }
    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;
        if (timer > 1f)
        {
            enemy.ChangeState(new PatrolState());
        }
    }
    public void OnExit(Enemy enemy)
    {

    }
}
