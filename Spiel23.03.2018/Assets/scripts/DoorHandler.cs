using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour {
    private Animator animator = null; 
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider collider)
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("isopen", true);
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        animator.SetBool("isopen", false); 
    }
}
