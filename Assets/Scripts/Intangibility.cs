using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Intangibility : MonoBehaviour
{
    
    public GameObject character;
    public float trans = 30f;
    float normal = 1f;

    [Header("Intangibility Stamina")]
    public float intaStamina = 100.0f;
    [SerializeField] public float maxIntaStamina = 100.0f;
    [HideInInspector] public bool IntaOn = false;//Determine weather intagibility is on or off
    [HideInInspector] public bool CanInta = true;//Determine whether player can use Intangibility or not

    [Header("Intangibility Regen Parameter")]
    [Range(0, 50)] [SerializeField] private float intaConsume = 20.0f;
    [Range(0, 50)] [SerializeField] private float intaRegen = 0.5f;

    [Header("UI Element")]
    [SerializeField] private Image intaProgressUI = null;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        //Switch to Intagibility mode
        if (Input.GetButtonDown("Intangibility") && CanInta)
        {
            IntaOn = true;
            ChangeAlpha(character.GetComponent<Renderer>().material, trans);//Make player more transparent
        }
        else if(Input.GetButtonUp("Intangibility"))
        {
            IntaOn = false;
            ChangeAlpha(character.GetComponent<Renderer>().material, normal);//Make player less transparent
        }
        if (CanInta == false)
        {
            IntaOn = false;
            ChangeAlpha(character.GetComponent<Renderer>().material, normal);//Make player less transparent
        }
        //Control Intangibility Stamina
        //Not inside intangibility
        if (!IntaOn)
        {
            if(intaStamina <= maxIntaStamina - 0.01)
            {
                //Regen if less than max
                intaStamina += intaRegen * Time.deltaTime;
                UpdateStamina();
              
            }
            //If stamina regened a little bit, player can go into intangibility again
            if (intaStamina > 0)
            {
                CanInta = true;
            } else if(intaStamina < 0)
            {
                //Regen if less than max
                intaStamina += intaRegen * Time.deltaTime;
                UpdateStamina();
            }
         //Using Intangibility
        }else if(IntaOn)
        {
            intaStamina -= intaConsume * Time.deltaTime;
            UpdateStamina();

            //If stamina below zero, turn of intangibility
            if (intaStamina <= 0)
            {
                IntaOn = false;
                CanInta = false;
            }
        }
        
    }

    void UpdateStamina()
    {
        //Update Stamina UI
        intaProgressUI.fillAmount = intaStamina / maxIntaStamina;
    }

    //Change opacity of player
    void ChangeAlpha(Material mat, float alphaVal)
    {
        Color oldColor = mat.color;
        Color newColor = new Color(alphaVal, alphaVal, oldColor.b, oldColor.a);
        mat.SetColor("_Color", newColor);
    }
}
