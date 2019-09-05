using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{
    [Tooltip("Altera o angulo da mira.")]
    public float offset;
    [Tooltip("Ativa e desativa o uso do controle.")]
    public bool useController;

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
            Vector3 playerDirection = Vector3.right * Input.GetAxisRaw("RHorizontal") + Vector3.forward * -Input.GetAxisRaw("RVertical");
            //float rotZ = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;
            if (playerDirection.sqrMagnitude > 0.0f)
            {
                transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up); //mudei de Up para right
                //transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
                //transform.rotation = Quaternion.AngleAxis(rotZ, playerDirection);
            }
        }

    }

}
