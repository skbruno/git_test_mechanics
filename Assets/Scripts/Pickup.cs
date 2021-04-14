using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);

    }

    void OnTriggerEnter(Collider other) 
    {
         if (other.gameObject.CompareTag ("Player") == true)
        {
            //Debug.Log("Bateu55");

             Destroy(gameObject, 2);
        }
    }
}
