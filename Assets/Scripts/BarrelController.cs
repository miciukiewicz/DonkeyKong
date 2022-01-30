using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelController : MonoBehaviour
{
    new Rigidbody2D rigidbody;
    bool facingRight;

    private void Start() {
        facingRight = true;
        rigidbody = GetComponent<Rigidbody2D>();        
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Platform") {
            if(facingRight) {
                rigidbody.velocity = new Vector2(3, 0);
                facingRight = false;
            } else {
                rigidbody.velocity = new Vector2(-3, 0);
                facingRight = true;
            }
        } else if(collision.gameObject.tag == "Oil") {
            Destroy(this.gameObject);
        }
    }
}
