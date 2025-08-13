using UnityEngine;

namespace VampireSurvivors.Camera
{
    public class CameraController : MonoBehaviour
    {
        [Header("Follow Settings")]
        [SerializeField] private Transform target;
        [SerializeField] private float smoothSpeed = 5f;
        [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);
        
        [Header("Camera Bounds (Optional)")]
        [SerializeField] private bool useBounds = false;
        [SerializeField] private Vector2 minBounds;
        [SerializeField] private Vector2 maxBounds;

        private Vector3 velocity = Vector3.zero;
        private Camera cam;

        private void Awake()
        {
            cam = GetComponent<Camera>();
            if (cam == null)
            {
                cam = gameObject.AddComponent<Camera>();
            }
            
            // Set camera to orthographic for 2D
            cam.orthographic = true;
            cam.orthographicSize = 5f; // Adjust for desired zoom level
        }

        private void Start()
        {
            // Try to find player if target not set
            if (target == null)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    target = player.transform;
                }
            }
        }

        private void LateUpdate()
        {
            if (target == null) return;

            FollowTarget();
        }

        private void FollowTarget()
        {
            Vector3 desiredPosition = target.position + offset;
            
            // Apply bounds if enabled
            if (useBounds)
            {
                desiredPosition.x = Mathf.Clamp(desiredPosition.x, minBounds.x, maxBounds.x);
                desiredPosition.y = Mathf.Clamp(desiredPosition.y, minBounds.y, maxBounds.y);
            }
            
            // Smooth camera movement
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, 1f / smoothSpeed);
            transform.position = smoothedPosition;
        }

        public void SetTarget(Transform newTarget)
        {
            target = newTarget;
        }

        public void SetZoom(float orthographicSize)
        {
            if (cam != null)
            {
                cam.orthographicSize = Mathf.Clamp(orthographicSize, 2f, 10f);
            }
        }

        public float GetZoom()
        {
            return cam != null ? cam.orthographicSize : 5f;
        }
    }
}
