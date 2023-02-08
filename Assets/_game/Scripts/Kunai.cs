using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    [SerializeField] private GameObject hitVFX;
    void Start()
    {
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnInit() {

        rb.velocity = transform.right * 10f;
        Invoke(nameof(OnDespawn), 3f);
    }

    void OnDespawn()
    {
        Destroy(gameObject);
 
    }

    void DestroyVFX()
    {
        Destroy(hitVFX.gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.tag == "enemy")
        {
            collision.GetComponent<Character>().OnHit(30f);
            Instantiate(hitVFX, transform.position, transform.rotation);
            Invoke(nameof(DestroyVFX), 1f);
            OnDespawn();
        }

    }
}
