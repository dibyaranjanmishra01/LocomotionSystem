using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class motion : MonoBehaviour
{

    public Animator anim;
    public Camera camera;
    public float rotation_speed = 6f;
    public bool IKActive = true;
    public GameObject head;

    private float val, val_2;
    private Vector3 movement_vector;
    private bool walk;
    float head_turn_velo = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        val = 0.1f;
        val_2 = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
        float horizontal_input = Input.GetAxisRaw("Horizontal");
        float vertical_input = Input.GetAxisRaw("Vertical");
        bool crouch = Input.GetButton("crouch");
        bool run = Input.GetButton("run");
        walk = Input.GetButton("Horizontal") | Input.GetButton("Vertical");

        anim.SetBool("move", walk);
        anim.SetBool("crouch", crouch);

        float magnitude = new Vector3(horizontal_input, vertical_input).magnitude;
        movement_vector = camera.transform.forward * vertical_input + camera.transform.right * horizontal_input; movement_vector.y = 0f;

        if (walk)
        {
            //val = (val < 0.1f) ? (val + 0.5f * Time.deltaTime) : 0.1f;
            val_2 = (run && val_2 < 1f) ? (val_2 + 1f * Time.deltaTime) : val_2;
            if (!run) val_2 = (val_2 > 0f) ? (val_2 - 1f * Time.deltaTime) : 0f;
            if (val + val_2 > 0.09f)
            {
                float angle = Quaternion.Angle(transform.rotation, Quaternion.LookRotation(movement_vector));
                if (Mathf.Abs(angle) > 150f)
                {
                    if (run)
                    {
                        anim.SetBool("run_180", true);
                    }
                    else anim.SetBool("walk_180", true);
                }
                else
                {
                    anim.SetBool("walk_180", false );
                    anim.SetBool("run_180", false);
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement_vector), rotation_speed * Time.deltaTime); //set rotation according to camera position
                }
            }
        }
        else
        {
            //val = (val>0f) ? (val - 0.5f * Time.deltaTime) : 0f;
            val_2 = (val_2>0f)? (val_2 - 1.6f*Time.deltaTime) :0f;
        }
        anim.SetFloat("magnitude", val+val_2);
    }

    bool AnimatorIsPlaying(string stateName)
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }

    private void OnDrawGizmos()
    {
        DrawArrow.ForGizmo(transform.position,movement_vector,Color.black);
        DrawArrow.ForGizmo(transform.position,transform.forward, Color.green);
        Gizmos.DrawSphere(head.transform.position+movement_vector, 0.05f);
    }

    void OnAnimatorIK()
    {
        if (anim)
        {
            if (IKActive)
            {
                anim.SetLookAtWeight(Mathf.SmoothDamp(0,1f,ref head_turn_velo,0.3f));
                if(walk)
                anim.SetLookAtPosition(head.transform.position+movement_vector);
            }
            else {
                anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                anim.SetLookAtWeight(0);
            }
        }
    }

}
