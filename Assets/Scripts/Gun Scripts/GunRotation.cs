using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotation : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private Vector3 originalScale;
    private void Update()
    {
        RotateGunTowardsMouse();
    }

    private void RotateGunTowardsMouse()
    {
        Vector3 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 directionToMouse = mousePosition - transform.position;
        float angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (directionToMouse.x < 0)
        {
            Vector3 newScale = originalScale;
            newScale.y = -newScale.y;
            transform.localScale = newScale;
        }
        else
        {
            transform.localScale = originalScale;
        }
    }
}
