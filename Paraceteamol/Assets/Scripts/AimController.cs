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
    public Vector2 dir;
	[HideInInspector]
	public bool IsPulling = false;
	[HideInInspector]
	public bool _canShoot = true;
    public float angle;
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
	private bool _ballInRange = false;
    private bool _canInhale=true;
    private bool _inhaleEffects;
	private GameObject _ballGO;
    private ContactPoint2D[] _contacts = new ContactPoint2D[1];
    private Collider2D _ballcontact;
    
	#region State

	// Here you name the states
	public enum State
	{
		Idle,
		Cooldown,
		Inhale,
		Exhale,
	}
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

	private IEnumerator InhaleTimer()
	{
        
		yield return new WaitForSeconds(InhaleTime);

        Debug.Log("cant inhale");
        _canInhale = false;
    
		InhaleParticles.Stop();
		 
		state = State.Exhale;
	}

	private IEnumerator InhaleCooldown()
	{

        
		yield return new WaitForSeconds(InhaleCooldownTime);
        ExhaleParticles.Play();
		state = State.Idle;
        _canInhale = false;
		Debug.Log("Can Inhale");
        ExhaleParticles.Stop();
	
   
    }

	private void Start()
	{
		teclado = GetComponentInParent<PlayerMovement>().teclado;
		_ballGO = GameObject.FindGameObjectWithTag("Ball");
		state = State.Idle;
        
	}

	private void FixedUpdate()
	{
        
        angle = gameObject.transform.Find("seta").transform.rotation.z*100;
       
             dir = new Vector2(-Mathf.Sin(Mathf.Deg2Rad * angle), Mathf.Cos(Mathf.Deg2Rad * angle));
        
		// Testa se vai usar teclado ou controle
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
  
		if (state == State.Idle && Input.GetButton(_inhaleBtn))
			InhaleParticles.Play();
		else
			InhaleParticles.Stop();

		if (state == State.Inhale && _ballInRange &&_canInhale)
		{
            InhaleParticles.Stop();
			if (Vector2.Distance(_ballGO.transform.position, gameObject.transform.Find("seta").transform.position) > .5f)
			{
				_ballGO.transform.position = Vector3.MoveTowards(_ballGO.transform.position, gameObject.transform.Find("seta").transform.position, Strenght);
			}
			else
			{
				_ballGO.transform.position = gameObject.transform.Find("seta").transform.position;
			}

            if (Input.GetButtonUp(_inhaleBtn))
            {

                InhaleParticles.Stop();

                state = State.Exhale;
            }
            

		}

		 if (state == State.Exhale)
		{
			 
         
			//Debug.Log(dir.normalized);
             
           
           // Vector2 normal = _contacts[0].normal;

            _ballGO.GetComponentInChildren<BallHit>().Velocity = dir;
            _ballGO.GetComponent<Rigidbody2D>().AddForce(_ballGO.GetComponentInChildren<BallHit>().Velocity * _ballGO.GetComponent<BallPhysics>().StartSpeed, ForceMode2D.Impulse);
            InhaleParticles.Stop();
          

            ExhaleParticles.Stop();
            state = State.Idle;
		}
       
       
	}

	private void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.tag == "Ball")
		{
            _ballcontact = col;
			_ballGO = col.gameObject;
			_ballInRange = true;
           
			if (state == State.Idle)
			{
                
				if (Input.GetButtonDown(_inhaleBtn))
                {
                    
					col.gameObject.GetComponentInChildren<BallHit>().Velocity = Vector2.zero;
                  //  StartCoroutine(InhaleCooldown());
					//StartCoroutine(InhaleTimer());
					state = State.Inhale;
				}
				else if (Input.GetButtonUp(_inhaleBtn))
				{
			//	StopCoroutine(InhaleTimer());
                    InhaleParticles.Stop();
					//ExhaleParticles.Play();
					state = State.Exhale;
				}
                 
			}
		}
	}

	private void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.tag == "Ball")
		{

			_ballInRange = false;

            
        }
	}
}