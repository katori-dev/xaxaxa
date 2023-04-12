using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public float speed = 0.2f;
    public float jumpForce = 1f;
    public float groundedDistance = 0.5f;
    float xRot;
    float yRot;
    public GameObject Camera;
    float sensivity = 5f;

    private Rigidbody rb;
    private bool isGrounded;

    void FixedUpdate()
    {
        MouseMove();
    }

    void MouseMove()
    {
        xRot += Input.GetAxis("Mouse X");
        yRot += Input.GetAxis("Mouse Y");

        Camera.transform.rotation = Quaternion.Euler(-yRot, xRot, 0f);
        this.gameObject.transform.rotation = Quaternion.Euler(0f, yRot, 0f);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        movementDirection = Vector3.ClampMagnitude(movementDirection, 1f);

        rb.AddForce(movementDirection * speed, ForceMode.Impulse);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundedDistance))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            isGrounded = false;
        }
    }
}
