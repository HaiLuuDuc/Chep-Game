using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject target;
    public Vector3 offset;
    public float speed = 20f;

    void Start()
    {
        target = GameObject.Find("player");
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position + offset, Time.fixedDeltaTime * speed);
    }
}
