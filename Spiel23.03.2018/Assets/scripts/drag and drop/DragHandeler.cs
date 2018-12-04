﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandeler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public static GameObject itemBeingDragged;
    public static bool draggingItem;
    [SerializeField] Vector3 startPosition;
    [SerializeField] Transform startParent;
    private Ray updateRay;
    private RaycastHit updateHit;
    public GameObject RadialMenue;
    public GameObject cam;
    public static GameObject Inventory;
    public GameObject InventoryCollision;
    public GameObject UICanvas;
	public GameObject Machine, machineNew, machineNewParent;
    GameObject[] shatter;
    List<GameObject> shatter1 = new List<GameObject>();
    public Slot MachineSlot;
    public GameObject player;
    public GameObject mesh;
    GameObject gameOverScreen;
    DestroyMachine desMachine;


    #region IBeginDragHandler implementation

    public void OnBeginDrag(PointerEventData eventData)
    {
		itemBeingDragged = gameObject;
        draggingItem = true;
        if (transform.parent != startParent)
        {
            startPosition = transform.position;
        }
		startParent = transform.parent;
		GetComponent<CanvasGroup>().blocksRaycasts = false;

		RadialMenue = GameObject.Find("RadialMenue");
        cam = GameObject.Find("Main Camera");
		Inventory = GameObject.Find("InventoryMenue");
		UICanvas = GameObject.Find("Canvas");
		Machine = GameObject.Find("Machine");
        gameOverScreen = GameObject.Find("Maschine_Kaputt");
		//MachineSlot = 

		//if(itemBeingDragged.name.Contains("placeholder"))
		//{
		//	player = GameObject.Find("player");
		//	mesh = player.transform.Find("clothes_green").gameObject;
		//	mesh.SetActive(false);
		//	Destroy (itemBeingDragged);
		//	GameObject temp = Instantiate(mesh.GetComponent<Clothes>().inventoryobject, new Vector3(0, 0, 0), Quaternion.identity);
		//	temp.transform.parent = startParent.transform;
		//}
    }

    #endregion

    #region IDragHandler implementation

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition; //eventData.position
        //itemBeingDragged.transform.SetParent(UICanvas.transform);

        //updateRay = Camera.main.ScreenPointToRay(Input.mousePosition);
    }

    #endregion

    #region IEndDragHandler implementation

    public void OnEndDrag(PointerEventData eventData)
    {
        //raycast
        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            //-----------------------------------Inventar Cubes----------------------------------
            //if (itemBeingDragged.name.Contains("blue"))
            //{
            //    Debug.Log("Mixing red and blue");
            //}
            //else if (hit.transform.name.Contains("blue") && itemBeingDragged.name.Contains("red"))
            //{
            //    Debug.Log("Mixing blue and red");
            //}
            //else if (hit.transform.name.Contains("Machine") && itemBeingDragged.name.Contains("clothes"))
            //{
            //    Debug.Log("Thats not what you use clothes for."); ;
            //}
            //else if (hit.transform.name.Contains("Machine"))
            //{
            //    Debug.Log("Dragged on Machine");
            //    GameObject temp = Instantiate(itemBeingDragged, new Vector3(0, 0, 0), Quaternion.identity);
            //    //temp.transform.parent = MachineSlot.transform;
            //    //Machine.GetComponent<Machine>().Interact();
            //}
            if (hit.transform.CompareTag("Machine") && itemBeingDragged.transform.tag == "Richtig")
            {
                Machine = GameObject.Find("Crazy_Machine_Shatter");
                //Machine.GetComponent<Rigidbody>().isKinematic = true;
                for(int i = 0; i < Machine.transform.childCount; i++)
                {
                    shatter1.Add(Machine.transform.GetChild(i).gameObject);
                }
                int childCount = Machine.transform.childCount;
                for(int i = 0; i < childCount; i++)
                {
                    Destroy(Machine.transform.GetChild(0).gameObject);
                }
                machineNewParent = GameObject.Find("NewMachineParent");
                machineNew = machineNewParent.transform.GetChild(0).gameObject;
                machineNew.SetActive(true);
                mesh = Machine.transform.Find("Gear1").gameObject;
                mesh.SetActive(true);
                Destroy(itemBeingDragged);
                Invoke("Gewonnen", 2);
            }
            else if(hit.transform.CompareTag("Machine") && itemBeingDragged.transform.tag == "FalschCheat")
            {
                //Text: du kleiner Cheater
                //Game Over
            }
            else if(hit.transform.CompareTag("Machine") && itemBeingDragged.transform.tag == "Falsch")
            {
                //GameOver
                Machine = GameObject.Find("Crazy_Machine_Shatter");
                Machine.SetActive(false);
                machineNewParent = GameObject.Find("NewMachineParent");
                machineNew = machineNewParent.transform.GetChild(0).gameObject;
                if (machineNew != null)
                {
                    machineNew.SetActive(true);
                    Invoke("DestroyMachine", 2);
                }
                else
                {
                    Debug.Log("Konnte keine machineNew finden!");
                    Debug.Log(machineNew);
                }
            }
            if (hit.transform.CompareTag("Player") && itemBeingDragged.name.Contains("Labcoat"))
            {
                player = GameObject.Find("Player");
                mesh = player.transform.Find("LabCoat").gameObject;
                mesh.SetActive(true);
                Destroy(itemBeingDragged);
            }
            else if (hit.transform.CompareTag("Player") && itemBeingDragged.name.Contains("Glove"))
            {
                player = GameObject.Find("Player");
                mesh = player.transform.Find("Glove_Left").gameObject;
                mesh.SetActive(true);
                Destroy(itemBeingDragged);
            }
        }

        itemBeingDragged = null;
        draggingItem = false;
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        if (Slot.otherSlot == false || transform.parent.name.Equals("Canvas")) //!Inventory.activeSelf &&
        {
            transform.position = startPosition;
            transform.SetParent(startParent);
        }
        //else if (transform.parent != startParent)
        //{
        //    transform.position = startPosition;
        //    transform.SetParent(transform.parent);
        //}

    }

    void DestroyMachine()
    {
        desMachine = machineNew.GetComponent<DestroyMachine>();
        desMachine.DestroyMe();
    }

    void Gewonnen()
    {
        desMachine = machineNew.GetComponent<DestroyMachine>();
        desMachine.ShowGameWonScreen();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {

        //Debug.Log("Leaving now: Inventory");

    }
    #endregion
}