using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switch_cam : MonoBehaviour
{
    public GameObject[] cameras;
    private int currentcam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            currentcam++;

            if(currentcam >= cameras.Length)
            {
                currentcam = 0;
            }

            for(int i = 0; i <cameras.Length; i++)
            {
                if (i == currentcam)
                {
                    cameras[i].SetActive(true);
                }
                else 
                {
                    cameras[i].SetActive(false);
                }
            }
        }
    }
}
