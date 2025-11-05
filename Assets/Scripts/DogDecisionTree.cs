using UnityEngine;

public class DogDecisionTree : MonoBehaviour
{
    private DogStateMachine stateMachine;
    private Transform player;
    
    [Header("Decision Parameters")]
    public float checkInterval = 0.5f;
    public float playerNearDistance = 5f;
    public float playerFarDistance = 10f;
    
    private float lastCheckTime;

    private void Start()
    {
        stateMachine = GetComponent<DogStateMachine>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lastCheckTime = 0f;
    }

    private void Update()
    {
        // Only make decisions periodically to save resources
        if (Time.time - lastCheckTime < checkInterval)
            return;

        lastCheckTime = Time.time;
        MakeDecisions();
    }

    private void MakeDecisions()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Decision Tree Structure:
        if (IsPlayerNear(distanceToPlayer))
        {
            // Player is within detection range
            if (IsPlayerTooFar(distanceToPlayer))
            {
                // Player has gone too far, return home
                stateMachine.enabled = true;
            }
            else
            {
                // Player is in good range, follow them
                stateMachine.enabled = true;
            }
        }
        else
        {
            // Player is not near, stay in idle state
            stateMachine.enabled = true;
        }
    }

    private bool IsPlayerNear(float distance)
    {
        return distance <= playerNearDistance;
    }

    private bool IsPlayerTooFar(float distance)
    {
        return distance > playerFarDistance;
    }
}