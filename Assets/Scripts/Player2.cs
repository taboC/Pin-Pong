using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento de la paleta
    public string inputAxis = "Vertical"; // Eje de entrada (por ejemplo, "Vertical" o "Horizontal")
    public float boundary = 4.5f; // Límite superior e inferior del movimiento de la paleta
    public float positionX;
    public float positiony;
    private Rigidbody2D rb;
    private AudioSource audioSource;
    public int mode;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        resetPosition();
        mode = PlayerPrefs.GetInt("mode");
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        switch (mode)
        {
            case 1:
                movetoIA(); break;
            case 2:
                moveToKeyBoard(); break; 
            default:
                break;
        }
        // Obtener la entrada del jugador
        

    }

    public void resetPosition()
    {
        rb.position = new Vector2(positionX, positiony);
    }

    void moveToKeyBoard()
    {
        Vector2 movement=new Vector2(0,0);
        if (Input.GetKey(KeyCode.UpArrow))
        {
            movement = new Vector2(0, 1) * moveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            movement = new Vector2(0, -1) * moveSpeed * Time.deltaTime;
        }      

        // Mover la paleta
        rb.MovePosition(rb.position + movement);
    }

    void movetoIA()
    {
        Vector2 movement = transform.position;
        var ass=FindObjectOfType<Ball>();
        if (ass != null)
        {
            movement.y = Mathf.MoveTowards(transform.position.y, ass.transform.position.y, moveSpeed * Time.deltaTime);
            transform.position = movement;

        }
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
