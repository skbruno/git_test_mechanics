using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lama : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag ("Player") == true)
        {
            playercar.instance.maxspeed = 5f;
            Debug.Log("entrou na lama");
        }    
    }

    void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.CompareTag ("Player") == true)
        {
            StartCoroutine(exitlama());
            playercar.instance.maxspeed = playercar.instance.isntavelmaxspeed;
            Debug.Log("entrou na lama");
        }    
    }

    IEnumerator exitlama ()
    {
        yield return new WaitForSecondsRealtime(0.2f);
    }
}
