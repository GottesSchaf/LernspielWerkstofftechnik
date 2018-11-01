using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunsenBrennerOpen : Interactive {

    public Transform BBWindow;
    public override void Interact()
    {
        Debug.Log("Interagiere mit Bunsen Brenner.");
        BBWindow.gameObject.SetActive(true);
        CameraFollow.instance.closeupInteraction = true;
    }
}
