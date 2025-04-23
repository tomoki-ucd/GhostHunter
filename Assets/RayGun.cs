using UnityEngine;

public class RayGun : MonoBehaviour
{
    public OVRInput.RawButton shootingButton;
    public LineRenderer linePrefab;
    public Transform shootingPoint; // Ray Starting Point
    public float maxLineDistance = 5.0f; // Ray max length
    public float lineShowTimer = 0.3f;  // Time length that line survives

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(OVRInput.GetDown(shootingButton))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        Debug.Log($"[{this.name}] Pew pew");


        // Visualize Ray
        LineRenderer line = Instantiate(linePrefab);
        line.positionCount = 2; // Line has 2 vertex
        line.SetPosition(0, shootingPoint.position); // SetPosition(Vertex index, its position)

        Vector3 endPoint = shootingPoint.position + shootingPoint.forward * maxLineDistance; // foward should be a unit vector 

        line.SetPosition(1, endPoint);

        Destroy(line.gameObject, lineShowTimer);
    }
}
