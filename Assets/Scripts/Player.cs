using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento de la paleta
    public string inputAxis = "Vertical"; // Eje de entrada (por ejemplo, "Vertical" o "Horizontal")
    public float boundary = 4.5f; // Límite superior e inferior del movimiento de la paleta
    public float positionX;
    public float positiony;
    private Rigidbody2D rb;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        resetPosition();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Vector2 movement = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            movement = new Vector2(0, 1) * moveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movement = new Vector2(0, -1) * moveSpeed * Time.deltaTime;
        }
        //Debug.Log(rb.position);
        // Mover la paleta
        rb.MovePosition(rb.position + movement);

    }

    public void resetPosition()
    {
        rb.position = new Vector2(positionX,positiony);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Cambiar dirección horizontal al colisionar con paletas
        if (collision.gameObject.CompareTag("ball"))
        {

            audioSource.Play();
        }

    }

} 
