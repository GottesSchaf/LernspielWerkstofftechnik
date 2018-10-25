using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatzGasSchalterScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.CompareTag("Player") && BunsenBrenner.platzGasSchalter == false)
        {
            BunsenBrenner.platzGasSchalter = true;
        }
        else
        {
            BunsenBrenner.platzGasSchalter = false;
        }
    }
}
