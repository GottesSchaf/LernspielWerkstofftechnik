using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BunsenBrenner : MonoBehaviour
{

    public GameObject[] bunsenBrennerObjekt;
    public GameObject[] gasSchalter;
    public Transform[] bunsenBrennerTransf;
    [SerializeField] Transform tempGOTransf;
    [SerializeField] GameObject tempGO;
    //Array zum abspeichern der Rate in °C der jeweiligen Bunsen Brenner
    [Header("Soll der 4. / 4. und 5. Graphpunkt berechnet werden?")]
    [Header("Nur auswählen, wenn auch valide Daten angegeben sind!")]
    public bool graphPunkt4, graphPunkt5;
    [Header("Element 0 ist die niedrigste Temp. und geht aufwärts.")]
    //--------------Zieltemperaturen-----------------
    public List<float> BB1_Zieltemp = new List<float>();
    public List<float> BB2_Zieltemp = new List<float>();
    public List<float> BB3_Zieltemp = new List<float>();
    public List<float> BB4_Zieltemp = new List<float>();
    //-----------Zeiten für Bunsen Brenner-----------
    public List<int> BB1_Zeit = new List<int>();
    public List<int> BB2_Zeit = new List<int>();
    public List<int> BB3_Zeit = new List<int>();
    public List<int> BB4_Zeit = new List<int>();
    //-----------------------------------------------
    public float[] istTemp = new float[4];
    public static Transform instance;
    public CameraFollow followCam;
    public static bool hauptGasSchalter = false, platzGasSchalter = false, bBGasSchalter = false, waiting = false; //Zur Überprüfung ob die jeweiligen Gas Schalter bereits betätigt wurden
    public Text ausgabeText;
    public Transform slot1, slot2, slot3, slot4;
    [SerializeField] GameObject flamme1, flamme2, flamme3, flamme4;
    public static bool flamme1Bool, flamme2Bool, flamme3Bool, flamme4Bool;
    [SerializeField] GameObject tiegelZahnrad;
    [SerializeField] Window_Graph windowGraph;
    [SerializeField] Window_Graph_Tiegel2 windowGraphTiegel2;
    [SerializeField] Window_Graph_Tiegel3 windowGraphTiegel3;
    [SerializeField] Window_Graph_Tiegel4 windowGraphTiegel4;
    [SerializeField] GameObject[] tiegelAufBB;
    [SerializeField] List<GameObject> zahnradAufBB = new List<GameObject>();
    [SerializeField] List<GameObject> pleuelAufBB = new List<GameObject>();
    [SerializeField] List<GameObject> wrenchAufBB = new List<GameObject>();
    int tiegelFarbe, tiegel2Farbe, tiegel3Farbe, tiegel4Farbe;
    [SerializeField] ParticleSystem[] BunsenBrennerFlammen;
    bool tiegel1Heated, tiegel2Heated, tiegel3Heated, tiegel4Heated;
    [SerializeField] Material[] tiegelMat;
    [SerializeField] Sprite[] tiegelSprite;
    bool zeigeVerbrennung = false;
    public static bool verbrannt = false;
    [SerializeField] GameObject verbranntFenster;
    [SerializeField] GameObject erstVerarztenFenster;
    [SerializeField] Transform infoBBAusgeschaltet;
    public static bool tiegelLocked20, tiegelLocked40, tiegelLocked60, tiegelLocked80;
    [SerializeField] GameObject hinweisToolSchmelzen;

    // Use this for initialization
    void Start()
    {
        //-------------Bei Neustart des Spiels werden alle Variablen zurückgesetzt----------------
        hauptGasSchalter = false;
        platzGasSchalter = false;
        bBGasSchalter = false;
        waiting = false;
        instance = null;
        verbrannt = false;
        tiegelLocked20 = false;
        tiegelLocked40 = false;
        tiegelLocked60 = false;
        tiegelLocked80 = false;
        flamme1Bool = false;
        flamme2Bool = false;
        flamme3Bool = false;
        flamme4Bool = false;
        //----------------------------------------------------------------------------------------
        Vector3[] shuffleArray = new Vector3[bunsenBrennerTransf.Length];
        List<int> usedRnd = new List<int>();
        for (int i = 0; i < bunsenBrennerObjekt.Length; i++)
        {
            shuffleArray[i] = bunsenBrennerObjekt[i].transform.position;
        }
        //Mische die Bunsen Brenner, sodass die Studenten nicht schummeln können
        for (int i = 0; i < bunsenBrennerObjekt.Length; i++)
        {
            int rnd = UnityEngine.Random.Range(0, bunsenBrennerObjekt.Length);
            while (usedRnd.Contains(rnd))
            {
                rnd = UnityEngine.Random.Range(0, bunsenBrennerObjekt.Length);
            }
            usedRnd.Add(rnd);
            bunsenBrennerObjekt[rnd].transform.position = shuffleArray[i];
        }
        for (int i = 0; i < istTemp.Length; i++)
        {
            istTemp[i] = 25;
        }
    }

    // Update is called once per frame
    void Update()
    {
        #region Objekte auf Bunsenbrenner
        #region Tiegel
        if (slot1.transform.childCount > 0 && slot1.transform.GetChild(0).name.Contains("Tiegel"))
        {
            tiegelAufBB[0].SetActive(true);
        }
        else
        {
            tiegelAufBB[0].SetActive(false);
        }
        if (slot2.transform.childCount > 0 && slot2.transform.GetChild(0).name.Contains("Tiegel"))
        {
            tiegelAufBB[1].SetActive(true);
        }
        else
        {
            tiegelAufBB[1].SetActive(false);
        }
        if (slot3.transform.childCount > 0 && slot3.transform.GetChild(0).name.Contains("Tiegel"))
        {
            tiegelAufBB[2].SetActive(true);
        }
        else
        {
            tiegelAufBB[2].SetActive(false);
        }
        if (slot4.transform.childCount > 0 && slot4.transform.GetChild(0).name.Contains("Tiegel"))
        {
            tiegelAufBB[3].SetActive(true);
        }
        else
        {
            tiegelAufBB[3].SetActive(false);
        }
        #endregion
        #region Zahnrad
        if (slot1.transform.childCount > 0 && slot1.transform.GetChild(0).name.Contains("Zahnrad"))
        {
            zahnradAufBB[0].SetActive(true);
        }
        else
        {
            zahnradAufBB[0].SetActive(false);
        }
        if (slot2.transform.childCount > 0 && slot2.transform.GetChild(0).name.Contains("Zahnrad"))
        {
            zahnradAufBB[1].SetActive(true);
        }
        else
        {
            zahnradAufBB[1].SetActive(false);
        }
        if (slot3.transform.childCount > 0 && slot3.transform.GetChild(0).name.Contains("Zahnrad"))
        {
            zahnradAufBB[2].SetActive(true);
        }
        else
        {
            zahnradAufBB[2].SetActive(false);
        }
        if (slot4.transform.childCount > 0 && slot4.transform.GetChild(0).name.Contains("Zahnrad"))
        {
            zahnradAufBB[3].SetActive(true);
        }
        else
        {
            zahnradAufBB[3].SetActive(false);
        }
        #endregion
        #region Pleuel
        if (slot1.transform.childCount > 0 && slot1.transform.GetChild(0).name.Contains("Pleuel"))
        {
            pleuelAufBB[0].SetActive(true);
        }
        else
        {
            pleuelAufBB[0].SetActive(false);
        }
        if (slot2.transform.childCount > 0 && slot2.transform.GetChild(0).name.Contains("Pleuel"))
        {
            pleuelAufBB[1].SetActive(true);
        }
        else
        {
            pleuelAufBB[1].SetActive(false);
        }
        if (slot3.transform.childCount > 0 && slot3.transform.GetChild(0).name.Contains("Pleuel"))
        {
            pleuelAufBB[2].SetActive(true);
        }
        else
        {
            pleuelAufBB[2].SetActive(false);
        }
        if (slot4.transform.childCount > 0 && slot4.transform.GetChild(0).name.Contains("Pleuel"))
        {
            pleuelAufBB[3].SetActive(true);
        }
        else
        {
            pleuelAufBB[3].SetActive(false);
        }
        #endregion
        #region Wrench
        if (slot1.transform.childCount > 0 && slot1.transform.GetChild(0).name.Contains("Wrench"))
        {
            wrenchAufBB[0].SetActive(true);
        }
        else
        {
            wrenchAufBB[0].SetActive(false);
        }
        if (slot2.transform.childCount > 0 && slot2.transform.GetChild(0).name.Contains("Wrench"))
        {
            wrenchAufBB[1].SetActive(true);
        }
        else
        {
            wrenchAufBB[1].SetActive(false);
        }
        if (slot3.transform.childCount > 0 && slot3.transform.GetChild(0).name.Contains("Wrench"))
        {
            wrenchAufBB[2].SetActive(true);
        }
        else
        {
            wrenchAufBB[2].SetActive(false);
        }
        if (slot4.transform.childCount > 0 && slot4.transform.GetChild(0).name.Contains("Wrench"))
        {
            wrenchAufBB[3].SetActive(true);
        }
        else
        {
            wrenchAufBB[3].SetActive(false);
        }
        #endregion
        #endregion
        //CheckInstance();
        if (hauptGasSchalter && platzGasSchalter && waiting == false)
        {
            //time = DateTime.Now.Ticks;
            StartCoroutine(BunsenBrennerRechnung());
        }
        else if (hauptGasSchalter == false || platzGasSchalter == false)
        {
            waiting = false;
            flamme1.SetActive(false);
            BunsenBrennerFlammen[0].gameObject.SetActive(false);
            flamme2.SetActive(false);
            BunsenBrennerFlammen[1].gameObject.SetActive(false);
            flamme3.SetActive(false);
            BunsenBrennerFlammen[2].gameObject.SetActive(false);
            flamme4.SetActive(false);
            BunsenBrennerFlammen[3].gameObject.SetActive(false);
        }
        if (flamme1.activeInHierarchy == true && slot1.transform.childCount == 0 && zeigeVerbrennung == false || (flamme1.activeInHierarchy && slot1.transform.childCount > 0 && slot1.transform.GetChild(0).CompareTag("Empty") && zeigeVerbrennung == false))
        {
            zeigeVerbrennung = true;
            verbranntFenster.SetActive(true);
            verbrannt = true;
            flamme1.SetActive(false);
            flamme1Bool = false;
            BunsenBrennerFlammen[0].gameObject.SetActive(false);
        }
        else if (flamme2.activeInHierarchy == true && slot2.transform.childCount == 0 && zeigeVerbrennung == false || (flamme2.activeInHierarchy && slot2.transform.childCount > 0 && slot2.transform.GetChild(0).CompareTag("Empty") && zeigeVerbrennung == false))
        {
            zeigeVerbrennung = true;
            verbranntFenster.SetActive(true);
            verbrannt = true;
            flamme2.SetActive(false);
            flamme2Bool = false;
            BunsenBrennerFlammen[1].gameObject.SetActive(false);
        }
        else if (flamme3.activeInHierarchy == true && slot3.transform.childCount == 0 && zeigeVerbrennung == false || (flamme3.activeInHierarchy && slot3.transform.childCount > 0 && slot3.transform.GetChild(0).CompareTag("Empty") && zeigeVerbrennung == false))
        {
            zeigeVerbrennung = true;
            verbranntFenster.SetActive(true);
            verbrannt = true;
            flamme3.SetActive(false);
            flamme3Bool = false;
            BunsenBrennerFlammen[2].gameObject.SetActive(false);
        }
        else if (flamme4.activeInHierarchy == true && slot4.transform.childCount == 0 && zeigeVerbrennung == false || (flamme4.activeInHierarchy && slot4.transform.childCount > 0 && slot4.transform.GetChild(0).CompareTag("Empty") && zeigeVerbrennung == false))
        {
            zeigeVerbrennung = true;
            verbranntFenster.SetActive(true);
            verbrannt = true;
            flamme4.SetActive(false);
            flamme4Bool = false;
            BunsenBrennerFlammen[3].gameObject.SetActive(false);
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

    bool tiegel20Heating;
    public IEnumerator BunsenBrennerRechnung()
    {
        waiting = true;
        //long timetocool = time + new TimeSpan(0, 0, 0, 10, 0).Ticks;
        while (true)
        {
            //time = DateTime.Now.Ticks;
            yield return new WaitForSeconds(1);
            //if (istTemp[0] <= 900 && istTemp[0] >= 650)
            //{
            //    modifier = 0.0f;
            //}
            //else if (time > timetocool)
            //{
            //    modifier = 1.0f;
            //}
            #region Bunsen Brenner Abfrage
            //20% Si / 80%Ge || Wenn der Tiegel auf einem Bunsen Brenner liegt und die jeweilige Flamme an ist, erhitze den Tiegel
            #region 20% / 80%
            if (slot1.transform.childCount > 0 || slot2.transform.childCount > 0 || slot3.transform.childCount > 0 || slot4.transform.childCount > 0)
            {
                if (flamme1Bool && (slot1.transform.GetChild(0).CompareTag("20SiCold") || slot1.transform.GetChild(0).CompareTag("20SiHot")) || flamme2Bool && (slot2.transform.GetChild(0).CompareTag("20SiCold") || slot2.transform.GetChild(0).CompareTag("20SiHot")) || flamme3Bool && (slot3.transform.GetChild(0).CompareTag("20SiCold") || slot3.transform.GetChild(0).CompareTag("20SiHot")) || flamme4Bool && (slot4.transform.GetChild(0).CompareTag("20SiCold") || slot4.transform.GetChild(0).CompareTag("20SiHot")))
                {
                    tiegelLocked20 = true;
                    if (tiegel1Heated)
                    {
                        windowGraph.DeleteGraph();
                        tiegel1Heated = false;
                    }
                    if (istTemp[0] < BB1_Zieltemp[0])
                    {
                        istTemp[0] += BB1_Zieltemp[0] / BB1_Zeit[0];
                        tiegel1Heated = true;
                    }
                    else if (istTemp[0] < BB1_Zieltemp[1])
                    {
                        istTemp[0] += (BB1_Zieltemp[1] - BB1_Zieltemp[0]) / BB1_Zeit[1];
                        tiegel1Heated = true;
                    }
                    else if (istTemp[0] < BB1_Zieltemp[2])
                    {
                        istTemp[0] += (BB1_Zieltemp[2] - BB1_Zieltemp[1]) / BB1_Zeit[2];
                        tiegel1Heated = true;
                    }
                    //3ter Punkt im Graphen
                    else if (istTemp[0] >= BB1_Zieltemp[2] && graphPunkt4 == false && graphPunkt5 == false)
                    {
                        if (slot1.transform.childCount > 0 && slot1.transform.GetChild(0).CompareTag("20SiCold"))
                        {
                            tiegel1Heated = true;
                            slot1.transform.GetChild(0).tag = "20SiHot";
                            tiegelAufBB[0].gameObject.GetComponent<Renderer>().material = tiegelMat[0];
                            slot1.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[0];
                            slot1.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[0];
                        }
                        else if (slot2.transform.childCount > 0 && slot2.transform.GetChild(0).CompareTag("20SiCold"))
                        {
                            tiegel1Heated = true;
                            slot2.transform.GetChild(0).tag = "20SiHot";
                            tiegelAufBB[1].gameObject.GetComponent<Renderer>().material = tiegelMat[0];
                            slot2.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[0];
                            slot2.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[0];
                        }
                        else if (slot3.transform.childCount > 0 && slot3.transform.GetChild(0).CompareTag("20SiCold"))
                        {
                            tiegel1Heated = true;
                            slot3.transform.GetChild(0).tag = "20SiHot";
                            tiegelAufBB[2].gameObject.GetComponent<Renderer>().material = tiegelMat[0];
                            slot3.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[0];
                            slot3.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[0];
                        }
                        else if (slot4.transform.childCount > 0 && slot4.transform.GetChild(0).CompareTag("20SiCold"))
                        {
                            tiegel1Heated = true;
                            slot4.transform.GetChild(0).tag = "20SiHot";
                            tiegelAufBB[3].gameObject.GetComponent<Renderer>().material = tiegelMat[0];
                            slot4.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[0];
                            slot4.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[0];
                        }
                        istTemp[0] = BB1_Zieltemp[2];
                    }
                    else if (istTemp[0] < BB1_Zieltemp[3] && graphPunkt4 == true && graphPunkt5 == false)
                    {
                        istTemp[0] += (BB1_Zieltemp[3] - BB1_Zieltemp[2]) / BB1_Zeit[3];
                        tiegel1Heated = true;
                    }
                    //4ter Punkt im Graphen
                    else if (istTemp[0] >= BB1_Zieltemp[3] && graphPunkt4 == true && graphPunkt5 == false)
                    {
                        if (slot1.transform.childCount > 0 && slot1.transform.GetChild(0).CompareTag("20SiCold"))
                        {
                            tiegel1Heated = true;
                            slot1.transform.GetChild(0).tag = "20SiHot";
                            tiegelAufBB[0].gameObject.GetComponent<Renderer>().material = tiegelMat[0];
                            slot1.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[0];
                            slot1.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[0];
                        }
                        else if (slot2.transform.childCount > 0 && slot2.transform.GetChild(0).CompareTag("20SiCold"))
                        {
                            tiegel1Heated = true;
                            slot2.transform.GetChild(0).tag = "20SiHot";
                            tiegelAufBB[1].gameObject.GetComponent<Renderer>().material = tiegelMat[0];
                            slot2.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[0];
                            slot2.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[0];
                        }
                        else if (slot3.transform.childCount > 0 && slot3.transform.GetChild(0).CompareTag("20SiCold"))
                        {
                            tiegel1Heated = true;
                            slot3.transform.GetChild(0).tag = "20SiHot";
                            tiegelAufBB[2].gameObject.GetComponent<Renderer>().material = tiegelMat[0];
                            slot3.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[0];
                            slot3.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[0];
                        }
                        else if (slot4.transform.childCount > 0 && slot4.transform.GetChild(0).CompareTag("20SiCold"))
                        {
                            tiegel1Heated = true;
                            slot4.transform.GetChild(0).tag = "20SiHot";
                            tiegelAufBB[3].gameObject.GetComponent<Renderer>().material = tiegelMat[0];
                            slot4.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[0];
                            slot4.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[0];
                        }
                        istTemp[0] = BB1_Zieltemp[3];
                    }
                    else if (istTemp[0] < BB1_Zieltemp[4] && graphPunkt4 == true && graphPunkt5 == true)
                    {
                        istTemp[0] += (BB1_Zieltemp[4] - BB1_Zieltemp[3]) / BB1_Zeit[4];
                        tiegel1Heated = true;
                    }
                    //5ter Graph Punkt
                    else if (istTemp[0] >= BB1_Zieltemp[4] && graphPunkt4 == true && graphPunkt5 == true)
                    {
                        if (slot1.transform.childCount > 0 && slot1.transform.GetChild(0).CompareTag("20SiCold"))
                        {
                            tiegel1Heated = true;
                            slot1.transform.GetChild(0).tag = "20SiHot";
                            tiegelAufBB[0].gameObject.GetComponent<Renderer>().material = tiegelMat[0];
                            slot1.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[0];
                            slot1.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[0];
                        }
                        else if (slot2.transform.childCount > 0 && slot2.transform.GetChild(0).CompareTag("20SiCold"))
                        {
                            tiegel1Heated = true;
                            slot2.transform.GetChild(0).tag = "20SiHot";
                            tiegelAufBB[1].gameObject.GetComponent<Renderer>().material = tiegelMat[0];
                            slot2.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[0];
                            slot2.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[0];
                        }
                        else if (slot3.transform.childCount > 0 && slot3.transform.GetChild(0).CompareTag("20SiCold"))
                        {
                            tiegel1Heated = true;
                            slot3.transform.GetChild(0).tag = "20SiHot";
                            tiegelAufBB[2].gameObject.GetComponent<Renderer>().material = tiegelMat[0];
                            slot3.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[0];
                            slot3.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[0];
                        }
                        else if (slot4.transform.childCount > 0 && slot4.transform.GetChild(0).CompareTag("20SiCold"))
                        {
                            tiegel1Heated = true;
                            slot4.transform.GetChild(0).tag = "20SiHot";
                            tiegelAufBB[3].gameObject.GetComponent<Renderer>().material = tiegelMat[0];
                            slot4.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[0];
                            slot4.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[0];
                        }
                        istTemp[0] = BB1_Zieltemp[4];
                    }
                }
                //Sonst kühl das ganze mit gleichen Raten ab
                else if(flamme1Bool == false && slot1.transform.childCount > 0 && (slot1.transform.GetChild(0).CompareTag("20SiCold") || slot1.transform.GetChild(0).CompareTag("20SiHot")) || flamme2Bool == false && slot2.transform.childCount > 0 && (slot2.transform.GetChild(0).CompareTag("20SiCold") || slot2.transform.GetChild(0).CompareTag("20SiHot")) || flamme3Bool == false && slot3.transform.childCount > 0 && (slot3.transform.GetChild(0).CompareTag("20SiCold") || slot3.transform.GetChild(0).CompareTag("20SiHot")) || flamme4Bool == false && slot4.transform.childCount > 0 && (slot4.transform.GetChild(0).CompareTag("20SiCold") || slot4.transform.GetChild(0).CompareTag("20SiHot")))
                {
                    tiegelLocked20 = false;
                    if (istTemp[0] > 25 && istTemp[0] <= BB1_Zieltemp[0])
                    {
                        if (istTemp[0] >= 25 && istTemp[0] < 100)
                        {
                            if (slot1.transform.childCount > 0 && slot1.transform.GetChild(0).CompareTag("20SiHot"))
                            {
                                slot1.transform.GetChild(0).tag = "20SiCold";
                                tiegelAufBB[0].gameObject.GetComponent<Renderer>().material = tiegelMat[1];
                                slot1.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[1];
                                slot1.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[1];
                            }
                            else if (slot2.transform.childCount > 0 && slot2.transform.GetChild(0).CompareTag("20SiHot"))
                            {
                                slot2.transform.GetChild(0).tag = "20SiCold";
                                tiegelAufBB[1].gameObject.GetComponent<Renderer>().material = tiegelMat[1];
                                slot2.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[1];
                                slot2.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[1];
                            }
                            else if (slot3.transform.childCount > 0 && slot3.transform.GetChild(0).CompareTag("20SiHot"))
                            {
                                slot3.transform.GetChild(0).tag = "20SiCold";
                                tiegelAufBB[2].gameObject.GetComponent<Renderer>().material = tiegelMat[1];
                                slot3.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[1];
                                slot3.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[1];
                            }
                            else if (slot4.transform.childCount > 0 && slot4.transform.GetChild(0).CompareTag("20SiHot"))
                            {
                                slot4.transform.GetChild(0).tag = "20SiCold";
                                tiegelAufBB[3].gameObject.GetComponent<Renderer>().material = tiegelMat[1];
                                slot4.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[1];
                                slot4.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[1];
                            }
                        }
                        if (slot1.transform.childCount > 0 && (slot1.transform.GetChild(0).CompareTag("20SiHot") || slot1.transform.GetChild(0).CompareTag("20SiCold")))
                        {
                            tiegelFarbe = 20;
                        }
                        else if (slot2.transform.childCount > 0 && (slot2.transform.GetChild(0).CompareTag("20SiHot") || slot2.transform.GetChild(0).CompareTag("20SiCold")))
                        {
                            tiegelFarbe = 40;
                        }
                        else if (slot3.transform.childCount > 0 && (slot3.transform.GetChild(0).CompareTag("20SiHot") || slot3.transform.GetChild(0).CompareTag("20SiCold")))
                        {
                            tiegelFarbe = 60;
                        }
                        else if (slot4.transform.childCount > 0 && (slot4.transform.GetChild(0).CompareTag("20SiHot") || slot4.transform.GetChild(0).CompareTag("20SiCold")))
                        {
                            tiegelFarbe = 80;
                        }
                        windowGraph.ShowGraph(istTemp[0], 10, tiegelFarbe);
                        istTemp[0] -= BB1_Zieltemp[0] / BB1_Zeit[0];
                    }
                    else if (istTemp[0] > BB1_Zieltemp[0] && istTemp[0] <= BB1_Zieltemp[1])
                    {
                        if (slot1.transform.childCount > 0 && (slot1.transform.GetChild(0).CompareTag("20SiHot") || slot1.transform.GetChild(0).CompareTag("20SiCold")))
                        {
                            tiegelFarbe = 20;
                        }
                        else if (slot2.transform.childCount > 0 && (slot2.transform.GetChild(0).CompareTag("20SiHot") || slot2.transform.GetChild(0).CompareTag("20SiCold")))
                        {
                            tiegelFarbe = 40;
                        }
                        else if (slot3.transform.childCount > 0 && (slot3.transform.GetChild(0).CompareTag("20SiHot") || slot3.transform.GetChild(0).CompareTag("20SiCold")))
                        {
                            tiegelFarbe = 60;
                        }
                        else if (slot4.transform.childCount > 0 && (slot4.transform.GetChild(0).CompareTag("20SiHot") || slot4.transform.GetChild(0).CompareTag("20SiCold")))
                        {
                            tiegelFarbe = 80;
                        }
                        windowGraph.ShowGraph(istTemp[0], 10, tiegelFarbe);
                        istTemp[0] -= (BB1_Zieltemp[1] - BB1_Zieltemp[0]) / BB1_Zeit[1];
                    }
                    //3ter Graph Punkt
                    else if (istTemp[0] > BB1_Zieltemp[1] && graphPunkt4 == false && graphPunkt5 == false)
                    {
                        if (slot1.transform.childCount > 0 && (slot1.transform.GetChild(0).CompareTag("20SiHot") || slot1.transform.GetChild(0).CompareTag("20SiCold")))
                        {
                            tiegelFarbe = 20;
                        }
                        else if (slot2.transform.childCount > 0 && (slot2.transform.GetChild(0).CompareTag("20SiHot") || slot2.transform.GetChild(0).CompareTag("20SiCold")))
                        {
                            tiegelFarbe = 40;
                        }
                        else if (slot3.transform.childCount > 0 && (slot3.transform.GetChild(0).CompareTag("20SiHot") || slot3.transform.GetChild(0).CompareTag("20SiCold")))
                        {
                            tiegelFarbe = 60;
                        }
                        else if (slot4.transform.childCount > 0 && (slot4.transform.GetChild(0).CompareTag("20SiHot") || slot4.transform.GetChild(0).CompareTag("20SiCold")))
                        {
                            tiegelFarbe = 80;
                        }
                        windowGraph.ShowGraph(istTemp[0], 10, tiegelFarbe);
                        istTemp[0] -= (BB1_Zieltemp[2] - BB1_Zieltemp[1]) / BB1_Zeit[2];
                    }
                    else if (istTemp[0] > BB1_Zieltemp[1] && istTemp[0] <= BB1_Zieltemp[2] && graphPunkt4 == true && graphPunkt5 == false)
                    {
                        if (slot1.transform.childCount > 0 && (slot1.transform.GetChild(0).CompareTag("20SiHot") || slot1.transform.GetChild(0).CompareTag("20SiCold")))
                        {
                            tiegelFarbe = 20;
                        }
                        else if (slot2.transform.childCount > 0 && (slot2.transform.GetChild(0).CompareTag("20SiHot") || slot2.transform.GetChild(0).CompareTag("20SiCold")))
                        {
                            tiegelFarbe = 40;
                        }
                        else if (slot3.transform.childCount > 0 && (slot3.transform.GetChild(0).CompareTag("20SiHot") || slot3.transform.GetChild(0).CompareTag("20SiCold")))
                        {
                            tiegelFarbe = 60;
                        }
                        else if (slot4.transform.childCount > 0 && (slot4.transform.GetChild(0).CompareTag("20SiHot") || slot4.transform.GetChild(0).CompareTag("20SiCold")))
                        {
                            tiegelFarbe = 80;
                        }
                        windowGraph.ShowGraph(istTemp[0], 10, tiegelFarbe);
                        istTemp[0] -= (BB1_Zieltemp[2] - BB1_Zieltemp[1]) / BB1_Zeit[2];
                    }
                    //4ter Graph Punkt
                    else if (istTemp[0] > BB1_Zieltemp[2] && graphPunkt4 == true && graphPunkt5 == false)
                    {
                        if (slot1.transform.childCount > 0 && (slot1.transform.GetChild(0).CompareTag("20SiHot") || slot1.transform.GetChild(0).CompareTag("20SiCold")))
                        {
                            tiegelFarbe = 20;
                        }
                        else if (slot2.transform.childCount > 0 && (slot2.transform.GetChild(0).CompareTag("20SiHot") || slot2.transform.GetChild(0).CompareTag("20SiCold")))
                        {
                            tiegelFarbe = 40;
                        }
                        else if (slot3.transform.childCount > 0 && (slot3.transform.GetChild(0).CompareTag("20SiHot") || slot3.transform.GetChild(0).CompareTag("20SiCold")))
                        {
                            tiegelFarbe = 60;
                        }
                        else if (slot4.transform.childCount > 0 && (slot4.transform.GetChild(0).CompareTag("20SiHot") || slot4.transform.GetChild(0).CompareTag("20SiCold")))
                        {
                            tiegelFarbe = 80;
                        }
                        windowGraph.ShowGraph(istTemp[0], 10, tiegelFarbe);
                        istTemp[0] -= (BB1_Zieltemp[3] - BB1_Zieltemp[2]) / BB1_Zeit[3];
                    }
                    else if (istTemp[0] > BB1_Zieltemp[2] && istTemp[0] <= BB1_Zieltemp[3] && graphPunkt4 == true && graphPunkt5 == true)
                    {
                        if (slot1.transform.childCount > 0 && (slot1.transform.GetChild(0).CompareTag("20SiHot") || slot1.transform.GetChild(0).CompareTag("20SiCold")))
                        {
                            tiegelFarbe = 20;
                        }
                        else if (slot2.transform.childCount > 0 && (slot2.transform.GetChild(0).CompareTag("20SiHot") || slot2.transform.GetChild(0).CompareTag("20SiCold")))
                        {
                            tiegelFarbe = 40;
                        }
                        else if (slot3.transform.childCount > 0 && (slot3.transform.GetChild(0).CompareTag("20SiHot") || slot3.transform.GetChild(0).CompareTag("20SiCold")))
                        {
                            tiegelFarbe = 60;
                        }
                        else if (slot4.transform.childCount > 0 && (slot4.transform.GetChild(0).CompareTag("20SiHot") || slot4.transform.GetChild(0).CompareTag("20SiCold")))
                        {
                            tiegelFarbe = 80;
                        }
                        windowGraph.ShowGraph(istTemp[0], 10, tiegelFarbe);
                        istTemp[0] -= (BB1_Zieltemp[3] - BB1_Zieltemp[2]) / BB1_Zeit[3];
                    }
                    //5ter Graph Punkt
                    else if (istTemp[0] > BB1_Zieltemp[3] && graphPunkt4 == true && graphPunkt5 == true)
                    {
                        if (slot1.transform.childCount > 0 && (slot1.transform.GetChild(0).CompareTag("20SiHot") || slot1.transform.GetChild(0).CompareTag("20SiCold")))
                        {
                            tiegelFarbe = 20;
                        }
                        else if (slot2.transform.childCount > 0 && (slot2.transform.GetChild(0).CompareTag("20SiHot") || slot2.transform.GetChild(0).CompareTag("20SiCold")))
                        {
                            tiegelFarbe = 40;
                        }
                        else if (slot3.transform.childCount > 0 && (slot3.transform.GetChild(0).CompareTag("20SiHot") || slot3.transform.GetChild(0).CompareTag("20SiCold")))
                        {
                            tiegelFarbe = 60;
                        }
                        else if (slot4.transform.childCount > 0 && (slot4.transform.GetChild(0).CompareTag("20SiHot") || slot4.transform.GetChild(0).CompareTag("20SiCold")))
                        {
                            tiegelFarbe = 80;
                        }
                        windowGraph.ShowGraph(istTemp[0], 10, tiegelFarbe);
                        istTemp[0] -= (BB1_Zieltemp[4] - BB1_Zieltemp[3]) / BB1_Zeit[4];
                    }
                    else if (istTemp[0] < 25)
                    {
                        istTemp[0] = 25;
                    }
                }
                #endregion
                //40% Si / 60% Ge
                #region 40% / 60%
                if (flamme1Bool && (slot1.transform.GetChild(0).CompareTag("40SiCold") || slot1.transform.GetChild(0).CompareTag("40SiHot")) || flamme2Bool && (slot2.transform.GetChild(0).CompareTag("40SiCold") || slot2.transform.GetChild(0).CompareTag("40SiHot")) || flamme3Bool && (slot3.transform.GetChild(0).CompareTag("40SiCold") || slot3.transform.GetChild(0).CompareTag("40SiHot")) || flamme4Bool && (slot4.transform.GetChild(0).CompareTag("40SiCold") || slot4.transform.GetChild(0).CompareTag("40SiHot")))
                {
                    tiegelLocked40 = true;
                    if (tiegel2Heated)
                    {
                        windowGraphTiegel2.DeleteGraph();
                        tiegel2Heated = false;
                    }
                    if (istTemp[1] <= BB2_Zieltemp[0])
                    {
                        istTemp[1] += BB2_Zieltemp[0] / BB2_Zeit[0];
                        tiegel2Heated = true;
                    }
                    else if (istTemp[1] <= BB2_Zieltemp[1])
                    {
                        istTemp[1] += (BB2_Zieltemp[1] - BB2_Zieltemp[0]) / BB2_Zeit[1];
                        tiegel2Heated = true;
                    }
                    else if (istTemp[1] < BB2_Zieltemp[2])
                    {
                        istTemp[1] += (BB2_Zieltemp[2] - BB2_Zieltemp[1]) / BB2_Zeit[2];
                        tiegel2Heated = true;
                    }
                    //3ter Punkt im Graphen
                    else if (istTemp[1] >= BB2_Zieltemp[2] && graphPunkt4 == false && graphPunkt5 == false)
                    {
                        if (slot1.transform.childCount > 0 && slot1.transform.GetChild(0).CompareTag("40SiCold"))
                        {
                            tiegel2Heated = true;
                            slot1.transform.GetChild(0).tag = "40SiHot";
                            tiegelAufBB[0].gameObject.GetComponent<Renderer>().material = tiegelMat[0];
                            slot1.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[0];
                            slot1.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[0];
                        }
                        else if (slot2.transform.childCount > 0 && slot2.transform.GetChild(0).CompareTag("40SiCold"))
                        {
                            tiegel2Heated = true;
                            slot2.transform.GetChild(0).tag = "40SiHot";
                            tiegelAufBB[1].gameObject.GetComponent<Renderer>().material = tiegelMat[0];
                            slot2.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[0];
                            slot2.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[0];
                        }
                        else if (slot3.transform.childCount > 0 && slot3.transform.GetChild(0).CompareTag("40SiCold"))
                        {
                            tiegel2Heated = true;
                            slot3.transform.GetChild(0).tag = "40SiHot";
                            tiegelAufBB[2].gameObject.GetComponent<Renderer>().material = tiegelMat[0];
                            slot3.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[0];
                            slot3.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[0];
                        }
                        else if (slot4.transform.childCount > 0 && slot4.transform.GetChild(0).CompareTag("40SiCold"))
                        {
                            tiegel2Heated = true;
                            slot4.transform.GetChild(0).tag = "40SiHot";
                            tiegelAufBB[3].gameObject.GetComponent<Renderer>().material = tiegelMat[0];
                            slot4.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[0];
                            slot4.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[0];
                        }
                        istTemp[1] = BB2_Zieltemp[2];
                    }
                    else if (istTemp[1] < BB2_Zieltemp[3] && graphPunkt4 == true && graphPunkt5 == false)
                    {
                        istTemp[1] += (BB2_Zieltemp[3] - BB2_Zieltemp[2]) / BB2_Zeit[3];
                        tiegel2Heated = true;
                    }
                    //4ter Punkt im Graphen
                    else if (istTemp[1] >= BB2_Zieltemp[3] && graphPunkt4 == true && graphPunkt5 == false)
                    {
                        if (slot1.transform.childCount > 0 && slot1.transform.GetChild(0).CompareTag("40SiCold"))
                        {
                            tiegel2Heated = true;
                            slot1.transform.GetChild(0).tag = "40SiHot";
                            tiegelAufBB[0].gameObject.GetComponent<Renderer>().material = tiegelMat[0];
                            slot1.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[0];
                            slot1.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[0];
                        }
                        else if (slot2.transform.childCount > 0 && slot2.transform.GetChild(0).CompareTag("40SiCold"))
                        {
                            tiegel2Heated = true;
                            slot2.transform.GetChild(0).tag = "40SiHot";
                            tiegelAufBB[1].gameObject.GetComponent<Renderer>().material = tiegelMat[0];
                            slot2.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[0];
                            slot2.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[0];
                        }
                        else if (slot3.transform.childCount > 0 && slot3.transform.GetChild(0).CompareTag("40SiCold"))
                        {
                            tiegel2Heated = true;
                            slot3.transform.GetChild(0).tag = "40SiHot";
                            tiegelAufBB[2].gameObject.GetComponent<Renderer>().material = tiegelMat[0];
                            slot3.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[0];
                            slot3.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[0];
                        }
                        else if (slot4.transform.childCount > 0 && slot4.transform.GetChild(0).CompareTag("40SiCold"))
                        {
                            tiegel2Heated = true;
                            slot4.transform.GetChild(0).tag = "40SiHot";
                            tiegelAufBB[3].gameObject.GetComponent<Renderer>().material = tiegelMat[0];
                            slot4.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[0];
                            slot4.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[0];
                        }
                        istTemp[1] = BB2_Zieltemp[3];
                    }
                    else if (istTemp[1] < BB2_Zieltemp[4] && graphPunkt4 == true && graphPunkt5 == true)
                    {
                        istTemp[1] += (BB2_Zieltemp[4] - BB2_Zieltemp[3]) / BB2_Zeit[4];
                        tiegel2Heated = true;
                    }
                    //5ter Graph Punkt
                    else if (istTemp[1] >= BB2_Zieltemp[4] && graphPunkt4 == true && graphPunkt5 == true)
                    {
                        if (slot1.transform.childCount > 0 && slot1.transform.GetChild(0).CompareTag("40SiCold"))
                        {
                            tiegel2Heated = true;
                            slot1.transform.GetChild(0).tag = "40SiHot";
                            tiegelAufBB[0].gameObject.GetComponent<Renderer>().material = tiegelMat[0];
                            slot1.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[0];
                            slot1.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[0];
                        }
                        else if (slot2.transform.childCount > 0 && slot2.transform.GetChild(0).CompareTag("40SiCold"))
                        {
                            tiegel2Heated = true;
                            slot2.transform.GetChild(0).tag = "40SiHot";
                            tiegelAufBB[1].gameObject.GetComponent<Renderer>().material = tiegelMat[0];
                            slot2.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[0];
                            slot2.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[0];
                        }
                        else if (slot3.transform.childCount > 0 && slot3.transform.GetChild(0).CompareTag("40SiCold"))
                        {
                            tiegel2Heated = true;
                            slot3.transform.GetChild(0).tag = "40SiHot";
                            tiegelAufBB[2].gameObject.GetComponent<Renderer>().material = tiegelMat[0];
                            slot3.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[0];
                            slot3.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[0];
                        }
                        else if (slot4.transform.childCount > 0 && slot4.transform.GetChild(0).CompareTag("40SiCold"))
                        {
                            tiegel2Heated = true;
                            slot4.transform.GetChild(0).tag = "40SiHot";
                            tiegelAufBB[3].gameObject.GetComponent<Renderer>().material = tiegelMat[0];
                            slot4.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[0];
                            slot4.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[0];
                        }
                        istTemp[1] = BB2_Zieltemp[4];
                    }
                }
                else if(flamme1Bool == false && slot1.transform.childCount > 0 && (slot1.transform.GetChild(0).CompareTag("40SiCold") || slot1.transform.GetChild(0).CompareTag("40SiHot")) || flamme2Bool == false && slot2.transform.childCount > 0 && (slot2.transform.GetChild(0).CompareTag("40SiCold") || slot2.transform.GetChild(0).CompareTag("40SiHot")) || flamme3Bool == false && slot3.transform.childCount > 0 && (slot3.transform.GetChild(0).CompareTag("40SiCold") || slot3.transform.GetChild(0).CompareTag("40SiHot")) || flamme4Bool == false && slot4.transform.childCount > 0 && (slot4.transform.GetChild(0).CompareTag("40SiCold") || slot4.transform.GetChild(0).CompareTag("40SiHot")))
                {
                    tiegelLocked40 = false;
                    if (istTemp[1] > 25 && istTemp[1] <= BB2_Zieltemp[0])
                    {
                        if (istTemp[1] >= 25 && istTemp[1] < 100)
                        {
                            if (slot1.transform.childCount > 0 && slot1.transform.GetChild(0).CompareTag("40SiHot"))
                            {
                                slot1.transform.GetChild(0).tag = "40SiCold";
                                tiegelAufBB[0].gameObject.GetComponent<Renderer>().material = tiegelMat[1];
                                slot1.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[1];
                                slot1.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[1];
                            }
                            else if (slot2.transform.childCount > 0 && slot2.transform.GetChild(0).CompareTag("40SiHot"))
                            {
                                slot2.transform.GetChild(0).tag = "40SiCold";
                                tiegelAufBB[1].gameObject.GetComponent<Renderer>().material = tiegelMat[1];
                                slot2.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[1];
                                slot2.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[1];
                            }
                            else if (slot3.transform.childCount > 0 && slot3.transform.GetChild(0).CompareTag("40SiHot"))
                            {
                                slot3.transform.GetChild(0).tag = "40SiCold";
                                tiegelAufBB[2].gameObject.GetComponent<Renderer>().material = tiegelMat[1];
                                slot3.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[1];
                                slot3.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[1];
                            }
                            else if (slot4.transform.childCount > 0 && slot4.transform.GetChild(0).CompareTag("40SiHot"))
                            {
                                slot4.transform.GetChild(0).tag = "40SiCold";
                                tiegelAufBB[3].gameObject.GetComponent<Renderer>().material = tiegelMat[1];
                                slot4.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[1];
                                slot4.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[1];
                            }
                        }
                        if (slot1.transform.childCount > 0 && (slot1.transform.GetChild(0).CompareTag("40SiHot") || slot1.transform.GetChild(0).CompareTag("40SiCold")))
                        {
                            tiegel2Farbe = 20;
                        }
                        else if (slot2.transform.childCount > 0 && (slot2.transform.GetChild(0).CompareTag("40SiHot") || slot2.transform.GetChild(0).CompareTag("40SiCold")))
                        {
                            tiegel2Farbe = 40;
                        }
                        else if (slot3.transform.childCount > 0 && (slot3.transform.GetChild(0).CompareTag("40SiHot") || slot3.transform.GetChild(0).CompareTag("40SiCold")))
                        {
                            tiegel2Farbe = 60;
                        }
                        else if (slot4.transform.childCount > 0 && (slot4.transform.GetChild(0).CompareTag("40SiHot") || slot4.transform.GetChild(0).CompareTag("40SiCold")))
                        {
                            tiegel2Farbe = 80;
                        }
                        windowGraphTiegel2.ShowGraph(istTemp[1], 10, tiegel2Farbe);
                        istTemp[1] -= BB2_Zieltemp[0] / BB2_Zeit[0];
                    }
                    else if (istTemp[1] > BB2_Zieltemp[0] && istTemp[1] <= BB2_Zieltemp[1])
                    {
                        if (slot1.transform.childCount > 0 && (slot1.transform.GetChild(0).CompareTag("40SiHot") || slot1.transform.GetChild(0).CompareTag("40SiCold")))
                        {
                            tiegel2Farbe = 20;
                        }
                        else if (slot2.transform.childCount > 0 && (slot2.transform.GetChild(0).CompareTag("40SiHot") || slot2.transform.GetChild(0).CompareTag("40SiCold")))
                        {
                            tiegel2Farbe = 40;
                        }
                        else if (slot3.transform.childCount > 0 && (slot3.transform.GetChild(0).CompareTag("40SiHot") || slot3.transform.GetChild(0).CompareTag("40SiCold")))
                        {
                            tiegel2Farbe = 60;
                        }
                        else if (slot4.transform.childCount > 0 && (slot4.transform.GetChild(0).CompareTag("40SiHot") || slot4.transform.GetChild(0).CompareTag("40SiCold")))
                        {
                            tiegel2Farbe = 80;
                        }
                        windowGraphTiegel2.ShowGraph(istTemp[1], 10, tiegel2Farbe);
                        istTemp[1] -= (BB2_Zieltemp[1] - BB2_Zieltemp[0]) / BB2_Zeit[1];
                    }
                    //3ter Graph Punkt
                    else if (istTemp[1] > BB2_Zieltemp[1] && graphPunkt4 == false && graphPunkt5 == false)
                    {
                        if (slot1.transform.childCount > 0 && (slot1.transform.GetChild(0).CompareTag("40SiHot") || slot1.transform.GetChild(0).CompareTag("40SiCold")))
                        {
                            tiegel2Farbe = 20;
                        }
                        else if (slot2.transform.childCount > 0 && (slot2.transform.GetChild(0).CompareTag("40SiHot") || slot2.transform.GetChild(0).CompareTag("40SiCold")))
                        {
                            tiegel2Farbe = 40;
                        }
                        else if (slot3.transform.childCount > 0 && (slot3.transform.GetChild(0).CompareTag("40SiHot") || slot3.transform.GetChild(0).CompareTag("40SiCold")))
                        {
                            tiegel2Farbe = 60;
                        }
                        else if (slot4.transform.childCount > 0 && (slot4.transform.GetChild(0).CompareTag("40SiHot") || slot4.transform.GetChild(0).CompareTag("40SiCold")))
                        {
                            tiegel2Farbe = 80;
                        }
                        windowGraphTiegel2.ShowGraph(istTemp[1], 10, tiegel2Farbe);
                        istTemp[1] -= (BB2_Zieltemp[2] - BB2_Zieltemp[1]) / BB2_Zeit[2];
                    }
                    else if (istTemp[1] > BB2_Zieltemp[1] && istTemp[1] <= BB2_Zieltemp[2] && graphPunkt4 == true && graphPunkt5 == false)
                    {
                        if (slot1.transform.childCount > 0 && (slot1.transform.GetChild(0).CompareTag("40SiHot") || slot1.transform.GetChild(0).CompareTag("40SiCold")))
                        {
                            tiegel2Farbe = 20;
                        }
                        else if (slot2.transform.childCount > 0 && (slot2.transform.GetChild(0).CompareTag("40SiHot") || slot2.transform.GetChild(0).CompareTag("40SiCold")))
                        {
                            tiegel2Farbe = 40;
                        }
                        else if (slot3.transform.childCount > 0 && (slot3.transform.GetChild(0).CompareTag("40SiHot") || slot3.transform.GetChild(0).CompareTag("40SiCold")))
                        {
                            tiegel2Farbe = 60;
                        }
                        else if (slot4.transform.childCount > 0 && (slot4.transform.GetChild(0).CompareTag("40SiHot") || slot4.transform.GetChild(0).CompareTag("40SiCold")))
                        {
                            tiegel2Farbe = 80;
                        }
                        windowGraphTiegel2.ShowGraph(istTemp[1], 10, tiegel2Farbe);
                        istTemp[1] -= (BB2_Zieltemp[2] - BB2_Zieltemp[1]) / BB2_Zeit[2];
                    }
                    //4ter Graph Punkt
                    else if (istTemp[1] > BB2_Zieltemp[2] && graphPunkt4 == true && graphPunkt5 == false)
                    {
                        if (slot1.transform.childCount > 0 && (slot1.transform.GetChild(0).CompareTag("40SiHot") || slot1.transform.GetChild(0).CompareTag("40SiCold")))
                        {
                            tiegel2Farbe = 20;
                        }
                        else if (slot2.transform.childCount > 0 && (slot2.transform.GetChild(0).CompareTag("40SiHot") || slot2.transform.GetChild(0).CompareTag("40SiCold")))
                        {
                            tiegel2Farbe = 40;
                        }
                        else if (slot3.transform.childCount > 0 && (slot3.transform.GetChild(0).CompareTag("40SiHot") || slot3.transform.GetChild(0).CompareTag("40SiCold")))
                        {
                            tiegel2Farbe = 60;
                        }
                        else if (slot4.transform.childCount > 0 && (slot4.transform.GetChild(0).CompareTag("40SiHot") || slot4.transform.GetChild(0).CompareTag("40SiCold")))
                        {
                            tiegel2Farbe = 80;
                        }
                        windowGraphTiegel2.ShowGraph(istTemp[1], 10, tiegel2Farbe);
                        istTemp[1] -= (BB2_Zieltemp[3] - BB2_Zieltemp[2]) / BB2_Zeit[3];
                    }
                    else if (istTemp[1] > BB2_Zieltemp[2] && istTemp[1] <= BB2_Zieltemp[3] && graphPunkt4 == true && graphPunkt5 == true)
                    {
                        if (slot1.transform.childCount > 0 && (slot1.transform.GetChild(0).CompareTag("40SiHot") || slot1.transform.GetChild(0).CompareTag("40SiCold")))
                        {
                            tiegel2Farbe = 20;
                        }
                        else if (slot2.transform.childCount > 0 && (slot2.transform.GetChild(0).CompareTag("40SiHot") || slot2.transform.GetChild(0).CompareTag("40SiCold")))
                        {
                            tiegel2Farbe = 40;
                        }
                        else if (slot3.transform.childCount > 0 && (slot3.transform.GetChild(0).CompareTag("40SiHot") || slot3.transform.GetChild(0).CompareTag("40SiCold")))
                        {
                            tiegel2Farbe = 60;
                        }
                        else if (slot4.transform.childCount > 0 && (slot4.transform.GetChild(0).CompareTag("40SiHot") || slot4.transform.GetChild(0).CompareTag("40SiCold")))
                        {
                            tiegel2Farbe = 80;
                        }
                        windowGraphTiegel2.ShowGraph(istTemp[1], 10, tiegel2Farbe);
                        istTemp[1] -= (BB2_Zieltemp[3] - BB2_Zieltemp[2]) / BB2_Zeit[3];
                    }
                    //5ter Graph Punkt
                    else if (istTemp[1] > BB2_Zieltemp[4] && graphPunkt4 == true && graphPunkt5 == true)
                    {
                        if (slot1.transform.childCount > 0 && (slot1.transform.GetChild(0).CompareTag("40SiHot") || slot1.transform.GetChild(0).CompareTag("40SiCold")))
                        {
                            tiegel2Farbe = 20;
                        }
                        else if (slot2.transform.childCount > 0 && (slot2.transform.GetChild(0).CompareTag("40SiHot") || slot2.transform.GetChild(0).CompareTag("40SiCold")))
                        {
                            tiegel2Farbe = 40;
                        }
                        else if (slot3.transform.childCount > 0 && (slot3.transform.GetChild(0).CompareTag("40SiHot") || slot3.transform.GetChild(0).CompareTag("40SiCold")))
                        {
                            tiegel2Farbe = 60;
                        }
                        else if (slot4.transform.childCount > 0 && (slot4.transform.GetChild(0).CompareTag("40SiHot") || slot4.transform.GetChild(0).CompareTag("40SiCold")))
                        {
                            tiegel2Farbe = 80;
                        }
                        windowGraphTiegel2.ShowGraph(istTemp[1], 10, tiegel2Farbe);
                        istTemp[1] -= (BB2_Zieltemp[4] - BB2_Zieltemp[3]) / BB2_Zeit[4];
                    }
                    else if (istTemp[1] < 25)
                    {
                        istTemp[1] = 25;
                    }
                }
                #endregion
                //60% Si / 40% Ge
                #region 60% / 40%
                if (flamme1Bool && (slot1.transform.GetChild(0).CompareTag("60SiCold") || slot1.transform.GetChild(0).CompareTag("60SiHot")) || flamme2Bool && (slot2.transform.GetChild(0).CompareTag("60SiCold") || slot2.transform.GetChild(0).CompareTag("60SiHot")) || flamme3Bool && (slot3.transform.GetChild(0).CompareTag("60SiCold") || slot3.transform.GetChild(0).CompareTag("60SiHot")) || flamme4Bool && (slot4.transform.GetChild(0).CompareTag("60SiCold") || slot4.transform.GetChild(0).CompareTag("60SiHot")))
                {
                    tiegelLocked60 = true;
                    if (tiegel3Heated)
                    {
                        windowGraphTiegel3.DeleteGraph();
                        tiegel3Heated = false;
                    }
                    if (istTemp[2] <= BB3_Zieltemp[0])
                    {
                        istTemp[2] += BB3_Zieltemp[0] / BB3_Zeit[0];
                        tiegel3Heated = true;
                    }
                    else if (istTemp[2] <= BB3_Zieltemp[1])
                    {
                        istTemp[2] += (BB3_Zieltemp[1] - BB3_Zieltemp[0]) / BB3_Zeit[1];
                        tiegel3Heated = true;
                    }
                    else if (istTemp[2] < BB3_Zieltemp[2])
                    {
                        istTemp[2] += (BB3_Zieltemp[2] - BB3_Zieltemp[1]) / BB3_Zeit[2];
                        tiegel3Heated = true;
                    }
                    //3ter Punkt im Graphen
                    else if (istTemp[2] >= BB3_Zieltemp[2] && graphPunkt4 == false && graphPunkt5 == false)
                    {
                        if (slot1.transform.childCount > 0 && slot1.transform.GetChild(0).CompareTag("60SiCold"))
                        {
                            tiegel1Heated = true;
                            slot1.transform.GetChild(0).tag = "60SiHot";
                            tiegelAufBB[0].gameObject.GetComponent<Renderer>().material = tiegelMat[0];
                            slot1.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[0];
                            slot1.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[0];
                        }
                        else if (slot2.transform.childCount > 0 && slot2.transform.GetChild(0).CompareTag("60SiCold"))
                        {
                            tiegel1Heated = true;
                            slot2.transform.GetChild(0).tag = "60SiHot";
                            tiegelAufBB[1].gameObject.GetComponent<Renderer>().material = tiegelMat[0];
                            slot2.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[0];
                            slot2.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[0];
                        }
                        else if (slot3.transform.childCount > 0 && slot3.transform.GetChild(0).CompareTag("60SiCold"))
                        {
                            tiegel1Heated = true;
                            slot3.transform.GetChild(0).tag = "60SiHot";
                            tiegelAufBB[2].gameObject.GetComponent<Renderer>().material = tiegelMat[0];
                            slot3.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[0];
                            slot3.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[0];
                        }
                        else if (slot4.transform.childCount > 0 && slot4.transform.GetChild(0).CompareTag("60SiCold"))
                        {
                            tiegel1Heated = true;
                            slot4.transform.GetChild(0).tag = "60SiHot";
                            tiegelAufBB[3].gameObject.GetComponent<Renderer>().material = tiegelMat[0];
                            slot4.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[0];
                            slot4.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[0];
                        }
                        istTemp[2] = BB3_Zieltemp[2];
                    }
                    else if (istTemp[2] < BB3_Zieltemp[3] && graphPunkt4 == true && graphPunkt5 == false)
                    {
                        istTemp[2] += (BB3_Zieltemp[3] - BB3_Zieltemp[2]) / BB3_Zeit[3];
                        tiegel3Heated = true;
                    }
                    //4ter Punkt im Graphen
                    else if (istTemp[2] >= BB3_Zieltemp[3] && graphPunkt4 == true && graphPunkt5 == false)
                    {
                        if (slot1.transform.childCount > 0 && slot1.transform.GetChild(0).CompareTag("60SiCold"))
                        {
                            tiegel3Heated = true;
                            slot1.transform.GetChild(0).tag = "60SiHot";
                            tiegelAufBB[0].gameObject.GetComponent<Renderer>().material = tiegelMat[0];
                            slot1.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[0];
                            slot1.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[0];
                        }
                        else if (slot2.transform.childCount > 0 && slot2.transform.GetChild(0).CompareTag("60SiCold"))
                        {
                            tiegel3Heated = true;
                            slot2.transform.GetChild(0).tag = "60SiHot";
                            tiegelAufBB[1].gameObject.GetComponent<Renderer>().material = tiegelMat[0];
                            slot2.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[0];
                            slot2.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[0];
                        }
                        else if (slot3.transform.childCount > 0 && slot3.transform.GetChild(0).CompareTag("60SiCold"))
                        {
                            tiegel3Heated = true;
                            slot3.transform.GetChild(0).tag = "60SiHot";
                            tiegelAufBB[2].gameObject.GetComponent<Renderer>().material = tiegelMat[0];
                            slot3.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[0];
                            slot3.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[0];
                        }
                        else if (slot4.transform.childCount > 0 && slot4.transform.GetChild(0).CompareTag("60SiCold"))
                        {
                            tiegel3Heated = true;
                            slot4.transform.GetChild(0).tag = "60SiHot";
                            tiegelAufBB[3].gameObject.GetComponent<Renderer>().material = tiegelMat[0];
                            slot4.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[0];
                            slot4.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[0];
                        }
                        istTemp[2] = BB3_Zieltemp[3];
                    }
                    else if (istTemp[2] < BB3_Zieltemp[4] && graphPunkt4 == true && graphPunkt5 == true)
                    {
                        istTemp[2] += (BB3_Zieltemp[4] - BB3_Zieltemp[3]) / BB3_Zeit[4];
                        tiegel3Heated = true;
                    }
                    //5ter Graph Punkt
                    else if (istTemp[2] >= BB3_Zieltemp[4] && graphPunkt4 == true && graphPunkt5 == true)
                    {
                        if (slot1.transform.childCount > 0 && slot1.transform.GetChild(0).CompareTag("60SiCold"))
                        {
                            tiegel3Heated = true;
                            slot1.transform.GetChild(0).tag = "60SiHot";
                            tiegelAufBB[0].gameObject.GetComponent<Renderer>().material = tiegelMat[0];
                            slot1.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[0];
                            slot1.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[0];
                        }
                        else if (slot2.transform.childCount > 0 && slot2.transform.GetChild(0).CompareTag("60SiCold"))
                        {
                            tiegel3Heated = true;
                            slot2.transform.GetChild(0).tag = "60SiHot";
                            tiegelAufBB[1].gameObject.GetComponent<Renderer>().material = tiegelMat[0];
                            slot2.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[0];
                            slot2.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[0];
                        }
                        else if (slot3.transform.childCount > 0 && slot3.transform.GetChild(0).CompareTag("60SiCold"))
                        {
                            tiegel3Heated = true;
                            slot3.transform.GetChild(0).tag = "60SiHot";
                            tiegelAufBB[2].gameObject.GetComponent<Renderer>().material = tiegelMat[0];
                            slot3.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[0];
                            slot3.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[0];
                        }
                        else if (slot4.transform.childCount > 0 && slot4.transform.GetChild(0).CompareTag("60SiCold"))
                        {
                            tiegel3Heated = true;
                            slot4.transform.GetChild(0).tag = "60SiHot";
                            tiegelAufBB[3].gameObject.GetComponent<Renderer>().material = tiegelMat[0];
                            slot4.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[0];
                            slot4.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[0];
                        }
                        istTemp[2] = BB3_Zieltemp[4];
                    }
                }
                else if(flamme1Bool == false && slot1.transform.childCount > 0 && (slot1.transform.GetChild(0).CompareTag("60SiCold") || slot1.transform.GetChild(0).CompareTag("60SiHot")) || flamme2Bool == false && slot2.transform.childCount > 0 && (slot2.transform.GetChild(0).CompareTag("60SiCold") || slot2.transform.GetChild(0).CompareTag("60SiHot")) || flamme3Bool == false && slot3.transform.childCount > 0 && (slot3.transform.GetChild(0).CompareTag("60SiCold") || slot3.transform.GetChild(0).CompareTag("60SiHot")) || flamme4Bool == false && slot4.transform.childCount > 0 && (slot4.transform.GetChild(0).CompareTag("60SiCold") || slot4.transform.GetChild(0).CompareTag("60SiHot")))
                {
                    tiegelLocked60 = false;
                    if (istTemp[2] > 25 && istTemp[2] <= BB3_Zieltemp[0])
                    {
                        if (istTemp[2] >= 25 && istTemp[2] < 100)
                        {
                            if (slot1.transform.childCount > 0 && slot1.transform.GetChild(0).CompareTag("60SiHot"))
                            {
                                slot1.transform.GetChild(0).tag = "60SiCold";
                                tiegelAufBB[0].gameObject.GetComponent<Renderer>().material = tiegelMat[1];
                                slot1.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[1];
                                slot1.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[1];
                            }
                            else if (slot2.transform.childCount > 0 && slot2.transform.GetChild(0).CompareTag("60SiHot"))
                            {
                                slot2.transform.GetChild(0).tag = "60SiCold";
                                tiegelAufBB[1].gameObject.GetComponent<Renderer>().material = tiegelMat[1];
                                slot2.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[1];
                                slot2.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[1];
                            }
                            else if (slot3.transform.childCount > 0 && slot3.transform.GetChild(0).CompareTag("60SiHot"))
                            {
                                slot3.transform.GetChild(0).tag = "60SiCold";
                                tiegelAufBB[2].gameObject.GetComponent<Renderer>().material = tiegelMat[1];
                                slot3.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[1];
                                slot3.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[1];
                            }
                            else if (slot4.transform.childCount > 0 && slot4.transform.GetChild(0).CompareTag("60SiHot"))
                            {
                                slot4.transform.GetChild(0).tag = "60SiCold";
                                tiegelAufBB[3].gameObject.GetComponent<Renderer>().material = tiegelMat[1];
                                slot4.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[1];
                                slot4.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[1];
                            }
                        }
                        if (slot1.transform.childCount > 0 && (slot1.transform.GetChild(0).CompareTag("60SiHot") || slot1.transform.GetChild(0).CompareTag("60SiCold")))
                        {
                            tiegel3Farbe = 20;
                        }
                        else if (slot2.transform.childCount > 0 && (slot2.transform.GetChild(0).CompareTag("60SiHot") || slot2.transform.GetChild(0).CompareTag("60SiCold")))
                        {
                            tiegel3Farbe = 40;
                        }
                        else if (slot3.transform.childCount > 0 && (slot3.transform.GetChild(0).CompareTag("60SiHot") || slot3.transform.GetChild(0).CompareTag("60SiCold")))
                        {
                            tiegel3Farbe = 60;
                        }
                        else if (slot4.transform.childCount > 0 && (slot4.transform.GetChild(0).CompareTag("60SiHot") || slot4.transform.GetChild(0).CompareTag("60SiCold")))
                        {
                            tiegel3Farbe = 80;
                        }
                        windowGraphTiegel3.ShowGraph(istTemp[2], 10, tiegel3Farbe);
                        istTemp[2] -= BB3_Zieltemp[0] / BB3_Zeit[0];
                    }
                    else if (istTemp[2] > BB3_Zieltemp[0] && istTemp[2] <= BB3_Zieltemp[1])
                    {
                        if (slot1.transform.childCount > 0 && (slot1.transform.GetChild(0).CompareTag("60SiHot") || slot1.transform.GetChild(0).CompareTag("60SiCold")))
                        {
                            tiegel3Farbe = 20;
                        }
                        else if (slot2.transform.childCount > 0 && (slot2.transform.GetChild(0).CompareTag("60SiHot") || slot2.transform.GetChild(0).CompareTag("60SiCold")))
                        {
                            tiegel3Farbe = 40;
                        }
                        else if (slot3.transform.childCount > 0 && (slot3.transform.GetChild(0).CompareTag("60SiHot") || slot3.transform.GetChild(0).CompareTag("60SiCold")))
                        {
                            tiegel3Farbe = 60;
                        }
                        else if (slot4.transform.childCount > 0 && (slot4.transform.GetChild(0).CompareTag("60SiHot") || slot4.transform.GetChild(0).CompareTag("60SiCold")))
                        {
                            tiegel3Farbe = 80;
                        }
                        windowGraphTiegel3.ShowGraph(istTemp[2], 10, tiegel3Farbe);
                        istTemp[2] -= (BB3_Zieltemp[1] - BB3_Zieltemp[0]) / BB3_Zeit[1];
                    }
                    //3ter Graph Punkt
                    else if (istTemp[2] > BB3_Zieltemp[1] && graphPunkt4 == false && graphPunkt5 == false)
                    {
                        if (slot1.transform.childCount > 0 && (slot1.transform.GetChild(0).CompareTag("60SiHot") || slot1.transform.GetChild(0).CompareTag("60SiCold")))
                        {
                            tiegel3Farbe = 20;
                        }
                        else if (slot2.transform.childCount > 0 && (slot2.transform.GetChild(0).CompareTag("60SiHot") || slot2.transform.GetChild(0).CompareTag("60SiCold")))
                        {
                            tiegel3Farbe = 40;
                        }
                        else if (slot3.transform.childCount > 0 && (slot3.transform.GetChild(0).CompareTag("60SiHot") || slot3.transform.GetChild(0).CompareTag("60SiCold")))
                        {
                            tiegel3Farbe = 60;
                        }
                        else if (slot4.transform.childCount > 0 && (slot4.transform.GetChild(0).CompareTag("60SiHot") || slot4.transform.GetChild(0).CompareTag("60SiCold")))
                        {
                            tiegel3Farbe = 80;
                        }
                        windowGraphTiegel3.ShowGraph(istTemp[2], 10, tiegel3Farbe);
                        istTemp[2] -= (BB3_Zieltemp[2] - BB3_Zieltemp[1]) / BB3_Zeit[2];
                    }
                    else if (istTemp[2] > BB3_Zieltemp[1] && istTemp[2] <= BB3_Zieltemp[2] && graphPunkt4 == true && graphPunkt5 == false)
                    {
                        if (slot1.transform.childCount > 0 && (slot1.transform.GetChild(0).CompareTag("60SiHot") || slot1.transform.GetChild(0).CompareTag("60SiCold")))
                        {
                            tiegel3Farbe = 20;
                        }
                        else if (slot2.transform.childCount > 0 && (slot2.transform.GetChild(0).CompareTag("60SiHot") || slot2.transform.GetChild(0).CompareTag("60SiCold")))
                        {
                            tiegel3Farbe = 40;
                        }
                        else if (slot3.transform.childCount > 0 && (slot3.transform.GetChild(0).CompareTag("60SiHot") || slot3.transform.GetChild(0).CompareTag("60SiCold")))
                        {
                            tiegel3Farbe = 60;
                        }
                        else if (slot4.transform.childCount > 0 && (slot4.transform.GetChild(0).CompareTag("60SiHot") || slot4.transform.GetChild(0).CompareTag("60SiCold")))
                        {
                            tiegel3Farbe = 80;
                        }
                        windowGraphTiegel3.ShowGraph(istTemp[2], 10, tiegel3Farbe);
                        istTemp[2] -= (BB3_Zieltemp[2] - BB3_Zieltemp[1]) / BB3_Zeit[2];
                    }
                    //4ter Graph Punkt
                    else if (istTemp[2] > BB3_Zieltemp[2] && graphPunkt4 == true && graphPunkt5 == false)
                    {
                        if (slot1.transform.childCount > 0 && (slot1.transform.GetChild(0).CompareTag("60SiHot") || slot1.transform.GetChild(0).CompareTag("60SiCold")))
                        {
                            tiegel3Farbe = 20;
                        }
                        else if (slot2.transform.childCount > 0 && (slot2.transform.GetChild(0).CompareTag("60SiHot") || slot2.transform.GetChild(0).CompareTag("60SiCold")))
                        {
                            tiegel3Farbe = 40;
                        }
                        else if (slot3.transform.childCount > 0 && (slot3.transform.GetChild(0).CompareTag("60SiHot") || slot3.transform.GetChild(0).CompareTag("60SiCold")))
                        {
                            tiegel3Farbe = 60;
                        }
                        else if (slot4.transform.childCount > 0 && (slot4.transform.GetChild(0).CompareTag("60SiHot") || slot4.transform.GetChild(0).CompareTag("60SiCold")))
                        {
                            tiegel3Farbe = 80;
                        }
                        windowGraphTiegel3.ShowGraph(istTemp[2], 10, tiegel3Farbe);
                        istTemp[2] -= (BB3_Zieltemp[2] - BB3_Zieltemp[1]) / BB3_Zeit[2];
                    }
                    else if (istTemp[2] > BB3_Zieltemp[2] && istTemp[2] <= BB3_Zieltemp[3] && graphPunkt4 == true && graphPunkt5 == true)
                    {
                        if (slot1.transform.childCount > 0 && (slot1.transform.GetChild(0).CompareTag("60SiHot") || slot1.transform.GetChild(0).CompareTag("60SiCold")))
                        {
                            tiegel3Farbe = 20;
                        }
                        else if (slot2.transform.childCount > 0 && (slot2.transform.GetChild(0).CompareTag("60SiHot") || slot2.transform.GetChild(0).CompareTag("60SiCold")))
                        {
                            tiegel3Farbe = 40;
                        }
                        else if (slot3.transform.childCount > 0 && (slot3.transform.GetChild(0).CompareTag("60SiHot") || slot3.transform.GetChild(0).CompareTag("60SiCold")))
                        {
                            tiegel3Farbe = 60;
                        }
                        else if (slot4.transform.childCount > 0 && (slot4.transform.GetChild(0).CompareTag("60SiHot") || slot4.transform.GetChild(0).CompareTag("60SiCold")))
                        {
                            tiegel3Farbe = 80;
                        }
                        windowGraphTiegel3.ShowGraph(istTemp[2], 10, tiegel3Farbe);
                        istTemp[2] -= (BB3_Zieltemp[3] - BB3_Zieltemp[2]) / BB3_Zeit[3];
                    }
                    //5ter Graph Punkt
                    else if (istTemp[2] > BB3_Zieltemp[4] && graphPunkt4 == true && graphPunkt5 == true)
                    {
                        if (slot1.transform.childCount > 0 && (slot1.transform.GetChild(0).CompareTag("60SiHot") || slot1.transform.GetChild(0).CompareTag("60SiCold")))
                        {
                            tiegel3Farbe = 20;
                        }
                        else if (slot2.transform.childCount > 0 && (slot2.transform.GetChild(0).CompareTag("60SiHot") || slot2.transform.GetChild(0).CompareTag("60SiCold")))
                        {
                            tiegel3Farbe = 40;
                        }
                        else if (slot3.transform.childCount > 0 && (slot3.transform.GetChild(0).CompareTag("60SiHot") || slot3.transform.GetChild(0).CompareTag("60SiCold")))
                        {
                            tiegel3Farbe = 60;
                        }
                        else if (slot4.transform.childCount > 0 && (slot4.transform.GetChild(0).CompareTag("60SiHot") || slot4.transform.GetChild(0).CompareTag("60SiCold")))
                        {
                            tiegel3Farbe = 80;
                        }
                        windowGraphTiegel3.ShowGraph(istTemp[2], 10, tiegel3Farbe);
                        istTemp[2] -= (BB3_Zieltemp[4] - BB3_Zieltemp[3]) / BB3_Zeit[4];
                    }
                    else if (istTemp[2] < 25)
                    {
                        istTemp[2] = 25;
                    }
                }
                #endregion
                //80% Si / 20% Ge
                #region 80% / 20%
                if (flamme1Bool && (slot1.transform.GetChild(0).CompareTag("80SiCold") || slot1.transform.GetChild(0).CompareTag("80SiHot")) || flamme2Bool && (slot2.transform.GetChild(0).CompareTag("80SiCold") || slot2.transform.GetChild(0).CompareTag("80SiHot")) || flamme3Bool && (slot3.transform.GetChild(0).CompareTag("80SiCold") || slot3.transform.GetChild(0).CompareTag("80SiHot")) || flamme4Bool && (slot4.transform.GetChild(0).CompareTag("80SiCold") || slot4.transform.GetChild(0).CompareTag("80SiHot")))
                {
                    tiegelLocked80 = true;
                    if (tiegel4Heated)
                    {
                        windowGraphTiegel4.DeleteGraph();
                        tiegel4Heated = false;
                    }
                    if (istTemp[3] <= BB4_Zieltemp[0])
                    {
                        istTemp[3] += BB4_Zieltemp[0] / BB4_Zeit[0];
                        tiegel4Heated = true;
                    }
                    else if (istTemp[3] <= BB4_Zieltemp[1])
                    {
                        istTemp[3] += (BB4_Zieltemp[1] - BB4_Zieltemp[0]) / BB4_Zeit[1];
                        tiegel4Heated = true;
                    }
                    else if (istTemp[3] < BB4_Zieltemp[2])
                    {
                        istTemp[3] += (BB4_Zieltemp[2] - BB4_Zieltemp[1]) / BB4_Zeit[2];
                        tiegel4Heated = true;
                    }
                    //3ter Punkt im Graphen
                    else if (istTemp[3] >= BB4_Zieltemp[2] && graphPunkt4 == false && graphPunkt5 == false)
                    {
                        if (slot1.transform.childCount > 0 && slot1.transform.GetChild(0).CompareTag("80SiCold"))
                        {
                            tiegel4Heated = true;
                            slot1.transform.GetChild(0).tag = "80SiHot";
                            tiegelAufBB[0].gameObject.GetComponent<Renderer>().material = tiegelMat[0];
                            slot1.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[0];
                            slot1.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[0];
                        }
                        else if (slot2.transform.childCount > 0 && slot2.transform.GetChild(0).CompareTag("80SiCold"))
                        {
                            tiegel4Heated = true;
                            slot2.transform.GetChild(0).tag = "80SiHot";
                            tiegelAufBB[1].gameObject.GetComponent<Renderer>().material = tiegelMat[0];
                            slot2.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[0];
                            slot2.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[0];
                        }
                        else if (slot3.transform.childCount > 0 && slot3.transform.GetChild(0).CompareTag("80SiCold"))
                        {
                            tiegel4Heated = true;
                            slot3.transform.GetChild(0).tag = "80SiHot";
                            tiegelAufBB[2].gameObject.GetComponent<Renderer>().material = tiegelMat[0];
                            slot3.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[0];
                            slot3.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[0];
                        }
                        else if (slot4.transform.childCount > 0 && slot4.transform.GetChild(0).CompareTag("80SiCold"))
                        {
                            tiegel4Heated = true;
                            slot4.transform.GetChild(0).tag = "80SiHot";
                            tiegelAufBB[3].gameObject.GetComponent<Renderer>().material = tiegelMat[0];
                            slot4.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[0];
                            slot4.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[0];
                        }
                        istTemp[3] = BB4_Zieltemp[2];
                    }
                    else if (istTemp[3] < BB4_Zieltemp[3] && graphPunkt4 == true && graphPunkt5 == false)
                    {
                        istTemp[3] += (BB4_Zieltemp[3] - BB4_Zieltemp[2]) / BB4_Zeit[3];
                        tiegel4Heated = true;
                    }
                    //4ter Punkt im Graphen
                    else if (istTemp[3] >= BB4_Zieltemp[3] && graphPunkt4 == true && graphPunkt5 == false)
                    {
                        if (slot1.transform.childCount > 0 && slot1.transform.GetChild(0).CompareTag("80SiCold"))
                        {
                            tiegel4Heated = true;
                            slot1.transform.GetChild(0).tag = "80SiHot";
                            tiegelAufBB[0].gameObject.GetComponent<Renderer>().material = tiegelMat[0];
                            slot1.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[0];
                            slot1.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[0];
                        }
                        else if (slot2.transform.childCount > 0 && slot2.transform.GetChild(0).CompareTag("80SiCold"))
                        {
                            tiegel4Heated = true;
                            slot2.transform.GetChild(0).tag = "80SiHot";
                            tiegelAufBB[1].gameObject.GetComponent<Renderer>().material = tiegelMat[0];
                            slot2.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[0];
                            slot2.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[0];
                        }
                        else if (slot3.transform.childCount > 0 && slot3.transform.GetChild(0).CompareTag("80SiCold"))
                        {
                            tiegel4Heated = true;
                            slot3.transform.GetChild(0).tag = "80SiHot";
                            tiegelAufBB[2].gameObject.GetComponent<Renderer>().material = tiegelMat[0];
                            slot3.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[0];
                            slot3.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[0];
                        }
                        else if (slot4.transform.childCount > 0 && slot4.transform.GetChild(0).CompareTag("80SiCold"))
                        {
                            tiegel4Heated = true;
                            slot4.transform.GetChild(0).tag = "80SiHot";
                            tiegelAufBB[3].gameObject.GetComponent<Renderer>().material = tiegelMat[0];
                            slot4.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[0];
                            slot4.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[0];
                        }
                        istTemp[3] = BB4_Zieltemp[3];
                    }
                    else if (istTemp[3] < BB4_Zieltemp[4] && graphPunkt4 == true && graphPunkt5 == true)
                    {
                        istTemp[3] += (BB4_Zieltemp[4] - BB4_Zieltemp[3]) / BB4_Zeit[4];
                        tiegel4Heated = true;
                    }
                    //5ter Graph Punkt
                    else if (istTemp[3] >= BB4_Zieltemp[4] && graphPunkt4 == true && graphPunkt5 == true)
                    {
                        if (slot1.transform.childCount > 0 && slot1.transform.GetChild(0).CompareTag("80SiCold"))
                        {
                            tiegel4Heated = true;
                            slot1.transform.GetChild(0).tag = "80SiHot";
                            tiegelAufBB[0].gameObject.GetComponent<Renderer>().material = tiegelMat[0];
                            slot1.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[0];
                            slot1.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[0];
                        }
                        else if (slot2.transform.childCount > 0 && slot2.transform.GetChild(0).CompareTag("80SiCold"))
                        {
                            tiegel4Heated = true;
                            slot2.transform.GetChild(0).tag = "80SiHot";
                            tiegelAufBB[1].gameObject.GetComponent<Renderer>().material = tiegelMat[0];
                            slot2.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[0];
                            slot2.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[0];
                        }
                        else if (slot3.transform.childCount > 0 && slot3.transform.GetChild(0).CompareTag("80SiCold"))
                        {
                            tiegel4Heated = true;
                            slot3.transform.GetChild(0).tag = "80SiHot";
                            tiegelAufBB[2].gameObject.GetComponent<Renderer>().material = tiegelMat[0];
                            slot3.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[0];
                            slot3.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[0];
                        }
                        else if (slot4.transform.childCount > 0 && slot4.transform.GetChild(0).CompareTag("80SiCold"))
                        {
                            tiegel4Heated = true;
                            slot4.transform.GetChild(0).tag = "80SiHot";
                            tiegelAufBB[3].gameObject.GetComponent<Renderer>().material = tiegelMat[0];
                            slot4.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[0];
                            slot4.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[0];
                        }
                        istTemp[3] = BB4_Zieltemp[4];
                    }
                }
                else if(flamme1Bool == false && slot1.transform.childCount > 0 && (slot1.transform.GetChild(0).CompareTag("80SiCold") || slot1.transform.GetChild(0).CompareTag("80SiHot")) || flamme2Bool == false && slot2.transform.childCount > 0 && (slot2.transform.GetChild(0).CompareTag("80SiCold") || slot2.transform.GetChild(0).CompareTag("80SiHot")) || flamme3Bool == false && slot3.transform.childCount > 0 && (slot3.transform.GetChild(0).CompareTag("80SiCold") || slot3.transform.GetChild(0).CompareTag("80SiHot")) || flamme4Bool == false && slot4.transform.childCount > 0 && (slot4.transform.GetChild(0).CompareTag("80SiCold") || slot4.transform.GetChild(0).CompareTag("80SiHot")))
                {
                    tiegelLocked80 = false;
                    if (istTemp[3] > 25 && istTemp[3] <= BB4_Zieltemp[0])
                    {
                        if (istTemp[3] >= 25 && istTemp[3] < 100)
                        {
                            if (slot1.transform.childCount > 0 && slot1.transform.GetChild(0).CompareTag("80SiHot"))
                            {
                                slot1.transform.GetChild(0).tag = "80SiCold";
                                tiegelAufBB[0].gameObject.GetComponent<Renderer>().material = tiegelMat[1];
                                slot1.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[1];
                                slot1.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[1];
                            }
                            else if (slot2.transform.childCount > 0 && slot2.transform.GetChild(0).CompareTag("80SiHot"))
                            {
                                slot2.transform.GetChild(0).tag = "80SiCold";
                                tiegelAufBB[1].gameObject.GetComponent<Renderer>().material = tiegelMat[1];
                                slot2.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[1];
                                slot2.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[1];
                            }
                            else if (slot3.transform.childCount > 0 && slot3.transform.GetChild(0).CompareTag("80SiHot"))
                            {
                                slot3.transform.GetChild(0).tag = "80SiCold";
                                tiegelAufBB[2].gameObject.GetComponent<Renderer>().material = tiegelMat[1];
                                slot3.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[1];
                                slot2.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[1];
                            }
                            else if (slot4.transform.childCount > 0 && slot4.transform.GetChild(0).CompareTag("80SiHot"))
                            {
                                slot4.transform.GetChild(0).tag = "80SiCold";
                                tiegelAufBB[3].gameObject.GetComponent<Renderer>().material = tiegelMat[1];
                                slot4.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = tiegelSprite[1];
                                slot4.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = tiegelSprite[1];
                            }
                        }
                        if (slot1.transform.childCount > 0 && (slot1.transform.GetChild(0).CompareTag("80SiHot") || slot1.transform.GetChild(0).CompareTag("80SiCold")))
                        {
                            tiegel4Farbe = 20;
                        }
                        else if (slot2.transform.childCount > 0 && (slot2.transform.GetChild(0).CompareTag("80SiHot") || slot2.transform.GetChild(0).CompareTag("80SiCold")))
                        {
                            tiegel4Farbe = 40;
                        }
                        else if (slot3.transform.childCount > 0 && (slot3.transform.GetChild(0).CompareTag("80SiHot") || slot3.transform.GetChild(0).CompareTag("80SiCold")))
                        {
                            tiegel4Farbe = 60;
                        }
                        else if (slot4.transform.childCount > 0 && (slot4.transform.GetChild(0).CompareTag("80SiHot") || slot4.transform.GetChild(0).CompareTag("80SiCold")))
                        {
                            tiegel4Farbe = 80;
                        }
                        windowGraphTiegel4.ShowGraph(istTemp[3], 10, tiegel4Farbe);
                        istTemp[3] -= BB4_Zieltemp[0] / BB4_Zeit[0];
                    }
                    else if (istTemp[3] > BB4_Zieltemp[0] && istTemp[3] <= BB4_Zieltemp[1])
                    {
                        if (slot1.transform.childCount > 0 && (slot1.transform.GetChild(0).CompareTag("80SiHot") || slot1.transform.GetChild(0).CompareTag("80SiCold")))
                        {
                            tiegel4Farbe = 20;
                        }
                        else if (slot2.transform.childCount > 0 && (slot2.transform.GetChild(0).CompareTag("80SiHot") || slot2.transform.GetChild(0).CompareTag("80SiCold")))
                        {
                            tiegel4Farbe = 40;
                        }
                        else if (slot3.transform.childCount > 0 && (slot3.transform.GetChild(0).CompareTag("80SiHot") || slot3.transform.GetChild(0).CompareTag("80SiCold")))
                        {
                            tiegel4Farbe = 60;
                        }
                        else if (slot4.transform.childCount > 0 && (slot4.transform.GetChild(0).CompareTag("80SiHot") || slot4.transform.GetChild(0).CompareTag("80SiCold")))
                        {
                            tiegel4Farbe = 80;
                        }
                        windowGraphTiegel4.ShowGraph(istTemp[3], 10, tiegel4Farbe);
                        istTemp[3] -= (BB4_Zieltemp[1] - BB4_Zieltemp[0]) / BB4_Zeit[1];
                    }
                    //3ter Graph Punkt
                    else if (istTemp[3] > BB4_Zieltemp[1] && graphPunkt4 == false && graphPunkt5 == false)
                    {
                        if (slot1.transform.childCount > 0 && (slot1.transform.GetChild(0).CompareTag("80SiHot") || slot1.transform.GetChild(0).CompareTag("80SiCold")))
                        {
                            tiegel4Farbe = 20;
                        }
                        else if (slot2.transform.childCount > 0 && (slot2.transform.GetChild(0).CompareTag("80SiHot") || slot2.transform.GetChild(0).CompareTag("80SiCold")))
                        {
                            tiegel4Farbe = 40;
                        }
                        else if (slot3.transform.childCount > 0 && (slot3.transform.GetChild(0).CompareTag("80SiHot") || slot3.transform.GetChild(0).CompareTag("80SiCold")))
                        {
                            tiegel4Farbe = 60;
                        }
                        else if (slot4.transform.childCount > 0 && (slot4.transform.GetChild(0).CompareTag("80SiHot") || slot4.transform.GetChild(0).CompareTag("80SiCold")))
                        {
                            tiegel4Farbe = 80;
                        }
                        windowGraphTiegel4.ShowGraph(istTemp[3], 10, tiegel4Farbe);
                        istTemp[3] -= (BB4_Zieltemp[2] - BB4_Zieltemp[1]) / BB4_Zeit[2];
                    }
                    else if (istTemp[3] > BB4_Zieltemp[1] && istTemp[3] <= BB4_Zieltemp[2] && graphPunkt4 == true && graphPunkt5 == false)
                    {
                        if (slot1.transform.childCount > 0 && (slot1.transform.GetChild(0).CompareTag("80SiHot") || slot1.transform.GetChild(0).CompareTag("80SiCold")))
                        {
                            tiegel4Farbe = 20;
                        }
                        else if (slot2.transform.childCount > 0 && (slot2.transform.GetChild(0).CompareTag("80SiHot") || slot2.transform.GetChild(0).CompareTag("80SiCold")))
                        {
                            tiegel4Farbe = 40;
                        }
                        else if (slot3.transform.childCount > 0 && (slot3.transform.GetChild(0).CompareTag("80SiHot") || slot3.transform.GetChild(0).CompareTag("80SiCold")))
                        {
                            tiegel4Farbe = 60;
                        }
                        else if (slot4.transform.childCount > 0 && (slot4.transform.GetChild(0).CompareTag("80SiHot") || slot4.transform.GetChild(0).CompareTag("80SiCold")))
                        {
                            tiegel4Farbe = 80;
                        }
                        windowGraphTiegel4.ShowGraph(istTemp[3], 10, tiegel4Farbe);
                        istTemp[3] -= (BB4_Zieltemp[2] - BB4_Zieltemp[1]) / BB4_Zeit[2];
                    }
                    //4ter Graph Punkt
                    else if (istTemp[3] > BB4_Zieltemp[2] && graphPunkt4 == true && graphPunkt5 == false)
                    {
                        if (slot1.transform.childCount > 0 && (slot1.transform.GetChild(0).CompareTag("80SiHot") || slot1.transform.GetChild(0).CompareTag("80SiCold")))
                        {
                            tiegel4Farbe = 20;
                        }
                        else if (slot2.transform.childCount > 0 && (slot2.transform.GetChild(0).CompareTag("80SiHot") || slot2.transform.GetChild(0).CompareTag("80SiCold")))
                        {
                            tiegel4Farbe = 40;
                        }
                        else if (slot3.transform.childCount > 0 && (slot3.transform.GetChild(0).CompareTag("80SiHot") || slot3.transform.GetChild(0).CompareTag("80SiCold")))
                        {
                            tiegel4Farbe = 60;
                        }
                        else if (slot4.transform.childCount > 0 && (slot4.transform.GetChild(0).CompareTag("80SiHot") || slot4.transform.GetChild(0).CompareTag("80SiCold")))
                        {
                            tiegel4Farbe = 80;
                        }
                        windowGraphTiegel4.ShowGraph(istTemp[3], 10, tiegel4Farbe);
                        istTemp[3] -= (BB4_Zieltemp[3] - BB4_Zieltemp[2]) / BB4_Zeit[3];
                    }
                    else if (istTemp[3] > BB4_Zieltemp[2] && istTemp[3] <= BB4_Zieltemp[3] && graphPunkt4 == true && graphPunkt5 == true)
                    {
                        if (slot1.transform.childCount > 0 && (slot1.transform.GetChild(0).CompareTag("80SiHot") || slot1.transform.GetChild(0).CompareTag("80SiCold")))
                        {
                            tiegel4Farbe = 20;
                        }
                        else if (slot2.transform.childCount > 0 && (slot2.transform.GetChild(0).CompareTag("80SiHot") || slot2.transform.GetChild(0).CompareTag("80SiCold")))
                        {
                            tiegel4Farbe = 40;
                        }
                        else if (slot3.transform.childCount > 0 && (slot3.transform.GetChild(0).CompareTag("80SiHot") || slot3.transform.GetChild(0).CompareTag("80SiCold")))
                        {
                            tiegel4Farbe = 60;
                        }
                        else if (slot4.transform.childCount > 0 && (slot4.transform.GetChild(0).CompareTag("80SiHot") || slot4.transform.GetChild(0).CompareTag("80SiCold")))
                        {
                            tiegel4Farbe = 80;
                        }
                        windowGraphTiegel4.ShowGraph(istTemp[3], 10, tiegel4Farbe);
                        istTemp[3] -= (BB4_Zieltemp[3] - BB4_Zieltemp[2]) / BB4_Zeit[3];
                    }
                    //5ter Graph Punkt
                    else if (istTemp[3] > BB4_Zieltemp[4] && graphPunkt4 == true && graphPunkt5 == true)
                    {
                        if (slot1.transform.childCount > 0 && (slot1.transform.GetChild(0).CompareTag("80SiHot") || slot1.transform.GetChild(0).CompareTag("80SiCold")))
                        {
                            tiegel4Farbe = 20;
                        }
                        else if (slot2.transform.childCount > 0 && (slot2.transform.GetChild(0).CompareTag("80SiHot") || slot2.transform.GetChild(0).CompareTag("80SiCold")))
                        {
                            tiegel4Farbe = 40;
                        }
                        else if (slot3.transform.childCount > 0 && (slot3.transform.GetChild(0).CompareTag("80SiHot") || slot3.transform.GetChild(0).CompareTag("80SiCold")))
                        {
                            tiegel4Farbe = 60;
                        }
                        else if (slot4.transform.childCount > 0 && (slot4.transform.GetChild(0).CompareTag("80SiHot") || slot4.transform.GetChild(0).CompareTag("80SiCold")))
                        {
                            tiegel4Farbe = 80;
                        }
                        windowGraphTiegel4.ShowGraph(istTemp[3], 10, tiegel4Farbe);
                        istTemp[3] -= (BB4_Zieltemp[4] - BB4_Zieltemp[3]) / BB4_Zeit[4];
                    }
                    else if (istTemp[3] < 25)
                    {
                        istTemp[3] = 25;
                    }
                }
                #endregion
                #endregion
                //ausgabeText.text = ("istTemp[0]: " + istTemp[0] + " / istTemp[1]: " + istTemp[1] + " / istTemp[2]: " + istTemp[2] + " / istTemp[3]: " + istTemp[3]);
                if (istTemp[0] >= 1550 && istTemp[1] >= 1550 && istTemp[2] >= 1550 && istTemp[3] >= 1550)
                {
                    waiting = false;
                    break;
                }
            }
        }
    }
    #region Bunsen Brenner Flammen
    //Zünde Bunsen Brenner ganz links an
    public void Flamme1Button()
    {
        //Wenn entweder das Hauptgas oder das Platzgas noch nicht eingeschaltet ist, dann zeige die Info, dass beides eingeschaltet sein muss
        if ((hauptGasSchalter == false || platzGasSchalter == false) && verbrannt == false)
        {
            infoBBAusgeschaltet.gameObject.SetActive(true);
        }
        //Wenn man sich verbrannt hat, zeige Fenster, dass man zuerst sich verarzten soll, bevor man weiter arbeiten kann
        if(flamme1.activeInHierarchy == false && hauptGasSchalter && platzGasSchalter && verbrannt)
        {
            erstVerarztenFenster.SetActive(true);
        }
        //Falls ein Tiegel auf dem 1. Bunsenbrenner liegt, aber nicht eines der geschmolzenen items, dann mache die Flamme an
        if (flamme1.activeInHierarchy == false && hauptGasSchalter == true && platzGasSchalter && verbrannt == false && slot1.transform.childCount > 0 && (slot1.transform.GetChild(0).CompareTag("Falsch") == false && slot1.transform.GetChild(0).CompareTag("Richtig") == false))
        {
            flamme1.SetActive(true);
            flamme1Bool = true;
            BunsenBrennerFlammen[0].gameObject.SetActive(true);
            BunsenBrennerFlammen[0].Play();
        }
        //Falls das Item auf dem Bunsenbrenner ein Tool ist, dann zeige das Info Fenster "InfoFalschesVerhalten"
        else if(slot1.transform.childCount > 0 && (slot1.transform.GetChild(0).CompareTag("Falsch") || slot1.transform.GetChild(0).CompareTag("Richtig")))
        {
            hinweisToolSchmelzen.SetActive(true);
            flamme1.SetActive(false);
            flamme1Bool = false;
            BunsenBrennerFlammen[0].gameObject.SetActive(false);
        }
        else
        {
            flamme1.SetActive(false);
            flamme1Bool = false;
            BunsenBrennerFlammen[0].gameObject.SetActive(false);
        }
    }
    //Zünde Bunsen Brenner links mittig an
    public void Flamme2Button()
    {
        if ((hauptGasSchalter == false || platzGasSchalter == false) && verbrannt == false)
        {
            infoBBAusgeschaltet.gameObject.SetActive(true);
        }
        if (flamme2.activeInHierarchy == false && hauptGasSchalter && platzGasSchalter && verbrannt)
        {
            erstVerarztenFenster.SetActive(true);
        }
        if (flamme2.activeInHierarchy == false && hauptGasSchalter == true && platzGasSchalter && verbrannt == false && slot2.transform.childCount > 0 && (slot2.transform.GetChild(0).CompareTag("Falsch") == false && slot2.transform.GetChild(0).CompareTag("Richtig") == false))
        {
            flamme2.SetActive(true);
            flamme2Bool = true;
            BunsenBrennerFlammen[1].gameObject.SetActive(true);
            BunsenBrennerFlammen[1].Play();
        }
        else if (slot2.transform.childCount > 0 && (slot2.transform.GetChild(0).CompareTag("Falsch") || slot2.transform.GetChild(0).CompareTag("Richtig")))
        {
            hinweisToolSchmelzen.SetActive(true);
            flamme2.SetActive(false);
            flamme2Bool = false;
            BunsenBrennerFlammen[1].gameObject.SetActive(false);
        }
        else
        {
            flamme2.SetActive(false);
            flamme2Bool = false;
            BunsenBrennerFlammen[1].gameObject.SetActive(false);
        }
    }
    //Zünde Bunsen Brenner rechts mittig an
    public void Flamme3Button()
    {
        if ((hauptGasSchalter == false || platzGasSchalter == false) && verbrannt == false)
        {
            infoBBAusgeschaltet.gameObject.SetActive(true);
        }
        if (flamme3.activeInHierarchy == false && hauptGasSchalter && platzGasSchalter && verbrannt)
        {
            erstVerarztenFenster.SetActive(true);
        }
        if (flamme3.activeInHierarchy == false && hauptGasSchalter == true && platzGasSchalter && verbrannt == false && slot3.transform.childCount > 0 && (slot3.transform.GetChild(0).CompareTag("Falsch") == false && slot3.transform.GetChild(0).CompareTag("Richtig") == false))
        {
            flamme3.SetActive(true);
            flamme3Bool = true;
            BunsenBrennerFlammen[2].gameObject.SetActive(true);
            BunsenBrennerFlammen[2].Play();
        }
        else if (slot3.transform.childCount > 0 && (slot3.transform.GetChild(0).CompareTag("Falsch") || slot3.transform.GetChild(0).CompareTag("Richtig")))
        {
            hinweisToolSchmelzen.SetActive(true);
            flamme3.SetActive(false);
            flamme3Bool = false;
            BunsenBrennerFlammen[2].gameObject.SetActive(false);
        }
        else
        {
            flamme3.SetActive(false);
            flamme3Bool = false;
            BunsenBrennerFlammen[2].gameObject.SetActive(false);
        }
    }
    //Zünde Bunsen Brenner ganz rechts an
    public void Flamme4Button()
    {
        if ((hauptGasSchalter == false || platzGasSchalter == false) && verbrannt == false)
        {
            infoBBAusgeschaltet.gameObject.SetActive(true);
        }
        if (flamme4.activeInHierarchy == false && hauptGasSchalter && platzGasSchalter && verbrannt)
        {
            erstVerarztenFenster.SetActive(true);
        }
        if (flamme4.activeInHierarchy == false && hauptGasSchalter == true && platzGasSchalter && verbrannt == false && slot4.transform.childCount > 0 && (slot4.transform.GetChild(0).CompareTag("Falsch") == false && slot4.transform.GetChild(0).CompareTag("Richtig") == false))
        {
            flamme4.SetActive(true);
            flamme4Bool = true;
            BunsenBrennerFlammen[3].gameObject.SetActive(true);
            BunsenBrennerFlammen[3].Play();
        }
        else if (slot4.transform.childCount > 0 && (slot4.transform.GetChild(0).CompareTag("Falsch") || slot4.transform.GetChild(0).CompareTag("Richtig")))
        {
            hinweisToolSchmelzen.SetActive(true);
            flamme4.SetActive(false);
            flamme4Bool = false;
            BunsenBrennerFlammen[3].gameObject.SetActive(false);
        }
        else
        {
            flamme4.SetActive(false);
            flamme4Bool = false;
            BunsenBrennerFlammen[3].gameObject.SetActive(false);
        }
    }
    #endregion

    public void CloseVerbranntWindow()
    {
        verbranntFenster.SetActive(false);
        zeigeVerbrennung = false;
    }

    public void VerbrennungBehandelt()
    {
        verbrannt = false;
    }

    public void CloseErstVerarztenWindow()
    {
        erstVerarztenFenster.SetActive(false);
    }
}
