using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ui_menager : MonoBehaviour
{
    public static Ui_menager instance;
    public TMP_Text lap_counter_text;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
