using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class quit : MonoBehaviour
{
    public void Onclickexit ()
    {  
          Application.Quit();
          //Debug.Log("SAIU");
    }

    public void Restartclick ()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
