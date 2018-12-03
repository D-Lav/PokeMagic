using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public GameObject playerPhysics;
    public GameObject playerAnimation;
    public AudioClip wall;
    private Animator animator;

    private bool moving = false;
    private float speed = 3;
    private Vector3 targetPosition;
    private BoxCollider2D boxCollider;
    public LayerMask blockingLayer;
    private int xDirection = 0;
    private int yDirection = 0;

    // Use this for initialization
    void Awake()
    {
        animator = playerAnimation.GetComponent<Animator>();
        boxCollider = playerPhysics.GetComponent<BoxCollider2D>();
        blockingLayer = LayerMask.GetMask("BlockingLayer");
    }

    // Update is called once per frame
    void Update()
    {
        GetMoveInput();
        MovePlayer();
    }

    protected bool Move(int xDir, int yDir, out RaycastHit2D hit)
    {
        Vector2 start = playerPhysics.transform.position;
        Vector2 end = start + new Vector2(xDir, yDir);

        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, blockingLayer);
        boxCollider.enabled = true;

        if (hit.transform == null)
        {
            //Debug.Log("no collision!");
            return true;
        }
        Debug.Log("collision detected!");
        SoundManager.instance.PlaySingle(wall);
        return false;
    }

    private void GetMoveInput()
    {
        if (!moving)
        {
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                if (Input.GetAxisRaw("Horizontal") < 0)
                {
                    xDirection = -1;
                }
                if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    xDirection = 1;
                }
                moving = true;
            }
            else
            {
                if (Input.GetAxisRaw("Vertical") != 0)
                {
                    if (Input.GetAxisRaw("Vertical") < 0)
                    {
                        yDirection = -1;
                    }
                    if (Input.GetAxisRaw("Vertical") > 0)
                    {
                        yDirection = 1;
                    }
                    moving = true;
                }
            }
            if (moving)
            {
                RaycastHit2D hit;
                if (Move(xDirection, yDirection, out hit))
                {
                    targetPosition = playerPhysics.transform.position;
                    targetPosition.x += xDirection;
                    targetPosition.y += yDirection;
                    playerPhysics.transform.position = targetPosition;
                    animate(xDirection, yDirection);
                }

            }
            xDirection = 0;
            yDirection = 0;
        }
    }

    private void animate(int x, int y)
    {
        string direction = "";
        if (x == -1) direction = "left";
        if (x == 1) direction = "right";
        if (y == -1) direction = "bottom";
        if (y == 1) direction = "top";
        animator.SetTrigger(direction);
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
