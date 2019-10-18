using UnityEngine;

public class BallHit : MonoBehaviour
{
	public Vector2 Velocity;

	private BallPhysics _ballPhysicsScript;
	private Rigidbody2D _rb;
	private ContactPoint2D[] _contacts = new ContactPoint2D[1];
	private float _speed;

	private void Awake()
	{
		_ballPhysicsScript = GetComponentInParent<BallPhysics>();
		_rb = _ballPhysicsScript.GetComponent<Rigidbody2D>();
		_rb.AddForce(Velocity, ForceMode2D.Impulse);
		_speed = GetComponentInParent<BallPhysics>().StartSpeed;
	}

	private void ReflectProjectile(Rigidbody2D rb, Vector2 reflectVector)
	{
		Velocity = Vector2.Reflect(Velocity, reflectVector);
		_rb.velocity = Velocity * _speed;
	}

	private void OnTriggerEnter2D(Collider2D col)	
	{
		if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Wall") || col.gameObject.CompareTag("Player"))
		{
			col.GetContacts(_contacts);
			Vector2 normal = _contacts[0].normal;
			ReflectProjectile(_rb, normal);
		}
	}
}