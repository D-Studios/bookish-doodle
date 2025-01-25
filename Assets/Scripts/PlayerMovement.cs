using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the player's movement

    private Vector3 moveDirection = Vector3.zero;
    private Rigidbody2D rb;

    [Header("Clamp Settings")]
    public Vector2 minClamp;
    public Vector2 maxClamp;

    void Start()
    {
        // Get the Rigidbody2D component attached to the player
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Check input for movement
        HandleInput();
    }

    void FixedUpdate()
    {
        // Apply movement to the Rigidbody2D
        rb.linearVelocity = moveDirection * moveSpeed;
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, minClamp.x, maxClamp.x), Mathf.Clamp(transform.position.y, minClamp.y, maxClamp.y));
    }

    void HandleInput()
    {
        // Reset move direction to ensure no diagonal movement
        moveDirection = Vector3.zero;

        // Check for specific key presses
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) // Move up
        {
            moveDirection = Vector3.up;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) // Move down
        {
            moveDirection = Vector3.down;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) // Move left
        {
            moveDirection = Vector3.left;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) // Move right
        {
            moveDirection = Vector3.right;
        }
    }
}