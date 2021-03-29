using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Race_Menager : MonoBehaviour
{
    public static Race_Menager instance;
    public Checkpoints[] allcheckpoint;

    public int total_laps;
    
    void Awake() 
    {
        instance = this;
    }
    
    void Start()
    {
        for (int i = 0; i < allcheckpoint.Length; i++)
        {
            allcheckpoint[i].checknumber = i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
