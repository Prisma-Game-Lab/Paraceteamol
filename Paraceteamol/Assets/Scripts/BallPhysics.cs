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
        _startingSpeed = Speed;
	}

	private void FixedUpdate()
	{
		if (state == State.Held)    // Disable Collider & RigidBody 2D
		{
			Speed = 0;
			_rb.isKinematic = true;
			_col.enabled = false;
		}
		if (state == State.Release)  // Enable Collider & RigidBody 2D
		{
             
			Speed = _startingSpeed;
			_rb.isKinematic = false;
			_col.enabled = true;
			 
		}
	}

	private void OnCollisionEnter2D(Collision2D col)
	{
		if (state == State.Release)
		{
			if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Wall") || col.gameObject.CompareTag("Player"))
			{
				//Debug.Log("Bola bateu em " + col.gameObject.tag);
				ReflectProjectile(_rb, col.contacts[0].normal);
			}
		}
	}

	private void ReflectProjectile(Rigidbody2D rb, Vector2 reflectVector)
	{
		Direction = Vector2.Reflect(Direction, reflectVector);
		rb.velocity = Speed * Direction;
		BallSound.Play();
	}
}