using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatzGasSchalterScript : Interactive
{
    [SerializeField] Material platzGasOn, platzGasOff;
    [SerializeField] GameObject airDischarger;

    public override void Interact()
    {
        if (BunsenBrenner.platzGasSchalter == false && BunsenBrenner.hauptGasSchalter == true)
        {
            BunsenBrenner.platzGasSchalter = true;
            airDischarger.GetComponent<Renderer>().material = platzGasOn;
            Debug.Log("Platz Gas ein geschaltet");
        }
        else
        {
            BunsenBrenner.platzGasSchalter = false;
            airDischarger.GetComponent<Renderer>().material = platzGasOff;
            Debug.Log("Platz Gas aus geschaltet");
        }
    }
}
