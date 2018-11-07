using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PleuelForm : Interactive
{
    public Transform ThisForm;              //Datenblatt Position
    public static Transform instance;       //Position
    public CameraFollow boop;               //Kamera Position
    public GameObject pleuelForm;           //Datenblatt Objekt

    void Update()
    {
        instance = this.ThisForm;
        boop = CameraFollow.instance;

        if (CameraFollow.instance.closeupInteraction == true)
        {
            ThisForm.GetComponent<BoxCollider>().enabled = true;
        }
    }
    //Wenn der Close Button gedrückt wird, wird die Form "geschlossen"
    public void CloseWindow()
    {
        pleuelForm.SetActive(false);
    }
    //Wenn man auf die Form klickt, öffnet sie sich
    public override void Interact()
    {
        pleuelForm.SetActive(true);
    }
}
