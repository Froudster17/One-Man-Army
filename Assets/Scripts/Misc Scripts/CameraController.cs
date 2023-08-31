using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector2 viewPortSize;
    private Camera camera;
    private Vector3 currentVelocity;
    private Vector2 distance;

    [SerializeField] private float viewPortFactor;
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private float followDuration;
    [SerializeField] private float maximumFollowSpeed;

    [SerializeField] private Transform player;

    private void Start()
    {
        camera = Camera.main;
        targetPosition = player.position;
    }

    private void Update()
    {
        viewPortSize = (camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)) - camera.ScreenToWorldPoint(Vector2.zero)) * viewPortFactor;

        distance = player.position - transform.position;
        if (Mathf.Abs(distance.x) > viewPortSize.x / 2)
        {
            targetPosition.x = player.position.x - (viewPortSize.x / 2 * Mathf.Sign(distance.x));
        }

        if (Mathf.Abs(distance.y) > viewPortSize.y / 2)
        {
            targetPosition.y = player.position.y - (viewPortSize.y / 2 * Mathf.Sign(distance.y));
        }

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition - new Vector3(0, 0, 10), ref currentVelocity, followDuration, maximumFollowSpeed);
    }

    private void OnDrawGizmos()
    {
        Color c = Color.red;
        c.a = 0.3f;
        Gizmos.color = c;

        Gizmos.DrawCube(transform.position, viewPortSize);
    }
}
