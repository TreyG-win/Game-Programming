using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class RelativeMovement : MonoBehaviour
{

    [SerializeField] Transform target;
    public float moveSpeed = 6.0f;
    public float rotSpeed = 15.0f;
    public float jumpSpeed = 15.0f;
    public float gravity = -9.8f;
    public float terminalVelocity = -10.0f;
    public float minFall = -1.5f;
    public float pushForce = 3.0f;

    private CharacterController charController;
    private ControllerColliderHit contact;
    private Animator animator;
    private float vertSpeed;

    void Start()
    {
        charController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        vertSpeed = minFall;
    }

    void Update()
    {
        //Sets the movement to be wherever the main camera is facing.
        Vector3 movement = Camera.main.transform.forward;
        bool hitGround = false;
        RaycastHit hit;

        if (vertSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            float check = (charController.height + charController.radius) / 1.9f;
            hitGround = hit.distance <= check;
        }

        float horInput = Input.GetAxis("Horizontal");
        float verInput = Input.GetAxis("Vertical");
        if (horInput != 0 || verInput != 0)
        {
            Vector3 right = target.right;
            Vector3 forward = Vector3.Cross(right, Vector3.up);
           

            movement = (right * horInput) + (forward * verInput);
            movement *= moveSpeed;
            movement = Vector3.ClampMagnitude(movement, moveSpeed);
            animator.SetFloat("Speed", movement.sqrMagnitude);

            if(hitGround)
            {
                JumpAction(animator);

            } else {

                vertSpeed += gravity * 5 * Time.deltaTime;

                if(vertSpeed < terminalVelocity)
                {
                    vertSpeed = terminalVelocity;
                }
                if(contact != null)
                {
                    animator.SetBool("Jumping", true);
                }

                CheckGround(movement);
            }
            movement.y = vertSpeed;

            movement *= Time.deltaTime;
            charController.Move(movement);

            //These two lines prevent the character from rotating forward after moving
            movement.y = 0f;
            movement.Normalize();

            Quaternion direction = Quaternion.LookRotation(movement, Vector3.up);

            transform.rotation = Quaternion.Lerp(transform.rotation, direction,
                rotSpeed * Time.deltaTime);

        }

    }
    //Stores the contact value into an external value
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        contact = hit;

        Rigidbody rb = hit.collider.attachedRigidbody;
        if(rb != null && !rb.isKinematic) {
            rb.velocity = hit.moveDirection * pushForce;
        }
    }
    //If the character is grounded, then the raycast will try to check the ground
    private void CheckGround(Vector3 movement)
    {
        if (charController.isGrounded)
        {
            if (Vector3.Dot(movement, contact.normal) < 0)
            {
                movement = contact.normal * moveSpeed;
            }
            else
            {
                movement += contact.normal * moveSpeed;
            }
        }
    }
    //Allows the character to jump
    private void JumpAction(Animator anim)
    {
        if (Input.GetButtonDown("Jump"))
        {
            vertSpeed = jumpSpeed;
        }
        else
        {
            vertSpeed = minFall;
            anim.SetBool("Jumping", false);
        }
    }
}
