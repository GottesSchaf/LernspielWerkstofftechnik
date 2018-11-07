using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private SpriteRenderer spriteToChange;
    [SerializeField] private Sprite[] tutSprites;
    [SerializeField] private GameObject target;

    [SerializeField] private Camera tutCam;
    [SerializeField] private Camera playerCam;

    [SerializeField] private GameObject door;
    [SerializeField] private GameObject table;
    [SerializeField] private GameObject cube;

    [SerializeField] private GameObject invOpen;
    [SerializeField] private GameObject invMenu;
    [SerializeField] private GameObject[] invSlots;

    [SerializeField] private GameObject Helpbtn;
    [SerializeField] private GameObject Screenshot;
    [SerializeField] private GameObject Speed1;
    [SerializeField] private GameObject Speed2;
    [SerializeField] private GameObject Questwindow;

    [SerializeField] private GameObject pauseScreen;

    [SerializeField] private GameObject[] Buttons;

    public Vector3 destination;

    private GameObject toDelete;

    private int slot = -1;

    private bool step2Done;
    private bool step3Done;
    public static bool step4Done;
    private bool step5Done;
    /*
     1. Willkommen Screen 
     2. > Weiter drücken auf Button (schon drinne) 
     3. "Willst du eine Einweisung?" - Screen 
     4. Aufploppen von zwei Buttons -> JA / NEIN 
     4.1. WENN JA: Laufen Screen 
     4.2. WENN NEIN: Starte Spiel 
     5. Laufen Screen -> Roter Kreis (target) aktiviert sich 
     -> Spieler muss reinlaufen 
     6. Wenn Spieler drinne: Pick Up Screen 
     -> Tisch mit Cube taucht auf 
     -> Spieler muss Cube anklicken (aufheben, script ist auch drinne) 
     7. Wenn Cube aufgehoben: Inventar Screen (v.w. das ist dein Inventar)
     8. Drag and Drop Screen (noch nicht drin als Sprite) 
     9. Pausenscreen (wenn P gedrückt wurde, dann weiter zu 10.) 
     10. Das sind deine Buttons (CANVAS BUTTONS) Screen (SPRITE) 
     11. Tür ploppt auf 
     -> SPieler geht raus und beendet Tutorial und "QUESTWINDOW" öffnet sich
    */

    private void Awake()
    {
        step2Done = false;
        step3Done = false;
        step4Done = false;
        step5Done = false; 
    }

    private void Update()
    {
        if(Player.GetComponent<CheckCollision>().HitTarget && !step2Done)
        {
            spriteToChange.sprite = tutSprites[2];
            target.SetActive(false);
            step2Done = true;
            table.SetActive(true);
            cube.SetActive(true);
        }

        if(step2Done && !step3Done)
        {
            foreach(GameObject x in invSlots)
            {
                slot++;
                if(x.GetComponentInChildren<DragHandeler>())
                {
                    table.SetActive(false);
                    step3Done = true;
                    spriteToChange.sprite = tutSprites[3];
                    toDelete = x;
                    break;
                }
            }
        }

        if(step2Done && step3Done && !step4Done)
        {
            spriteToChange.sprite = tutSprites[4]; // In der Tasche sind deine Objekte (Bild)
            invOpen.SetActive(true);               // Icon für Inventar taucht auf
            foreach (GameObject x in invSlots)
            {
                
                if (x.GetComponentInChildren<DragHandeler>())
                {
                    if(x == invSlots[slot])
                    {
                        UI.tutorialinventory = true; 
                    }
                    else
                    {
                        invMenu.SetActive(false);
                        invOpen.SetActive(true);
                        Destroy(toDelete);
                        step4Done = true;
                    }
                    break;
                }
            }
        }
        
        if(step4Done && !step5Done)
        {
            spriteToChange.sprite = tutSprites[5];
            if (pauseScreen.activeSelf)
            {
                spriteToChange.sprite = tutSprites[6];
            }
            step5Done = true;
        }
        
    }

    // Change Sprite
    public void Forward()
    {
        spriteToChange.sprite = tutSprites[0];
        Buttons[0].SetActive(false);
        Buttons[1].SetActive(true);
        Buttons[2].SetActive(true);
    }

    // Button "Ja" pressed
    public void Yes()
    {
        tutCam.enabled = false;
        playerCam.enabled = true;
        spriteToChange.sprite = tutSprites[1];
        Buttons[1].SetActive(false);
        Buttons[2].SetActive(false);
        target.SetActive(true);
    }

    // Button "No" pressed
    public void No()
    {
        tutCam.enabled = false;
        playerCam.enabled = true;
        Buttons[1].SetActive(false);
        Buttons[2].SetActive(false);
        door.SetActive(true);
        agent.Warp(destination);
        Helpbtn.SetActive(true);
        Screenshot.SetActive(true);
        invOpen.SetActive(true);
        Speed1.SetActive(true);
        Speed2.SetActive(true);
        Questwindow.SetActive(true);
    }
}
