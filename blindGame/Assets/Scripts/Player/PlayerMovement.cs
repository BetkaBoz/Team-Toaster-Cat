using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player")]
    public Rigidbody2D rgPlayer;
    public bool isGrounded = true;
    private float speed = 3.0f;
    public float defaultSpeed = 3.0f;
    private float jump = 15.0f;
    public float defaultJump = 15.0f;


    [Header("Sounds")]
    public AudioSource falling;
    public AudioSource death;
    public AudioSource jumpBasic;
    public AudioSource superJump;
    private bool jumpType = true; // true = basic

    
    void Update()
    {
        if (isGrounded)
        {
            rgPlayer.velocity = new Vector2(speed, 0.0f);
        }
              
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    { 
        //Debug.Log(other.tag);
        switch (other.tag) { 
            case "jumpCollider": {
                    if (jumpType)  jumpBasic.Play() ; 
                    else superJump.Play();
                    isGrounded = false;
                    rgPlayer.velocity = new Vector2(speed*1.5f, jump);
                    speed = defaultSpeed;
                    jump = defaultJump;
                    jumpType = true;
                    break;
            }
            case "groundCollider":
            {
                    isGrounded = true;
                    break;
            }
            case "stickyCollider":
            {
                    isGrounded = true;
                    speed = speed / 1.5f;
                    jump = jump / 1.5f;
                    break;
            }
            case "speedingCollider":
            {
                    isGrounded = true;
                    speed = speed * 2f;
                    break;
            }
            case "superJumpCollider":
            {
                    jumpType = false; ;
                    isGrounded = true;
                    jump = jump * 1.5f;
                    break;
            }
            case "deathCollider":
            {
                    death.Play();
                    isGrounded = true;
                    speed = 0;
                    break;
            }
            case "voidCollider":
                {
                    falling.Play();
                    break;
                }
        }

    }
}
