using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ui_menager : MonoBehaviour
{
    //counter laps
    public static Ui_menager instance;
    public TMP_Text lap_counter_text;


    //recover stamina
    public Slider specialbar;
    public float cooldown;
    bool iscooldown = false;
    public KeyCode specialbarrcode;
    public float recover;
    private WaitForSeconds regentick = new WaitForSeconds(20f);



    //wingame
    public GameObject wingame;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;

       recover = specialbar.value;
        
    }

    void Update()
    {
        if (playercar.instance.lapscheck == Race_Menager.instance.total_laps)
        {
            wingame.SetActive(true);
        }
        boost();    
    }

    void boost ()
    {
        if (Input.GetKey(specialbarrcode) && iscooldown == false)
        {
           iscooldown = true;
        }

        if (iscooldown)
        {
            specialbar.value -= 1 / cooldown * Time.deltaTime;

            if (specialbar.value <= 0)
            {
                iscooldown = false;
               
            }
                StartCoroutine(regenstamina());   
        }
    }

    private IEnumerator regenstamina()
    {
        yield return new WaitForSeconds(5);

        while (specialbar.value < recover)
        {
            // preciso consertar o bug que trava
            //Debug.Log("voltou a PALHAÇADA");
            specialbar.value += 1 / cooldown * Time.deltaTime;

            yield return regentick;
        }
    }





















    /*public void Barspecial (int amount)
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
    } */





}
