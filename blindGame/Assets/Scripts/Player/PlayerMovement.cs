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
    public AudioSource win;
    public AudioSource jumpBasic;
    public AudioSource superJump;
    private bool jumpType = true; // true = basic

    [Header("Game Manager")]
    public GameManager gameManager;

    [Header("Game Manager")]
    public Animator mAnimator;

    void Update()
    {
        if (isGrounded)
        {
            mAnimator.Play("Walking");
            rgPlayer.velocity = new Vector2(speed, 0.0f);
            RaycastHit2D hit = Physics2D.Raycast(PlayerOb.transform.position, Vector2.right, 2f, LayerMask.GetMask("Platform"));
            RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(PlayerOb.transform.position.x, PlayerOb.transform.position.y - 0.25f), Vector2.right, 2f, LayerMask.GetMask("Platform"));
            if (hit.collider != null || hit2.collider != null)
            {
                Jumping();
            }

        }

    }

    public void Jumping()
    {
        if (jumpType) jumpBasic.Play();
        else superJump.Play();
        isGrounded = false;
        rgPlayer.velocity = new Vector2(speed * 2f, jump);
        speed = defaultSpeed;
        jump = defaultJump;
        jumpType = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (dead) return;
            switch (other.tag) {
           
            case "jumpCollider": {
                    Jumping();
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
                    speed = speed * 1.74f;
                    break;
            }
            case "superJumpCollider":
            {
                    jumpType = false; ;
                    isGrounded = true;
                    jump = jump * 1.7f;
                    break;
            }
            case "deathCollider":
            {
                    mAnimator.enabled = false;
                    death.Play();
                    isGrounded = true; dead = true;
                    speed = 0;
                    jump = 0;
                    rgPlayer.gravityScale = 20;
                    gameManager.GameOver(true);
                    //PlayerOb.GetComponent<CapsuleCollider2D>().enabled = false;
                    break;
            }
            case "voidCollider":
                {
                    falling.Play();
                    dead = true;
                    speed = 0;
                    jump = 0;
                    gameManager.GameOver(true);
                    break;
                }
            case "winCollider":
                {
                    win.Play();
                    isGrounded = true;
                    dead = true;
                    speed = 0;
                    jump = 0;
                    gameManager.GameOver(false);
                    break;
                }
        }

    }
}
