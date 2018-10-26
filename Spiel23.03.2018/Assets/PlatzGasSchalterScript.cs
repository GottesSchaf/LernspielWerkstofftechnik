using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatzGasSchalterScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.CompareTag("Player") && BunsenBrenner.platzGasSchalter == false && BunsenBrenner.hauptGasSchalter == true)
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
