using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 playerInput;
    Rigidbody2D rb;
    public float moveSpeedModifier;

    Animator animator;
    private PlayerController playerController;
    private SpriteRenderer sprite;

    void Awake(){
        //playerController = GetComponent<PlayerController>();
    }
    void Start()
    {
        playerController= GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerInput = new Vector2( Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.velocity = playerInput.normalized * moveSpeedModifier * Mathf.Log(playerController.characteristics[PlayerBuffs.agility]*2);
        if(playerInput.normalized.x<0)
            sprite.flipX =true;
        if(playerInput.normalized.x>0)
            sprite.flipX =false;
        if(playerInput.normalized.x==0 && playerInput.normalized.y==0)
            animator.Play("Base Layer.PlayerIdle");
        else
            animator.Play("Base Layer.Player");
    }
}
