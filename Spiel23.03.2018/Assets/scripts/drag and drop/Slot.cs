using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    public GameObject item
    {
        get
        {
            if (DragHandeler.oldParent.GetChild(0).gameObject != DragHandeler.itemBeingDragged)
            {
                Debug.Log("2 tf: " + transform.childCount);
                return transform.GetChild(0).gameObject;
            }
            else if (transform.childCount > 0)
            {
                Debug.Log("1 tf: " + transform.childCount);
                return transform.GetChild(0).gameObject;
            }
            
            Debug.Log("get item else");
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