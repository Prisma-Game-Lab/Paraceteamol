using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShoot : MonoBehaviour
{ 
    private Vector2 _ballPosition;
    private Vector2 _ballTarget;
    private bool _hasball;
    private bool _ispulling; //verifica se o jogador está puxando a bola
    private GameObject Target;

    public Rigidbody2D _ball;
    public Transform FirePoint; //Seta de onde a arma vai sair, por enquanto coloquei na frente do quadrado, mas quando tivermos sprites colocaremos na ponta do aspirador
    
    

    public float Speed;

   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Pool();
            
        }
        else if (Input.GetMouseButton(1))
        {
            Push();
            if (_hasball) //verifica se o jogador ja possui a bola, caso possua, no momento que empurrar ela vai sair
            {
                FreeTheBall();
            }
           
        }
        if (Input.GetMouseButtonUp(0)) {
            _ispulling = false; // indica que parou de puxar

        }
         
    }
    void Pool()
    {
        _ispulling = true; 
      RaycastHit2D _hitInfo=  Physics2D.Raycast(FirePoint.position, FirePoint.right); //Seta de onde e para onde o raycast vai
      
      if (_hitInfo && _hitInfo.collider.tag == "Ball")
      {
            Debug.Log("fngrjgf");
            _ball = _hitInfo.transform.parent.GetComponent<Rigidbody2D>();
               
      if (_ball != null) //Verifica se o raycast encontrou uma bola 
       {
           Debug.DrawLine(FirePoint.position, _ball.transform.position, Color.blue); //serve para testar na cena se o raio esta saindo
           Target = GameObject.FindGameObjectWithTag("Player");
            _ballPosition = _ball.transform.position;
            _ballTarget = Target.transform.position;
            _ball.transform.position = Vector2.MoveTowards(_ballPosition,_ballTarget,Speed*Time.deltaTime); //Cria um vetor que anda da direcao atual a direcao do jogador a uma velocidade determinada
          //  _ball.AddForce(_teste,ForceMode2D.Impulse); //Pega o vetor criado acima e o usa com impulso
        }

      }

       
    }
    void Push()
    {
         
        RaycastHit2D _hitInfo = Physics2D.Raycast(FirePoint.position, FirePoint.right); //Seta de onde e para onde o raycast vai
       
        if (_hitInfo && _hitInfo.collider.tag == "Ball")
        {
           

            _ball = _hitInfo.transform.parent.GetComponent<Rigidbody2D>();

            if (_ball != null) //Verifica se o raycast encontrou uma bola 
            {
                Debug.DrawLine(FirePoint.position, _ball.transform.position, Color.blue);
                Target = GameObject.FindGameObjectWithTag("AntiPlayer");
                _ballPosition = _ball.transform.position;
                _ballTarget = Target.transform.position;
                _ball.transform.position = Vector2.MoveTowards(_ballPosition, _ballTarget, Speed*Time.deltaTime);//Cria um vetor que anda da direcao atual a direcao do jogador a uma velocidade determinada
                // _ball.AddForce(-_teste, ForceMode2D.Impulse); //Pega o vetor criado acima e o usa com impulso
            }

        }
         
    }


    void OnCollisionEnter2D(Collision2D col)
    {
     
        if (col.gameObject.tag == "Ball" && _ispulling==true && _hasball==false) //essas condicoes sao necessarias pois ele verifica se o jogador esta puxando( se nao tivesse essa condicao, bastaria tocar na bola que ela sumiria) e se tem a bola( caso nao tenha, o objeto acoplado ao jogador ira sofrer as mudancas)
        {
             col.gameObject.SetActive(false); //faz com que o objeto suma
             _hasball = true;  
        } 
    }

    void FreeTheBall() //serve para liberar a bola quando o jogador aperta o botao de empurrar
    {
            _ball.gameObject.SetActive(true);
            _ball.transform.position = Target.transform.position;
            _hasball = false;
        
    }
     
}
