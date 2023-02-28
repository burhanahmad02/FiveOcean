using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DirectionCollider : MonoBehaviour
{
    public float force;
    public enum Dir
    {
        Left,
        Right
    }
    public Dir direction;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (direction == Dir.Right)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * force);
        }
        else
        if (direction == Dir.Left)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * force);
        }
    }
}