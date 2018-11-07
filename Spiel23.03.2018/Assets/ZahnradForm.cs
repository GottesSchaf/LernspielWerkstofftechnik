﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZahnradForm : Interactive
{
    public Transform ThisForm;              //Datenblatt Position
    public static Transform instance;       //Position
    public CameraFollow boop;               //Kamera Position
    public GameObject zahnradForm;           //Datenblatt Objekt

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
        zahnradForm.SetActive(false);
    }
    //Wenn man auf die Form klickt, öffnet sie sich
    public override void Interact()
    {
        zahnradForm.SetActive(true);
    }
}
