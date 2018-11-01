using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class playerscript : MonoBehaviour
{
    public static int points;

    public Animator animator = null;

    public Vector3 tempPos;

    private void Start()
    {
        animator = GetComponent<Animator>();
        tempPos = this.gameObject.transform.position;
    }

    public void Update()
    {
        if(this.gameObject.transform.position == tempPos)
        {
            animator.SetBool("isWalking", false);
            tempPos = this.gameObject.transform.position;
        }
        else if(this.gameObject.transform.position != tempPos)
        {
            animator.SetBool("isWalking", true);
            tempPos = this.gameObject.transform.position;
        }
    }
}
