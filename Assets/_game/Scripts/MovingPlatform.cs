using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 target;
    public Vector3 aPosition;
    public Vector3 bPosition;
    public float speed;
    void Start()
    {
        aPosition = transform.position;
        bPosition = transform.position + Vector3.up * 3;
        target = bPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, target) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, Time.fixedDeltaTime * speed);
        }
        else // da den dich
        {
            if (target == bPosition)
            {
                target = aPosition;
            }
            else target = bPosition;

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            collision.transform.SetParent(transform);
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.transform.SetParent(null);
        }
    }
}
