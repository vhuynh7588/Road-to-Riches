using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCarController : MonoBehaviour
{
    [Header("Car settings")]
    public float driftFactor  = 0.95f;
    public float accelerationFactor = 30.0f;
    public float turnFactor = 3.5f;
    public float maxSpeed = 200;

    //Local variables
    float accelerationInput = 0;
    float steeringInput = 0;

    float rotationAngle = 0;

    float velocityVsUp = 0;

    //Components
    Rigidbody2D carRigidbody2D;

    //Awake is called when the script instance is being loaded
    void Awake()
    {
        carRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Frame-rate independent for physics calculations
    void FixedUpdate()
    {
        ApplyEngineForce();

        KillOrthogonalVelocity();

        ApplySteering();
    }

    void ApplyEngineForce()
    {
        velocityVsUp = Vector2.Dot(transform.up, carRigidbody2D.velocity);

        if(velocityVsUp > maxSpeed && accelerationInput > 0)
            return;

        if(velocityVsUp < -maxSpeed *0.5f && accelerationInput < 0)
            return;

        if(carRigidbody2D.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0)
            return;

        if (accelerationInput == 0)
            carRigidbody2D.drag = Mathf.Lerp(carRigidbody2D.drag, 3.0f, Time.fixedDeltaTime * 3);
        else carRigidbody2D.drag = 0;

        Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor;

        carRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);

    
    // // Get forward velocity vs. car orientation
    //     velocityVsUp = Vector2.Dot(transform.up, carRigidbody2D.velocity);

    // // Early return if exceeding max speed in forward or reverse direction
    //     if ((velocityVsUp > maxSpeed && accelerationInput > 0) ||
    //         (velocityVsUp < -maxSpeed * 0.5f && accelerationInput < 0))
    //         return;

    // // Set drag when no input is applied
    //     if (accelerationInput == 0)
    //     {
    //         carRigidbody2D.drag = Mathf.Lerp(carRigidbody2D.drag, 3.0f, Time.fixedDeltaTime * 3);
    //     }
    //     else
    //     {
    //         carRigidbody2D.drag = 0;
    //     }

    // // Apply forward engine force
    //     Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor;
    //     carRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);
    }

    void ApplySteering()
    {
        float minSpeedBeforeAllowTurningFactor = (carRigidbody2D.velocity.magnitude / 8);
        minSpeedBeforeAllowTurningFactor = Mathf.Clamp01(minSpeedBeforeAllowTurningFactor);

        rotationAngle -= steeringInput * turnFactor;

        carRigidbody2D.MoveRotation(rotationAngle);

        // float speedFactor = carRigidbody2D.velocity.magnitude / maxSpeed;
        // float effectiveTurnFactor = Mathf.Lerp(turnFactor, turnFactor * 0.2f, speedFactor);

        // if (carRigidbody2D.velocity.magnitude > 0.1f){
        //    rotationAngle -= steeringInput * effectiveTurnFactor;
        // }
        // carRigidbody2D.MoveRotation(rotationAngle);
        // float speedFactor = carRigidbody2D.velocity.magnitude / maxSpeed;
        // float effectiveTurnFactor = Mathf.Lerp(turnFactor, turnFactor * 0.2f, speedFactor);

    
        // if (Mathf.Abs(velocityVsUp) > 0.1f) 
        // {
        //     float torque = -steeringInput * effectiveTurnFactor * carRigidbody2D.velocity.magnitude;
        //     carRigidbody2D.AddTorque(torque, ForceMode2D.Force);
        // }
    }

    void KillOrthogonalVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody2D.velocity, transform.right);

        carRigidbody2D.velocity = forwardVelocity + rightVelocity * driftFactor;
        // float adjustedDriftFactor = Mathf.Lerp(1.0f, driftFactor, carRigidbody2D.velocity.magnitude / maxSpeed);
        // Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody2D.velocity, transform.up);
        // Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody2D.velocity, transform.right);
        // carRigidbody2D.velocity = forwardVelocity + rightVelocity * adjustedDriftFactor;
    }

    public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }

}
