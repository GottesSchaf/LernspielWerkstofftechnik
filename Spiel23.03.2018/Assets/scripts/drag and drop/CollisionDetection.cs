using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour {

    bool isCollidingBlue, isCollidingRed;
    GameObject UICanvas;

    private void Start()
    {
        UICanvas = GameObject.Find("Canvas");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("redCube") && DragHandeler.itemBeingDragged.name.Contains("blue"))
        {
            Debug.Log("Item blau über Item rot");
            isCollidingRed = true;
        }
        else if (collision.CompareTag("blueCube") && DragHandeler.itemBeingDragged.name.Contains("red"))
        {
            Debug.Log("Item rot über Item blau");
            isCollidingBlue = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("OnTriggerExit2D");
        if (collision.CompareTag("Inventory"))
        {
            DragHandeler.Inventory.SetActive(false);
            DragHandeler.itemBeingDragged.transform.SetParent(UICanvas.transform);
        }
    }
}
