using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBar : Singleton<PlayerBar>
{
    private Rigidbody2D rb;
    private bool isDrag = false;
    private Vector2 offset = Vector2.zero;
    private float horizontal = 0f;

    public float Speed = 30f;
    public float MaxBounceAngle = 75f;

    public float MaxX;
    public float MinX;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        ResetPlayerBarPosition();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        transform.position = new Vector2(transform.position.x + (horizontal * Speed * Time.deltaTime), transform.position.y);

        if (isDrag)
        {
            transform.position = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x + offset.x, transform.position.y);
        }

        transform.position = new Vector2
              (
              Mathf.Clamp(transform.position.x, MinX, MaxX),
              transform.position.y
              );
    }

    private void OnMouseDown()
    {
        isDrag = true;
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        isDrag = false;
    }

    public void ResetPlayerBarPosition()
    {
        rb.velocity = Vector2.zero;
        transform.position = new Vector2(0f, transform.position.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ball"))
        {
            return;
        }

        Rigidbody2D ball = collision.gameObject.GetComponent<Rigidbody2D>();
        Collider2D bar = collision.otherCollider;

        Vector2 ballDirection = ball.velocity.normalized;
        Vector2 contactDistance = bar.bounds.center - ball.transform.position;

        float bounceAngle = (contactDistance.x / bar.bounds.size.x) * MaxBounceAngle;
        ballDirection = Quaternion.AngleAxis(bounceAngle, Vector3.forward) * ballDirection;

        ball.velocity = ballDirection * ball.velocity.magnitude;
    }
}
