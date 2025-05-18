using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    public GameObject cameraGO;

    private CameraFollow camFoll;
    // Start is called before the first frame update
    void Start()
    {
        camFoll = cameraGO.GetComponent<CameraFollow>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeCamLimits(float minX, float maxX, float minY, float maxY)
    {
        camFoll.setMinX(minX);
        camFoll.setMaxX(maxX);
        camFoll.setMinY(minY);
        camFoll.setMaxY(maxY);
    }
}
