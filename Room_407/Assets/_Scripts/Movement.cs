using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public bool IsMoving;
    [SerializeField] private Transform Cam;
    [SerializeField] private Rigidbody Rb;
    [SerializeField] private GameEvent OnStep;
    [SerializeField] private float StepDistance;
    [SerializeField] private float MovementSpeed;
    [SerializeField] private float SprintMultiplier;
    private Vector3 _movementVector;
    private Vector3 _lastPosition;
    private float _stepProgress;
    
    private void Start()
    {
        IsMoving = false;
        _lastPosition = transform.position;
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        var input = ctx.ReadValue<Vector2>();
        _movementVector = new Vector3(input.x, 0, input.y);
        IsMoving = _movementVector != Vector3.zero;
    }

    private void FixedUpdate()
    {
        var moveSpeed = Keyboard.current.shiftKey.isPressed ? MovementSpeed * SprintMultiplier : MovementSpeed;
        Rb.AddForce(GetMovementDirection() * (moveSpeed * Time.fixedDeltaTime), ForceMode.VelocityChange);
        HandleSteps();
    }

    private void HandleSteps()
    {
        if (!IsMoving) 
        {
            _lastPosition = transform.position;
            _stepProgress = 0;
            return;
        }

        var horizontalMove = transform.position - _lastPosition;
        horizontalMove.y = 0;

        _stepProgress += horizontalMove.magnitude;

        if (_stepProgress >= StepDistance)
        {
            OnStep.Raise();
            _stepProgress = 0f;
        }

        _lastPosition = transform.position;
    }


    private Vector3 GetMovementDirection()
    {
        var camForward = Cam.forward;
        var camForwardX = camForward.x;
        var camForwardZ = camForward.z;

        var forward = new Vector3(camForwardX, 0, camForwardZ).normalized;
        var right = new Vector3(camForwardZ, 0, -camForwardX).normalized;

        return forward * _movementVector.z + right * _movementVector.x;
    }
}