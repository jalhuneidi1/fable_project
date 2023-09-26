using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 changePlayerDirection;
    private Animator animator; // reference to animator component
   
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        changePlayerDirection = Vector3.zero;
        //add to change then move player based on change, fixed horizontal and vertical change
        // GetAxisRaw doesn't interpolate but gives us where the new value is
        changePlayerDirection.x = Input.GetAxisRaw("Horizontal");
        changePlayerDirection.y = Input.GetAxisRaw("Vertical");
        // Debug.Log("changePlayerDirection");
        UpdateMovementAnimation();
    }

    void UpdateMovementAnimation()
    {
        if (changePlayerDirection != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", changePlayerDirection.x);
            animator.SetFloat("moveY", changePlayerDirection.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    void MoveCharacter()
    {
        // postion of player + change in position(1) * speed * small amount of time per frame
        myRigidbody.MovePosition(transform.position + changePlayerDirection * speed * Time.deltaTime);
    }
}
