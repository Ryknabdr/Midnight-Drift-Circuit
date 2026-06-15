using UnityEngine;

public class mobil : MonoBehaviour
{
    private float horizontalInput, verticalInput;
    private float buttonHorizontal, buttonVertical;

    private float currentSteerAngle, currentBreakForce;
    private bool isBreaking;
    private bool brakeButton;

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
        float keyboardHorizontal = Input.GetAxis("Horizontal");
        float keyboardVertical = Input.GetAxis("Vertical");

        horizontalInput = Mathf.Clamp(keyboardHorizontal + buttonHorizontal, -1f, 1f);
        verticalInput = Mathf.Clamp(keyboardVertical + buttonVertical, -1f, 1f);

        isBreaking = Input.GetKey(KeyCode.Space) || brakeButton;
    }

    public void LeftDown()
    {
        buttonHorizontal = -1f;
    }

    public void RightDown()
    {
        buttonHorizontal = 1f;
    }

    public void TurnUp()
    {
        buttonHorizontal = 0f;
    }

    public void GasDown()
    {
        buttonVertical = 1f;
    }

    public void GasUp()
    {
        buttonVertical = 0f;
    }

    public void BrakeDown()
    {
        if (rb.linearVelocity.magnitude > 1f)
        {
            brakeButton = true;
            buttonVertical = 0f;
        }
        else
        {
            brakeButton = false;
            buttonVertical = -1f;
        }
    }

    public void BrakeUp()
    {
        brakeButton = false;
        buttonVertical = 0f;
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
        SetRearGrip(isBreaking ? driftGrip : normalGrip);
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

        wheelTransform.position = pos;
        wheelTransform.rotation = rot;
    }
}