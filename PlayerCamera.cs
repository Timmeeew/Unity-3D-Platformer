using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementScriptV2 : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;
    private Rigidbody rb;
    private bool grounded;
    private Vector2 direction;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float groundDrag;
    [SerializeField] private float airDrag;
    [SerializeField] private float playerHeight;
    [SerializeField] private LayerMask isGround;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!grounded)
        {
            grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, isGround);
            if (grounded)
            {
                rb.AddForce(gameObject.transform.forward * 20, ForceMode.Impulse);
            }
        }

        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, isGround);

        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;

        }
    }

    private void FixedUpdate()
    {
        Vector3 inputDirection = new Vector3(direction.x, 0, direction.y);
        Vector3 moveDirection = transform.TransformDirection(inputDirection);
        rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Force);
    }
    public void JumpAction(InputAction.CallbackContext context)
    {
        if (grounded) //Only jump when on ground
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(Vector3.up * 10, ForceMode.Impulse);
        }
    }
    public void MoveAction(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }
}
