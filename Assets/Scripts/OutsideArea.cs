using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideArea : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null && collision.gameObject.CompareTag("Ball"))
        {
            GameManager.Instance.BallOut();
        }
    }

    
}
