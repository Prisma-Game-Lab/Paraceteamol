using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
public class MatchScript : MonoBehaviour
{

    private Text _resultado;
    private float _timer;
    private bool _doOnce = false;
    private bool _canCount = true;
    private bool _redconfetti;
    private ParticleSystem Confetti;
    public Text ScoreTextRedGoal;
    public Text ScoreTextBlueGoal;
    public int ScoreRedTeam;
    public int ScoreBlueTeam;
    public AudioSource Goal;
    public float startTime;
    public Text UIText;
    public GameObject RedGoal;
    public GameObject BlueGoal;
    public GameObject EndGame;

    void Start()
    {
        ScoreRedTeam = 0;
        ScoreBlueTeam = 0;
        UpdateScore(ScoreTextRedGoal, ScoreBlueTeam);
        UpdateScore(ScoreTextBlueGoal, ScoreRedTeam);
        Confetti = gameObject.GetComponentInChildren<ParticleSystem>();
        _timer = startTime;
        Time.timeScale = 1f;
    }

    void FixedUpdate()
    {
        if (_timer >= 0.0f && _canCount)
        {

            _timer -= Time.deltaTime;

            UIText.text = "" + Mathf.Ceil(_timer);
        }
        else if (_timer <= 0.0f && !_doOnce)
        {
            _canCount = false;
            EndGame.SetActive(true);

            Time.timeScale = 0f;

            if (ScoreRedTeam > ScoreBlueTeam)
            {
                _resultado.text = "Player 1 won";
            }
            else if (ScoreRedTeam < ScoreBlueTeam)
            {
                _resultado.text = "Player 2 won";
            }
            else
            {
                _resultado.text = "Empate";
            }
        }

    }

    public void AddScore(int score, int newScoreValue)
    {
        score += newScoreValue;

    }

    void UpdateScore(Text scoretext, int score)
    {
        scoretext.text = "" + score;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ball")
        {
            if( _redconfetti == true)
                Confetti = RedGoal.gameObject.GetComponentInChildren<ParticleSystem>();
            else Confetti = BlueGoal.gameObject.GetComponentInChildren<ParticleSystem>();




            Goal.Play();
            Confetti = RedGoal.GetComponentInChildren<ParticleSystem>();
            Confetti.Play();
            other.transform.position = new Vector2(0, 0);
            other.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -1) * 10, ForceMode2D.Impulse);
        }
    }
}


