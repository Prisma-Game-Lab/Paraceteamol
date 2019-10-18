using UnityEngine;

public class BallPhysics : MonoBehaviour
{
	[Tooltip("Forca com que a bola vai ser ricocheteada ")]
	public float Strenght;
	public float StartSpeed;
	[Tooltip("Direcao inicial da sua bola")]
	public Vector2 Direction = Vector2.one;

	private bool _playerIsPulling;

	private void Start()
	{
		//transform.Translate(Direction * StartSpeed * Time.deltaTime);
		GetComponent<Rigidbody2D>().AddForce(Direction * StartSpeed, ForceMode2D.Impulse);
	}
}