using UnityEngine;


public class BallHit : MonoBehaviour
{
   // [Tooltip("Checks if it will be a vertical check (up/down), or horizontal check (left/right)")]
 //   public bool VerticalCheck = false;
 //   public bool HorizontalCheck = false;

    private BallPhysics _ballPhysicsScript;
    private float _strenght;
    private float _previous_y_position;
    private float _dif_y_position; //Serve para verificar se a bola está na diagonal ou nao
    private float _previous_x_position;
    private float _dif_x_position; //Serve para verificar se a bola está na diagonal ou nao
   

    private void Awake()
    {
        _ballPhysicsScript = GetComponentInParent<BallPhysics>();
        _previous_y_position = GetComponentInParent<BallPhysics>().transform.position.y; //determina uma posicao y inicial

        _previous_x_position = GetComponentInParent<BallPhysics>().transform.position.x;  //determina uma posicao x inicial
        _strenght = _ballPhysicsScript.Strenght;
    }
    private void FixedUpdate()
    {

        _dif_y_position = _previous_y_position - GetComponentInParent<BallPhysics>().transform.position.y;  //verifica se o y dela está alterando ou nao, assim vendo se está na horizontal ou diagonal
        _dif_x_position = _previous_x_position - GetComponentInParent<BallPhysics>().transform.position.x; //verifica se o x dela está alterando ou nao, assim vendo se está na vertical ou diagonal


    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Wall")
        {
          
                _ballPhysicsScript.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                _ballPhysicsScript.GetComponent<Rigidbody2D>().AddForce(new Vector2(_dif_x_position, -_dif_y_position) *_strenght, ForceMode2D.Impulse); 
            //Nesse caso como é uma parede, dif x sempre vai ser diferente de 0 porque a bola vai estar se mexendo na horizontal então sempre vai aplicar uma força horizontal. O que pode acontecer é dif y ser diferente de 0 se ser aplicada uma força diagonal

        }
        if (col.gameObject.tag == "Ground")
        {
          
                _ballPhysicsScript.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                _ballPhysicsScript.GetComponent<Rigidbody2D>().AddForce(new Vector2(-_dif_x_position, _dif_y_position) * _strenght, ForceMode2D.Impulse);
            //Nesse caso como é um chao ou um teto, dif y sempre vai ser diferente de 0 porque a bola vai estar se mexendo na vertical então sempre vai aplicar uma força vertical. O que pode acontecer é dif x ser diferente de 0 se ser aplicada uma força diagonal
            
        }
        if (col.gameObject.tag == "TurnedWall")
        {

            _ballPhysicsScript.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            _ballPhysicsScript.GetComponent<Rigidbody2D>().AddForce(new Vector2(_dif_x_position, _dif_y_position) * _strenght , ForceMode2D.Impulse);
            //Nesse caso como é um chao ou um teto, dif y sempre vai ser diferente de 0 porque a bola vai estar se mexendo na vertical então sempre vai aplicar uma força vertical. O que pode acontecer é dif x ser diferente de 0 se ser aplicada uma força diagonal

        }
            if (col.gameObject.tag == "Player")
            {
                _ballPhysicsScript.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                _ballPhysicsScript.GetComponent<Rigidbody2D>().AddForce(new Vector2(_dif_x_position, _dif_y_position) * _strenght, ForceMode2D.Impulse);

            }



        }

    }

