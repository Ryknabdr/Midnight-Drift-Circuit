using UnityEngine;

public class AICarController : MonoBehaviour
{
    [Header("Waypoints")]
    public Transform[] waypoints;
    public float motorForce = 3000f;
    public float maxSteerAngle = 20f;
    public float waypointDistance = 8f;

    private int currentWaypoint = 0;

    [Header("Wheel Colliders")]
    public WheelCollider frontLeftWheelCollider;
    public WheelCollider frontRightWheelCollider;
    public WheelCollider rearLeftWheelCollider;
    public WheelCollider rearRightWheelCollider;

    [Header("Wheel Meshes")]
    public Transform frontLeftWheelTransform;
    public Transform frontRightWheelTransform;
    public Transform rearLeftWheelTransform;
    public Transform rearRightWheelTransform;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Sama seperti mobil player
        rb.centerOfMass = new Vector3(0f, -0.8f, 0f);
    }

    void FixedUpdate()
    {
        if (waypoints == null || waypoints.Length == 0)
            return;

        Transform target = waypoints[currentWaypoint];

        Vector3 localTarget = transform.InverseTransformPoint(target.position);

        float steerInput = Mathf.Clamp(
            localTarget.x / localTarget.magnitude,
            -1f,
            1f
        );

        float steerAngle = steerInput * maxSteerAngle;

        frontLeftWheelCollider.steerAngle = steerAngle;
        frontRightWheelCollider.steerAngle = steerAngle;

        frontLeftWheelCollider.motorTorque = motorForce;
        frontRightWheelCollider.motorTorque = motorForce;

        if (Vector3.Distance(transform.position, target.position) < waypointDistance)
        {
            currentWaypoint++;

            if (currentWaypoint >= waypoints.Length)
                currentWaypoint = 0;
        }

        UpdateWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateWheel(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateWheel(rearRightWheelCollider, rearRightWheelTransform);
    }

    void UpdateWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;

        wheelCollider.GetWorldPose(out pos, out rot);

        wheelTransform.position = pos;
        wheelTransform.rotation = rot;
    }
}