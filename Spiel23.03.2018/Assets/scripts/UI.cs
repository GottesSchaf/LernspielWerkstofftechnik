using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    public Transform RadialMenue;
    public Transform MainMenue;
    public Transform Inventory;
    public Transform Album;
    public Transform Map;
    public Transform CloseupBack;
    public Transform InventoryCollision;
    public Transform MachineWindow;
    public Text zieltemp;   //Textfeld für die Zieltemperatur
    public InputField inputZieltemp;    //Das Eingabefeld für die Zieltemperatur
    bool inputZieltempBool = false;
    public InputField inputRateTemp;
    public Text rateTemp;
    bool inputRateBool = false;
    float laufzeitSek, laufzeitMin, LaufzeitStu;
    public Text laufzeitText;
    bool laufzeitBool = false;

    public void Update()
    {
        if (inputZieltemp.isFocused)
        {
            inputZieltemp.image.color = Color.green;
        }
        else
        {
            inputZieltemp.image.color = Color.white;
        }
        InputFieldUpdate();
        if (laufzeitBool)
        {
            Laufzeit_Ofen();
        }
    }

    public void Button_OpenMenue()
    {
        Debug.Log("The MenueButton was clicked.");

        if (RadialMenue.gameObject.activeInHierarchy == false)
        {
            RadialMenue.gameObject.SetActive(true);
        }
        else
        {
            RadialMenue.gameObject.SetActive(false);
            MainMenue.gameObject.SetActive(false);
            Inventory.gameObject.SetActive(false);
            Album.gameObject.SetActive(false);
            Map.gameObject.SetActive(false);
        }
    }

    public void Button_Screenshot()
    {
        Debug.Log("A screenshot was taken.");
    }

    public void Button_MainMenue()
    {
        Debug.Log("The MainMenueButton was clicked.");

        if (MainMenue.gameObject.activeInHierarchy == false)
        {
            MainMenue.gameObject.SetActive(true);
            Inventory.gameObject.SetActive(false);
            Album.gameObject.SetActive(false);
        }
        else
        {
            MainMenue.gameObject.SetActive(false);
        }
    }

    public void Button_Inventory()
    {
        Debug.Log("The InventoryButton was clicked.");

        if (Inventory.gameObject.activeInHierarchy == false)
        {
            MainMenue.gameObject.SetActive(false);
            Inventory.gameObject.SetActive(true);
            Album.gameObject.SetActive(false);
            InventoryCollision.gameObject.SetActive(true);
        }
        else
        {
            Inventory.gameObject.SetActive(false);
            InventoryCollision.gameObject.SetActive(false);
        }
    }

    public void Button_Album()
    {
        Debug.Log("The AlbumButton was clicked.");

        if (Album.gameObject.activeInHierarchy == false)
        {
            MainMenue.gameObject.SetActive(false);
            Inventory.gameObject.SetActive(false);
            Album.gameObject.SetActive(true);
        }
        else
        {
            Album.gameObject.SetActive(false);
        }
    }

    public void Button_Map()
    {
        Debug.Log("The MapButton was clicked.");
        Map.gameObject.SetActive(true);
    }

    public void Button_CloseMap()
    {
        Debug.Log("The Map was closed.");
        Map.gameObject.SetActive(false);
    }

    public void Button_Closeup()
    {
        Debug.Log("The Closeup was closed.");

        if (CameraFollow.instance.closeupInteraction == true)
        {
            CloseupBack.gameObject.SetActive(false);
            MachineWindow.gameObject.SetActive(false);
            CameraFollow.instance.closeupInteraction = false;
            CameraFollow.instance.playerToFollow.GetComponent<MeshRenderer>().enabled = true;
			CameraFollow.instance.playerToFollow.Find("clothes_green").GetComponent<MeshRenderer>().enabled = true;
            Book.instance.GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void InputFieldUpdate()
    {
        if(inputZieltemp.isFocused == true)
        {
            inputZieltempBool = true;
            inputRateBool = false;
        }
        else if (inputRateTemp.isFocused == true)
        {
            inputZieltempBool = false;
            inputRateBool = true;
        }
    }
    //Numpad Button 1 fügt, je nach ausgewähltem Eingabefeld, eine 1 der Zahl hinzu. Gleiches gilt für Numapad 2 - 0
    public void Button_Numpad1()
    {
        if(inputZieltempBool)
        {
            inputZieltemp.text += "1";
        }
        else if (inputRateBool)
        {
            inputRateTemp.text += "1";
        }
    }

	public void Button_Numpad2()
	{
        if (inputZieltempBool)
        {
            inputZieltemp.text += "2";
        }
        else if (inputRateBool)
        {
            inputRateTemp.text += "2";
        }
    }

	public void Button_Numpad3()
	{
        if (inputZieltempBool)
        {
            inputZieltemp.text += "3";
        }
        else if (inputRateBool)
        {
            inputRateTemp.text += "3";
        }
    }

	public void Button_Numpad4()
	{
        if (inputZieltempBool)
        {
            inputZieltemp.text += "4";
        }
        else if (inputRateBool)
        {
            inputRateTemp.text += "4";
        }
    }

	public void Button_Numpad5()
	{
        if (inputZieltempBool)
        {
            inputZieltemp.text += "5";
        }
        else if (inputRateBool)
        {
            inputRateTemp.text += "5";
        }
    }

	public void Button_Numpad6()
	{
        if (inputZieltempBool)
        {
            inputZieltemp.text += "6";
        }
        else if (inputRateBool)
        {
            inputRateTemp.text += "6";
        }
    }

    public void Button_Numpad7()
    {
        if (inputZieltempBool)
        {
            inputZieltemp.text += "7";
        }
        else if (inputRateBool)
        {
            inputRateTemp.text += "7";
        }
    }

	public void Button_Numpad8()
	{
        if (inputZieltempBool)
        {
            inputZieltemp.text += "8";
        }
        else if (inputRateBool)
        {
            inputRateTemp.text += "8";
        }
    }

	public void Button_Numpad9()
	{
        if (inputZieltempBool)
        {
            inputZieltemp.text += "9";
        }
        else if (inputRateBool)
        {
            inputRateTemp.text += "9";
        }
    }

	public void Button_Numpad0()
	{
        if (inputZieltempBool)
        {
            inputZieltemp.text += "0";
        }
        else if (inputRateBool)
        {
            inputRateTemp.text += "0";
        }
    }
    //Resettet das ausgewählte Eingabefeld
	public void Button_NumpadC()
	{
        if (inputZieltempBool)
        {
            inputZieltemp.text = "";
        }
        else if (inputRateBool)
        {
            inputRateTemp.text = "";
        }
    }
    //Übergibt die eingegebenen Zahlen an die Schmelze
	public void Button_NumpadOK()
	{
        if (inputZieltempBool)
        {
            inputZieltemp.text = "Schmelze in Betrieb!";
        }
        else if (inputRateBool)
        {
            inputRateTemp.text = "Schmelze in Betrieb!";
        }
    }

    public void Button_Start()
    {
        if (inputZieltemp.text != "" && inputRateTemp.text != "")
        {
            laufzeitBool = true;
        }
    }

    public void Laufzeit_Ofen()
    {
        laufzeitSek += Time.deltaTime;
        if(laufzeitSek >= 60f)
        {
            laufzeitMin++;
            laufzeitSek = 0;
        }
        if(laufzeitMin >= 60f)
        {
            LaufzeitStu++;
            laufzeitMin = 0;
        }
        laufzeitText.text = "Laufzeit: " + LaufzeitStu + ":" + laufzeitMin + ":" + Mathf.Round(laufzeitSek);
    }
}
