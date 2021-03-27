using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercar : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rigi;

    public float fowardaccel = 8f;
    public float reverseaccel = 4f;
    public float maxspeed = 5f;
    public float turnstrenght = 180f;
    public float gravityfoce = 10f;

    public float dragground = 3f;

    private float speedinput;
    private float turninput;

    private bool isground;

    public LayerMask whatisground;
    public float groundraylength = 0.5f;
    public Transform groundraypoint;



    public Transform whellfrontleft;
    public Transform whellfrontright;
    public float maxwhellturn = 25f;


    private int nextcheck;
    public int lapscheck;


    // Start is called before the first frame update
    void Start()
    {
        rigi.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {

        //input carro
        speedinput = 0f;
        if (Input.GetAxis("Vertical") > 0)
        {
            speedinput = Input.GetAxis("Vertical") * fowardaccel * 1000f;

        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            speedinput = Input.GetAxis("Vertical") * reverseaccel * 1000f;
        }

        turninput = Input.GetAxis("Horizontal");


        //virar só quando estiver em movimento
        if (isground)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turninput * turnstrenght * Time.deltaTime * Input.GetAxis("Vertical"), 0f));
        }



        //virar a roda do carro
        whellfrontleft.localRotation = Quaternion.Euler(whellfrontleft.localRotation.eulerAngles.x, (turninput * maxwhellturn) - 180f, whellfrontleft.localRotation.eulerAngles.z);

        whellfrontright.localRotation = Quaternion.Euler(whellfrontright.localRotation.eulerAngles.x, turninput * maxwhellturn , whellfrontright.localRotation.eulerAngles.z);

        transform.position = rigi.transform.position;
    }

    void FixedUpdate()
    {

        isground = false;
        RaycastHit hit;

        if (Physics.Raycast(groundraypoint.position, -transform.up, out hit, groundraylength, whatisground))
        {
            isground = true;

            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }


        //acelerrar o carro
        if (isground)
        {
            rigi.drag = dragground;

            if (Mathf.Abs(speedinput) > 0)
            {
                rigi.AddForce(transform.forward * speedinput);
            }
        }
        //gravidade quando o carro estiver no ar
        else
        {
            rigi.drag = 0.1f;

            rigi.AddForce(-Vector3.up * gravityfoce * 100f);
        }
        //max speed do carro
        if (rigi.velocity.magnitude > maxspeed)
        {
            rigi.velocity = rigi.velocity.normalized * maxspeed;
        }

        //Debug.Log(rigi.velocity.magnitude);

    }


    public void Checkpointhit (int checknumber)
    {
        //Debug.Log(checknumber);
        if(checknumber == nextcheck)
        {
            nextcheck++;
            

            if (nextcheck == Race_Menager.instance.allcheckpoint.Length)
            {
                nextcheck = 0;
                lapscheck++;
            }
        }
    }
}
