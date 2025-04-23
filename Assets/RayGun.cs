using UnityEngine;

public class RayGun : MonoBehaviour
{
    public LayerMask layerMask;
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
        Ray ray = new Ray(shootingPoint.position, shootingPoint.forward);   // Memo: No argument to specify the endPoint.
        bool hasHit = Physics.Raycast(ray, out RaycastHit hit, maxLineDistance, layerMask); // Physics.Raycast(Ray, HitInfo, MaxDistance, LayerMask)
                                                                                            // Return true if it intersects any collider.
        Vector3 endPoint = Vector3.zero;    // (0, 0, 0)

        if(hasHit)
        {
            // Stop the ray
            endPoint = hit.point;
        }
        else
        {
            endPoint = shootingPoint.position + shootingPoint.forward * maxLineDistance; // foward should be a unit vector 
        }

        // Visualize Ray
        LineRenderer line = Instantiate(linePrefab);
        line.positionCount = 2; // Line has 2 vertex
        line.SetPosition(0, shootingPoint.position); // SetPosition(Vertex index, its position)

        line.SetPosition(1, endPoint);

        Destroy(line.gameObject, lineShowTimer);
    }
}
