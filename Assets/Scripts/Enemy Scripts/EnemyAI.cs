using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 200f;
    [SerializeField] private float nextWaypointDistance = 3f;

    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;

    [SerializeField] private Seeker seeker;
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private Transform enemyGraphics;

    private void Start()
    {
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    private void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rigidbody2D.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rigidbody2D.AddForce(force);

        float distance = Vector2.Distance(rigidbody2D.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (rigidbody2D.velocity.x >= 0.01f)
        {
            enemyGraphics.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (rigidbody2D.velocity.x <= -0.01f)
        {
            enemyGraphics.localScale = new Vector3(-1f, 1f, 1f);
        }

    }

    private void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rigidbody2D.position, target.position, OnPathComplete);
        }
    }

    private void OnPathComplete(Path path)
    {
        if (!path.error)
        {
            this.path = path;
            currentWaypoint = 0;
        }
    }
}
