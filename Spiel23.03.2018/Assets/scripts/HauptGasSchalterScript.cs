using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HauptGasSchalterScript : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.CompareTag("Player") && BunsenBrenner.hauptGasSchalter == false)
        {
            BunsenBrenner.hauptGasSchalter = true;
            Debug.Log("Haupt Gas ein geschaltet");
        }
        else
        {
            BunsenBrenner.hauptGasSchalter = false;
            Debug.Log("Haupt Gas aus geschaltet");
        }
    }
}
