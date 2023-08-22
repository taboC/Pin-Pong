using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class GameControllerP : MonoBehaviour
{
    [SerializeField] GameObject prefBal;
    [SerializeField] Player player1;
    [SerializeField] Player2 player2;
    [SerializeField] TextMeshProUGUI TextpuntajeP1;
    [SerializeField] TextMeshProUGUI TextpuntajeP2;
    [SerializeField] TextMeshProUGUI GameOver;
    [SerializeField] int cantidadBall=1;
    [SerializeField] GameObject[] gm=new GameObject[3];
    [SerializeField] float velocityBall;
    public Button btnReiniciar;
    public Button btnGoToMenu;
    private AudioSource audioSource;
    int puntaje1, puntaje2;
    [SerializeField] public int finalScore;
    float tiempoPasado=0.0f;
    bool executeTime;

    bool isPosibleImpulse;

    public void increaseScore(int p)
    {
        isPosibleImpulse = true;
        switch (p) {
            case 1: 
                puntaje1 += 1; 
                TextpuntajeP1.text=puntaje1 + " ";
                if (puntaje1 >= finalScore)
                {
                    gameOver("Player1");
                }
                else
                {
                    player1.resetPosition();
                    player2.resetPosition();
                    doInstantiate(6.67f);
                }
                break;
            case 2:
                puntaje2 += 1;
                TextpuntajeP2.text = puntaje2 + " ";
                if (puntaje2 >= finalScore)
                {
                    gameOver("Player2");
                }
                else
                {
                    player1.resetPosition();
                    player2.resetPosition();
                    doInstantiate(-6.67f);
                }
                break;
        }
    }

    private void Start()
    {
        reiniciar();
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        audioSource.loop = true;
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.X) && isPosibleImpulse)
        {
            impulse();
        }

        //Aumentar velocidad
        tiempoPasado += Time.deltaTime;

        if (Mathf.FloorToInt(tiempoPasado) % 10 ==0 && !executeTime)
        {
            velocityBall += 2;
            executeTime = true;
            for (int i = 0; i < cantidadBall; i++)
            {
                if (gm[i] != null)
                {
                    //int inpulse = gm[i].GetComponent<Rigidbody2D>().transform.position.x > 0 ? -1 : 1;
                    //float direccionHorizontal = Random.Range(0, 2) == 0 ? -1f : 1f; // Dirección aleatoria
                    //gm[i].GetComponent<Rigidbody2D>().velocity = new Vector2(velocityBall * inpulse, direccionHorizontal);
                    gm[i].GetComponent<Ball>().force = velocityBall;
                }
            }
        }

        if (Mathf.FloorToInt(tiempoPasado) % 5 != 0)
        {
            executeTime = false;
        }
    }

    private void doInstantiate(float positionBall)
    {
        gm[cantidadBall - 1] = null;
        //prefBal.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        for (int i=0; gm.Length>i;i++)
        {
            if (gm[cantidadBall-1]!=null )
            {
                break;
            }
            gm[i]= Instantiate(prefBal, new Vector2(positionBall, -0.91f), Quaternion.identity);
            gm[i].GetComponent<Ball>().force = velocityBall;
        }
        
    }

    private void gameOver(string player)
    {
        GameOver.text = "Fin del Juego, Ha ganado el: " + player;
        GameOver.enabled = true;
        btnReiniciar.gameObject.SetActive(true);
        btnGoToMenu.gameObject.SetActive(true);
    }

    private void impulse()
    {
        isPosibleImpulse = false;
        for (int i = 0; i < cantidadBall; i++)
        {
            if (gm[i] != null)
            {
                int inpulse = gm[i].GetComponent<Rigidbody2D>().transform.position.x > 0 ? -1 : 1;
                float direccionHorizontal = Random.Range(0, 2) == 0 ? -1f : 1f; // Dirección aleatoria
                gm[i].GetComponent<Rigidbody2D>().velocity = new Vector2(velocityBall * inpulse, direccionHorizontal);
            }
        }
        
    }

    public void reiniciar()
    {
        puntaje1 = 0;
        puntaje2 = 0;
        TextpuntajeP1.text = puntaje1 + " ";
        TextpuntajeP2.text = puntaje2 + " ";
        velocityBall = 5;
        GameOver.enabled = false;
        doInstantiate(0);
        impulse();
        btnReiniciar.gameObject.SetActive(false);
        btnGoToMenu.gameObject.SetActive(false);
    }

    public void goToMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

}
