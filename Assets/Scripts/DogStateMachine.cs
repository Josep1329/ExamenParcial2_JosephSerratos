using UnityEngine;

public enum DogState
{
    Idle,
    FollowPlayer,
    SearchPlayer,
    ReturnHome
}

public class DogStateMachine : MonoBehaviour
{
    private DogState currentState;
    private Transform player;
    private Vector3 homePosition;
    public float detectionRange = 5f;
    public float maxFollowRange = 10f;
    public float moveSpeed = 3f;
    public float searchTime = 10f;
    public float rotationSpeed = 100f;
    
    private float searchTimer;
    private Vector3 randomSearchDirection;

    private void Start()
    {
        currentState = DogState.Idle;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        homePosition = transform.position;
    }

    private void Update()
    {
        switch (currentState)
        {
            case DogState.Idle:
                HandleIdleState();
                break;
            case DogState.FollowPlayer:
                HandleFollowPlayerState();
                break;
            case DogState.SearchPlayer:
                HandleSearchPlayerState();
                break;
            case DogState.ReturnHome:
                HandleReturnHomeState();
                break;
        }
    }

    private void HandleIdleState()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        
        // If player is within detection range, start following
        if (distanceToPlayer <= detectionRange)
        {
            currentState = DogState.FollowPlayer;
        }
    }

    private void HandleFollowPlayerState()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        
        // If player is too far, start searching
        if (distanceToPlayer > maxFollowRange)
        {
            currentState = DogState.SearchPlayer;
            searchTimer = searchTime;
            randomSearchDirection = Random.insideUnitSphere;
            randomSearchDirection.y = 0;
            randomSearchDirection.Normalize();
            return;
        }

        // Move towards player
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
        
        // Look at player
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
    }

    private void HandleSearchPlayerState()
    {
        // Reduce search timer
        searchTimer -= Time.deltaTime;

        // Check if we can see the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRange)
        {
            currentState = DogState.FollowPlayer;
            return;
        }

        // If search time is up, go home
        if (searchTimer <= 0)
        {
            currentState = DogState.ReturnHome;
            return;
        }

        // Move in the random search direction
        transform.position += randomSearchDirection * moveSpeed * Time.deltaTime;
        
        // Rotate while searching
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // Every few seconds, change direction
        if (searchTimer % 2 < Time.deltaTime)
        {
            randomSearchDirection = Random.insideUnitSphere;
            randomSearchDirection.y = 0;
            randomSearchDirection.Normalize();
        }
    }

    private void HandleReturnHomeState()
    {
        float distanceToHome = Vector3.Distance(transform.position, homePosition);
        
        // If we're close to home, go back to idle
        if (distanceToHome < 0.1f)
        {
            transform.position = homePosition;
            currentState = DogState.Idle;
            return;
        }

        // Move towards home
        Vector3 direction = (homePosition - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
        
        // Look towards home
        transform.LookAt(new Vector3(homePosition.x, transform.position.y, homePosition.z));
    }

    // This can be used to visualize the detection and max follow ranges in the editor
    private void OnDrawGizmosSelected()
    {
        // Draw detection range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        
        // Draw max follow range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxFollowRange);

        // Draw home position
        if (Application.isPlaying)
        {
            // Draw home position marker
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(homePosition, Vector3.one);
            Gizmos.DrawLine(transform.position, homePosition);
        }
        else
        {
            // Show current position as potential home
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, Vector3.one);
        }
    }
}