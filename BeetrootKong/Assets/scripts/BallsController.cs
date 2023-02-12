using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsController : MonoBehaviour
{
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.name == "RightWall"){
            rb.velocity = new Vector2(-2.5f, 0);
        }
        if(other.gameObject.name == "LeftWall"){
            rb.velocity = new Vector2(2.5f, 0);
        }
        if(other.gameObject.name == "KillBalls"){
            Destroy(gameObject);
        }
    }
}
