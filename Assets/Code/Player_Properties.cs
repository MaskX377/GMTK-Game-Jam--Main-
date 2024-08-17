using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Properties : MonoBehaviour
{
    Rigidbody rb;

    public Transform Camera;
    public Transform GroundCheck;

    Vector3 mousePosition;

    float groundCheckRadius = 0.1f;
    public float speed;
    public float walkSpeed;
    public float runSpeed;
    public float jumpForce;
    public float mouseSensitivity = 100f;
    public float xRotationMaxAngle;
    float xRotation;
    float yRotation;

    public int maxNumberOfJumps = 2;
    public int numberOfJumpsLeft;

    public LayerMask groundLayer;

    bool IsGrounded()
    {
        return Physics.CheckSphere(GroundCheck.position, groundCheckRadius, groundLayer);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
        }
        else
        {
            speed = walkSpeed;
        }

        if (gameObject != null)
        {
            CameraMovement();
            Jump();
        }
    }

    void FixedUpdate()
    {
        if (gameObject != null)
        {
            Movement();
        }
    }

    void CameraMovement()
    {
        
    }

    void Movement()
    {
        Vector3 move = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            move += transform.forward * speed * Time.fixedDeltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            move += -transform.forward * speed * Time.fixedDeltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            move += -transform.right * speed * Time.fixedDeltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            move += transform.right * speed * Time.fixedDeltaTime;
        }

        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);
    }

    void Jump()
    {
        if (IsGrounded())
        {
            numberOfJumpsLeft = maxNumberOfJumps;
        }
        if (Input.GetKeyDown(KeyCode.Space) && numberOfJumpsLeft > 0)
        {
            numberOfJumpsLeft--;
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // Reset vertical velocity
            rb.velocity = transform.up * jumpForce;
        }
    }
}