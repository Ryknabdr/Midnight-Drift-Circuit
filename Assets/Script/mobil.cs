using UnityEngine;

public class mobil : MonoBehaviour
{
    private float horizontalInput, verticalInput;
    private float currentSteerAngle, currentBreakForce;
    private bool isBreaking;
    private Rigidbody rb;

    [SerializeField] private float motorForce = 1000f;
    [SerializeField] private float breakForce = 3000f;
    [SerializeField] private float maxSteerAngle = 12f;

    [Header("Stability Setting")]
    [SerializeField] private float centerOfMassY = -0.8f;

    [Header("Drift Setting")]
    [SerializeField] private float normalGrip = 1.2f;
    [SerializeField] private float driftGrip = 0.8f;

    [SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider, rearRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0f, centerOfMassY, 0f);
    }

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        HandleDrift();
        UpdateWheels();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;

        currentBreakForce = isBreaking ? breakForce * 0.3f : 0f;
        ApplyBreaking();
    }

    private void ApplyBreaking()
    {
        frontLeftWheelCollider.brakeTorque = currentBreakForce;
        frontRightWheelCollider.brakeTorque = currentBreakForce;

        rearLeftWheelCollider.brakeTorque = isBreaking ? breakForce : 0f;
        rearRightWheelCollider.brakeTorque = isBreaking ? breakForce : 0f;
    }

    private void HandleDrift()
    {
        if (isBreaking)
            SetRearGrip(driftGrip);
        else
            SetRearGrip(normalGrip);
    }

    private void SetRearGrip(float grip)
    {
        WheelFrictionCurve leftFriction = rearLeftWheelCollider.sidewaysFriction;
        WheelFrictionCurve rightFriction = rearRightWheelCollider.sidewaysFriction;

        leftFriction.stiffness = grip;
        rightFriction.stiffness = grip;

        rearLeftWheelCollider.sidewaysFriction = leftFriction;
        rearRightWheelCollider.sidewaysFriction = rightFriction;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateWheelPos(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateWheelPos(frontRightWheelCollider, frontRightWheelTransform);
        UpdateWheelPos(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateWheelPos(rearRightWheelCollider, rearRightWheelTransform);
    }

    private void UpdateWheelPos(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;

        wheelCollider.GetWorldPose(out pos, out rot);

        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}