﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    public GameObject item
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
            if (DragHandeler.oldParent.GetChild(0).gameObject != DragHandeler.itemBeingDragged)
            {
                return null;
            }
            return null;
        }
    }

    #region IDropHandler implementation
    public void OnDrop(PointerEventData eventData)
    {
        if (!item)
        {
            DragHandeler.itemBeingDragged.transform.SetParent(transform);
            //ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject, null, (x, y) => x.HasChanged());
            Debug.Log("Innerhalb: " + transform.childCount);
        }
        Debug.Log("Außerhalb: " + transform.childCount);
    }
    #endregion
}