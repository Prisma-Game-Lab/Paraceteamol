using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{
    public float offset;

    private void FixedUpdate()
    {
        //Calcula a direção da arma até o local aonde está o cursor do mouse
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        // calcula a rotacao que a arma do jogador deve fazer para ir até o local do cursor do mouse
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

    }

}
