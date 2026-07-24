using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 12f;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private float gravityModifier = 2f;
    [SerializeField] private LayerMask groundedMask;
    [SerializeField] private float Jumpheight = 5f;
    private float groundDistance = 0.4f;
    private CharacterController characterController;

    private Vector3 velocity;
    private Vector2 moveVec;
    private bool isGrounded;
    private float gravity = -9.81f;
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveVec = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(isGrounded && context.performed)
            velocity.y = Mathf.Sqrt(Jumpheight * -2 * gravity);
    }

    private void Update()
    {
        if (characterController == null)
        {
            return;
        }

        isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, groundedMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = moveVec.x;
        float z = moveVec.y;

        Vector3 move = transform.right * x + transform.forward * z;
        characterController.Move(move * moveSpeed * Time.deltaTime);

        velocity.y += gravity * gravityModifier * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);
    }
}
