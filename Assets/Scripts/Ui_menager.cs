using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ui_menager : MonoBehaviour
{
    public static Ui_menager instance;
    public TMP_Text lap_counter_text;
    

    public GameObject wingame;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (playercar.instance.lapscheck == Race_Menager.instance.total_laps)
        {
            wingame.SetActive(true);
        }
    }
}
