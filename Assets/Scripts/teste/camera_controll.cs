using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_controll : MonoBehaviour
{

    public GameObject player;
    private teste_script RR;
    public GameObject child;
    public float speed;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        child = player.transform.Find("CAMERA_testando").gameObject;
        RR = player.GetComponent<teste_script>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Follow();

        speed = (RR.KPH >= 50)? 20 : RR.KPH / 4;
    }


    void Follow ()
    {
        gameObject.transform.position = Vector3.Lerp(transform.position, player.transform.position, Time.deltaTime * speed);
        gameObject.transform.LookAt(child.gameObject.transform.position);
    }
}
