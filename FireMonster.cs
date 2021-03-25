using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMonster : MonoBehaviour
{
    public float speed;

    private bool movingRight = true;

    public Transform groundDetection;
    private int groundLayerMask;

    void Start() {
        groundLayerMask = LayerMask.GetMask("Ground");
    }

    void Update(){
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 1f);
        if(groundInfo.collider == false || WallDetection()){
            if(movingRight == true){
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            } else {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
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