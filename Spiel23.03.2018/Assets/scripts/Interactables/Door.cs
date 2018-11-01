using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class Door : Interactive
{
	public Vector3 destination;

    public override void Interact()
    {
		if (this.name != "door_elevatorout")
        {
			playerAgent.Warp(destination);
        }
        else
        {
            Debug.Log("Choose a floor first.");
        }
    }
}