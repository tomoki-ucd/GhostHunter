using System.Collections.Generic;
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

    public GameObject GetClosestOrb()
    {
        GameObject closest = null;

        float minDistance = Mathf.Infinity; // Mathf.Infinity represets the largest number as a float
                                            // This is often used to make comparison starting with to find the smallest one.
        
        List<GameObject> orbs = OrbsSpawner.instance.spawnedOrbs;

        foreach(var orb in orbs)
        {
            float d = Vector3.Distance(transform.position, orb.transform.position);

            if(d < minDistance)
            {
                minDistance = d;
                closest = orb;
            }
        }
        return closest;
    }

    // Update is called once per frame
    void Update()
    {
        if(!agent.enabled)
            return;

//        Vector3 targetPosition = Camera.main.transform.position;       

        GameObject closest = GetClosestOrb();
        if(closest)
        {
            Vector3 targetPosition = closest.transform.position;
            agent.SetDestination(targetPosition);
            agent.speed = speed;
        }
        else
        {
            Debug.Log($"[{this.name}] Closet orb not found.");
        }
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
