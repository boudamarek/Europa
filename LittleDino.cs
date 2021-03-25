using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleDino : MonoBehaviour
{
    public float speed;
    public float chaseSpeed;

    private bool movingRight = true;
    public GameObject target;
    private Vector2 targetPos;
    private int groundLayerMask;
    private int playerLayerMask;
    public Transform groundDetection;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player");
        targetPos = target.transform.position;
        groundLayerMask = LayerMask.GetMask("Ground");
        playerLayerMask = LayerMask.GetMask("Player");

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D playerInfo;
        if(movingRight) {
            playerInfo = Physics2D.Raycast(groundDetection.position, Vector2.right, 15f, playerLayerMask);
        } else {
            playerInfo = Physics2D.Raycast(groundDetection.position, Vector2.left, 15f, playerLayerMask);
        }

        if(playerInfo) {
            Chase();
        } else {
            Patrol();
        }
    }

    void Patrol() {
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 1.5f, groundLayerMask);

        if(groundInfo.collider == false || WallDetection()) {
            if(movingRight == false){
                transform.eulerAngles = new Vector3(0, 0, 0); //doprava
                movingRight = true;
                rb.velocity = new Vector3(speed, 0, 0);
            } else {
                transform.eulerAngles = new Vector3(0, -180, 0); //doleva
                movingRight = false;
                rb.velocity = new Vector3(-speed, 0, 0);
            }
        } else if(movingRight == false) {
            rb.velocity = new Vector3(-speed, 0, 0);
        } else {
            rb.velocity = new Vector3(speed, 0, 0);
        }
    }

    void Chase() {
        if(transform.position.x < targetPos.x) {
            movingRight = true;
            rb.velocity = new Vector3(chaseSpeed, 0, 0);
            transform.eulerAngles = new Vector3(0, 0, 0);
        } else {
            movingRight = false;
            rb.velocity = new Vector3(-chaseSpeed, 0, 0);
            transform.eulerAngles = new Vector3(0, -180, 0);
        }
    }

    private bool WallDetection() {
        RaycastHit2D wallInfo;
        if(movingRight) {
            wallInfo = Physics2D.Raycast(groundDetection.position, Vector2.right, 0.5f, groundLayerMask);
        } else {
            wallInfo = Physics2D.Raycast(groundDetection.position, Vector2.left, 0.5f, groundLayerMask);
        }

        return wallInfo;
    }
}
