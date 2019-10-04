using UnityEngine;

public class BallPhysics : MonoBehaviour
{
	public float StartSpeed = 10;
	//[HideInInspector]
	public Vector2 Direction = Vector2.one;

	private bool _playerIsPulling;

	private void FixedUpdate()
	{
		 transform.Translate(Direction * StartSpeed * Time.deltaTime);
	}
}
