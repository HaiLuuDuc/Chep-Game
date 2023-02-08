using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class IdleState : IState
{

    float timer;
    float randomTime = 2f;
    public void OnEnter(Enemy enemy)
    {
        enemy.StopMoving();
        timer = 0;
    }
    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;
        if (timer > randomTime)
        {
            enemy.ChangeState(new PatrolState());
        }
    }
    public void OnExit(Enemy enemy)
    {

    }
}
