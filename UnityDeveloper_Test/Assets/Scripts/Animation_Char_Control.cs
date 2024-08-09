using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Char_Control : MonoBehaviour
{
    Animator playerAnim;
    public float speed = 5.0f; 
    public float gravity = -9.81f;
    public float turnSmoothVelocity;
    public float turn_speed = 0;
    public float jumpForce = 1.0f;
    public Transform cam;
    public CharacterController controller;

    private Vector3 velocity;
    private bool isGrounded;
    public ScoreManager scoreManager; // Reference to the ScoreManager

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerAnim = GetComponent<Animator>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small negative value to keep the player grounded
            playerAnim.SetBool("isJumping", false); // Reset jump animation when grounded
        }

        bool isRunning = playerAnim.GetBool("isRunning");
        bool rnng = Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d");
        if(!isRunning && rnng)
        {
            playerAnim.SetBool("isRunning",true);
        }
        if(isRunning && !rnng)
        {
            playerAnim.SetBool("isRunning",false);
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turn_speed);
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;  
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        // Jump functionality
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            playerAnim.SetBool("isJumping", true); // Trigger the jump animation
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered by: " + other.gameObject.name);
        if (other.gameObject.CompareTag("ScoreCube"))
        {
            scoreManager.IncreaseScore(); // Increase score by 1
            other.gameObject.SetActive(false); // Deactivate the cube
        }
    }

}
