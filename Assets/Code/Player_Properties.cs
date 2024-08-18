using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class Player_Properties : MonoBehaviour
{
    Rigidbody rb;

    Animator anim;

    public Transform Camera;
    public Transform GroundCheck;
    public Transform Hips;

    Vector3 mousePosition;
    public Vector3 movement;

    float groundCheckRadius = 0.1f;
    public float speed;
    public float walkSpeed;
    public float runSpeed;
    public float jumpForce;

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
        anim = GetComponent<Animator>();
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

    void Movement()
    {
        Vector3 move = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            move += Vector3.forward * speed * Time.fixedDeltaTime;
            transform.rotation = Quaternion.Euler(0, Hips.rotation.x + 0, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            move += -Vector3.forward * speed * Time.fixedDeltaTime;
            transform.rotation = Quaternion.Euler(0, Hips.rotation.x + 180, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            move += -Vector3.right * speed * Time.fixedDeltaTime;
            transform.rotation = Quaternion.Euler(0, Hips.rotation.x - 90, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            move += Vector3.right * speed * Time.fixedDeltaTime;
            transform.rotation = Quaternion.Euler(0, Hips.rotation.x + 90, 0);
        }

        movement = move;

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