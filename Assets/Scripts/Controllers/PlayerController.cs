using System;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        //private InputSystem playerInput;
        private Rigidbody2D rb;
        public Animator animator;
        SpriteRenderer spriteRenderer;
        public PlayerController player;
        public Interactable interactable;
        [SerializeField] public float speed = 5f;
        private Vector2 moving;
        public GameObject text;

        void Awake()
        {
            //playerInput = new InputSystem();
            rb = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            player = GetComponent<PlayerController>();
            text.SetActive(false);
        }

        void FixedUpdate()
        {
            if (GameController.Instance.state == eState.GAME)
            {
                //Vector2 moveInput = playerInput.Player.Move.ReadValue<Vector2>();
                //rb.velocity = moveInput * speed;

                rb.velocity = speed * moving;

                animator.SetFloat("Speed", rb.velocity.magnitude);

                if (rb.velocity.x > 0) spriteRenderer.flipX = false;
                if (rb.velocity.x < 0) spriteRenderer.flipX = true;
            }

        }
    }
}
