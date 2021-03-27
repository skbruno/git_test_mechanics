using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint_checker : MonoBehaviour
{
    public playercar car;
    void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Checkpoint")
        {
            //Debug.Log("passou" + other.GetComponent<Checkpoints>().checknumber);

            car.Checkpointhit(other.GetComponent<Checkpoints>().checknumber);
        }
    }
}
