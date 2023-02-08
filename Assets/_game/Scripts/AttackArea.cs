using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    /*[SerializeField] private Enemy enemy;*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.gameObject.GetComponent<Character>().OnHit(50f);
            Debug.Log("attacked player !!!!!!!!!!!!!!");
            gameObject.SetActive(false);
        }

    }

}
