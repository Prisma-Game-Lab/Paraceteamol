using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
	// Public variables
	public float MovementSpeed = 18;
	public AudioSource Steps;
	public float JumpHeight = 240;
	public float GravitySpeedModifier = 8;
	[Space]
	[Header("Controller")]
	public string HorizontalControl = "p1_horizontal";  //Keyboard
	public string VerticalControl = "p1_vertical";  //Keyboard
	public string JoystickHorizontal = "p1_ps4_horizontal";
	public string JoystickVertical = "p1_ps4_vertical";
	public string JumpButton = "p1_jump";
	[Tooltip("True if using keyboard")] public bool Keyboard;

	// Private variables
	private bool grounded = false;
	private float _horizontal = 0;
	private float _vertical = 0;
	private Transform _obj;
	private Rigidbody2D _rb;
	private AnimationCode _anim;

	private void Start()
	{
		_obj = gameObject.transform;
		_rb = GetComponent<Rigidbody2D>();
		_anim = GetComponentInChildren<AnimationCode>();
	}

	private void FixedUpdate()
	{
		_rb.AddForce(new Vector2(0, -10 * GravitySpeedModifier));

		if (Keyboard == true)
		{
			_horizontal = Input.GetAxis(HorizontalControl);
			_vertical = Input.GetAxis(VerticalControl);
		}
		else
		{
			_horizontal = Input.GetAxis(JoystickHorizontal);
			_vertical = Input.GetAxis(JoystickVertical);
		}

		Vector3 tempVect = new Vector3(_horizontal, 0, 0);
		tempVect = tempVect.normalized * MovementSpeed * Time.deltaTime;
		_anim.PararDeAndar();
		Steps.Pause();

		if (_horizontal < 0)
		{
			transform.rotation = new Quaternion(0, 180, 0, 0);
			_anim.Andar();
			Steps.Play();
		}
		else if (_horizontal > 0)
		{
			transform.rotation = new Quaternion(0, 0, 0, 0);
			_anim.Andar();
			Steps.Play();
		}

		_obj.transform.position += tempVect;

        if (Keyboard == true)
        {
            if (Input.GetButton(JumpButton) && grounded)
            {
                _rb.AddForce(new Vector2(0, 1) * JumpHeight * 5f);
                _anim.Pular();
            }
        }
        else
        {
            if ((Input.GetButton(JumpButton) || _vertical > 0.1f) && grounded)
            {
                _rb.AddForce(new Vector2(0, 1) * JumpHeight * 5f);
                _anim.Pular();
            }
        }
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.otherCollider is BoxCollider2D)
		{
			if (collision.collider.tag == "Ground")
			{
				grounded = true;
			}
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.otherCollider is BoxCollider2D)
		{
			if (collision.collider.tag == "Ground")
			{
				grounded = false;
			}
		}
	}
}
