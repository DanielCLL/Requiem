using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject objectFollowing;
    //public float minX = -110.0f, maxX = 100.0435f, minY = -7f, maxY = 7f;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(objectFollowing.transform.position.x, objectFollowing.transform.position.y + 0.5f, -10f);
        /*transform.position = new Vector3(Mathf.Max(minX,Mathf.Min(maxX,objectFollowing.transform.position.x)),
                                        Mathf.Max(minY,Mathf.Min(maxY,objectFollowing.transform.position.y)) + 0.5f,
                                        objectFollowing.transform.position.z - 10f);
        /*if (objectFollowing.transform.position.x > 13f)
        {
            minX = 17.913f;
            maxX = 35.37f;
        }*/
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        c
    }*/
    /*
    public void setMinX(float x)
    {
        minX = x;
    }
    public void setMaxX(float x)
    {
        maxX = x;
    }
    public void setMinY(float y)
    {
        minY = y;
    }
    public void setMaxY(float y)
    {
        maxY = y;
    }
    */
}
