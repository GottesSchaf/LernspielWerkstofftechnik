using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatzGasSchalterScript : Interactive
{
    public override void Interact()
    {
        if (BunsenBrenner.platzGasSchalter == false && BunsenBrenner.hauptGasSchalter == true)
        {
            BunsenBrenner.platzGasSchalter = true;
            Debug.Log("Platz Gas ein geschaltet");
        }
        else
        {
            BunsenBrenner.platzGasSchalter = false;
            Debug.Log("Platz Gas aus geschaltet");
        }
    }
}
