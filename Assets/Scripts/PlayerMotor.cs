using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 moveVector;
    private float verticalVelocity = 0.0f;
    private float gravity = 12.0f;
    private float startTime;

    private float speed = 5.0f;


    private float animationDuration = 4.0f; //camera duration


    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController> ();
        startTime = Time.time;
        
    }

    // Update is called once per frame
    void Update()
    {
        checkyaxis();

        if (isDead)
        {
            return;
        }

        

        if (Time.time -startTime< animationDuration)
        {
            controller.Move((Vector3.forward * speed) * Time.deltaTime);
            return;
        }

        if (controller.isGrounded)
        {
            verticalVelocity = -0.5f;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        moveVector = Vector3.zero;
        //X left, right
        moveVector.x = Input.GetAxisRaw("Horizontal") * speed ;
        //X left, right
        moveVector.y = verticalVelocity;
        //moveVector.y = Input.GetAxisRaw("Vertical") * 2;
        //Z forward backward
        moveVector.z = speed;

        controller.Move(moveVector * Time.deltaTime);
    }

    public void SetSpeed(float modifier)
    {
        speed = 5.0f + modifier;
    }

    //called whenever our capsule collider hits something
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.point.z > transform.position.z + controller.radius)
        {
            Death();
        }
        
    }

    //if player falls
    private void checkyaxis()
    {
        if (transform.position.y <= -4.0f)
        {
            Death();
        }
    }

    private void Death()
    {
        
        //Debug.Log("Death");
        isDead = true;
        GetComponent<Score>().OnDeath();
    }
}
