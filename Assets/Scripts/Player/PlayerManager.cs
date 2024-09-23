using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public Transform ball;
    public Animator anim;
    public float speed = 6f;
    public float dashSpeed = 20f; 
    public float dashDuration = 0.2f;
    public float knockbackForce = 15f; // Fuerza del golpe
    public float knockbackDuration = 0.2f; // Duración del knockback
    public float turnSmoothTime = 0.1f;
    public float rotationSpeed = 100f;
    float turnSmoothVelocity;
    private bool isGrounded = true;
    private bool isDashing = false;
    private float dashTime;
    private float knockbackTime;
    public static PlayerManager playerManager;

    private void Awake()
    {
        if (playerManager == null) playerManager = this;
        else Destroy(this);
    }
    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

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

        if (direction.magnitude >= 0.1f && !isKnockedBack)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            float currentSpeed = isDashing ? dashSpeed : speed;
            controller.Move(moveDir.normalized * currentSpeed * Time.deltaTime);

            Vector3 rotationDirection = new Vector3(vertical, 0f, -horizontal);
            ball.Rotate(rotationDirection * rotationSpeed * Time.deltaTime);
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Jump");
            ball.rotation = Quaternion.identity;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    {
        if (!isKnockedBack)
        {
            isKnockedBack = true;
            knockbackTime = knockbackDuration;
        }
    }
}
