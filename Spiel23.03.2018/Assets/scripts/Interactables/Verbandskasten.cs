using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Verbandskasten : Interactive {

    [SerializeField] GameObject verbandskastenFenster, nichtVerbrannt;

    public override void Interact()
    {
        if (BunsenBrenner.verbrannt == true)
        {
            verbandskastenFenster.SetActive(true);
            BunsenBrenner.verbrannt = false;
        }
        else
        {
            nichtVerbrannt.SetActive(true);
        }
    }

    public void SchließeFenster()
    {
        verbandskastenFenster.SetActive(false);
        nichtVerbrannt.SetActive(false);
    }
}
