using UnityEngine.UI;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
	public Text scoreText;
	public GameObject BallPrefab;

	[HideInInspector]
	public int score = 0;
	
	void Start()
	{
		score = 0;
		scoreText.text = "Gols: " + score;
	}

	public void AddScore(int newScoreValue, GameObject target)
	{
		score += newScoreValue;
		scoreText.text = "Gols: " + score;

		Destroy(target);
		Instantiate(BallPrefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Ball")
		{
			AddScore(1, col.gameObject);
			//other.transform.position = new Vector2(0, 0);
		}
	}
}
