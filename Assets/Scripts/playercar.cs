using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercar : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rigi;

    public float fowardaccel = 8f;
    public float reverseaccel = 4f;
    public float maxspeed = 45f;
    public float turnstrenght = 180f;
    public float gravityfoce = 10f;

    public float dragground = 3f;

    

    public float isntavelmaxspeed;

    
    public float speedinput;
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




    public bool isai;
    public int currenttarget;
    private Vector3 targetpoint;
    public float aiacceleratespeed =1f;
    public float iaturnspeed = .8f;
    public float aireachpointrange = 5f;
    public float aipointvariance = 3f;
    private float aispeedinput;
    public float aimaxturn =15f;


    public GameObject roda;


    public static playercar instance;


    public float boost;
    public float speedcooldown;
    public float normalspeed;
    private bool goboost = true;
    private WaitForSeconds quitdurantion = new WaitForSeconds(2f);


    // Start is called before the first frame update
    void Start()
    {
        isntavelmaxspeed = maxspeed;

        instance = this;

        rigi.transform.parent = null;

        if (isai)
        {
            targetpoint = Race_Menager.instance.allcheckpoint[currenttarget].transform.position;
            Randomiatarget();
        }

        //assim que iniciar ja mudar o numero de voltas
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!isai)
        {

            //input carro
        speedinput = 0f;
        if (Input.GetAxis("Vertical") > 0)
        {
            speedinput = Input.GetAxis("Vertical") * fowardaccel * 1000f;
            roda.transform.Rotate (new Vector3(x: 0, y:0,z:2));

        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            speedinput = Input.GetAxis("Vertical") * reverseaccel * 1000f;
            roda.transform.Rotate (new Vector3(x: 0, y:0,z:-2));
        }

        turninput = Input.GetAxis("Horizontal");

        } else 
        {

            targetpoint.y = transform.position.y;

            if (Vector3.Distance(transform.position, targetpoint) < aireachpointrange)
            {
                currenttarget ++;;
                if(currenttarget >= Race_Menager.instance.allcheckpoint.Length)
                {
                    currenttarget = 0;
                }

                targetpoint = Race_Menager.instance.allcheckpoint[currenttarget].transform.position;
                Randomiatarget();


            }


            // NAO MEXER (POR ENQUANTO) AQUI NAO SEI O QUE FIZ MAS FUNCIONOU (EM PARTES)

            Vector3 targetdir = targetpoint - transform.position;
            float angle = Vector3.Angle(targetdir, transform.forward);

            Vector3 localpos = transform.InverseTransformPoint(targetpoint);
            if (localpos.x <0f)
            {
                angle = -angle;
            }

            turninput = Mathf.Clamp( angle / aimaxturn, -1f, 1f);

            if (Mathf.Abs(angle) < aimaxturn)
            {
                aispeedinput = Mathf.MoveTowards(aispeedinput, 1f, aiacceleratespeed);
            } else
            {
                aispeedinput = Mathf.MoveTowards(aispeedinput, iaturnspeed,aiacceleratespeed);
            }




            aispeedinput = 100f;
            speedinput = aispeedinput * fowardaccel *10;
        }
        


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

            //boost car 
            normalspeed = speedinput;

             if (Input.GetAxis("Fire3") > 0)
                {   
                   
                   if (Ui_menager.instance.specialbar.value > 0 && goboost == true)
                   {
                    speedinput = boost;
                    rigi.AddForce(transform.forward * speedinput);
                    Debug.Log("VAI DESGRAÇA");
                    StartCoroutine(speedduration());
                   
                   }  
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


    //coisas do boost
    /*IEnumerator stopandcontinue ()
    {
        yield return new WaitForSecondsRealtime(15f);
        speedinput = normalspeed;
        goboost = true;   
        //Debug.Log("voltou a PALHAÇADA");
    } */

    IEnumerator speedduration () 
    {

        yield return new WaitForSecondsRealtime(speedcooldown);
         goboost = false; 
        //Debug.Log("AACABOU A PALHAÇADA");

        yield return new WaitForSecondsRealtime(15f);
        Debug.Log("louco PALHAÇADA");
        speedinput = normalspeed;
        goboost = true;   

        yield return quitdurantion;
    }

    //contador de check point para voltar completas
    public void Checkpointhit (int checknumber)
    {
        //Debug.Log(checknumber);
        if(checknumber == nextcheck)
        {
            nextcheck++;
            

            if (nextcheck == Race_Menager.instance.allcheckpoint.Length)
            {
                nextcheck = 0;
                Lap_complete ();
            }
        }
    }


    // contador de voltas completadas
    public void Lap_complete ()
    {
        lapscheck++;


        if (!isai)
        {
            Ui_menager.instance.lap_counter_text.text = lapscheck + "/" + Race_Menager.instance.total_laps;
        }

    }

    public void Randomiatarget ()
    {
        targetpoint += new Vector3(Random.Range(-aipointvariance, aipointvariance), 0f,Random.Range(-aipointvariance, aipointvariance) );
    }
}