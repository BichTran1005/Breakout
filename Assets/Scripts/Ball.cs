using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : Singleton<Ball>
{
    private Rigidbody2D rb;
    private float speed = 30f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void SetRandomTrajectory()
    {
        Vector2 force = Vector2.zero;
        force.x = Random.Range(-1f, 1f);
        force.y = -1f;

        rb.velocity = (force.normalized * speed);
    }

    public void ResetBallPosition()
    {
        rb.position = Vector2.zero;
        rb.velocity = Vector2.zero;
    }

    public void StopBall()
    {
        rb.velocity = Vector2.zero;
    }
    
    public void ReStartBall()
    {
        Invoke("SetRandomTrajectory", 1f);
    } 
    
    public void SetSpeed(float s)
    {
        speed = s;
    }
}
