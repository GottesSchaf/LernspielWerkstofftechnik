using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunsenBrennerOpen : Interactive {

    public Transform BBWindow;              //Bunsen Brenner Fenster
    public GameObject Laborkittel;          //Laborkittel des Spielers
    public GameObject LaborkittelError;     //Error Fenster
    public override void Interact()
    {
        //Wenn das Laborkittel bereits angezogen ist, dann öffne das Bunsen Brenner Fenster,
        if (Laborkittel.activeSelf)
        {
            BBWindow.gameObject.SetActive(true);
        }
        //Falls nicht, dann öffne den Warnhinweis zum Laborkittel
        else
        {
            LaborkittelError.SetActive(true);
        }
        //CameraFollow.instance.closeupInteraction = true;
    }
}
