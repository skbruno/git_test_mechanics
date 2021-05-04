using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class input_menager : MonoBehaviour
{
    public float vertical;
    public float horizontal;
    public bool handbrake;
    public bool boosting;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        handbrake = (Input.GetAxis("Jump") != 0)? true:false;
        if(Input.GetKey(KeyCode.LeftShift)) boosting = true; else boosting = false;
    }
}
