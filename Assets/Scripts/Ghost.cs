using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Represent Ghost behavior.
/// </summary>
public class Ghost : MonoBehaviour
{
    public NavMeshAgent agent;
    public float speed = 1.0f;
    public Animator animator ;

    // Update is called once per frame
    void Update()
    {
        if(!agent.enabled)
            return;

        Vector3 targetPosition = Camera.main.transform.position;       

        agent.SetDestination(targetPosition);
        agent.speed = speed;
    }

    public void Kill()
    {
        agent.enabled = false;
        animator.SetTrigger("Death");   // Set "Death" trigger to the condition of the transition to GhostDeath.anim.
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
