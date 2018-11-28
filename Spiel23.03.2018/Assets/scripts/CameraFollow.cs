using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform playerToFollow;
    public static CameraFollow instance;
    public bool closeupInteraction = false;
    public Transform CloseupBack;
    public GameObject Camera;


    void Start()
    {
        instance = this; 
    }
    void Update()
    {
        if(closeupInteraction == true)
        {
            CloseupBack.gameObject.SetActive(true);
            playerToFollow.GetComponent<SkinnedMeshRenderer>().enabled = false;
			//playerToFollow.Find("clothes_green").GetComponent<MeshRenderer>().enabled = false;
            Camera = GameObject.Find("Main Camera");

            if (Camera.transform.position.z < -6)
            {
                Vector3 newPosition = transform.position;
                newPosition = Camera.transform.position;
                newPosition.z += 1f;
                transform.position = newPosition;
            }
        }
        else if (playerToFollow)
        {
            Vector3 newPosition = transform.position;
            newPosition.x = playerToFollow.position.x;
			newPosition.y = playerToFollow.position.y +1f;
            newPosition.z = -7;
            transform.position = newPosition;
        }
    }
}