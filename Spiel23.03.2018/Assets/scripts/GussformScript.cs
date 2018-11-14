using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GussformScript : MonoBehaviour {

    public GameObject slotWrench, slotPleuel, slotZahnrad;
    public GameObject[] legierungForm;
    public int abkuehlZeit;

    IEnumerator KuehleAb()
    {
        yield return new WaitForSeconds(1);
        #region Slot Ueberpruefung
        //Schaue nach, welche Legierung in welchem Slot ist und kühle diese ab
        if (slotWrench.transform.childCount > 0 && slotWrench.GetComponentInChildren<GameObject>().CompareTag("20SiHot"))
        {
            
        }
        else if (slotPleuel.transform.childCount > 0 && slotWrench.GetComponentInChildren<GameObject>().CompareTag("40SiHot"))
        {

        }
        else if (slotPleuel.transform.childCount > 0 && slotWrench.GetComponentInChildren<GameObject>().CompareTag("60SiHot"))
        {

        }
        else if (slotPleuel.transform.childCount > 0 && slotWrench.GetComponentInChildren<GameObject>().CompareTag("80SiHot"))
        {

        }
        else if (slotPleuel.transform.childCount > 0 && slotPleuel.GetComponentInChildren<GameObject>().CompareTag("20SiHot"))
        {

        }
        else if (slotPleuel.transform.childCount > 0 && slotPleuel.GetComponentInChildren<GameObject>().CompareTag("40SiHot"))
        {

        }
        else if (slotPleuel.transform.childCount > 0 && slotPleuel.GetComponentInChildren<GameObject>().CompareTag("60SiHot"))
        {

        }
        else if (slotPleuel.transform.childCount > 0 && slotPleuel.GetComponentInChildren<GameObject>().CompareTag("80SiHot"))
        {

        }
        else if (slotZahnrad.transform.childCount > 0 && slotZahnrad.GetComponentInChildren<GameObject>().CompareTag("20SiHot"))
        {

        }
        else if (slotZahnrad.transform.childCount > 0 && slotZahnrad.GetComponentInChildren<GameObject>().CompareTag("40SiHot"))
        {

        }
        else if (slotZahnrad.transform.childCount > 0 && slotZahnrad.GetComponentInChildren<GameObject>().CompareTag("60SiHot"))
        {

        }
        else if (slotZahnrad.transform.childCount > 0 && slotZahnrad.GetComponentInChildren<GameObject>().CompareTag("80SiHot"))
        {

        }
        #endregion
    }
}
