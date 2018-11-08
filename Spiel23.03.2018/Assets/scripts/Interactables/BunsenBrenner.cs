using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BunsenBrenner : MonoBehaviour {

    public GameObject[] bunsenBrennerObjekt;
    public GameObject[] gasSchalter;
    public Transform[] bunsenBrennerTransf;
    GameObject tempGO;
    public int[] bBRate;                        //Array zum abspeichern der Rate in °C der jeweiligen Bunsen Brenner
    public int[] bBZeit;                        //Array zum abspeichern der Zeit in sekunden der jeweiligen Bunsen Brenner
    float[] istTemp = new float[4];
    public static Transform instance;
    public CameraFollow followCam;
    public static bool hauptGasSchalter = false, platzGasSchalter = false, bBGasSchalter = false, waiting = false; //Zur Überprüfung ob die jeweiligen Gas Schalter bereits betätigt wurden
    public Text ausgabeText;
    public Slot slot1, slot2, slot3, slot4;
    // Use this for initialization

    void Start () {
        //Mische die Bunsen Brenner, sodass die Studenten nicht schummeln können
        //for (int i = 0; i < bunsenBrennerObjekt.Length; i++)
        //{
        //    int rnd = Random.Range(0, bunsenBrennerObjekt.Length);
        //    tempGO = bunsenBrennerObjekt[rnd];
        //    bunsenBrennerObjekt[rnd] = bunsenBrennerObjekt[i];
        //    bunsenBrennerObjekt[i] = tempGO;
        //}
        //for (int i = 0; i < bunsenBrennerObjekt.Length; i++)
        //{
        //    if (slot1.transform.childCount == 0)
        //    {
        //        GameObject temp = Instantiate(bunsenBrennerObjekt[i], new Vector3(0, 0, 0), Quaternion.identity);
        //        temp.transform.SetParent(slot1.transform);
        //    }
        //    else if (slot2.transform.childCount == 0)
        //    {
        //        GameObject temp = Instantiate(bunsenBrennerObjekt[i], new Vector3(0, 0, 0), Quaternion.identity);
        //        temp.transform.SetParent(slot2.transform);
        //    }
        //    else if (slot3.transform.childCount == 0)
        //    {
        //        GameObject temp = Instantiate(bunsenBrennerObjekt[i], new Vector3(0, 0, 0), Quaternion.identity);
        //        temp.transform.SetParent(slot3.transform);
        //    }
        //    else if (slot4.transform.childCount == 0)
        //    {
        //        GameObject temp = Instantiate(bunsenBrennerObjekt[i], new Vector3(0, 0, 0), Quaternion.identity);
        //        temp.transform.SetParent(slot4.transform);
        //    }
        //}
        

        //StartCoroutine(BunsenBrennerRechnung());
	}
	
	// Update is called once per frame
	void Update ()
    {
        //CheckInstance();
        if(hauptGasSchalter && platzGasSchalter && waiting == false)
        {
            StartCoroutine(BunsenBrennerRechnung());
        }
    }

    public void CheckInstance()
    {
        if (instance == this.bunsenBrennerTransf[0])
        {
            instance = this.bunsenBrennerTransf[0];
            followCam = CameraFollow.instance;

            if (CameraFollow.instance.closeupInteraction)
            {
                bunsenBrennerTransf[0].GetComponent<BoxCollider>().enabled = true;
            }
        }
        else if (instance == this.bunsenBrennerTransf[1])
        {
            instance = this.bunsenBrennerTransf[1];
            followCam = CameraFollow.instance;

            if (CameraFollow.instance.closeupInteraction)
            {
                bunsenBrennerTransf[0].GetComponent<BoxCollider>().enabled = true;
            }

        }
        else if (instance == this.bunsenBrennerTransf[2])
        {
            instance = this.bunsenBrennerTransf[2];
            followCam = CameraFollow.instance;

            if (CameraFollow.instance.closeupInteraction)
            {
                bunsenBrennerTransf[0].GetComponent<BoxCollider>().enabled = true;
            }

        }
        else if (instance == this.bunsenBrennerTransf[3])
        {
            instance = this.bunsenBrennerTransf[3];
            followCam = CameraFollow.instance;

            if (CameraFollow.instance.closeupInteraction)
            {
                bunsenBrennerTransf[0].GetComponent<BoxCollider>().enabled = true;
            }

        }
    }

    public IEnumerator BunsenBrennerRechnung()
    {
        waiting = true;
        while (true)
        {
            yield return new WaitForSeconds(1);
            //20% Si / 80%Ge
            if (istTemp[0] <= 1250)
            {
                istTemp[0] += bBRate[0] / bBZeit[0];
            }
            else if (istTemp[0] <= 1400)
            {
                istTemp[0] += bBRate[1] / bBZeit[1];
            }
            else if (istTemp[0] <= 1550)
            {
                istTemp[0] += bBRate[2] / bBZeit[2];
            }
            //40% Si / 60% Ge
            if (istTemp[1] <= 1100)
            {
                istTemp[1] += bBRate[3] / bBZeit[3];
            }
            else if (istTemp[1] <= 1350)
            {
                istTemp[1] += bBRate[4] / bBZeit[4];
            }
            else if (istTemp[1] <= 1550)
            {
                istTemp[1] += bBRate[5] / bBZeit[5];
            }
            //60% Si / 40% Ge
            if (istTemp[2] <= 1125)
            {
                istTemp[2] += bBRate[6] / bBZeit[6];
            }
            else if (istTemp[2] <= 1250)
            {
                istTemp[2] += bBRate[7] / bBZeit[7];
            }
            else if (istTemp[2] <= 1550)
            {
                istTemp[2] += bBRate[8] / bBZeit[8];
            }
            //80% Si / 20% Ge
            if (istTemp[3] <= 950)
            {
                istTemp[3] += bBRate[9] / bBZeit[9];
            }
            else if (istTemp[3] <= 1100)
            {
                istTemp[3] += bBRate[10] / bBZeit[10];
            }
            else if (istTemp[3] <= 1550)
            {
                istTemp[3] += bBRate[11] / bBZeit[11];
            }
            ausgabeText.text = ("istTemp[0]: " + istTemp[0] + " / istTemp[1]: " + istTemp[1] + " / istTemp[2]: " + istTemp[2] + " / istTemp[3]: " + istTemp[3]);
            if (istTemp[0] >= 1550 && istTemp[1] >= 1550 && istTemp[2] >= 1550 && istTemp[3] >= 1550)
            {
                waiting = false;
                break;
            }
        }
    }
}
