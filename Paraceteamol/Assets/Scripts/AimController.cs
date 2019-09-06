using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AimController : MonoBehaviour
{
	[Tooltip("Altera o angulo da mira.")]
	public float offset;
	[Tooltip("Ativa e desativa o uso do controle.")]
	public bool useController;

	private Vector3 startPos;
	private Transform thisTransform;
	private bool _playerOne;

	private void Start()
	{
		thisTransform = transform;
		startPos = thisTransform.position;
		_playerOne = GetComponentInParent<PlayerMovement>().PlayerOne;
	}
	private void FixedUpdate()
	{
		if (!useController)
		{
			//Calcula a direção da arma até o local aonde está o cursor do mouse
			Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
			// calcula a rotacao que a arma do jogador deve fazer para ir até o local do cursor do mouse
			float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
		}
		//Rotacionar com o controle
		if (useController)
		{
			Vector3 inputDirection = Vector3.zero;
			inputDirection.x = Input.GetAxis(_playerOne ? "p1_mousex" : "p2_mousex");
			inputDirection.y = -Input.GetAxis(_playerOne ? "p1_mousey" : "p2_mousey");
			float rotZ = Mathf.Atan2(inputDirection.y, inputDirection.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
		}
	}
}