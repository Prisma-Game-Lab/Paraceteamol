using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
public class MatchScript : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string Inputmusica;
    private Vector2 _oldvelocity;
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
    public Rigidbody2D BallRB;
    public float BallTimer;
 public IEnumerator StartCountdown()
 {
     
        
         yield return new WaitForSeconds(BallTimer);


         BallRB.velocity = _oldvelocity;
         BallRB.AddForce(new Vector2(0, -1) * 10, ForceMode2D.Impulse);

     
 }

    void Start()
    {
        FMODUnity.RuntimeManager.PlayOneShot(Inputmusica);
        ScoreRedTeam = 0;
        ScoreBlueTeam = 0;
        UpdateScore(ScoreTextBlueGoal, ScoreRedTeam);
        UpdateScore(ScoreTextRedGoal, ScoreBlueTeam);
        _oldvelocity = BallRB.velocity;
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
                _resultado.text = "Time 1 ganhou!";
            }
            else if (ScoreRedTeam < ScoreBlueTeam)
            {
                _resultado.text = "Time 2 ganhou!";
            }
            else
            {
                _resultado.text = "Empate";
            }
        }

    }
    private void GoalEffects() {
        Confetti.Play();
        Goal.Play();
        BallRB.gameObject.transform.position = new Vector2(0, 0);
        BallRB.velocity = new Vector2(0, 0);
        StartCoroutine(StartCountdown());


    }

    public void RedTeamGol() {
        ScoreRedTeam += 1;
        UpdateScore(ScoreTextBlueGoal, ScoreRedTeam);
        Confetti = RedGoal.GetComponentInChildren<ParticleSystem>();
        GoalEffects();
      
     
    }
    public void BlueTeamGol()
    {
        ScoreBlueTeam += 1;
        UpdateScore(ScoreTextRedGoal, ScoreBlueTeam);
        Confetti = BlueGoal.GetComponentInChildren<ParticleSystem>();
        GoalEffects();
      


    }
    public void AddScore(int score, int newScoreValue)
    {
        score += newScoreValue;
        
        

    }

    void UpdateScore(Text scoretext, int score)
    {
        scoretext.text = "" + score;
    }


    
}


