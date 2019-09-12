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

    // Start is called before the first frame update
    void Start()
    {
        /*timer = mainTimer;*/
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

            uiText.text = minutes + ":" + seconds;
        }
        else if(timer <= 0.0f && !doOnce)
        {
            canCount = false;
            /*iText.text = ("0.00");
            timer = 0.0f;*/
        }
    }
}
