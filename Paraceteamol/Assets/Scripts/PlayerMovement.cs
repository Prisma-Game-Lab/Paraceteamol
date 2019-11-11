using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
	// Public variables
	[Space]
	public GameObject PlayerSprite;
	[Space]
	public float MovementSpeed = 18;
    public AudioSource Steps;
	public float JumpHeight = 240;
	public float GravitySpeedModifier = 8;
	[Space]
	[Header("Controller")]
	public string horizontalControl = "p1_horizontal";  //Teclado
    public string verticalControl = "p1_vertical";  //Teclado
	public string joystickHorizontal = "p1_ps4_horizontal";
    public string joystickVertical = "p1_ps4_Vertical";
    public string jumpButton = "p1_jump";
	[Tooltip("True if it's Player 1, false if Player 2.")] public bool PlayerOne = true;
	[Tooltip("True if using keyboard")] public bool teclado;
    public bool grounded = false;

    // Private variables
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

        if (teclado == true)
        {
            _horizontal = Input.GetAxis(horizontalControl);
            _vertical = Input.GetAxis(verticalControl);
        }
        else
        {
            _horizontal = Input.GetAxis(joystickHorizontal);
            _vertical = Input.GetAxis(joystickVertical);
        }

		Vector3 tempVect = new Vector3(_horizontal, 0, 0);
		tempVect = tempVect.normalized * MovementSpeed * Time.deltaTime;
		_anim.PararDeAndar();
        Steps.Pause();

        if(_horizontal < 0)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
            _anim.Andar();
            Steps.Play();
        }

        else if(_horizontal > 0)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
            _anim.Andar();
            Steps.Play();
        }

		_obj.transform.position += tempVect;


		if ((Input.GetButton(jumpButton) ||  _vertical > 1) && grounded)
		{
            _rb.AddForce(new Vector2(0, 1) * JumpHeight * 5f);
            _anim.Pular();
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
