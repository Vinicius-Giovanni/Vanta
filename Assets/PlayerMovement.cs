using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float sprintSpeed = 8f;
    public float jumpHeight = 5f;
    public float gravity = -9.81f;

    private CharacterController controller;

    private PlayerInputActions input;

    private Vector2 move;

    private Vector3 velocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        input = new PlayerInputActions();
    }

    private void OnEnable()
    {
        input.Player.Enable();
    }

    private void OnDisable()
    {
        input.Player.Disable();
    }

    void Update()
    {
        move = input.Player.Move.ReadValue<Vector2>();

        float speed = input.Player.Sprint.IsPressed()
            ? sprintSpeed
            : walkSpeed;

        Vector3 direction =
            transform.right * move.x +
            transform.forward * move.y;

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (input.Player.Jump.WasPressedThisFrame() && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        Vector3 finalMove = direction * speed;
        finalMove.y = velocity.y;

        controller.Move(finalMove * Time.deltaTime);

        if (input.Player.Jump.WasPressedThisFrame())
        {
            Debug.Log("Jump pressionado");
        }

        Debug.Log(controller.isGrounded);
    }
}