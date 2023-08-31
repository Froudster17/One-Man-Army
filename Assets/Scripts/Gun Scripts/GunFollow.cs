using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFollow : MonoBehaviour
{
    [SerializeField] private Transform playerPosition;

    private void Update()
    {
        transform.position = playerPosition.position;
    }
}
