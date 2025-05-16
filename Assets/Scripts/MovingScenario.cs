using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MovingScenario : MonoBehaviour
{
    public GameObject nextPrefab;
    public float tileSize, speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        if (transform.position.x < -8f)
        {
            transform.position = new Vector2(nextPrefab.transform.position.x + tileSize, nextPrefab.transform.position.y);
        }
    }
}
