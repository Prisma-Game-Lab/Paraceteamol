using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    /*[SerializeField] private Text uiText;
    [SerializeField] private float mainTimer;
    private float timer;
    private bool canCount = true;
    private bool doOnce = false;*/

    [Tooltip("Tempo restante.")]
    [SerializeField] private Text uiTimer;
    [Tooltip("Pontuação de game over")]
    [SerializeField] private Text resultado;
    public float startTime;
    private PauseMenu _pauseMenu;
    private float timer;
    private bool canCount = true;
    private bool doOnce = false;

    public GameObject player1;
    public GameObject player2;
    public GameObject EndGame;

    // Start is called before the first frame update
    void Start()
    {
        timer = startTime;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timer >= 0.0f && canCount)
        {
            timer -= Time.deltaTime;

            string minutes = ((int)timer / 60).ToString();
            string seconds = (timer % 60).ToString("f2");

            uiTimer.text = minutes + ":" + seconds;
        }
        else if(timer <= 0.0f && !doOnce)
        {
            canCount = false;
            EndGame.SetActive(true);
             
            Time.timeScale = 0f;
            int scorePlayer1 = player1.GetComponent<GoalScript>().score;
            int scorePlayer2 = player2.GetComponent<GoalScript>().score;
            if(scorePlayer1 > scorePlayer2)
            {
                resultado.text = "Player 1 won";
            }
            else if (scorePlayer1 < scorePlayer2)
            {
                resultado.text = "Player 2 won";
            }
            else
            {
                resultado.text = "Empate"; 
            }
        }
        Time.timeScale = 1f;
    }

    
}
