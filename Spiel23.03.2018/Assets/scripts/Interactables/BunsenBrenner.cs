using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunsenBrenner : MonoBehaviour {

    public GameObject[] bunsenBrennerObjekt;
    public GameObject[] gasSchalter;
    public Transform[] bunsenBrennerTransf;
    GameObject tempGO;
    public int[] bBRate;                        //Array zum abspeichern der Rate in °C der jeweiligen Bunsen Brenner
    public int[] bBZeit;                        //Array zum abspeichern der Zeit in sekunden der jeweiligen Bunsen Brenner
    float[] istTemp;
    public static Transform instance;
    public CameraFollow followCam;
    bool hauptGasSchalter, platzGasSchalter, bBGasSchalter; //Zur Überprüfung ob die jeweiligen Gas Schalter bereits betätigt wurden
	// Use this for initialization
	void Start () {
        //Mische die Bunsen Brenner, sodass die Studenten nicht schummeln können
		for(int i = 0; i < bunsenBrennerObjekt.Length; i++)
        {
            int rnd = Random.Range(0, bunsenBrennerObjekt.Length);
            tempGO = bunsenBrennerObjekt[rnd];
            bunsenBrennerObjekt[rnd] = bunsenBrennerObjekt[i];
            bunsenBrennerObjekt[i] = tempGO;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        CheckInstance();
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
        if (hauptGasSchalter && platzGasSchalter && bBGasSchalter)
        {
            yield return new WaitForSeconds(1);
            if (istTemp[0] <= 1250)
            {
                istTemp[0] += bBRate[0] / bBZeit[0];
            }
            else if (istTemp[0] <= 1400)
            {
                istTemp[0] += bBRate[1] / bBZeit[1];
            }
            else if(istTemp[0] <= 1550)
            {
                istTemp[0] += bBRate[2] / bBZeit[2];
            }

        }
        else
        {
            yield return null;
        }
    }
}
