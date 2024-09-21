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
    public float turnSmoothTime = 0.1f;

    public float rotationSpeed = 100f;
    float turnSmoothVelocity;
    private bool isGrounded = true;

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);

            Vector3 rotationDirection = new Vector3(vertical, 0f, -horizontal);
            ball.Rotate(rotationDirection * rotationSpeed * Time.deltaTime);
        }

        if(isGrounded && Input.GetKeyDown(KeyCode.Space))
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
}