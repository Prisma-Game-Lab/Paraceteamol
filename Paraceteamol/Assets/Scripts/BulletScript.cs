using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [Tooltip("Velocidade com que a bola é lançada quando está com o player")]
    public float Speed = 20;
    public Rigidbody2D Rb;

    private Rigidbody2D _target;
    private BallShoot ShootScript; //Usamos isso para poder setar no outro codigo que o jogador tem a bola

    // Start is called before the first frame update
    void Start()
    {
        Rb.velocity = transform.up * Speed;

        GameObject _player = GameObject.FindGameObjectWithTag("Player"); //ele pega o jogador para poder referenciar o script
        ShootScript = _player.GetComponent<BallShoot>();
         

          
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag=="Player"&& ShootScript.IsPulling == true)
        {    ShootScript.HasBall = true;
            Destroy(gameObject);
            
        }
    }


}
