using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanYouMove : MonoBehaviour
{
    // Start is called before the first frame update
    float speedX, speedY;
    public float movSpeed;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        speedX = Input.GetAxisRaw("Horizontal") * movSpeed;
        speedY = Input.GetAxisRaw("Vertical") * movSpeed;
        rb.velocity = new Vector2(speedX, speedY);
    }
}
