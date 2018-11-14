using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandeler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public static GameObject itemBeingDragged;
    public static bool draggingItem;
    Vector3 startPosition;
    Transform startParent;
    private Ray updateRay;
    private RaycastHit updateHit;
    public GameObject RadialMenue;
    public GameObject cam;
    public static GameObject Inventory;
    public GameObject InventoryCollision;
    public GameObject UICanvas;
	public GameObject Machine;
    public Slot MachineSlot;
    public GameObject player;
    public GameObject mesh;


    #region IBeginDragHandler implementation

    public void OnBeginDrag(PointerEventData eventData)
    {
		itemBeingDragged = gameObject;
        draggingItem = true;
		startPosition = transform.position;
		startParent = transform.parent;
		GetComponent<CanvasGroup>().blocksRaycasts = false;

		RadialMenue = GameObject.Find("RadialMenue");
        cam = GameObject.Find("Main Camera");
		Inventory = GameObject.Find("InventoryMenue");
		UICanvas = GameObject.Find("Canvas");
		Machine = GameObject.Find("Machine");
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
            Debug.Log("Raycast hitto: " + hit.transform.name);
            #region Gussformen
            ////-----------------------------------Pleuel Form-------------------------------------------
            //if (itemBeingDragged.name.StartsWith("60%") && hit.transform.name == "pleuelForm")
            //{
            //    Debug.Log("Befuelle Form mit Legierung // richtige Form // richtige Legierung");
            //}
            //else if (itemBeingDragged.name.StartsWith("40%") && hit.transform.name == "pleuelForm")
            //{
            //    Debug.Log("Befuelle Form mit Legierung // richtige Form // falsche Legierung");
            //}
            //else if (itemBeingDragged.name.StartsWith("80%") && hit.transform.name == "pleuelForm")
            //{
            //    Debug.Log("Befuelle Form mit Legierung // richtige Form // falsche Legierung");
            //}
            //else if (itemBeingDragged.name.StartsWith("20%") && hit.transform.name == "pleuelForm")
            //{
            //    Debug.Log("Befuelle Form mit Legierung // richtige Form // falsche Legierung");
            //}
            ////-----------------------------------Zahnrad Form-----------------------------------------
            //else if (itemBeingDragged.name.StartsWith("60%") && hit.transform.name == "zahnradForm")
            //{
            //    Debug.Log("Befuelle Form mit Legierung // falsche Form // richtige Legierung");
            //}
            //else if (itemBeingDragged.name.StartsWith("40%") && hit.transform.name == "zahnradForm")
            //{
            //    Debug.Log("Befuelle Form mit Legierung // falsche Form // falsche Legierung");
            //}
            //else if (itemBeingDragged.name.StartsWith("80%") && hit.transform.name == "zahnradForm")
            //{
            //    Debug.Log("Befuelle Form mit Legierung // falsche Form // falsche Legierung");
            //}
            //else if (itemBeingDragged.name.StartsWith("20%") && hit.transform.name == "zahnradForm")
            //{
            //    Debug.Log("Befuelle Form mit Legierung // falsche Form // falsche Legierung");
            //}
            ////-----------------------------------??? Form---------------------------------------
            //else if (itemBeingDragged.name.StartsWith("60%") && hit.transform.name == "???Form")
            //{
            //    Debug.Log("Befuelle Form mit Legierung // falsche Form // richtige Legierung");
            //}
            //else if (itemBeingDragged.name.StartsWith("40%") && hit.transform.name == "???Form")
            //{
            //    Debug.Log("Befuelle Form mit Legierung // falsche Form // falsche Legierung");
            //}
            //else if (itemBeingDragged.name.StartsWith("80%") && hit.transform.name == "???Form")
            //{
            //    Debug.Log("Befuelle Form mit Legierung // falsche Form // falsche Legierung");
            //}
            //else if (itemBeingDragged.name.StartsWith("20%") && hit.transform.name == "???Form")
            //{
            //    Debug.Log("Befuelle Form mit Legierung // falsche Form // falsche Legierung");
            //}
            #endregion
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
            if (hit.transform.CompareTag("Player") && itemBeingDragged.name.Contains("Labcoat"))
            {
                if (itemBeingDragged.name.Contains("Labcoat"))
                {
                    player = GameObject.Find("Player");
                    mesh = player.transform.Find("LabCoat").gameObject;
                    mesh.SetActive(true);
                    Destroy(itemBeingDragged);
                }
            }
            else if (hit.transform.CompareTag("Player") && itemBeingDragged.name.Contains("Glove"))
            {
                if (itemBeingDragged.name.Contains("Glove"))
                {
                    player = GameObject.Find("Player");
                    mesh = player.transform.Find("Glove_Left").gameObject;
                    mesh.SetActive(true);
                    Destroy(itemBeingDragged);
                }
            }
        }

        itemBeingDragged = null;
        draggingItem = false;
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        if (!Inventory.activeSelf && Slot.otherSlot == false)
        {
            transform.position = startPosition;
            transform.SetParent(startParent);
        }
        //if (transform.parent != startParent)
        //{
        //    transform.position = startPosition;
        //    transform.SetParent(transform.parent);
        //}

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