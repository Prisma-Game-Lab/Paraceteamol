using UnityEngine;
using System.Collections;

public class BallPhysics : MonoBehaviour
{
	[Header("Physics")]
	[Tooltip("Velocidade da bola")]
	public float Speed = 50;
	[Tooltip("Direcao inicial da bola")]
	public Vector2 Direction = Vector2.one;

	private AudioSource BallSound;
	private Rigidbody2D _rb;
	private Collider2D _col;
	private float _startingSpeed;
	private Collider2D _tempCol;

	#region StateMachine
	public enum State
	{
		Idle,
		Held,
		Release,
	}
	public State state;

	IEnumerator IdleState()
	{
		while (state == State.Idle)
			yield return 0;
	}

	IEnumerator HeldState()
	{
		while (state == State.Held)
			yield return 0;
	}

	IEnumerator ReleaseState()
	{
		while (state == State.Release)
			yield return 0;
	}
	#endregion
	/*
	 * Idle
	 * Held
	 * Release
	 */

	private void Awake()
	{
		_rb = GetComponent<Rigidbody2D>();
		_rb.AddForce(Direction * Speed, ForceMode2D.Impulse);
		BallSound = GetComponent<AudioSource>();
		_col = gameObject.GetComponent<Collider2D>();
		_startingSpeed = Speed;
	}

	private void FixedUpdate()
	{
		switch (state)
		{
			case State.Held:
				Speed = 0;
				//_rb.isKinematic = true;
				//_col.enabled = false;
				break;
			case State.Release:
				//_rb.isKinematic = false;
				//_col.enabled = true;
				Speed = _startingSpeed;
				Direction = ReleaseDirection(GameObject.FindGameObjectWithTag("Player").transform.position);
				state = State.Idle;
				break;
		}
	}

	private void OnCollisionEnter2D(Collision2D col)
	{
		switch (state)
		{
			case State.Idle:
				if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Wall") || col.gameObject.CompareTag("Player"))
				{
					ReflectProjectile(_rb, col.contacts[0].normal);
				}
				break;
			case State.Held:
				if (col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Wall"))
					Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>(), true);
				break;
			case State.Release:
				Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>(), false);
				break;
		}
	}

	private void ReflectProjectile(Rigidbody2D rb, Vector2 reflectVector)
	{
		Direction = Vector2.Reflect(Direction, reflectVector);
		rb.velocity = Speed * Direction;
		BallSound.Play();
	}

	private Vector2 ReleaseDirection(Vector2 playerPos)
	{
		Vector2 dir = gameObject.transform.position;
		dir = playerPos - dir;
		return dir.normalized;
	}
}