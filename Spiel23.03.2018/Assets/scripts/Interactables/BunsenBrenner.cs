using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BunsenBrenner : MonoBehaviour {

    public GameObject[] bunsenBrennerObjekt;
    public GameObject[] gasSchalter;
    public Transform[] bunsenBrennerTransf;
    GameObject tempGO;
    public float[] bBZieltemp;                        //Array zum abspeichern der Rate in °C der jeweiligen Bunsen Brenner
    public int[] bBZeit;                        //Array zum abspeichern der Zeit in sekunden der jeweiligen Bunsen Brenner
    public float[] istTemp = new float[4];
    public static Transform instance;
    public CameraFollow followCam;
    public static bool hauptGasSchalter = false, platzGasSchalter = false, bBGasSchalter = false, waiting = false; //Zur Überprüfung ob die jeweiligen Gas Schalter bereits betätigt wurden
    public Text ausgabeText;
    public Transform slot1, slot2, slot3, slot4;
    [SerializeField] GameObject flamme1, flamme2, flamme3, flamme4;
    bool flamme1Bool, flamme2Bool, flamme3Bool, flamme4Bool;
    [SerializeField] GameObject tiegelZahnrad;
    [SerializeField] private Window_Graph windowGraph;
    [SerializeField] Window_Graph_Tiegel2 windowGraphTiegel2;
    [SerializeField] Window_Graph_Tiegel3 windowGraphTiegel3;
    [SerializeField] Window_Graph_Tiegel4 windowGraphTiegel4;
    int tiegelFarbe;
    // Use this for initialization
    void Start ()
    {
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
            if (flamme1Bool && (slot1.transform.GetChild(0).CompareTag("20SiCold") || slot1.transform.GetChild(0).CompareTag("20SiHot")) || flamme2Bool && (slot2.transform.GetChild(0).CompareTag("20SiCold") || slot2.transform.GetChild(0).CompareTag("20SiHot")) || flamme3Bool && (slot3.transform.GetChild(0).CompareTag("20SiCold") || slot3.transform.GetChild(0).CompareTag("20SiHot")) || flamme4Bool && (slot4.transform.GetChild(0).CompareTag("20SiCold") || slot4.transform.GetChild(0).CompareTag("20SiHot")))
            {
                Debug.Log("Tiegel 1 wird erhitzt");
                if (istTemp[0] <= 1250)
                {
                    istTemp[0] += bBZieltemp[0] / bBZeit[0];
                    tiegelFarbe = 20;
                    windowGraph.ShowGraph(istTemp[0], 10, tiegelFarbe);
                }
                else if (istTemp[0] <= 1400)
                {
                    istTemp[0] += (bBZieltemp[1] - bBZieltemp[0]) / bBZeit[1];
                    tiegelFarbe = 20;
                    windowGraph.ShowGraph(istTemp[0], 10, tiegelFarbe);
                }
                else if (istTemp[0] < 1550)
                {
                    istTemp[0] += (bBZieltemp[2] - bBZieltemp[1]) / bBZeit[2];
                    tiegelFarbe = 20;
                    windowGraph.ShowGraph(istTemp[0], 10, tiegelFarbe);
                }
                else if(istTemp[0] >= 1550)
                {
                    if (slot1.transform.childCount > 0)
                    {
                        if (slot1.transform.GetChild(0).CompareTag("20SiCold"))
                        {
                            slot1.transform.GetChild(0).tag = "20SiHot";
                        }
                    }
                    else if (slot2.transform.childCount > 0)
                    {
                        if (slot2.transform.GetChild(0).CompareTag("20SiCold"))
                        {
                            slot2.transform.GetChild(0).tag = "20SiHot";
                        }
                    }
                    else if (slot3.transform.childCount > 0)
                    {
                        if (slot3.transform.GetChild(0).CompareTag("20SiCold"))
                        {
                            slot3.transform.GetChild(0).tag = "20SiHot";
                        }
                    }
                    else if (slot4.transform.childCount > 0)
                    {
                        if (slot4.transform.GetChild(0).CompareTag("20SiCold"))
                        {
                            slot4.transform.GetChild(0).tag = "20SiHot";
                        }
                    }
                }
            }
            //Sonst kühl das ganze mit gleichen Raten ab
            else
            {
                Debug.Log("Tiegel 1 wird abgekühlt");
                if(istTemp[0] > 25 && istTemp[0] <= 1250)
                {
                    istTemp[0] -= bBZieltemp[0] / bBZeit[0];
                }
                else if (istTemp[0] > 1250 && istTemp[0] <= 1400)
                {
                    istTemp[0] -= (bBZieltemp[1] - bBZieltemp[0]) / bBZeit[1];
                }
                else if (istTemp[0] > 1400)
                {
                    istTemp[0] -= (bBZieltemp[2] - bBZieltemp[1]) / bBZeit[2];
                }
            }
            //40% Si / 60% Ge
            if (flamme1Bool && (slot1.transform.GetChild(0).CompareTag("40SiCold") || slot1.transform.GetChild(0).CompareTag("40SiHot")) || flamme2Bool && (slot2.transform.GetChild(0).CompareTag("40SiCold") || slot2.transform.GetChild(0).CompareTag("40SiHot")) || flamme3Bool && (slot3.transform.GetChild(0).CompareTag("40SiCold") || slot3.transform.GetChild(0).CompareTag("40SiHot")) || flamme4Bool && (slot4.transform.GetChild(0).CompareTag("40SiCold") || slot4.transform.GetChild(0).CompareTag("40SiHot")))
            {
                if (istTemp[1] <= 1100)
                {
                    istTemp[1] += bBZieltemp[3] / bBZeit[3];
                    tiegelFarbe = 40;
                    windowGraphTiegel2.ShowGraph(istTemp[0], 10, tiegelFarbe);
                }
                else if (istTemp[1] <= 1350)
                {
                    istTemp[1] += (bBZieltemp[4] - bBZieltemp[3]) / bBZeit[4];
                    tiegelFarbe = 40;
                    windowGraphTiegel2.ShowGraph(istTemp[0], 10, tiegelFarbe);
                }
                else if (istTemp[1] < 1550)
                {
                    istTemp[1] += (bBZieltemp[5] - bBZieltemp[4]) / bBZeit[5];
                    tiegelFarbe = 40;
                    windowGraphTiegel2.ShowGraph(istTemp[0], 10, tiegelFarbe);
                }
                else if (istTemp[1] >= 1550)
                {
                    if (slot1.transform.childCount > 0)
                    {
                        if (slot1.transform.GetChild(0).CompareTag("40SiCold"))
                        {
                            slot1.transform.GetChild(0).tag = "40SiHot";
                        }
                    }
                    else if (slot2.transform.childCount > 0)
                    {
                        if (slot2.transform.GetChild(0).CompareTag("40SiCold"))
                        {
                            slot2.transform.GetChild(0).tag = "40SiHot";
                        }
                    }
                    else if (slot3.transform.childCount > 0)
                    {
                        if (slot3.transform.GetChild(0).CompareTag("40SiCold"))
                        {
                            slot3.transform.GetChild(0).tag = "40SiHot";
                        }
                    }
                    else if (slot4.transform.childCount > 0)
                    {
                        if (slot4.transform.GetChild(0).CompareTag("40SiCold"))
                        {
                            slot4.transform.GetChild(0).tag = "40SiHot";
                        }
                    }
                }
            }
            else
            {
                if (istTemp[1] > 25 && istTemp[1] <= 1250)
                {
                    istTemp[1] -= bBZieltemp[3] / bBZeit[3];
                }
                else if (istTemp[1] > 1250 && istTemp[1] <= 1400)
                {
                    istTemp[1] -= (bBZieltemp[4] - bBZieltemp[3]) / bBZeit[4];
                }
                else if (istTemp[1] > 1400)
                {
                    istTemp[1] -= (bBZieltemp[5] - bBZieltemp[4]) / bBZeit[5];
                }
            }
            //60% Si / 40% Ge
            if (flamme1Bool && (slot1.transform.GetChild(0).CompareTag("60SiCold") || slot1.transform.GetChild(0).CompareTag("60SiHot")) || flamme2Bool && (slot2.transform.GetChild(0).CompareTag("60SiCold") || slot2.transform.GetChild(0).CompareTag("60SiHot")) || flamme3Bool && (slot3.transform.GetChild(0).CompareTag("60SiCold") || slot3.transform.GetChild(0).CompareTag("60SiHot")) || flamme4Bool && (slot4.transform.GetChild(0).CompareTag("60SiCold") || slot4.transform.GetChild(0).CompareTag("60SiHot")))
            {
                if (istTemp[2] <= 1125)
                {
                    istTemp[2] += bBZieltemp[6] / bBZeit[6];
                    tiegelFarbe = 60;
                    windowGraphTiegel3.ShowGraph(istTemp[0], 10, tiegelFarbe);
                }
                else if (istTemp[2] <= 1250)
                {
                    istTemp[2] += (bBZieltemp[7] - bBZieltemp[6]) / bBZeit[7];
                    tiegelFarbe = 60;
                    windowGraphTiegel3.ShowGraph(istTemp[0], 10, tiegelFarbe);
                }
                else if (istTemp[2] < 1550)
                {
                    istTemp[2] += (bBZieltemp[8] - bBZieltemp[7]) / bBZeit[8];
                    tiegelFarbe = 60;
                    windowGraphTiegel3.ShowGraph(istTemp[0], 10, tiegelFarbe);
                }
                else if (istTemp[2] >= 1550)
                {
                    if (slot1.transform.childCount > 0)
                    {
                        if (slot1.transform.GetChild(0).CompareTag("60SiCold"))
                        {
                            slot1.transform.GetChild(0).tag = "60SiHot";
                        }
                    }
                    else if (slot2.transform.childCount > 0)
                    {
                        if (slot2.transform.GetChild(0).CompareTag("60SiCold"))
                        {
                            slot2.transform.GetChild(0).tag = "60SiHot";
                        }
                    }
                    else if (slot3.transform.childCount > 0)
                    {
                        if (slot3.transform.GetChild(0).CompareTag("60SiCold"))
                        {
                            slot3.transform.GetChild(0).tag = "60SiHot";
                        }
                    }
                    else if (slot4.transform.childCount > 0)
                    {
                        if (slot4.transform.GetChild(0).CompareTag("60SiCold"))
                        {
                            slot4.transform.GetChild(0).tag = "60SiHot";
                        }
                    }
                }
            }
            else
            {
                if (istTemp[2] > 25 && istTemp[2] <= 1250)
                {
                    istTemp[2] -= bBZieltemp[6] / bBZeit[6];
                }
                else if (istTemp[2] > 1250 && istTemp[2] <= 1400)
                {
                    istTemp[2] -= (bBZieltemp[7] - bBZieltemp[6]) / bBZeit[7];
                }
                else if (istTemp[2] > 1400)
                {
                    istTemp[2] -= (bBZieltemp[8] - bBZieltemp[7]) / bBZeit[8];
                }
            }
            //80% Si / 20% Ge
            if (flamme1Bool && (slot1.transform.GetChild(0).CompareTag("80SiCold") || slot1.transform.GetChild(0).CompareTag("80SiHot")) || flamme2Bool && (slot2.transform.GetChild(0).CompareTag("80SiCold") || slot2.transform.GetChild(0).CompareTag("80SiHot")) || flamme3Bool && (slot3.transform.GetChild(0).CompareTag("80SiCold") || slot3.transform.GetChild(0).CompareTag("80SiHot")) || flamme4Bool && (slot4.transform.GetChild(0).CompareTag("80SiCold") || slot4.transform.GetChild(0).CompareTag("80SiHot")))
            {
                if (istTemp[3] <= 950)
                {
                    istTemp[3] += bBZieltemp[9] / bBZeit[9];
                    tiegelFarbe = 80;
                    windowGraphTiegel4.ShowGraph(istTemp[0], 10, tiegelFarbe);
                }
                else if (istTemp[3] <= 1100)
                {
                    istTemp[3] += (bBZieltemp[10] - bBZieltemp[9]) / bBZeit[10];
                    tiegelFarbe = 80;
                    windowGraphTiegel4.ShowGraph(istTemp[0], 10, tiegelFarbe);
                }
                else if (istTemp[3] < 1550)
                {
                    istTemp[3] += (bBZieltemp[11] - bBZieltemp[10]) / bBZeit[11];
                    tiegelFarbe = 80;
                    windowGraphTiegel4.ShowGraph(istTemp[0], 10, tiegelFarbe);
                }
                else if (istTemp[3] >= 1550)
                {
                    if (slot1.transform.childCount > 0)
                    {
                        if (slot1.transform.GetChild(0).CompareTag("80SiCold"))
                        {
                            slot1.transform.GetChild(0).tag = "80SiHot";
                        }
                    }
                    else if (slot2.transform.childCount > 0)
                    {
                        if (slot2.transform.GetChild(0).CompareTag("80SiCold"))
                        {
                            slot2.transform.GetChild(0).tag = "80SiHot";
                        }
                    }
                    else if (slot3.transform.childCount > 0)
                    {
                        if (slot3.transform.GetChild(0).CompareTag("80SiCold"))
                        {
                            slot3.transform.GetChild(0).tag = "80SiHot";
                        }
                    }
                    else if (slot4.transform.childCount > 0)
                    {
                        if (slot4.transform.GetChild(0).CompareTag("80SiCold"))
                        {
                            slot4.transform.GetChild(0).tag = "80SiHot";
                        }
                    }
                }
            }
            else
            {
                if (istTemp[3] > 25 && istTemp[3] <= 1250)
                {
                    istTemp[3] -= bBZieltemp[9] / bBZeit[9];
                }
                else if (istTemp[3] > 1250 && istTemp[3] <= 1400)
                {
                    istTemp[3] -= (bBZieltemp[10] - bBZieltemp[9]) / bBZeit[10];
                }
                else if (istTemp[3] > 1400)
                {
                    istTemp[3] -= (bBZieltemp[11] - bBZieltemp[10]) / bBZeit[11];
                }
            }
            //Eingeschmolzenes Zahnrad
            //if (flamme1.activeInHierarchy && slot1.transform.GetChild(0).CompareTag("ZahnradKaputt") || flamme2.activeInHierarchy && slot2.transform.GetChild(0).CompareTag("ZahnradKaputt") || flamme3.activeInHierarchy && slot3.transform.GetChild(0).CompareTag("ZahnradKaputt") || flamme4.activeInHierarchy && slot4.transform.GetChild(0).CompareTag("ZahnradKaputt"))
            //{
            //    Debug.Log("Zahnrad wird erhitzt");
            //    Debug.Log("istTemp[1] gerade: " + istTemp[1]);
            //    if (istTemp[1] <= 1100)
            //    {
            //        istTemp[1] += bBZieltemp[3] / bBZeit[3];
            //    }
            //    else if (istTemp[1] <= 1350)
            //    {
            //        istTemp[1] += (bBZieltemp[4] - bBZieltemp[3]) / bBZeit[4];
            //    }
            //    else if (istTemp[1] < 1550)
            //    {
            //        istTemp[1] += (bBZieltemp[5] - bBZieltemp[4]) / bBZeit[5];
            //    }
            //    else if (istTemp[1] >= 1550)
            //    {
            //        if (slot1.transform.GetChild(0).CompareTag("ZahnradKaputt"))
            //        {
            //            Destroy(slot1.transform.GetChild(0));
            //            GameObject newTiegel = Instantiate(tiegelZahnrad);
            //            newTiegel.transform.SetParent(slot1);
            //            istTemp[1] = 25;

            //        }
            //        else if (slot2.transform.GetChild(0).CompareTag("ZahnradKaputt"))
            //        {
            //            Destroy(slot2.transform.GetChild(0));
            //            GameObject newTiegel = Instantiate(tiegelZahnrad);
            //            newTiegel.transform.SetParent(slot2);
            //            istTemp[1] = 25;
            //        }
            //        else if (slot3.transform.GetChild(0).CompareTag("ZahnradKaputt"))
            //        {
            //            Destroy(slot3.transform.GetChild(0));
            //            GameObject newTiegel = Instantiate(tiegelZahnrad);
            //            newTiegel.transform.SetParent(slot3);
            //            istTemp[1] = 25;
            //        }
            //        else if (slot4.transform.GetChild(0).CompareTag("ZahnradKaputt"))
            //        {
            //            Destroy(slot4.transform.GetChild(0));
            //            GameObject newTiegel = Instantiate(tiegelZahnrad);
            //            newTiegel.transform.SetParent(slot4);
            //            istTemp[1] = 25;
            //        }
            //    }
            //}
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
            flamme1Bool = true;
        }
        else
        {
            flamme1.SetActive(false);
            flamme1Bool = false;
        }
    }
    //Zünde Bunsen Brenner links mittig an
    public void Flamme2Button()
    {
        if(flamme2.activeInHierarchy == false)
        {
            flamme2.SetActive(true);
            flamme2Bool = true;
        }
        else
        {
            flamme2.SetActive(false);
            flamme2Bool = false;
        }
    }
    //Zünde Bunsen Brenner rechts mittig an
    public void Flamme3Button()
    {
        if (flamme3.activeInHierarchy == false)
        {
            flamme3.SetActive(true);
            flamme3Bool = true;
        }
        else
        {
            flamme3.SetActive(false);
            flamme3Bool = false;
        }
    }
    //Zünde Bunsen Brenner ganz rechts an
    public void Flamme4Button()
    {
        if (flamme4.activeInHierarchy == false)
        {
            flamme4.SetActive(true);
            flamme4Bool = true;
        }
        else
        {
            flamme4.SetActive(false);
            flamme4Bool = false;
        }
    }
    #endregion
}
