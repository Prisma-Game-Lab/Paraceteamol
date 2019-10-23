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
	public float InhaleTime = 2;
	public float InhaleCooldownTime = 3;

	[HideInInspector]
	public bool IsPulling = false;
	[HideInInspector]
	public bool _canShoot = true;

	[Header("Analogico esquerdo")]
	public string _horizontalControl = "p1_horizontal";
	public string _verticalControl = "p1_vertical";

	[Header("Analogico direito")]
	public string _joystickHorizontal = "p1_ps4_R_horizontal";
	public string _joystickVertical = "p1_ps4_R_vertical";

	[Header("Buttons")]
	public string _inhaleBtn = "p1_fire1";

	public Vector2 _rightStickInput;
	private float _horizontal;
	private bool teclado;
	private bool _canInhale = true;
	private bool _ballCheck = true;

	private IEnumerator InhaleTimer()
	{
		yield return new WaitForSeconds(InhaleTime);
		_canInhale = false;
		InhaleParticles.Stop();
		ExhaleParticles.Play();
	}

	private IEnumerator InhaleCooldown()
	{
		yield return new WaitForSeconds(InhaleCooldownTime);
		_canInhale = true;
		Debug.Log("Can Inhale");
	}

	private void Start()
	{
		teclado = GetComponentInParent<PlayerMovement>().teclado;
	}

	private void FixedUpdate()
	{
		if (teclado == true)
		{
			//Inputs horizontais
			if (Input.GetAxis(_horizontalControl) > 0)
				_horizontal = 0;
			else if (Input.GetAxis(_horizontalControl) < 0)
				_horizontal = 180;

			//Inputs Verticais
			if (Input.GetAxis(_verticalControl) > 0)
				transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
			else if (Input.GetAxis(_verticalControl) < 0)
				transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
			else
				transform.rotation = Quaternion.Euler(new Vector3(0, _horizontal, 0));
		}
		else
		{
			_rightStickInput = new Vector2(Input.GetAxis(_joystickHorizontal), Input.GetAxis(_joystickVertical));

			if (_rightStickInput.magnitude > 0.1f)
			{
				Vector3 curRotation = Vector3.left * _rightStickInput.x + Vector3.up * _rightStickInput.y;
				Quaternion aimRotation = Quaternion.FromToRotation(new Vector3(-16f, 0, 0), curRotation);
				transform.rotation = aimRotation;
			}
		}

		if (_canShoot)
		{
			if (Input.GetButton(_inhaleBtn))
				IsPulling = true;
			else
				IsPulling = false;
		}
	}

	private void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.tag == "Ball" && _canShoot)
		{
			if (Input.GetButtonDown(_inhaleBtn))
				InhaleParticles.Play();

			if (Input.GetButton(_inhaleBtn) && _canInhale)
			{
				Debug.Log("click");
				col.gameObject.GetComponentInChildren<BallHit>().Velocity = Vector2.zero;

				if (Vector2.Distance(gameObject.transform.Find("seta").transform.position, col.transform.position) > .5f)
					col.transform.position = Vector3.MoveTowards(col.transform.position, gameObject.transform.Find("seta").transform.position, Strenght);
				else
				{
					col.transform.position = gameObject.transform.Find("seta").transform.position;
					_ballCheck = true;
				}
			}

			if (Input.GetButtonUp(_inhaleBtn))
			{
				if (_ballCheck)
					col.gameObject.GetComponentInChildren<BallHit>().Velocity = Vector3.left * -1;
				_ballCheck = false;
				InhaleParticles.Stop();
				StopCoroutine(InhaleTimer());
				_canInhale = false;
				ExhaleParticles.Play();
			}
		}
	}
}