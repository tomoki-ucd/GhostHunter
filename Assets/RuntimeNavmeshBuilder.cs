using UnityEngine;
using Unity.AI.Navigation;
using Meta.XR.MRUtilityKit;
using System.Collections;

/// <summary>
/// Provides the functionality to build Nav Mesh based on the player's scene.
/// </summary>
public class RuntimeNavmeshBuilder : MonoBehaviour
{
    private NavMeshSurface navmeshSurface;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navmeshSurface = GetComponent<NavMeshSurface>();    // Get NavMeshSurface component of NavMeshSurface gameobject.
        MRUK.Instance.RegisterSceneLoadedCallback(BuildNavmesh);    // Need to wait for scene loaded before calling navmeshSurface.BUildNavMesh();
                                                                    // That's why to use RegisterSceneLoadedCallback().
    }

    /// <summary>
    /// Call BuildNavmeshRoutine coroutine.
    /// </summary>
    public void BuildNavmesh()
    {
        StartCoroutine(BuildNavmeshRoutine());
    }


    /// <summary>
    /// Coroutine to wait till the end of frame to call NavMesh generation.
    /// </summary>
    public IEnumerator BuildNavmeshRoutine()
    {
        yield return new WaitForEndOfFrame(); // When coroutine encounter this, it pauses the execution unilt the end of the current frame.
        navmeshSurface.BuildNavMesh();
    }
}
