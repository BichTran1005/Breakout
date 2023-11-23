using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private int health = 1;
    private int point = 1;
  

    public void InitData(int h, int p)
    {
        health = h;
        point = p;
    }    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null && collision.gameObject.CompareTag("Ball"))
        {
            BallCollide();
        }
    }

    private void BallCollide()
    {
        health--;

        GameManager.Instance.BallHitBrick(point);

        if (health <= 0)
        {
            ObjectPool.Instance.Return(gameObject);
        }
    }
}
