using UnityEngine;

public class AnimalBehavior : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float changeDirectionInterval = 2f;

    private Vector3 moveDirection;
    private float changeDirectionTimer;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetRandomDirection();
        changeDirectionTimer = changeDirectionInterval;
    }

    private void Update()
    {
        changeDirectionTimer -= Time.deltaTime;
        if (changeDirectionTimer <= 0f)
        {
            SetRandomDirection();
            changeDirectionTimer = changeDirectionInterval;
        }

        MoveAnimal();
    }

    private void MoveAnimal()
    {
        rb.velocity = moveDirection * moveSpeed;
    }

    private void SetRandomDirection()
    {
        moveDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
        moveDirection.Normalize();
    }

    private void OnTriggerEnter(Collider collision)
    {
        SetRandomDirection();
    }
}
