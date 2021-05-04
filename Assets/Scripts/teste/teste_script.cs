using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teste_script : MonoBehaviour
{
    internal enum driveType {
        frontwheeldriver,
        rearwheeldriver,
        allwheeldrive
    }

    [SerializeField]
    private driveType drive;

    public AnimationCurve enginepower;
    private input_menager iM;
    public WheelCollider[] wheels = new WheelCollider[4];
    public GameObject[] wheelsMesh = new GameObject[4];
    private Rigidbody rigidbody;
    public float KPH;
    public float wheelsRPM;
    public float totalpower;
    //public float motorTorque = 100;
    public float brakpower;
    public float radius = 6;
    public float steeringMax = 4;
    public float thrust =1000f;

    public float [] slip = new float[4];




    public float engineRPM;
    public float smoothTime = 0.01f;
    public float[] gears;
    public int gearNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        getobjects();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        steervehicle();
        animatewheels();
        //getFriction();
        calculateEnginePower();
        
    }

//Engine curva para gerenciar a curva
    private void calculateEnginePower()
    {
        wheelRPM();
        
        totalpower = enginepower.Evaluate(engineRPM) * (gears[gearNum]) * iM.vertical;
        float velocity = 0.0f;
        engineRPM = Mathf.SmoothDamp(engineRPM, 1000 + (Mathf.Abs(wheelsRPM) * 3.6f * (gears[gearNum])), ref velocity, smoothTime);

        movevehicle();
    }

    private void wheelRPM()
    {
        float sum = 0;
        int R = 0;
        for (int i = 0; i < 4; i++)
        {
            sum += wheels[i].rpm;
            R++;
        }
        wheelsRPM = (R != 0)? sum / R : 0;
    }

    //input e movimentação horizontal
    private void movevehicle()
    {
        

        if (drive == driveType.allwheeldrive)
        {
                 for (int i = 0; i < wheels.Length; i++)
                {
                    wheels[i].motorTorque =  totalpower / 4;
                }
        } 
        else if (drive == driveType.rearwheeldriver)
        {
            for (int i = 2; i < wheels.Length; i++)
            {
                wheels[i].motorTorque =  (totalpower / 2);
            }
        }
        else 
        {
             for (int i = 0; i < wheels.Length - 2; i++)
            {
                wheels[i].motorTorque = (totalpower / 2);
            }
        }
        
       KPH = rigidbody.velocity.magnitude * 3.6f;

       if(iM.handbrake)
       {
           wheels[3].brakeTorque = wheels[2].brakeTorque = brakpower;
       } else
       {
           wheels[3].brakeTorque = wheels[2].brakeTorque = 0;
       }

       if(iM.boosting)
       {
           rigidbody.AddForce(-Vector3.forward * thrust);
       }
        
    }
 //input e movimentação vertical
    private void steervehicle ()
    {
        if(iM.horizontal > 0) {
            wheels[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * iM.horizontal;
            wheels[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * iM.horizontal;
        }
        else if (iM.horizontal < 0) {
            wheels[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * iM.horizontal;
            wheels[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * iM.horizontal;
        }
        else {
            wheels[0].steerAngle =0;
            wheels[1].steerAngle =0;
        }




         //for (int i = 0; i < wheels.Length -2; i++)
            //{
               // wheels[i].steerAngle = iM.horizontal * steeringMax;
            //}
    }
 //roda girando
    private void animatewheels()
    {
        Vector3 wheelPosition = Vector3.zero;
        Quaternion wheelRotation = Quaternion.identity;

        for (int i = 0; i < 4; i++)
        {
            wheels[i].GetWorldPose (out wheelPosition, out wheelRotation);
            wheelsMesh [i].transform.position = wheelPosition;
            wheelsMesh [i].transform.rotation = wheelRotation;
        }
    }
 //associando componentes
    private void getobjects ()
    {
        iM = GetComponent<input_menager>();
        rigidbody = GetComponent<Rigidbody>();
    }
 //fricação com o chao e deslizando
    private  void getFriction()
    {
        for (int i = 0; i < wheels.Length; i++)
        {
            WheelHit wheelHit;
            wheels[i].GetGroundHit(out wheelHit);

            slip[i] = wheelHit.forwardSlip;
        }
    }



}
