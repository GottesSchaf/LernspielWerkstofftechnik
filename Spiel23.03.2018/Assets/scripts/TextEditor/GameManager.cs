using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Awake ()
    {
        GetComponent<TextFileReader>().ReadTextFile(@"C:\Users\hannd\Documents\Level 1 WST\Spiel23.03.2018\Assets\scripts\TextEditor\Text_Editor_ViaMaterialia_Level1.txt");
	}
}
