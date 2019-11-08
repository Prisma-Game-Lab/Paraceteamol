using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Tooltip("A plataforma que se moverá")]
    [SerializeField] private Rigidbody2D platformRb;
    [SerializeField] private float moveSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        platformRb.MovePosition(new Vector2(platformRb.position.x - 1*moveSpeed, platformRb.position.y));
    }
}
