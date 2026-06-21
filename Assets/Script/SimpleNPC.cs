using UnityEngine;

public class SimpleNPC : MonoBehaviour
{
    [Header("Waypoints")]
    public Transform[] waypoints;

    [Header("Movement")]
    public float speed = 8f;
    public float turnSpeed = 8f;
    public float reachDistance = 3f;

    private int currentWaypoint = 0;

    void Update()
    {
        if (waypoints == null || waypoints.Length == 0)
            return;

        Transform target = waypoints[currentWaypoint];

        Vector3 direction = target.position - transform.position;

        if (direction.magnitude < reachDistance)
        {
            currentWaypoint++;

            if (currentWaypoint >= waypoints.Length)
                currentWaypoint = 0;

            return;
        }

        // Putar ke arah waypoint
        Quaternion targetRotation = Quaternion.LookRotation(direction.normalized);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            turnSpeed * Time.deltaTime
        );

        // Gerak maju
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}