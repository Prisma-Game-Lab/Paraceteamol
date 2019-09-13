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

    [SerializeField] private Text uiText;
    public float startTime;

    private float timer;
    private bool canCount = true;
    private bool doOnce = false;

    public GameObject player1;
    public GameObject player2;
    public GameObject FinalScore;

    // Start is called before the first frame update
    void Start()
    {
        timer = startTime;
        FinalScore.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(timer >= 0.0f && canCount)
        {
            timer -= Time.deltaTime;

            string minutes = ((int)timer / 60).ToString();
            string seconds = (timer % 60).ToString("f2");

            uiText.text = minutes + ":" + seconds;
        }
        else if(timer <= 0.0f && !doOnce)
        {
            canCount = false;
            FinalScore.SetActive(true);
            Time.timeScale = 0f;

        }
    }
}
