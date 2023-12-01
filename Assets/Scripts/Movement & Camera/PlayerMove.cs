using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private bool useGMPositioning = true;

    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    [Header("Jumping")]

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGrounded;
    bool grounded;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    //Is the variable used for actual movement. Is used to calculate the orientaiton of the player and ensure the player is moving along with the camera.
    Vector3 moveDirection;

    //RigidBody
    Rigidbody rb;

    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {

        Application.targetFrameRate = 60;

        //Gets the rigidbody
        rb = GetComponent<Rigidbody>();

        //Freezes the rotation so the player doesn't rotate
        rb.freezeRotation = true;

        readyToJump = true;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (useGMPositioning)
        {
            Vector3 newPos;
            float newOri;
            gm.GetSpawnPos(out newPos, out newOri);

            if (newOri != 999999)
            {
                transform.position = newPos;
                transform.eulerAngles = new Vector3(0, newOri, 0);
            }
        }
        gm.UpdateViewerCount();
        gm.LogEnteredScene(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {

        //grounded
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGrounded);

        MyInput();

        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }

        SpeedControl();
    }

    private void FixedUpdate()
    {
        MovePlayer();

    }

    //Gets the input from the arrow keys from 0-1.
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(resetJump), jumpCooldown);
        }
    }


    //Actually moves the player
    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //grounded
        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //limit velocity if needed

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        //reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

    }

    private void resetJump()
    {
        readyToJump = true;
    }
}
