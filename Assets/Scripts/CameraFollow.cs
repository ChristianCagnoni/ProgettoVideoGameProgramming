using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public static Vector3 refOffset;
    public Quaternion rotOffset;
    public static bool isChanged;

    private void Start()
    {
        refOffset = offset;
        isChanged = false;
    }

    private void LateUpdate()
    {
        if (refOffset != offset && isChanged)
        {
            offset= refOffset;
        }
        Vector3 desideredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desideredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        Quaternion desideredRotation = target.rotation*rotOffset;
        transform.rotation = Quaternion.Lerp(transform.rotation, desideredRotation, smoothSpeed);
    }

}