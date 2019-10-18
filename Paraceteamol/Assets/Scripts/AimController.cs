using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AimController : MonoBehaviour
{
	[Space]
	[SerializeField] private ParticleSystem InhaleParticles;
	[SerializeField] private ParticleSystem ExhaleParticles;
	[Space]
	public float Strenght = .5f;
	public float CooldownTimer = 0;

	[Space]
	[Header("Analogico esquerdo")]
	public string horizontalControl = "p1_horizontal";
	public string verticalControl = "p1_vertical";
	public string joystickHorizontal = "p1_ps4_horizontal";
	public string joystickVertical = "p1_ps4_vertical";
	[Space]
	[Header("Analogico direito")]
	public Vector2 rightStickInput;
	public string rJoystickHorizontal = "p1_ps4_R_horizontal";
	public string rJoystickVertical = "p1_ps4_R_vertical";

	[HideInInspector]
	public bool IsPulling = false;
	[HideInInspector]
	public bool _canShoot = true;

	private bool _playerOne;
	private float _horizontal;
	private bool teclado;

	private IEnumerator Cooldown()
	{
		Debug.Log("começa cooldown");
		yield return new WaitForSeconds(0.1f);
		_canShoot = false;
		yield return new WaitForSeconds(CooldownTimer);
		_canShoot = true;
		Debug.Log("termina cooldown");
	}

	private void Start()
	{
		_playerOne = GetComponentInParent<PlayerMovement>().PlayerOne;
		teclado = GetComponentInParent<PlayerMovement>().teclado;
	}

	private void Update()
	{
		if (teclado == true)
		{
			//Inputs horizontais
			if (Input.GetAxis(horizontalControl) > 0)
				_horizontal = 0;
			else if (Input.GetAxis(horizontalControl) < 0)
				_horizontal = 180;

			//Inputs Verticais
			if (Input.GetAxis(verticalControl) > 0)
				transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
			else if (Input.GetAxis(verticalControl) < 0)
				transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
			else
				transform.rotation = Quaternion.Euler(new Vector3(0, _horizontal, 0));

		}
		else
		{
			rightStickInput = new Vector2(Input.GetAxis(rJoystickHorizontal), Input.GetAxis(rJoystickVertical));

			if (rightStickInput.magnitude > 0.19f)
			{
				Vector3 curRotation = Vector3.left * rightStickInput.x + Vector3.up * rightStickInput.y;
				Quaternion aimRotation = Quaternion.FromToRotation(new Vector3(transform.rotation.x, transform.position.y, transform.position.z), curRotation);
				transform.SetPositionAndRotation(transform.position, aimRotation);
			}
		}

		if (Input.GetButton(_playerOne ? "p1_fire1" : "p2_fire1") && _canShoot)
			InhaleParticles.Play();
		else
			InhaleParticles.Stop();

		if (Input.GetButton(_playerOne ? "p1_fire2" : "p2_fire2") && _canShoot)
			ExhaleParticles.Play();
		else
			ExhaleParticles.Stop();

		if (_canShoot)
		{
			if (Input.GetButton(_playerOne ? "p1_fire1" : "p2_fire1"))
				IsPulling = true;
			else
				IsPulling = false;
		}
	}

	private void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.tag == "Ball" && _canShoot)
		{
			if (Input.GetButton(_playerOne ? "p1_fire1" : "p2_fire1"))
			{

			}
			else if (Input.GetButton(_playerOne ? "p1_fire2" : "p2_fire2"))
			{
				col.transform.position = Vector3.MoveTowards(col.transform.position, -transform.position, Strenght);

				col.GetComponent<BallPhysics>().Direction = new Vector2(-GameObject.FindGameObjectWithTag("Aim").transform.position.x, -GameObject.FindGameObjectWithTag("Aim").transform.position.y).normalized;
			}
		}
	}
}