using UnityEngine;

public class camerafollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 4f, -8f);

    void LateUpdate()
    {
        transform.position = target.position + target.TransformDirection(offset);
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}