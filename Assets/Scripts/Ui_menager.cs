using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ui_menager : MonoBehaviour
{
    public static Ui_menager instance;
    public TMP_Text lap_counter_text;



    //public Image specialbar;
    //public float cooldown;
    //bool iscooldown = false;
    //public KeyCode specialbarrcode;



    public Slider staminabar;

    private int maxstamina = 100;
    private int currentstamina;

    private WaitForSeconds regentick = new WaitForSeconds(0.5f);

    

    public GameObject wingame;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;


        currentstamina = maxstamina;
        staminabar.maxValue = maxstamina;
        staminabar.value = maxstamina;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playercar.instance.lapscheck == Race_Menager.instance.total_laps)
        {
            wingame.SetActive(true);
        }


        
    }


    public void Barspecial (int amount)
    {

        if(currentstamina - amount >= 0)
        {
            currentstamina -= amount;   
            staminabar.value = currentstamina;

            StartCoroutine(regenstamina());
        }
    }

    private IEnumerator regenstamina()
    {
        yield return new WaitForSeconds(2);

        while (currentstamina < maxstamina)
        {
            currentstamina += maxstamina / 100;
            staminabar.value = currentstamina;
             yield return regentick;
        }
    }
}
