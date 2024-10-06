using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanYouMove : MonoBehaviour
{
    public float movSpeed;
    public Rigidbody2D rb2d;
    private Vector2 moveInput;

    private float activeMoveSpeed;
    public float dashSpeed;
    public float dashLength = .5f, dashCooldown = 1f;
    private float dashCounter;
    private float dashCoolCounter;
    private Camera cam;

    void Start()
    {
        activeMoveSpeed = movSpeed;
        cam = Camera.main;
    }

    void Update()
    {
        // Handle mouse rotation
        Vector3 mousePos = Input.mousePosition;
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        angle -= 90; // Adjust the angle by 90 degrees
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // Handle movement input
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize(); // Prevent diagonal movement from being faster

        // Handle dashing
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dashCoolCounter <= 0 && dashCounter <= 0)
            {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
            }
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;
            if (dashCounter <= 0)
            {
                activeMoveSpeed = movSpeed;
                dashCoolCounter = dashCooldown;
            }
        }

        if (dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        // Apply velocity based on movement input and active speed
        rb2d.velocity = moveInput * activeMoveSpeed;

        // Clamp player position within camera bounds
        Vector3 newPosition = rb2d.position;
        Vector3 camMin = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector3 camMax = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));

        newPosition.x = Mathf.Clamp(newPosition.x, camMin.x, camMax.x);
        newPosition.y = Mathf.Clamp(newPosition.y, camMin.y, camMax.y);

        // Apply clamped position to the Rigidbody
        rb2d.position = newPosition;
    }
}
