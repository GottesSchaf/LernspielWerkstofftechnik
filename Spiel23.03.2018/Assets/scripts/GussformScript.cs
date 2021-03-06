﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GussformScript : MonoBehaviour {

    public Transform slotWrenchTiegel, slotPleuelTiegel, slotZahnradTiegel, slotWrench, slotPleuel, slotZahnrad;
    public GameObject[] legierungForm;
    //public int abkuehlZeit;
    public GameObject zahnrad, pleuel, wrench;
    public GameObject BBScriptObjekt;
    BunsenBrenner bunsenBrenner;
    bool waiting;
    [SerializeField] Sprite tiegelLeer;

    void Start()
    {
        bunsenBrenner = BBScriptObjekt.GetComponent<BunsenBrenner>();
    }

    private void Update()
    {
        if (waiting == false)
        {
            StartCoroutine(KuehleAb());
        }
    }
    IEnumerator KuehleAb()
    {
        waiting = true;
        yield return new WaitForSeconds(1);
        //Schaue nach, welche Legierung in welchem Slot ist und kühle sie ab
        #region Slot Ueberpruefung
        //Schraubenschlüssel Slot
        if (slotWrenchTiegel.transform.childCount > 0 && slotWrenchTiegel.transform.GetChild(0).CompareTag("20SiHot"))
        {
            slotWrenchTiegel.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelLeer;
            slotWrenchTiegel.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelLeer;
            if (bunsenBrenner.istTemp[0] > 25 && bunsenBrenner.istTemp[0] <= 1250)
            {
                bunsenBrenner.istTemp[0] -= bunsenBrenner.bBZieltemp[0] / bunsenBrenner.bBZeit[0];
            }
            else if (bunsenBrenner.istTemp[0] > 1250 && bunsenBrenner.istTemp[0] <= 1400)
            {
                bunsenBrenner.istTemp[0] -= (bunsenBrenner.bBZieltemp[1] - bunsenBrenner.bBZieltemp[0]) / bunsenBrenner.bBZeit[1];
            }
            else if (bunsenBrenner.istTemp[0] > 1400)
            {
                bunsenBrenner.istTemp[0] -= (bunsenBrenner.bBZieltemp[2] - bunsenBrenner.bBZieltemp[1]) / bunsenBrenner.bBZeit[2];
            }
            else if (bunsenBrenner.istTemp[0] <= 25)
            {
                bunsenBrenner.istTemp[0] = 25;
                GameObject newWrench = Instantiate(wrench);
                newWrench.transform.SetParent(slotWrench);
                newWrench.transform.tag = "Falsch";
                slotWrenchTiegel.transform.GetChild(0).tag = "Empty";
            }
        }
        //40%Si / 60%Ge
        else if (slotWrenchTiegel.transform.childCount > 0 && slotWrenchTiegel.transform.GetChild(0).CompareTag("40SiHot"))
        {
            slotWrenchTiegel.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelLeer;
            slotWrenchTiegel.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelLeer;
            if (bunsenBrenner.istTemp[1] > 25 && bunsenBrenner.istTemp[1] <= 1250)
            {
                bunsenBrenner.istTemp[1] -= bunsenBrenner.bBZieltemp[3] / bunsenBrenner.bBZeit[3];
            }
            else if (bunsenBrenner.istTemp[1] > 1250 && bunsenBrenner.istTemp[1] <= 1400)
            {
                bunsenBrenner.istTemp[1] -= (bunsenBrenner.bBZieltemp[4] - bunsenBrenner.bBZieltemp[3]) / bunsenBrenner.bBZeit[4];
            }
            else if (bunsenBrenner.istTemp[1] > 1400)
            {
                bunsenBrenner.istTemp[1] -= (bunsenBrenner.bBZieltemp[5] - bunsenBrenner.bBZieltemp[4]) / bunsenBrenner.bBZeit[5];
            }
            else if (bunsenBrenner.istTemp[1] <= 25)
            {
                bunsenBrenner.istTemp[1] = 25;
                GameObject newWrench = Instantiate(wrench);
                newWrench.transform.SetParent(slotWrench);
                newWrench.transform.tag = "Falsch";
                slotWrenchTiegel.transform.GetChild(0).tag = "Empty";
            }
        }
        //60%Si / 40%Ge
        else if (slotWrenchTiegel.transform.childCount > 0 && slotWrenchTiegel.transform.GetChild(0).CompareTag("60SiHot"))
        {
            slotWrenchTiegel.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelLeer;
            slotWrenchTiegel.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelLeer;
            if (bunsenBrenner.istTemp[2] > 25 && bunsenBrenner.istTemp[2] <= 1250)
            {
                bunsenBrenner.istTemp[2] -= bunsenBrenner.bBZieltemp[6] / bunsenBrenner.bBZeit[6];
            }
            else if (bunsenBrenner.istTemp[2] > 1250 && bunsenBrenner.istTemp[2] <= 1400)
            {
                bunsenBrenner.istTemp[2] -= (bunsenBrenner.bBZieltemp[7] - bunsenBrenner.bBZieltemp[6]) / bunsenBrenner.bBZeit[7];
            }
            else if (bunsenBrenner.istTemp[2] > 1400)
            {
                bunsenBrenner.istTemp[2] -= (bunsenBrenner.bBZieltemp[8] - bunsenBrenner.bBZieltemp[7]) / bunsenBrenner.bBZeit[8];
            }
            else if (bunsenBrenner.istTemp[2] <= 25)
            {
                bunsenBrenner.istTemp[2] = 25;
                GameObject newWrench = Instantiate(wrench);
                newWrench.transform.SetParent(slotWrench);
                newWrench.transform.tag = "Falsch";
                slotWrenchTiegel.transform.GetChild(0).tag = "Empty";
            }
        }
        //80%Si / 20%Ge
        else if (slotWrenchTiegel.transform.childCount > 0 && slotWrenchTiegel.transform.GetChild(0).CompareTag("80SiHot"))
        {
            slotWrenchTiegel.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelLeer;
            slotWrenchTiegel.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelLeer;
            if (bunsenBrenner.istTemp[3] > 25 && bunsenBrenner.istTemp[3] <= 1250)
            {
                bunsenBrenner.istTemp[3] -= bunsenBrenner.bBZieltemp[9] / bunsenBrenner.bBZeit[9];
            }
            else if (bunsenBrenner.istTemp[3] > 1250 && bunsenBrenner.istTemp[3] <= 1400)
            {
                bunsenBrenner.istTemp[3] -= (bunsenBrenner.bBZieltemp[10] - bunsenBrenner.bBZieltemp[9]) / bunsenBrenner.bBZeit[10];
            }
            else if (bunsenBrenner.istTemp[3] > 1400)
            {
                bunsenBrenner.istTemp[3] -= (bunsenBrenner.bBZieltemp[11] - bunsenBrenner.bBZieltemp[10]) / bunsenBrenner.bBZeit[11];
            }
            else if (bunsenBrenner.istTemp[3] <= 25)
            {
                bunsenBrenner.istTemp[3] = 25;
                GameObject newWrench = Instantiate(wrench);
                newWrench.transform.SetParent(slotWrench);
                newWrench.transform.tag = "Falsch";
                slotWrenchTiegel.transform.GetChild(0).tag = "Empty";
            }
        }
        //Kaputtes Zahnrad
        //else if (slotWrenchTiegel.transform.childCount > 0 && slotWrenchTiegel.transform.GetChild(0).CompareTag("Geschmolzen"))
        //{
        //    if (bunsenBrenner.istTemp[1] > 25 && bunsenBrenner.istTemp[1] <= 1250)
        //    {
        //        bunsenBrenner.istTemp[1] -= bunsenBrenner.bBZieltemp[3] / bunsenBrenner.bBZeit[3];
        //    }
        //    else if (bunsenBrenner.istTemp[1] > 1250 && bunsenBrenner.istTemp[1] <= 1400)
        //    {
        //        bunsenBrenner.istTemp[1] -= (bunsenBrenner.bBZieltemp[4] - bunsenBrenner.bBZieltemp[3]) / bunsenBrenner.bBZeit[4];
        //    }
        //    else if (bunsenBrenner.istTemp[1] > 1400)
        //    {
        //        bunsenBrenner.istTemp[1] -= (bunsenBrenner.bBZieltemp[5] - bunsenBrenner.bBZieltemp[4]) / bunsenBrenner.bBZeit[5];
        //    }
        //    else if (bunsenBrenner.istTemp[1] <= 25)
        //    {
        //        bunsenBrenner.istTemp[1] = 25;
        //        GameObject newWrench = Instantiate(wrench);
        //        newWrench.transform.SetParent(slotWrench);
        //        newWrench.transform.tag = "Falsch";
        //        slotWrenchTiegel.transform.GetChild(0).tag = "GeschmolzenEmpty";
        //    }
        //}
        //Pleuel Slot
        else if (slotPleuelTiegel.transform.childCount > 0 && slotPleuelTiegel.transform.GetChild(0).CompareTag("20SiHot"))
        {
            slotPleuelTiegel.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelLeer;
            slotPleuelTiegel.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelLeer;
            if (bunsenBrenner.istTemp[0] > 25 && bunsenBrenner.istTemp[0] <= 1250)
            {
                bunsenBrenner.istTemp[0] -= bunsenBrenner.bBZieltemp[0] / bunsenBrenner.bBZeit[0];
            }
            else if (bunsenBrenner.istTemp[0] > 1250 && bunsenBrenner.istTemp[0] <= 1400)
            {
                bunsenBrenner.istTemp[0] -= (bunsenBrenner.bBZieltemp[1] - bunsenBrenner.bBZieltemp[0]) / bunsenBrenner.bBZeit[1];
            }
            else if (bunsenBrenner.istTemp[0] > 1400)
            {
                bunsenBrenner.istTemp[0] -= (bunsenBrenner.bBZieltemp[2] - bunsenBrenner.bBZieltemp[1]) / bunsenBrenner.bBZeit[2];
            }
            else if (bunsenBrenner.istTemp[0] <= 25)
            {
                bunsenBrenner.istTemp[0] = 25;
                GameObject newPleuel = Instantiate(pleuel);
                newPleuel.transform.SetParent(slotPleuel.transform);
                newPleuel.transform.tag = "Falsch";
                slotPleuelTiegel.transform.GetChild(0).tag = "Empty";
            }
        }
        //40%Si / 60%Ge
        else if (slotPleuelTiegel.transform.childCount > 0 && slotPleuelTiegel.transform.GetChild(0).CompareTag("40SiHot"))
        {
            slotPleuelTiegel.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelLeer;
            slotPleuelTiegel.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelLeer;
            if (bunsenBrenner.istTemp[1] > 25 && bunsenBrenner.istTemp[1] <= 1250)
            {
                bunsenBrenner.istTemp[1] -= bunsenBrenner.bBZieltemp[3] / bunsenBrenner.bBZeit[3];
            }
            else if (bunsenBrenner.istTemp[1] > 1250 && bunsenBrenner.istTemp[1] <= 1400)
            {
                bunsenBrenner.istTemp[1] -= (bunsenBrenner.bBZieltemp[4] - bunsenBrenner.bBZieltemp[3]) / bunsenBrenner.bBZeit[4];
            }
            else if (bunsenBrenner.istTemp[1] > 1400)
            {
                bunsenBrenner.istTemp[1] -= (bunsenBrenner.bBZieltemp[5] - bunsenBrenner.bBZieltemp[4]) / bunsenBrenner.bBZeit[5];
            }
            else if (bunsenBrenner.istTemp[1] <= 25)
            {
                bunsenBrenner.istTemp[1] = 25;
                GameObject newPleuel = Instantiate(pleuel);
                newPleuel.transform.SetParent(slotPleuel.transform);
                newPleuel.transform.tag = "Falsch";
                slotPleuelTiegel.transform.GetChild(0).tag = "Empty";
            }
        }
        //60%Si / 40%Ge
        else if (slotPleuelTiegel.transform.childCount > 0 && slotPleuelTiegel.transform.GetChild(0).CompareTag("60SiHot"))
        {
            slotPleuelTiegel.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelLeer;
            slotPleuelTiegel.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelLeer;
            if (bunsenBrenner.istTemp[2] > 25 && bunsenBrenner.istTemp[2] <= 1250)
            {
                bunsenBrenner.istTemp[2] -= bunsenBrenner.bBZieltemp[6] / bunsenBrenner.bBZeit[6];
            }
            else if (bunsenBrenner.istTemp[2] > 1250 && bunsenBrenner.istTemp[2] <= 1400)
            {
                bunsenBrenner.istTemp[2] -= (bunsenBrenner.bBZieltemp[7] - bunsenBrenner.bBZieltemp[6]) / bunsenBrenner.bBZeit[7];
            }
            else if (bunsenBrenner.istTemp[2] > 1400)
            {
                bunsenBrenner.istTemp[2] -= (bunsenBrenner.bBZieltemp[8] - bunsenBrenner.bBZieltemp[7]) / bunsenBrenner.bBZeit[8];
            }
            else if (bunsenBrenner.istTemp[2] <= 25)
            {
                bunsenBrenner.istTemp[2] = 25;
                GameObject newPleuel = Instantiate(pleuel);
                newPleuel.transform.SetParent(slotPleuel.transform);
                newPleuel.transform.tag = "Falsch";
                slotPleuelTiegel.transform.GetChild(0).tag = "Empty";
            }
        }
        //80%Si / 20%Ge
        else if (slotPleuelTiegel.transform.childCount > 0 && slotPleuelTiegel.transform.GetChild(0).CompareTag("80SiHot"))
        {
            slotPleuelTiegel.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelLeer;
            slotPleuelTiegel.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelLeer;
            if (bunsenBrenner.istTemp[3] > 25 && bunsenBrenner.istTemp[3] <= 1250)
            {
                bunsenBrenner.istTemp[3] -= bunsenBrenner.bBZieltemp[9] / bunsenBrenner.bBZeit[9];
            }
            else if (bunsenBrenner.istTemp[3] > 1250 && bunsenBrenner.istTemp[3] <= 1400)
            {
                bunsenBrenner.istTemp[3] -= (bunsenBrenner.bBZieltemp[10] - bunsenBrenner.bBZieltemp[9]) / bunsenBrenner.bBZeit[10];
            }
            else if (bunsenBrenner.istTemp[3] > 1400)
            {
                bunsenBrenner.istTemp[3] -= (bunsenBrenner.bBZieltemp[11] - bunsenBrenner.bBZieltemp[10]) / bunsenBrenner.bBZeit[11];
            }
            else if (bunsenBrenner.istTemp[3] <= 25)
            {
                bunsenBrenner.istTemp[3] = 25;
                GameObject newPleuel = Instantiate(pleuel);
                newPleuel.transform.SetParent(slotPleuel.transform);
                newPleuel.transform.tag = "Falsch";
                slotPleuelTiegel.transform.GetChild(0).tag = "Empty";
            }
        }
        //Kaputtes Zahnrad
        //else if (slotPleuelTiegel.transform.childCount > 0 && slotPleuelTiegel.transform.GetChild(0).CompareTag("Geschmolzen"))
        //{
        //    if (bunsenBrenner.istTemp[1] > 25 && bunsenBrenner.istTemp[1] <= 1250)
        //    {
        //        bunsenBrenner.istTemp[1] -= bunsenBrenner.bBZieltemp[3] / bunsenBrenner.bBZeit[3];
        //    }
        //    else if (bunsenBrenner.istTemp[1] > 1250 && bunsenBrenner.istTemp[1] <= 1400)
        //    {
        //        bunsenBrenner.istTemp[1] -= (bunsenBrenner.bBZieltemp[4] - bunsenBrenner.bBZieltemp[3]) / bunsenBrenner.bBZeit[4];
        //    }
        //    else if (bunsenBrenner.istTemp[1] > 1400)
        //    {
        //        bunsenBrenner.istTemp[1] -= (bunsenBrenner.bBZieltemp[5] - bunsenBrenner.bBZieltemp[4]) / bunsenBrenner.bBZeit[5];
        //    }
        //    else if (bunsenBrenner.istTemp[1] <= 25)
        //    {
        //        bunsenBrenner.istTemp[1] = 25;
        //        GameObject newPleuel = Instantiate(pleuel);
        //        newPleuel.transform.SetParent(slotPleuel.transform);
        //        newPleuel.transform.tag = "Falsch";
        //        slotPleuelTiegel.transform.GetChild(0).tag = "GeschmolzenEmpty";
        //    }
        //}
        //Zahnrad Slot
        else if (slotZahnradTiegel.transform.childCount > 0 && slotZahnradTiegel.transform.GetChild(0).CompareTag("20SiHot"))
        {
            slotZahnradTiegel.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelLeer;
            slotZahnradTiegel.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelLeer;
            if (bunsenBrenner.istTemp[0] > 25 && bunsenBrenner.istTemp[0] <= 1250)
            {
                bunsenBrenner.istTemp[0] -= bunsenBrenner.bBZieltemp[0] / bunsenBrenner.bBZeit[0];
            }
            else if (bunsenBrenner.istTemp[0] > 1250 && bunsenBrenner.istTemp[0] <= 1400)
            {
                bunsenBrenner.istTemp[0] -= (bunsenBrenner.bBZieltemp[1] - bunsenBrenner.bBZieltemp[0]) / bunsenBrenner.bBZeit[1];
            }
            else if (bunsenBrenner.istTemp[0] > 1400)
            {
                bunsenBrenner.istTemp[0] -= (bunsenBrenner.bBZieltemp[2] - bunsenBrenner.bBZieltemp[1]) / bunsenBrenner.bBZeit[2];
            }
            else if (bunsenBrenner.istTemp[0] <= 25)
            {
                bunsenBrenner.istTemp[0] = 25;
                GameObject newZahnrad = Instantiate(zahnrad);
                newZahnrad.transform.SetParent(slotZahnrad.transform);
                newZahnrad.transform.tag = "Falsch";
                slotZahnradTiegel.transform.GetChild(0).tag = "Empty";
            }
        }
        //40%Si / 60%Ge
        else if (slotZahnradTiegel.transform.childCount > 0 && slotZahnradTiegel.transform.GetChild(0).CompareTag("40SiHot"))
        {
            slotZahnradTiegel.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelLeer;
            slotZahnradTiegel.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelLeer;
            if (bunsenBrenner.istTemp[1] > 25 && bunsenBrenner.istTemp[1] <= 1250)
            {
                bunsenBrenner.istTemp[1] -= bunsenBrenner.bBZieltemp[3] / bunsenBrenner.bBZeit[3];
            }
            else if (bunsenBrenner.istTemp[1] > 1250 && bunsenBrenner.istTemp[1] <= 1400)
            {
                bunsenBrenner.istTemp[1] -= (bunsenBrenner.bBZieltemp[4] - bunsenBrenner.bBZieltemp[3]) / bunsenBrenner.bBZeit[4];
            }
            else if (bunsenBrenner.istTemp[1] > 1400)
            {
                bunsenBrenner.istTemp[1] -= (bunsenBrenner.bBZieltemp[5] - bunsenBrenner.bBZieltemp[4]) / bunsenBrenner.bBZeit[5];
            }
            else if (bunsenBrenner.istTemp[1] <= 25)
            {
                bunsenBrenner.istTemp[1] = 25;
                GameObject newZahnrad = Instantiate(zahnrad);
                newZahnrad.transform.SetParent(slotZahnrad.transform);
                newZahnrad.transform.tag = "Richtig";
                slotZahnradTiegel.transform.GetChild(0).tag = "Empty";
            }
        }
        //60%Si / 40%Ge
        else if (slotZahnradTiegel.transform.childCount > 0 && slotZahnradTiegel.transform.GetChild(0).CompareTag("60SiHot"))
        {
            slotZahnradTiegel.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelLeer;
            slotZahnradTiegel.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelLeer;
            if (bunsenBrenner.istTemp[2] > 25 && bunsenBrenner.istTemp[2] <= 1250)
            {
                bunsenBrenner.istTemp[2] -= bunsenBrenner.bBZieltemp[6] / bunsenBrenner.bBZeit[6];
            }
            else if (bunsenBrenner.istTemp[2] > 1250 && bunsenBrenner.istTemp[2] <= 1400)
            {
                bunsenBrenner.istTemp[2] -= (bunsenBrenner.bBZieltemp[7] - bunsenBrenner.bBZieltemp[6]) / bunsenBrenner.bBZeit[7];
            }
            else if (bunsenBrenner.istTemp[2] > 1400)
            {
                bunsenBrenner.istTemp[2] -= (bunsenBrenner.bBZieltemp[8] - bunsenBrenner.bBZieltemp[7]) / bunsenBrenner.bBZeit[8];
            }
            else if (bunsenBrenner.istTemp[2] <= 25)
            {
                bunsenBrenner.istTemp[2] = 25;
                GameObject newZahnrad = Instantiate(zahnrad);
                newZahnrad.transform.SetParent(slotZahnrad.transform);
                newZahnrad.transform.tag = "Falsch";
                slotZahnradTiegel.transform.GetChild(0).tag = "Empty";
            }
        }
        //80%Si / 20%Ge
        else if (slotZahnradTiegel.transform.childCount > 0 && slotZahnradTiegel.transform.GetChild(0).CompareTag("80SiHot"))
        {
            slotZahnradTiegel.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelLeer;
            slotZahnradTiegel.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelLeer;
            if (bunsenBrenner.istTemp[3] > 25 && bunsenBrenner.istTemp[3] <= 1250)
            {
                bunsenBrenner.istTemp[3] -= bunsenBrenner.bBZieltemp[9] / bunsenBrenner.bBZeit[9];
            }
            else if (bunsenBrenner.istTemp[3] > 1250 && bunsenBrenner.istTemp[3] <= 1400)
            {
                bunsenBrenner.istTemp[3] -= (bunsenBrenner.bBZieltemp[10] - bunsenBrenner.bBZieltemp[9]) / bunsenBrenner.bBZeit[10];
            }
            else if (bunsenBrenner.istTemp[3] > 1400)
            {
                bunsenBrenner.istTemp[3] -= (bunsenBrenner.bBZieltemp[11] - bunsenBrenner.bBZieltemp[10]) / bunsenBrenner.bBZeit[11];
            }
            else if (bunsenBrenner.istTemp[3] <= 25)
            {
                bunsenBrenner.istTemp[3] = 25;
                GameObject newZahnrad = Instantiate(zahnrad);
                newZahnrad.transform.SetParent(slotZahnrad.transform);
                newZahnrad.transform.tag = "Falsch";
                slotZahnradTiegel.transform.GetChild(0).tag = "Empty";
            }
        }
        //Kaputtes Zahnrad
        //else if (slotZahnradTiegel.transform.childCount > 0 && slotZahnradTiegel.transform.GetChild(0).CompareTag("Geschmolzen"))
        //{
        //    if (bunsenBrenner.istTemp[1] > 25 && bunsenBrenner.istTemp[1] <= 1250)
        //    {
        //        bunsenBrenner.istTemp[1] -= bunsenBrenner.bBZieltemp[3] / bunsenBrenner.bBZeit[3];
        //    }
        //    else if (bunsenBrenner.istTemp[1] > 1250 && bunsenBrenner.istTemp[1] <= 1400)
        //    {
        //        bunsenBrenner.istTemp[1] -= (bunsenBrenner.bBZieltemp[4] - bunsenBrenner.bBZieltemp[3]) / bunsenBrenner.bBZeit[4];
        //    }
        //    else if (bunsenBrenner.istTemp[1] > 1400)
        //    {
        //        bunsenBrenner.istTemp[1] -= (bunsenBrenner.bBZieltemp[5] - bunsenBrenner.bBZieltemp[4]) / bunsenBrenner.bBZeit[5];
        //    }
        //    else if (bunsenBrenner.istTemp[1] <= 25)
        //    {
        //        bunsenBrenner.istTemp[1] = 25;
        //        GameObject newZahnrad = Instantiate(zahnrad);
        //        newZahnrad.transform.SetParent(slotZahnrad.transform);
        //        newZahnrad.transform.tag = "FalschCheat";
        //        slotZahnradTiegel.transform.GetChild(0).tag = "GeschmolzenEmpty";
        //    }
        //}
        waiting = false;
        #endregion
    }
}
