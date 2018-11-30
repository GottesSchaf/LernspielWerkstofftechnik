﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HauptGasSchalterScript : Interactive
{
    [SerializeField] Material fuseOn, fuseOff;

    public override void Interact()
    {
        if (BunsenBrenner.hauptGasSchalter == false)
        {
            BunsenBrenner.hauptGasSchalter = true;
            this.gameObject.GetComponent<Renderer>().material = fuseOn;
            Debug.Log("Haupt Gas ein geschaltet");
        }
        else
        {
            BunsenBrenner.hauptGasSchalter = false;
            this.gameObject.GetComponent<Renderer>().material = fuseOff;
            Debug.Log("Haupt Gas aus geschaltet");
        }
    }
}
