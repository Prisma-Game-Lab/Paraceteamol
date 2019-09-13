using UnityEngine.UI;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
	public Text scoreText;

	[HideInInspector]
	public int score = 0;
	
	void Start()
	{
		score = 0;
		scoreText.text = "Gols: " + score;
	}

	public void AddScore(int newScoreValue)
	{
		score += newScoreValue;
		scoreText.text = "Gols: " + score;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Ball")
		{
			AddScore(1);
			other.transform.position = new Vector2(0, 0);
		}
	}
}
