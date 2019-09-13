using UnityEngine;

public class BulletScript : MonoBehaviour
{
	[Tooltip("Velocidade com que a bola é lançada quando está com o player")]
	public float Speed = 20;

	[HideInInspector]
	public Rigidbody2D Rb;
	[HideInInspector]
	public bool PlayerIsPulling = false;
	
	void OnCollisionStay2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			PlayerIsPulling = col.gameObject.GetComponentInChildren<AimController>().IsPulling;
			if (PlayerIsPulling)
			{
                transform.position = GameObject.FindGameObjectWithTag("Aim").transform.position;

                Debug.Log(col.gameObject.name + " touched ball");

				// O player não segura mais a bola
				//col.gameObject.GetComponentInChildren<AimController>().HasBall = true;
				//AimScript.HasBall = true;
				//Destroy(this.gameObject);
			}
             
            { 
            }

		}
	}
}
