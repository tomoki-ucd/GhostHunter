using UnityEngine;
using Meta.XR.MRUtilityKit;
using System.Collections.Generic;

/// <summary>
/// Provides functionality to spawn orbs.
/// </summary>
public class OrbsSpawner : MonoBehaviour
{
    public static OrbsSpawner instance;
    public int numberOfOrbsToSpawn = 5;
    public GameObject orbPrefab;
    public float height;    // Height of Orbs to be spawend.

    public List<GameObject> spawnedOrbs;

    public int tryCount = 1000;

    void Awake()
    {
        instance = this; // "this" refers to the component itself, i.e., OrbsSpawner (MonoBehavior)   
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MRUK.Instance.RegisterSceneLoadedCallback(SpawnOrbs);
    }

    public void DestroyOrb(GameObject orb)
    {
        spawnedOrbs.Remove(orb);    // List type has Remove method.
        Destroy(orb);

        if(spawnedOrbs.Count == 0)
        {
            // Tentative solution
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }

    /// <summary>
    /// Spawn orbs.
    /// </summary>
    public void SpawnOrbs()
    {
        int _currentTry = 0;

        for(int i = 0; i < numberOfOrbsToSpawn; i++)
        {
            Vector3 randomPosition = Vector3.zero;
            MRUKRoom room = MRUK.Instance.GetCurrentRoom();

            float minDistanceToEdge = 1;

            while(_currentTry < tryCount)
            {
                // TO DO: Orb is sometimes spawn on "Not Walkable" area where Ghost cannot enter, which needs to be fixed.
                bool hasFound = room.GenerateRandomPositionOnSurface(MRUK.SurfaceType.FACING_UP, 
                                                                     minDistanceToEdge,
                                                                     new LabelFilter(MRUKAnchor.SceneLabels.FLOOR),
                                                                     out randomPosition, 
                                                                     out Vector3 norm);
                _currentTry++;

                if(hasFound)
                {
                    break;
                }
                else
                {
                    Debug.Log($"[{this.name}] GenerateRandomPositionOnSurface tried {_currentTry} times but failed");
                }
            }
            randomPosition.y = height;

            GameObject spawned = Instantiate(orbPrefab, randomPosition, Quaternion.identity);

            spawnedOrbs.Add(spawned);
        }
    }
}
