using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{

    public Animator anim;
    public new Camera camera;

    private float aim_velo = 0.5f;
    private int layerIndex;
    float head_turn_velo = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        layerIndex = 1; // 1
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Debug.Log("Pressed");
            anim.SetLayerWeight(layerIndex, 1f);
            anim.SetBool("shoot", Input.GetMouseButton(0));
        }
        else 
        {
            Debug.Log("NOt Pressed");
            if (anim.GetLayerWeight(layerIndex) != 0 )
            {
                anim.SetLayerWeight(layerIndex, 0);
            }
        }
    }
    private void OnAnimatorIK(int layerIndex)
    {
        if (anim)
        {
            anim.SetLookAtWeight(Mathf.SmoothDamp(0, 1f, ref head_turn_velo, 0.3f));
            anim.SetLookAtPosition(camera.transform.position + 12*camera.transform.forward);
        }
    }
}
