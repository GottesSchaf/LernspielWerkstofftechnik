using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MouseInput : MonoBehaviour
{

    NavMeshAgent playerAgent;
    Ray ray;
    RaycastHit hit;
    public string RayHitsThis;

    void Start()
    {
        playerAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            RayHitsThis = hit.collider.name;
        }

        if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() && CameraFollow.instance.closeupInteraction == false)
        {
            GetInput();
        }
        else if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() && CameraFollow.instance.closeupInteraction == true && RayHitsThis == "Book")
        {
            GetInput();
        }
    }

    void GetInput()
    {
        Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit interactionInfo;
        if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity))
        {
            GameObject interactiveObject = interactionInfo.collider.gameObject;
            if (interactiveObject.tag == "Interactive")
            {
                interactiveObject.GetComponent<Interactive>().MoveToInteraction(playerAgent);
            }
            if (interactiveObject.tag == "Collectible")
            {
                interactiveObject.GetComponent<Collectible>().MoveToCollectible(playerAgent);
            }
            else
            {
                playerAgent.destination = interactionInfo.point;
                this.gameObject.transform.LookAt(interactionInfo.point);
            }
        }

    }
}