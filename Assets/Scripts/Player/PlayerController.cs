using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform mainCamera;
    [SerializeField] private float speed = 15f;

    private CharacterController characterController;
    private InputAction moveAction;
    private InputAction jumpAction;
    private Vector2 moveValue;

    private Vector3 direction;
    private float rotationTime = 0.1f;
    private float rotationSpeed;

    private float gravity = 30f;
    private float jumpSpeed = 23f;
    private float vecticalMovement = 0f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    void Update()
    {
        moveValue = moveAction.ReadValue<Vector2>();
        BuildSurfaceMovement();
        BuildVerticalMovement();
        characterController.Move(direction);
    }

    private void BuildSurfaceMovement()
    {
        direction = new Vector3(moveValue.x, 0f, moveValue.y);

        if (direction.magnitude > 1.0f) direction = direction.normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, rotationTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            float tempSpeed = speed;
            if (!characterController.isGrounded) tempSpeed /= 2;

            Vector3 directionWithCamera = (Quaternion.Euler(0f, angle, 0f) * Vector3.forward).normalized;
            float originalMovementMagnitude = direction.magnitude;
            direction.x = directionWithCamera.x * tempSpeed * originalMovementMagnitude * Time.deltaTime;
            direction.z = directionWithCamera.z * tempSpeed * originalMovementMagnitude * Time.deltaTime;
        }
        else
        {
            direction = Vector3.zero;
        }
    }

    private void BuildVerticalMovement()
    {
        if (!characterController.isGrounded)
            vecticalMovement -= gravity * Time.deltaTime;

        if (jumpAction.WasPerformedThisFrame())
        {
            if (characterController.isGrounded)
                vecticalMovement = jumpSpeed;
        }

        direction.y = vecticalMovement * Time.deltaTime;
    }
}
