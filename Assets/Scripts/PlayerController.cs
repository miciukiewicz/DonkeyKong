using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField]
    private float playerSpeed = 1f;
    [SerializeField]
    private float jumpStrenght = 1f;

    bool isGrounded;
    bool isClimbing;

    private Vector2 directionToMove;
    new Rigidbody2D rigidbody;
    new CapsuleCollider2D collider;

    private void Start() {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        collider = gameObject.GetComponent<CapsuleCollider2D>();
    }

    private void Update() {
        collisionCheck();
        movePlayer();
    }
    private void FixedUpdate() {
        rigidbody.MovePosition(rigidbody.position + directionToMove * Time.fixedDeltaTime);
    }

    private void movePlayer() {
        if(isClimbing) {
            directionToMove.y = Input.GetAxis("Vertical") * playerSpeed;
        } else if(Input.GetButtonDown("Jump") && isGrounded) {
            directionToMove = Vector2.up * jumpStrenght;
        } else {
            directionToMove += Physics2D.gravity * Time.deltaTime;
        }

        directionToMove.x = Input.GetAxis("Horizontal") * playerSpeed;

        if(isGrounded) {
            directionToMove.y = Mathf.Max(directionToMove.y, -1f);
        }
    }

    private void collisionCheck() {
        isGrounded = false;
        isClimbing = false;

        Vector2 colliderSizeChanged = collider.bounds.size;
        colliderSizeChanged.y += 0.1f;
        colliderSizeChanged.x = 0.2f;

        Collider2D[] colliders = Physics2D.OverlapBoxAll(gameObject.transform.position, colliderSizeChanged, 0f);

        for(int i = 0; i < colliders.Length; i++) {
            var hit = colliders[i].gameObject;
            if(hit.tag == "Platform") {
                isGrounded = hit.transform.position.y < (gameObject.transform.position.y - 0.2f);
                Physics2D.IgnoreCollision(collider, colliders[i], !isGrounded); //prevent hitting the upper platforms
            } else if(hit.tag == "Ladder") {
                isClimbing = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Barrel") {
            GameManager.ResetLevel();
        } else if(collision.gameObject.tag == "Princess") {
            GameManager.AddPoints();
            GameManager.ResetLevel();
        }
    }


}
