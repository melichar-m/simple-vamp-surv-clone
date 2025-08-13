using UnityEngine;

namespace VampireSurvivors.Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float acceleration = 10f;
        [SerializeField] private float deceleration = 10f;

        [Header("Components")]
        private Rigidbody2D rb;
        private SpriteRenderer spriteRenderer;
        
        private Vector2 moveInput;
        private Vector2 currentVelocity;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                rb = gameObject.AddComponent<Rigidbody2D>();
                rb.gravityScale = 0f;
                rb.freezeRotation = true;
            }

            spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            }
        }

        private void Start()
        {
            // Set up the player's visual (blue circle)
            SetupPlayerVisual();
        }

        private void Update()
        {
            HandleInput();
        }

        private void FixedUpdate()
        {
            HandleMovement();
        }

        private void HandleInput()
        {
            // Get input from WASD or Arrow keys
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");
            
            // Normalize diagonal movement
            if (moveInput.magnitude > 1f)
            {
                moveInput.Normalize();
            }
        }

        private void HandleMovement()
        {
            if (moveInput.magnitude > 0)
            {
                // Accelerate towards target velocity
                currentVelocity = Vector2.MoveTowards(currentVelocity, moveInput * moveSpeed, acceleration * Time.fixedDeltaTime);
            }
            else
            {
                // Decelerate to stop
                currentVelocity = Vector2.MoveTowards(currentVelocity, Vector2.zero, deceleration * Time.fixedDeltaTime);
            }

            // Apply movement
            rb.velocity = currentVelocity;

            // Flip sprite based on movement direction
            if (moveInput.x != 0)
            {
                spriteRenderer.flipX = moveInput.x < 0;
            }
        }

        private void SetupPlayerVisual()
        {
            // Create a blue circle for the player
            GameObject circle = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            MeshRenderer meshRenderer = circle.GetComponent<MeshRenderer>();
            
            if (meshRenderer != null)
            {
                // Create a simple blue material
                Material blueMaterial = new Material(Shader.Find("Sprites/Default"));
                blueMaterial.color = new Color(0.2f, 0.4f, 0.9f, 1f); // Nice blue color
                spriteRenderer.material = blueMaterial;
                
                // Clean up 3D components since we're in 2D
                Destroy(circle.GetComponent<MeshRenderer>());
                Destroy(circle.GetComponent<SphereCollider>());
                Destroy(circle);
            }

            // Set sprite to a circle (will be set in Unity editor)
            transform.localScale = new Vector3(0.3f, 0.3f, 1f);
        }

        public Vector2 GetVelocity()
        {
            return currentVelocity;
        }

        public Vector2 GetMoveInput()
        {
            return moveInput;
        }
    }
}
