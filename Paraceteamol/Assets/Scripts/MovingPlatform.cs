using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private Rigidbody2D platformRb;
    [SerializeField] private float moveSpeed = 0.5f;
    [Tooltip("O GameObject pai dos waypoints pelos quais a plataforma trafegará")]
    [SerializeField] private Transform waypointsParent;

    private List<Transform> waypoints = new List<Transform>();
    private int currentWaypoint;
    private Vector2 currentMoveDirection;

    void Start()
    {
        // Pegando referências
        platformRb = GetComponent<Rigidbody2D>();
        for(int i = 0 ; i < waypointsParent.childCount ; ++i)
        {
            waypoints.Add(waypointsParent.GetChild(i));
        }

        // Setando valores para iniciar o movimento
        transform.position = waypoints[0].position;
        currentWaypoint = 1;
        currentMoveDirection = GetDirectionBetweenPoints(waypoints[currentWaypoint].position, transform.position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
