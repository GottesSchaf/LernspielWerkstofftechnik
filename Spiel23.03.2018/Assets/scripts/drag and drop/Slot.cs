using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
            return null;
        }
    }

    #region IDropHandler implementation
    public void OnDrop(PointerEventData eventData)
    {
        if (!item)
        {
            Debug.Log("Hier ist noch kein Item im Slot " + this.gameObject.name);
            DragHandeler.itemBeingDragged.transform.SetParent(transform);
            //ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject, null, (x, y) => x.HasChanged());
        }
        Debug.Log("Hier ist schon ein Item im Slot " + this.gameObject.name);
    }
    #endregion
}