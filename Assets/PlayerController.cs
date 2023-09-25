using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public ContactFilter2D movementFilter;
    public float collisionOffset = 0.05f;

    Vector2 movementInput;
    Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // if movement  is not 0, try not to move
        if (movementInput != Vector2.zero)
        {
            /*
            movementInput (direction) - X and Y values between - 1 and 1 that reprsent the direction from the body to look for collisions
            movementFilter - The settings that determine where a collision can occur on such as layers to collide with 
            castCollisions - List of collisions to store the found collisons inyo after the Cast is finished
            moveSpeed * Time.fixedDeltaTime + collisionsOffset - The ammount to cast equal to the movement plus the offset (dont get stuck in the wall), time between the frame (fixed deltatime)
            */
            int count = rb.Cast(movementInput, movementFilter, castCollisions, moveSpeed * Time.fixedDeltaTime + collisionOffset);

            if (count == 0)
            {
                rb.MovePosition(rb.position + movementInput * moveSpeed * Time.fixedDeltaTime);
            }

        }

    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }
}
