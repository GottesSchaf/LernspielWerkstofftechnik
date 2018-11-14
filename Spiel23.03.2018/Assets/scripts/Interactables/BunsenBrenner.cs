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
    [SerializeField] GameObject flamme1, flamme2, flamme3, flamme4;
    // Use this for initialization

    void Start () {
        //Mische die Bunsen Brenner, sodass die Studenten nicht schummeln können
        for (int i = 0; i < bunsenBrennerObjekt.Length; i++)
        {
            int rnd = Random.Range(0, bunsenBrennerObjekt.Length);
            tempGO = bunsenBrennerObjekt[rnd];
            bunsenBrennerObjekt[rnd] = bunsenBrennerObjekt[i];
            bunsenBrennerObjekt[i] = tempGO;
        }
        for(int i = 0; i < istTemp.Length; i++)
        {
            istTemp[i] = 25;
        }
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
            #region Bunsen Brenner Abfrage
            //20% Si / 80%Ge || Wenn der Tiegel auf einem Bunsen Brenner liegt und die jeweilige Flamme an ist, erhitze den Tiegel
            if (flamme1.activeInHierarchy && slot1.transform.gameObject.GetComponentInChildren<Slot>().CompareTag("20SiCold") || flamme2.activeInHierarchy && slot2.gameObject.GetComponentInChildren<Slot>().gameObject.CompareTag("20SiCold") || flamme3.activeInHierarchy && slot3.gameObject.GetComponentInChildren<Slot>().gameObject.CompareTag("20SiCold") || flamme4.activeInHierarchy && slot4.gameObject.GetComponentInChildren<Slot>().gameObject.CompareTag("20SiCold"))
            {
                Debug.Log("Tiegel 1 wird erhitzt");
                if (istTemp[0] <= 1250)
                {
                    istTemp[0] += bBRate[0] / bBZeit[0];
                }
                else if (istTemp[0] <= 1400)
                {
                    istTemp[0] += bBRate[1] / bBZeit[1];
                }
                else if (istTemp[0] < 1550)
                {
                    istTemp[0] += bBRate[2] / bBZeit[2];
                }
                else if(istTemp[0] >= 1550)
                {
                    if (slot1.gameObject.GetComponentInChildren<Slot>().gameObject.CompareTag("20SiCold"))
                    {
                        slot1.transform.gameObject.GetComponentInChildren<GameObject>().tag = "20SiHot";
                    }
                    else if (slot2.gameObject.GetComponentInChildren<Slot>().gameObject.CompareTag("20SiCold"))
                    {
                        slot2.transform.gameObject.GetComponentInChildren<GameObject>().tag = "20SiHot";
                    }
                    else if (slot3.gameObject.GetComponentInChildren<Slot>().gameObject.CompareTag("20SiCold"))
                    {
                        slot3.transform.gameObject.GetComponentInChildren<GameObject>().tag = "20SiHot";
                    }
                    else if (slot4.gameObject.GetComponentInChildren<Slot>().gameObject.CompareTag("20SiCold"))
                    {
                        slot4.transform.gameObject.GetComponentInChildren<GameObject>().tag = "20SiHot";
                    }
                }
            }
            //Sonst kühl das ganze mit gleichen Raten ab
            else
            {
                Debug.Log("Tiegel 1 wird abgekühlt");
                if(istTemp[0] > 25 && istTemp[0] <= 1250)
                {
                    istTemp[0] -= bBRate[0] / bBZeit[0];
                }
                else if (istTemp[0] > 1250 && istTemp[0] <= 1400)
                {
                    istTemp[0] -= bBRate[1] / bBZeit[1];
                }
                else if (istTemp[0] > 1400)
                {
                    istTemp[0] -= bBRate[2] / bBZeit[2];
                }
            }
            //40% Si / 60% Ge
            if (flamme1.activeInHierarchy && slot1.gameObject.GetComponentInChildren<Slot>().gameObject.CompareTag("40SiCold") || flamme2.activeInHierarchy && slot2.gameObject.GetComponentInChildren<Slot>().gameObject.CompareTag("40SiCold") || flamme3.activeInHierarchy && slot3.gameObject.GetComponentInChildren<Slot>().gameObject.CompareTag("40SiCold") || flamme4.activeInHierarchy && slot4.gameObject.GetComponentInChildren<Slot>().gameObject.CompareTag("40SiCold"))
            {
                if (istTemp[1] <= 1100)
                {
                    istTemp[1] += bBRate[3] / bBZeit[3];
                }
                else if (istTemp[1] <= 1350)
                {
                    istTemp[1] += bBRate[4] / bBZeit[4];
                }
                else if (istTemp[1] < 1550)
                {
                    istTemp[1] += bBRate[5] / bBZeit[5];
                }
                else if (istTemp[1] >= 1550)
                {
                    if (slot1.gameObject.GetComponentInChildren<Slot>().gameObject.CompareTag("40SiCold"))
                    {
                        slot1.transform.gameObject.GetComponentInChildren<GameObject>().tag = "40SiHot";
                    }
                    else if (slot2.gameObject.GetComponentInChildren<Slot>().gameObject.CompareTag("40SiCold"))
                    {
                        slot2.transform.gameObject.GetComponentInChildren<GameObject>().tag = "40SiHot";
                    }
                    else if (slot3.gameObject.GetComponentInChildren<Slot>().gameObject.CompareTag("40SiCold"))
                    {
                        slot3.transform.gameObject.GetComponentInChildren<GameObject>().tag = "40SiHot";
                    }
                    else if (slot4.gameObject.GetComponentInChildren<Slot>().gameObject.CompareTag("40SiCold"))
                    {
                        slot4.transform.gameObject.GetComponentInChildren<GameObject>().tag = "40SiHot";
                    }
                }
            }
            else
            {
                if (istTemp[1] > 25 && istTemp[1] <= 1250)
                {
                    istTemp[1] -= bBRate[3] / bBZeit[3];
                }
                else if (istTemp[1] > 1250 && istTemp[1] <= 1400)
                {
                    istTemp[1] -= bBRate[4] / bBZeit[4];
                }
                else if (istTemp[1] > 1400)
                {
                    istTemp[1] -= bBRate[5] / bBZeit[5];
                }
            }
            //60% Si / 40% Ge
            if (flamme1.activeInHierarchy && slot1.gameObject.GetComponentInChildren<Slot>().gameObject.CompareTag("60SiCold") || flamme2.activeInHierarchy && slot2.gameObject.GetComponentInChildren<Slot>().gameObject.CompareTag("60SiCold") || flamme3.activeInHierarchy && slot3.gameObject.GetComponentInChildren<Slot>().gameObject.CompareTag("60SiCold") || flamme4.activeInHierarchy && slot4.gameObject.GetComponentInChildren<Slot>().gameObject.CompareTag("60SiCold"))
            {
                if (istTemp[2] <= 1125)
                {
                    istTemp[2] += bBRate[6] / bBZeit[6];
                }
                else if (istTemp[2] <= 1250)
                {
                    istTemp[2] += bBRate[7] / bBZeit[7];
                }
                else if (istTemp[2] < 1550)
                {
                    istTemp[2] += bBRate[8] / bBZeit[8];
                }
                else if (istTemp[2] >= 1550)
                {
                    if (slot1.gameObject.GetComponentInChildren<Slot>().gameObject.CompareTag("60SiCold"))
                    {
                        slot1.transform.gameObject.GetComponentInChildren<GameObject>().tag = "60SiHot";
                    }
                    else if (slot2.gameObject.GetComponentInChildren<Slot>().gameObject.CompareTag("60SiCold"))
                    {
                        slot2.transform.gameObject.GetComponentInChildren<GameObject>().tag = "60SiHot";
                    }
                    else if (slot3.gameObject.GetComponentInChildren<Slot>().gameObject.CompareTag("60SiCold"))
                    {
                        slot3.transform.gameObject.GetComponentInChildren<GameObject>().tag = "60SiHot";
                    }
                    else if (slot4.gameObject.GetComponentInChildren<Slot>().gameObject.CompareTag("60SiCold"))
                    {
                        slot4.transform.gameObject.GetComponentInChildren<GameObject>().tag = "60SiHot";
                    }
                }
            }
            else
            {
                if (istTemp[2] > 25 && istTemp[2] <= 1250)
                {
                    istTemp[2] -= bBRate[6] / bBZeit[6];
                }
                else if (istTemp[2] > 1250 && istTemp[2] <= 1400)
                {
                    istTemp[2] -= bBRate[7] / bBZeit[7];
                }
                else if (istTemp[2] > 1400)
                {
                    istTemp[2] -= bBRate[8] / bBZeit[8];
                }
            }
            //80% Si / 20% Ge
            if (flamme1.activeInHierarchy && slot1.gameObject.GetComponentInChildren<Slot>().gameObject.CompareTag("80SiCold") || flamme2.activeInHierarchy && slot2.gameObject.GetComponentInChildren<Slot>().gameObject.CompareTag("80SiCold") || flamme3.activeInHierarchy && slot3.gameObject.GetComponentInChildren<Slot>().gameObject.CompareTag("80SiCold") || flamme4.activeInHierarchy && slot4.gameObject.GetComponentInChildren<Slot>().gameObject.CompareTag("80SiCold"))
            {
                if (istTemp[3] <= 950)
                {
                    istTemp[3] += bBRate[9] / bBZeit[9];
                }
                else if (istTemp[3] <= 1100)
                {
                    istTemp[3] += bBRate[10] / bBZeit[10];
                }
                else if (istTemp[3] < 1550)
                {
                    istTemp[3] += bBRate[11] / bBZeit[11];
                }
                else if (istTemp[3] >= 1550)
                {
                    if (slot1.gameObject.GetComponentInChildren<Slot>().gameObject.CompareTag("80SiCold"))
                    {
                        slot1.transform.gameObject.GetComponentInChildren<GameObject>().tag = "80SiHot";
                    }
                    else if (slot2.gameObject.GetComponentInChildren<Slot>().gameObject.CompareTag("80SiCold"))
                    {
                        slot2.transform.gameObject.GetComponentInChildren<GameObject>().tag = "80SiHot";
                    }
                    else if (slot3.gameObject.GetComponentInChildren<Slot>().gameObject.CompareTag("80SiCold"))
                    {
                        slot3.transform.gameObject.GetComponentInChildren<GameObject>().tag = "80SiHot";
                    }
                    else if (slot4.gameObject.GetComponentInChildren<Slot>().gameObject.CompareTag("80SiCold"))
                    {
                        slot4.transform.gameObject.GetComponentInChildren<GameObject>().tag = "80SiHot";
                    }
                }
            }
            else
            {
                if (istTemp[3] > 25 && istTemp[3] <= 1250)
                {
                    istTemp[3] -= bBRate[9] / bBZeit[9];
                }
                else if (istTemp[3] > 1250 && istTemp[3] <= 1400)
                {
                    istTemp[3] -= bBRate[10] / bBZeit[10];
                }
                else if (istTemp[3] > 1400)
                {
                    istTemp[3] -= bBRate[11] / bBZeit[11];
                }
            }
            #endregion
            //ausgabeText.text = ("istTemp[0]: " + istTemp[0] + " / istTemp[1]: " + istTemp[1] + " / istTemp[2]: " + istTemp[2] + " / istTemp[3]: " + istTemp[3]);
            if (istTemp[0] >= 1550 && istTemp[1] >= 1550 && istTemp[2] >= 1550 && istTemp[3] >= 1550)
            {
                waiting = false;
                break;
            }
        }
    }
    #region Bunsen Brenner Flammen
    //Zünde Bunsen Brenner ganz links an
    public void Flamme1Button()
    {
        if (flamme1.activeInHierarchy == false)
        {
            flamme1.SetActive(true);
        }
        else
        {
            flamme1.SetActive(false);
        }
    }
    //Zünde Bunsen Brenner links mittig an
    public void Flamme2Button()
    {
        if(flamme2.activeInHierarchy == false)
        {
            flamme2.SetActive(true);
        }
        else
        {
            flamme2.SetActive(false);
        }
    }
    //Zünde Bunsen Brenner rechts mittig an
    public void Flamme3Button()
    {
        if (flamme3.activeInHierarchy == false)
        {
            flamme3.SetActive(true);
        }
        else
        {
            flamme3.SetActive(false);
        }
    }
    //Zünde Bunsen Brenner ganz rechts an
    public void Flamme4Button()
    {
        if (flamme4.activeInHierarchy == false)
        {
            flamme4.SetActive(true);
        }
        else
        {
            flamme4.SetActive(false);
        }
    }
    #endregion
}
