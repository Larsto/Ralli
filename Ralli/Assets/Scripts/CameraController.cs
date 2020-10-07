using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CarController target;
    private Vector3 offsetDir;

    public float minDistansce, maxDistance;
    private float activeDistance;

    public Transform startTargetOffset;
    // Start is called before the first frame update
    void Start()
    {
        offsetDir = transform.position - startTargetOffset.position;

        activeDistance = minDistansce;

        offsetDir.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        activeDistance = minDistansce + ((maxDistance - minDistansce) * (target.theRB.velocity.magnitude / target.maxSpeed));

        transform.position = target.transform.position + (offsetDir * activeDistance);
    }
}
