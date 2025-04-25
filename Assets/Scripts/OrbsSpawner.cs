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
    private int _currentTry = 0;

    void Awake()
    {
        instance = this; // "this" refers to the component itself, i.e., OrbsSpawner (MonoBehavior)   
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MRUK.Instance.RegisterSceneLoadedCallback(SpawnOrbs);
    }

    /// <summary>
    /// Spawn orbs.
    /// </summary>
    public void SpawnOrbs()
    {

        for(int i = 0; i < numberOfOrbsToSpawn; i++)
        {
            Vector3 randomPosition = Vector3.zero;
            MRUKRoom room = MRUK.Instance.GetCurrentRoom();

            float minDistanceToEdge = 1;

            while(_currentTry < tryCount)
            {
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
