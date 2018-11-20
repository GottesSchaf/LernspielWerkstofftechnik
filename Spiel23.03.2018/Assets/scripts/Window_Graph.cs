using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window_Graph : MonoBehaviour {
    [SerializeField] Sprite circleSprite;
    [SerializeField] private RectTransform graphContainer;
    private int i = 0;
    //BunsenBrenner bunsenBrenner = new BunsenBrenner();

    private void Awake()
    {       
        Debug.Log(graphContainer);
        //List<int> valueList = new List<int>() { 5, 98, 56, 45, 30, 22, 17, 15, 13, 17, 25, 37, 40, 36, 33 }; //Liste für y Koordinaten (°C)
        //ShowGraph(valueList);
    }

    private GameObject CreatCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(10, 10); //Größe des Punktes
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    public void ShowGraph(float value, int sekunden) //Vorher: ShowGraph(List<int> valueList)
    {
        graphContainer = this.gameObject.GetComponentsInChildren<RectTransform>(true)[1];
        //if(transform.Find("GraphContainer").GetComponent<RectTransform>())
        //{
        //    Debug.Log("Habe dich gefunden!");
        //}
        Debug.Log(graphContainer);
        float graphHeight = graphContainer.sizeDelta.y; //Größe des Graphen
        float yMaximum = 100f; //Maximale Größe des Graphen
        float xSize = sekunden; //Abstand zwischen X Positionen (sekunden)
        GameObject lastCircleGameObject = null; //Letzter Punkt, der erstellt wurde
        if(i < 4) //Vorher: i < valueList.Count
        {
            float xPosition = i * xSize;
            float yPosition = (value / yMaximum) * graphHeight;
            GameObject circleGameObject = CreatCircle(new Vector2(xPosition, yPosition));
            //Falls ein vorheriger Punkt vorhanden, erstelle eine Verbindung
            if(lastCircleGameObject != null)
            {
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
            }
            lastCircleGameObject = circleGameObject; //setze den letzten Punkt zum aktuellen
            i++;
        }
    }

    private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, .5f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0); //Ankerpunkt unten links
        rectTransform.anchorMax = new Vector2(0, 0); //Ankerpunkt unten links
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(dir));
    }

    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }
}
