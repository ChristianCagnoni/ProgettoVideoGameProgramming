using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private void LateUpdate()
    {
        Vector3 desideredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desideredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

}