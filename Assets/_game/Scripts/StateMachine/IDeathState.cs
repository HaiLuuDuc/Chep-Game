using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDeathState : IState
{

    public void OnEnter(Enemy enemy)
    {
        enemy.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        enemy.OnDeath();
    }
    public void OnExecute(Enemy enemy)
    {

    }
    public void OnExit(Enemy enemy)
    {

    }
}
