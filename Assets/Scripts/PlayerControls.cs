using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {
    public GameObject playerPhysics;
    public GameObject playerAnimation;
    private Animator animator;

    private bool moving = false;
    private string direction;
    private float speed = 3;
    private Vector3 targetPosition;

    // Use this for initialization
    void Awake()
    {
        animator = playerAnimation.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetMoveInput();
        MovePlayer();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision!");
        moving = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("Collision!");
        moving = false;
    }

    private void GetMoveInput()
    {
        if (!moving)
        {
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                if (Input.GetAxisRaw("Horizontal") < 0)
                {
                    direction = "left";
                }
                if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    direction = "right";
                }
                moving = true;
            }
            else
            {
                if (Input.GetAxisRaw("Vertical") != 0)
                {
                    if (Input.GetAxisRaw("Vertical") < 0)
                    {
                        direction = "bottom";
                    }
                    if (Input.GetAxisRaw("Vertical") > 0)
                    {
                        direction = "top";
                    }
                    moving = true;
                }
            }
            if (moving)
            {
                targetPosition = playerPhysics.transform.position;
                switch (direction)
                {
                    case "left":
                        targetPosition.x -= 1;
                        animator.SetTrigger("left");
                        break;
                    case "right":
                        targetPosition.x += 1;
                        animator.SetTrigger("right");
                        break;
                    case "bottom":
                        targetPosition.y -= 1;
                        animator.SetTrigger("bottom");
                        break;
                    case "top":
                        targetPosition.y += 1;
                        animator.SetTrigger("top");
                        break;
                }
                playerPhysics.transform.position = targetPosition;
            }
        }
    }

    private void MovePlayer()
    {
        if (moving)
        {
            float step = speed * Time.deltaTime;
            playerAnimation.transform.position = Vector3.MoveTowards(playerAnimation.transform.position, targetPosition, step);
            if (playerAnimation.transform.position == targetPosition)
            {
                moving = false;
            }
        }
    }
}
