using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanYouMove : MonoBehaviour
{
    public float movSpeed; // Player's normal movement speed
    public Rigidbody2D rb2d; // Rigidbody2D component reference
    private Vector2 moveInput;

    private float activeMoveSpeed;
    public float dashSpeed; // Speed during a dash
    public float dashLength = .5f, dashCooldown = 1f; // Dash duration and cooldown time
    private float dashCounter;
    private float dashCoolCounter;
    private Camera cam;

    void Start()
    {
        // Automatically assign Rigidbody2D if not assigned in the Inspector
        if (rb2d == null)
        {
            rb2d = GetComponent<Rigidbody2D>();

            if (rb2d == null)
            {
                Debug.LogError("No Rigidbody2D component found on this GameObject. Please attach a Rigidbody2D.");
            }
        }

        activeMoveSpeed = movSpeed;
        cam = Camera.main; // Reference to the main camera
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
        if (rb2d != null)
        {
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
}
