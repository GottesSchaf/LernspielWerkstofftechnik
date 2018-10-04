using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : Interactive
{
    public Transform ThisBook;
    public static Transform instance;
    public string URL;

    void Update()
    {
        instance = this.ThisBook;

        if (CameraFollow.instance.closeupInteraction == true)
        {
            ThisBook.GetComponent<BoxCollider>().enabled = true;
        }
    }
      
    public override void Interact()
        {
            Debug.Log("You opend the URL.");
            Application.OpenURL(URL);
        }
}
