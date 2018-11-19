using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class Door : Interactive
{
	public Vector3 destination;
    [SerializeField] private GameObject questWindow;

    public override void Interact()
    {
		if (this.name != "door_elevatorout")
        {
			playerAgent.Warp(destination);
            if(this.gameObject.name == "Doors_Tutorial_2")
            {
                questWindow.SetActive(true);
            }           
        }
        else
        {
            Debug.Log("Choose a floor first.");
        }
    }
}