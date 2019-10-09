using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
	public float MovementSpeed = 10;
	public float JumpHeight = 1;
	public float GravitySpeedModifier = 1;
	[Tooltip("True if it's Player 1, false if Player 2.")] public bool PlayerOne = true;
	public GameObject PlayerSprite;

    public float Pointer;
	private float _horizontal = 0;

	private Transform _obj;
	private Rigidbody2D _rb;
    private AnimationCode _anim;

	public string horizontalControl;
	public string joystickHorizontal;
	public string jumpButton;
	public bool teclado;

	private void Start()
	{
		_obj = gameObject.transform;
		_rb = GetComponent<Rigidbody2D>();
        _anim = GetComponentInChildren<AnimationCode>();
        GameObject.FindGameObjectWithTag("Aim").transform.position = new Vector3(GameObject.FindGameObjectWithTag("Aim").transform.position.x + Pointer, GameObject.FindGameObjectWithTag("Aim").transform.position.y, 0);
        	}

	private void FixedUpdate()
	{
		_rb.AddForce(new Vector2(0, -10 * GravitySpeedModifier));

		//_horizontal = Input.GetAxis(PlayerOne ? "p1_horizontal" : "p2_horizontal");
		if(teclado == true){
			_horizontal = Input.GetAxis(horizontalControl);
		}
		else{
			_horizontal = Input.GetAxis(joystickHorizontal);
		}

		Vector3 tempVect = new Vector3(_horizontal, 0, 0);
		tempVect = tempVect.normalized * MovementSpeed * Time.deltaTime;
        _anim.PararDeAndar();


        if (_horizontal < 0)
        {
            PlayerSprite.transform.rotation = new Quaternion(0, 180, 0, 0);
            _anim.Andar();
        }
        else if (_horizontal > 0)
        {
            PlayerSprite.transform.rotation = new Quaternion(0, 0, 0, 0);
            _anim.Andar();
        }

		_obj.transform.position += tempVect;

		Collider2D colBounds = GetComponent<Collider2D>();
		Debug.DrawRay(transform.position - new Vector3(.4f, colBounds.bounds.extents.y + 0.01f - colBounds.offset.y), Vector2.down, Color.red);
		Debug.DrawRay(transform.position - new Vector3(0, colBounds.bounds.extents.y + 0.01f - colBounds.offset.y), Vector2.down, Color.green);
		Debug.DrawRay(transform.position - new Vector3(-.4f, colBounds.bounds.extents.y + 0.01f - colBounds.offset.y), Vector2.down, Color.blue);

	//	Debug.Log("can jump");

	//if (Input.GetButton(PlayerOne ? "p1_jump" : "p2_jump"))
		if (Input.GetButton(jumpButton))
		{
			RaycastHit2D hit1 = Physics2D.Raycast(transform.position - new Vector3(.4f, colBounds.bounds.extents.y + 0.01f - colBounds.offset.y), Vector2.down, 0.1f);
			RaycastHit2D hit2 = Physics2D.Raycast(transform.position - new Vector3(0, colBounds.bounds.extents.y + 0.01f - colBounds.offset.y), Vector2.down, 0.1f);
			RaycastHit2D hit3 = Physics2D.Raycast(transform.position - new Vector3(-.4f, colBounds.bounds.extents.y + 0.01f - colBounds.offset.y), Vector2.down, 0.1f);
			
			if ((hit1 || hit2 || hit3) && (hit1.transform.tag == "Ground" || hit2.transform.tag == "Ground" || hit3.transform.tag == "Ground"))
			{
				_rb.AddForce(new Vector2(0, 1) * JumpHeight * 10);
                _anim.Pular();
			}
		}
	}
}
