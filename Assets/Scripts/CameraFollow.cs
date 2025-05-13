using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject objectFollowing;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3 (objectFollowing.transform.position.x, objectFollowing.transform.position.y + 0.5f, objectFollowing.transform.position.z - 10f);
    }
}
