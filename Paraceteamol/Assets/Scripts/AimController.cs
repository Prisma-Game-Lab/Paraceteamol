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

    private void Start()
    {
        thisTransform = transform;
        startPos = thisTransform.position;
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
            inputDirection.x = Input.GetAxis("RHorizontal");
            inputDirection.y = -Input.GetAxis("RVertical");
            float rotZ = Mathf.Atan2(inputDirection.y, inputDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
        }

    }

}
