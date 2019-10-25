using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;


public class GoalScript : MonoBehaviour
{

    public Text scoreText;
    public int score;
    [Tooltip("Colocar aqui os prefab da bola")]
    public GameObject BallPrefab;
    private ParticleSystem Confetti;
    
    void Start()
    {
        score = 0;
        UpdateScore();
		Confetti = gameObject.GetComponentInChildren<ParticleSystem>();
    }


    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Gols: " + score;
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Ball")
        {

            AddScore(1);
            other.transform.position = new Vector2(0, 0);
            other.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -1) * 10, ForceMode2D.Impulse);
            Confetti.Play();
        }

    }

}
