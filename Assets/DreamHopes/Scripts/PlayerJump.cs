using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerJump : MonoBehaviour
{
    public float jumpYVel = 5f;
    public float gravity = 9.81f;
    private bool isGrounded;
    private Vector3 velocity;
    private CharacterController charControl;
    private Animator anim;

    private void Start()
    {
        charControl = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckForGround();
        HandleJump();
        ApplyGravity();
        charControl.Move(velocity * Time.deltaTime);
    }

    private void CheckForGround()
    {
        // Use a small raycast below the character to check for ground
        isGrounded = charControl.isGrounded || Physics.Raycast(transform.position, Vector3.down, 0.2f);
        anim.SetBool("isGrounded", isGrounded);

        // Reset downward velocity when landing to avoid excessive fall speed accumulation
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Keeps player "sticking" to the ground slightly
        }
    }

    private void HandleJump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = jumpYVel;
            anim.SetTrigger("Jump");
        }
    }

    private void ApplyGravity()
    {
        // Apply gravity acceleration over time
        velocity.y += -gravity * Time.deltaTime;

        // Limit fall speed (optional)
        velocity.y = Mathf.Max(velocity.y, -gravity);
    }
}
