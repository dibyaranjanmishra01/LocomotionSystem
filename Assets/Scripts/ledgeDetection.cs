using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ledgeDetection : MonoBehaviour
{
    //public GameObject game;

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position,transform.forward,out hit,5f))
        {
            Debug.DrawRay(transform.position+Vector3.up, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            float distance = hit.distance;
            Debug.Log(distance);
        }
    }
    private void OnDrawGizmos()
    {
        
    }
}
