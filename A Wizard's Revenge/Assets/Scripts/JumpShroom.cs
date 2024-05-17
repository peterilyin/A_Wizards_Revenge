using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpShroom : MonoBehaviour
{
    public float jumpForce;
    public GameObject playerObj;
    public PlayerController playerScript;

    void Start()
    {
        playerObj = GameObject.FindWithTag("Player");
    }

    
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
