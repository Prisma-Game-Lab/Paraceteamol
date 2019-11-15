using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AimController : MonoBehaviour
{
	[Space]
	[SerializeField] private ParticleSystem InhaleParticles;
	[SerializeField] private ParticleSystem ExhaleParticles;
	[SerializeField] private GameObject Sight;
	[SerializeField] private GameObject Crosshair;
	[Space]
	public float Strenght = .5f;
	[Tooltip("Time in seconds the player will not be able to inhale")]
	public float CooldownTime = 0;
	[Tooltip("Time in seconds the player")]
	public float InhaleTime = 2;

	[Header("Analogico esquerdo")]
	public string KeyboardHorizontal = "p1_horizontal";
	public string KeyboardVertical = "p1_vertical";

	[Header("Analogico direito")]
	public string JoystickHorizontal = "p1_ps4_R_horizontal";
	public string JoystickVertical = "p1_ps4_R_vertical";

	[Header("Buttons")]
	public string InhaleButton = "p1_fire1";

	private Vector2 _rightStickInput;
	private float _horizontal;
	private bool _keyboard;
	private GameObject _ballGO;

	#region State
	public enum State
	{
		Idle,
		Cooldown,
		Inhale,
		Exhale,
	}
	[HideInInspector]
	public State state;

	IEnumerator IdleState()
	{
		while (state == State.Idle)
		{
			yield return 0;
		}
		NextState();
	}

	IEnumerator CooldownState()
	{
		while (state == State.Cooldown)
		{
			yield return 0;
		}
		NextState();
	}

	IEnumerator InhaleState()
	{
		while (state == State.Inhale)
		{
			yield return 0;
		}
		NextState();
	}

	IEnumerator ExhaleState()
	{
		while (state == State.Exhale)
		{
			yield return 0;
		}
		NextState();
	}

	void NextState()
	{
		string methodName = state.ToString() + "State";
		System.Reflection.MethodInfo info =
			GetType().GetMethod(methodName,
								System.Reflection.BindingFlags.NonPublic |
								System.Reflection.BindingFlags.Instance);
		StartCoroutine((IEnumerator)info.Invoke(this, null));
	}
	#endregion
	/* 
     * States:
     *  Idle
	 *  Cooldown
	 *  Inhale
	 *  Exhale
    */

	// Time that the player can keep Inhaling
	private IEnumerator InhaleTimer(GameObject ballGameObject)
	{
		yield return new WaitForSeconds(InhaleTime);

		InhaleParticles.Stop();

		state = State.Exhale;
	}

	// Time the player can't use the inhale
	private IEnumerator Cooldown()
	{
		yield return new WaitForSeconds(CooldownTime);
		state = State.Idle;
	}

	private void Start()
	{
		_keyboard = GetComponent<PlayerMovement>().Keyboard;
		state = State.Idle;
	}

	private void FixedUpdate()
	{
		// Testa se vai usar teclado ou controle
		#region Verifica Teclado
		if (_keyboard == true)
		{
			//Inputs horizontais
			if (Input.GetAxis(KeyboardHorizontal) > 0)
				_horizontal = 0;
			else if (Input.GetAxis(KeyboardHorizontal) < 0)
				_horizontal = 180;

			//Inputs Verticais
			if (Input.GetAxis(KeyboardVertical) > 0)
				Sight.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
			else if (Input.GetAxis(KeyboardVertical) < 0)
				Sight.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
			else
				Sight.transform.rotation = Quaternion.Euler(new Vector3(0, _horizontal, 0));
		}
		else
		{
			_rightStickInput = new Vector2(Input.GetAxis(JoystickHorizontal), Input.GetAxis(JoystickVertical));

			if (_rightStickInput.magnitude > 0.1f)
			{
				Vector3 curRotation = Vector3.left * _rightStickInput.x + Vector3.up * _rightStickInput.y;
				Quaternion aimRotation = Quaternion.FromToRotation(new Vector3(-16f, 0, 0), curRotation);
				Sight.transform.rotation = aimRotation;
			}
		}
		#endregion

		switch (state)
		{
			case State.Idle:
				Debug.Log("Idle");
				if (Input.GetButtonDown(InhaleButton))
					InhaleParticles.Play();
				break;
			case State.Inhale:
				Debug.Log("Inhale");
				if (Vector2.Distance(_ballGO.transform.position, Crosshair.transform.position) > .1f)
				{
					_ballGO.transform.position = Vector3.MoveTowards(_ballGO.transform.position, Crosshair.transform.position, Strenght);
				}
				else
				{
					_ballGO.GetComponent<BallPhysics>().state = BallPhysics.State.Held;
					_ballGO.transform.position = Crosshair.transform.position;
				}
				break;
			case State.Cooldown:
				Debug.Log("Cooldown");
				break;
			case State.Exhale:
				Debug.Log("Exhale");
				_ballGO.GetComponent<BallPhysics>().state = BallPhysics.State.Release;
				StartCoroutine(Cooldown());
				state = State.Cooldown;
				break;
		}
	}

	private void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.tag == "Ball")
		{
			_ballGO = col.gameObject;

			switch (state)
			{
				case State.Idle:
					if (Input.GetButtonDown(InhaleButton))
					{
						Debug.Log("Está puxando.");
						col.GetComponent<BallPhysics>().state = BallPhysics.State.Held;
						StartCoroutine(InhaleTimer(col.gameObject));
						state = State.Inhale;
					}
					break;
				case State.Inhale:
					if (Input.GetButtonUp(InhaleButton))
					{
						Debug.Log("Parou de apertar o botão.");
						InhaleParticles.Stop();
						state = State.Exhale;
					}
					break;
			}
		}
	}
}