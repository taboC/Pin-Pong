using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rgb;
    public GameObject obj;
    public GameControllerP gc;
    public float force;
    
    void Start()
    {
        rgb= GetComponent<Rigidbody2D>();
        //StartBall();
        gc= FindObjectOfType<GameControllerP>();    
        obj =gameObject;
    }

    public void ResetBall()
    {
        // Reiniciar la posición y velocidad de la pelota
        rgb.velocity = Vector3.zero;
        transform.position = Vector3.zero;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Cambiar dirección horizontal al colisionar con paletas
        if (collision.gameObject.CompareTag("Player"))
        {

            changeDirection();
        }
        if (collision.gameObject.CompareTag("EnemyDestroy"))
        {
            Debug.Log("aut");
            var playerPuntaje = transform.position.x > 0 ? 1 : 2;
            gc.increaseScore(playerPuntaje);
            Destroy(obj);
        }
        
    }

    private void changeDirection()
    {
        force *= -1;
        float direccionHorizontal = Random.Range(-5, 4); // Dirección aleatoria
        rgb.velocity = new Vector2(force, direccionHorizontal);

        //Debug.Log(force*-1);
    }
}
