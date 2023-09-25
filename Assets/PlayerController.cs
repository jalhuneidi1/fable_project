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
    Animator animator;
    SpriteRenderer spriteRenderer;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        // if movement  is not 0, try not to move
        if (movementInput != Vector2.zero)
        {
           bool success = TryMove(movementInput);
            if (!success)
            {
                success = TryMove(new Vector2(movementInput.x, 0));

                if (!success)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }
            }

            animator.SetBool("isMoving", success);
        } else
        {
            animator.SetBool("isMoving", false);
        }

        // set movement of sprite to movement direction
        if(movementInput.x < 0)
        {
            spriteRenderer.flipX = true;
        } else if (movementInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        


    }
    private bool TryMove(Vector2 direction) {
        if (direction != Vector2.zero)
        {
            // check for potential collisions
            int count = rb.Cast(
                direction, // movementInput (direction) - X and Y values between - 1 and 1 that reprsent the direction from the body to look for collisions
                movementFilter, // movementFilter - The settings that determine where a collision can occur on such as layers to collide with 
                castCollisions, // castCollisions - List of collisions to store the found collisons into after the Cast is finished
                moveSpeed * Time.fixedDeltaTime + collisionOffset); // moveSpeed * Time.fixedDeltaTime + collisionsOffset - The ammount to cast equal to the movement plus the offset (dont get stuck in the wall), time between the frame (fixed deltatime)

            if (count == 0)
            {
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            //can't move if there is no direction to move in
            return false;
        }
        }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }
}
