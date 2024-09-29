using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerV2Manager : MonoBehaviour
{
    public float speed = 6.0f;
    public float gravity = -9.81f;   
    public float jumpHeight = 1.5f;   
    public float rotationSpeed = 100f;
    public Transform cam;
    public Transform ball;
    public CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    private bool isDashing = false;
    public float dashDuration = 0.2f, dashTime, dashSpeed = 2.0f;
    private bool isKnockedBack = false;
    public float knockbackDuration = 0.2f, knockbackTime;
    private float knockbackForce;
    public static PlayerV2Manager playerV2Manager;

    private void Awake()
    {
        if (playerV2Manager == null) playerV2Manager = this;
        else Destroy(this);
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(moveX, 0f, moveZ).normalized;

        if(direction.magnitude >= 0.1f && !isKnockedBack)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            float currentSpeed = isDashing ? dashSpeed : speed;
            controller.Move(moveDir * currentSpeed * Time.deltaTime);

            Vector3 rotationDirection = new Vector3(moveZ, 0f, -moveX);
            ball.Rotate(rotationDirection * rotationSpeed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.LeftShift) && direction.magnitude >= 0.1f && !isDashing)
        {
            isDashing = true;
            dashTime = dashDuration;
        }

        if (isDashing)
        {
            dashTime -= Time.deltaTime;
            if (dashTime <= 0f)
            {
                isDashing = false;
            }
        }

        if (isKnockedBack)
        {
            knockbackTime -= Time.deltaTime;
            if (knockbackTime <= 0f)
            {
                isKnockedBack = false;
            }
            else
            {
                controller.Move(-transform.forward * knockbackForce * Time.deltaTime);
                return;
            }
        }
    }

    public void ApplyKnockback(float force)
    {
        if (!isKnockedBack)
        {
            knockbackForce = force;
            isKnockedBack = true;
            knockbackTime = knockbackDuration;
        }
    }
}
