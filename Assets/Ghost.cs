using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Represent Ghost behavior.
/// </summary>
public class Ghost : MonoBehaviour
{
    public NavMeshAgent agent;
    public float speed = 1.0f;

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = Camera.main.transform.position;       

        agent.SetDestination(targetPosition);
        agent.speed = speed;
    }
}
