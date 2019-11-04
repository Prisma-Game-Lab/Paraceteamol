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
	public float Pointer = 1.5f;
	[Space]
	[Header("Controller")]
	public string horizontalControl = "p1_horizontal";
	public string joystickHorizontal = "p1_ps4_horizontal";
	public string jumpButton = "p1_jump";
	[Tooltip("True if it's Player 1, false if Player 2.")] public bool PlayerOne = true;
	[Tooltip("True if using keyboard")] public bool teclado;
    [Tooltip("True if player is facing right")] public bool facingRight;

    // Private variables
    private float _horizontal = 0;
	private Transform _obj;
	private Rigidbody2D _rb;
	private AnimationCode _anim;
    private bool grounded = false;

    public CharacterController controller;

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
			_horizontal = Input.GetAxis(horizontalControl);
		else
			_horizontal = Input.GetAxis(joystickHorizontal);

		Vector3 tempVect = new Vector3(_horizontal, 0, 0);
		tempVect = tempVect.normalized * MovementSpeed * Time.deltaTime;
		_anim.PararDeAndar();
        Steps.Pause();

        if((_horizontal > 0 && !facingRight) || (_horizontal < 0 && facingRight)){
            Flip();
            _anim.Andar();
            Steps.Play();
        }

		/*if (_horizontal < 0)
		{
			//PlayerSprite.transform.rotation = new Quaternion(0, 180, 0, 0);
            Flip();
			_anim.Andar();
            Steps.Play();
		}
		else if (_horizontal > 0)
		{
            //PlayerSprite.transform.rotation = new Quaternion(0, 0, 0, 0);
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z); //Se o jogador estiver andando para a esquerda ele vai virar o personagem
			_anim.Andar();
            Steps.Play();
		}*/

		_obj.transform.position += tempVect;

		Collider2D colBounds = GetComponent<Collider2D>();
		Debug.DrawRay(transform.position - new Vector3(.4f, colBounds.bounds.extents.y + 0.1f - colBounds.offset.y), Vector2.down, Color.red);
		Debug.DrawRay(transform.position - new Vector3(0, colBounds.bounds.extents.y + 0.1f - colBounds.offset.y), Vector2.down, Color.green);
		Debug.DrawRay(transform.position - new Vector3(-.4f, colBounds.bounds.extents.y + 0.1f - colBounds.offset.y), Vector2.down, Color.blue);

		//	Debug.Log("can jump");

		//if (Input.GetButton(PlayerOne ? "p1_jump" : "p2_jump"))
		if (Input.GetButtonDown(jumpButton) && grounded)
		{
            /*ycastHit2D hit1 = Physics2D.Raycast(transform.position - new Vector3(.4f, colBounds.bounds.extents.y + 0.1f - colBounds.offset.y), Vector2.down, 0.1f);
			RaycastHit2D hit2 = Physics2D.Raycast(transform.position - new Vector3(0, colBounds.bounds.extents.y + 0.1f - colBounds.offset.y), Vector2.down, 0.1f);
			RaycastHit2D hit3 = Physics2D.Raycast(transform.position - new Vector3(-.4f, colBounds.bounds.extents.y + 0.1f - colBounds.offset.y), Vector2.down, 0.1f);

			if ((hit1 || hit2 || hit3) && (hit1.transform.tag == "Ground" || hit2.transform.tag == "Ground" || hit3.transform.tag == "Ground"))
			{
				_rb.AddForce(new Vector2(0, 1) * JumpHeight * 10);
				_anim.Pular();
			}*/

            _rb.AddForce(new Vector2(0, 1) * JumpHeight * 10f);
            _anim.Pular();
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            grounded = false;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}
