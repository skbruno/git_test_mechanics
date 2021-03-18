using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAR : MonoBehaviour
{

    private Rigidbody rb;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float turnspeed;

    public float gravitymuli;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        Move();
        Turn();
        fall();
    }


    void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddRelativeForce(new Vector3(Vector3.forward.x, 0, Vector3.forward.z) * speed * 10);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.AddRelativeForce(new Vector3(Vector3.forward.x, 0, Vector3.forward.z) * -speed * 10);
        }

        Vector3 localvelocity = transform.InverseTransformDirection(rb.velocity);
        localvelocity.x = 0f;
        rb.velocity = transform.TransformDirection(localvelocity);
    }
    void Turn()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddTorque(Vector3.up * turnspeed);
        }

        else if (Input.GetKey(KeyCode.A))
        {
            rb.AddTorque(-Vector3.up * turnspeed);
        }
    }
    void fall()
    {
        rb.AddForce(Vector3.down * gravitymuli * 10f);
    }
}
