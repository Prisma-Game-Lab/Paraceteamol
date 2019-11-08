using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Tooltip("A plataforma que se moverá")]
    [SerializeField] private Rigidbody2D platformRb;
    [SerializeField] private float moveSpeed = 0.5f;

    [SerializeField] private Transform playersParent;
    [Tooltip("Pontos pelos quais a plataforma se moverá")]
    [SerializeField] List<Transform> waypoints;
    private int currentWaypoint;
    private Vector2 currentMoveDirection;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = waypoints[0].position;
        currentWaypoint = 1;
        currentMoveDirection = GetDirectionBetweenPoints(waypoints[currentWaypoint].position, transform.position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(waypoints[currentWaypoint].position);
        if (Vector2.Distance(transform.position, waypoints[currentWaypoint].position) > 1)
        {
            platformRb.MovePosition(platformRb.position + currentMoveDirection * moveSpeed);
        }
        else
        {
            GetNextWaypoint();
            currentMoveDirection = GetDirectionBetweenPoints(waypoints[currentWaypoint].position, transform.position);
        }
    }

    // Returns a normalized vector indicating the direction the platform will move
    private Vector2 GetDirectionBetweenPoints(Vector2 target, Vector2 currentPosition)
    {
        Vector2 direction = target - currentPosition;
        return direction.normalized;
    }

    private int GetNextWaypoint()
    {
        currentWaypoint++;
        if (currentWaypoint > waypoints.Count - 1)
            currentWaypoint = 0;

        return currentWaypoint;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.rigidbody.velocity = Vector2.zero;
            collision.rigidbody.position = new Vector2(collision.rigidbody.position.x, transform.position.y + 2);
        }
    }
    private void OnCollisionLeave2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.rigidbody.velocity = Vector2.zero;
            collision.rigidbody.position = new Vector2(collision.rigidbody.position.x, transform.position.y);
        }
    }
}
