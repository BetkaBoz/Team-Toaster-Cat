using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rgPlayer;
    public bool isGrounded = true;
    public float speed = 1.0f;
    public float jump = 3.0f;

    void Update()
    {
        if (isGrounded)
        {
            rgPlayer.velocity = new Vector2(speed, 0.0f);
        }
        
        

    }
    
    private void OnTriggerEnter2D(Collider2D other)
    { 
        Debug.Log(other.tag);
        switch (other.tag) { 
            case "jumpCollider": {
                
                isGrounded = false;
                break;
            }
        case "groundCollider":
            {
               isGrounded = true;
               break;
            }
        case "stickyCollider":
            {
                break;
            }
            case "speedingCollider":
            {
                break;
            }
            case "superJumpCollider":
            {
                break;
            }
            case "deathCollider":
            {
                break;
            }

        }

    }
}
