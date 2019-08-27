using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAtraction : MonoBehaviour
{
    public GameObject Ball;
    public Rigidbody2D BallBody;
    public Rigidbody2D Player;
    public float Speed;
    private Vector2 _ballPosition;
    private Vector2 _ballTarget;
    
    public Transform FirePoint; //Seta de onde a arma vai sair, por enquanto coloquei na frente do quadrado, mas quando tivermos sprites colocaremos na ponta do aspirador

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Push();
           
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            Pool();
           
        }
    }
    void Push()
    {
      RaycastHit2D _hitInfo=  Physics2D.Raycast(FirePoint.position, FirePoint.right); //Seta de onde e para onde o raycast vai
      if (_hitInfo)
      {
       BallPhysics _ball= _hitInfo.transform.GetComponent<BallPhysics>();
      if (_ball != null) //Verifica se o raycast encontrou uma bola 
       {
            _ballPosition = Ball.transform.position;
            _ballTarget = Player.transform.position;
           Vector2 _teste = Vector2.MoveTowards(_ballPosition,_ballTarget,Speed); //Cria um vetor que anda da direcao atual a direcao do jogador a uma velocidade determinada
            BallBody.AddForce(_teste,ForceMode2D.Impulse); //Pega o vetor criado acima e o usa com impulso
        }

      }
    }
    void Pool()
    {
        RaycastHit2D _hitInfo = Physics2D.Raycast(FirePoint.position, FirePoint.right); //Seta de onde e para onde o raycast vai
        if (_hitInfo)
        {
            BallPhysics _ball = _hitInfo.transform.GetComponent<BallPhysics>();
            if (_ball != null) //Verifica se o raycast encontrou uma bola 
            {
                _ballPosition = Ball.transform.position;
                _ballTarget = Player.transform.position;
                Vector2 _teste = Vector2.MoveTowards(_ballPosition, -_ballTarget, Speed); //Cria um vetor que anda da direcao contraria a direcao do jogador a uma velocidade determinada
                BallBody.AddForce(_teste, ForceMode2D.Impulse); //Pega o vetor criado acima e o usa com impulso
            }

        }
    }

}
