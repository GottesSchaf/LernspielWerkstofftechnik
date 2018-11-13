using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GussformScript : MonoBehaviour {

    public GameObject slotWrench, slotPleuel, slotZahnrad;
    public GameObject[] legierungForm;
    public int abkuehlZeit;

    void kuehleAb()
    {
        if(slotWrench.transform.childCount > 0 && slotWrench.GetComponentInChildren<GameObject>().CompareTag("40SiHot"))
        {

        }
        else if (slotPleuel.transform.childCount > 0 && slotPleuel.GetComponentInChildren<GameObject>().CompareTag("40SiHot"))
        {

        }
        else if (slotZahnrad.transform.childCount > 0 && slotZahnrad.GetComponentInChildren<GameObject>().CompareTag("40SiHot"))
        {

        }
    }
}
