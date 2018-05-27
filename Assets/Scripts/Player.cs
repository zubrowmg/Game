// Player
using UnityEngine;

public class Player : MonoBehaviour
{
    public int jitter_accuracy = 5;
    public float speed = 2f;
    public float rot_speed = 15f;
    private float sprint = 1f;
    private bool last_turn_left = true;
    private bool last_turn_right;
    public float transV;
    public float transH;
    private bool in_air;
    public int in_air_drag = 2;

    private Vector3 camera_forward;
    private Vector3 player_forward;
    private Vector3 controller;

    private Rigidbody rg;
    public Animator anim;

    private void Start()
    {
        rg = base.GetComponent<Rigidbody>();
        anim = base.GetComponent<Animator>();
    }

    private void Update()
    {
        transV = -Input.GetAxis("Vertical") * speed;
        transH = -Input.GetAxis("Horizontal") * speed;
        controller.x = transH;
        controller.z = transV;
        controller = Vector3.Normalize(controller);
        if (transV > 1.5f || transV < -1.5f || transH > 1.5f || transH < -1.5f)
        {
            sprint = 4f;
        }
        else if (transV > 1f || transV < -1f || transH > 1f || transH < -1f)
        {
            sprint = 2f;
        }
        else
        {
            sprint = 1f;
        }

        camera_forward = base.gameObject.GetComponent<Camera_Control>().forward;
        player_forward = base.transform.right;
        player_forward.y = 0f;
        player_forward = Vector3.Normalize(player_forward);
        float num = Vector3.SignedAngle(player_forward, controller, Vector3.up);


        if (((controller.x < 0.1) && (controller.x > -0.1)) && ((controller.z < 0.1) && (controller.z > -0.1)))
        {
            // No movement
        }
        else 
        {
            PlayerMovement();

            float num2 = Vector3.SignedAngle(-1f * Vector3.forward, controller, Vector3.up);
            float num3 = Vector3.SignedAngle(-1f * camera_forward, player_forward, Vector3.up);
            if (num2 < 0f)
            {
                num2 = 360f - Mathf.Abs(num2);
            }
            if (num3 < 0f)
            {
                num3 = 360f - Mathf.Abs(num3);
            }
            
        }
        
        if (!(transV > 0f))
        {
            return;
        }
    }

    private void rotatePlayer(bool input)
    {
        if (input)
        {
            last_turn_right = true;
            last_turn_left = false;
            base.transform.Rotate(rot_speed * Vector3.up);
        }
        else
        {
            last_turn_right = true;
            last_turn_left = false;
            base.transform.Rotate((0f - rot_speed) * Vector3.up);
        }
    }

    private void rotLeftRight()
    {
    }

    private void OnCollisionStay(Collision col)
    {
        if (col.gameObject.name == "Plane")
        {
            in_air = false;
        }
    }

    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.name == "Plane")
        {
            in_air = true;
        }
    }

    private void PlayerMovement()
    {
        if (!in_air)
        {
            Vector3 new_postion = new Vector3((sprint * transH * Time.deltaTime) + transform.position.x, transform.position.y, (sprint * transV * Time.deltaTime) + transform.position.z);          
            rg.MovePosition(new_postion);
        }
        else
        {
            Vector3 new_postion = new Vector3((sprint * transH * Time.deltaTime)/in_air_drag + transform.position.x, transform.position.y, (sprint * transV * Time.deltaTime)/in_air_drag + transform.position.z);          
            rg.MovePosition(new_postion);
        }
    }
}
