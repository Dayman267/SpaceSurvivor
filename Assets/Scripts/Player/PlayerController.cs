using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private Transform mainCamera;
    [SerializeField] private float speed = 5f;
    [SerializeField] private Joystick joystick;
    private Vector3 moveDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(
            transform.rotation.eulerAngles.x, 
            mainCamera.rotation.eulerAngles.y,
            transform.rotation.eulerAngles.z);
    }

    private void FixedUpdate()
    {
        Walk();
    }

    private void Walk()
    {
        moveDirection = new Vector3(joystick.Horizontal, 0f, joystick.Vertical);
        moveDirection = transform.TransformDirection(moveDirection);
        rb.velocity = moveDirection * speed;
    }
}
