using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player")]
    public GameObject PlayerOb;
    public Rigidbody2D rgPlayer;
    public bool isGrounded = true;
    private float speed = 3.0f;
    public float defaultSpeed = 3.0f;
    private float jump = 15.0f;
    public float defaultJump = 15.0f;
    public bool dead = false;


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
        switch (other.tag) {
           
            case "jumpCollider": {
                    Debug.Log("sda");
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
                    Debug.Log("klasika");
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
                    dead = true;
                    speed = 0;
                    jump = 0;
                    PlayerOb.GetComponent<CapsuleCollider2D>().enabled = false;
                    break;
            }
            case "voidCollider":
                {
                    falling.Play();
                    dead = true;
                    speed = 0;
                    jump = 0;
                    rgPlayer.gravityScale = 0;
                    break;
                }
        }

    }
}
