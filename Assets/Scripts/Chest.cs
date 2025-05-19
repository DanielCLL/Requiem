using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Sprite cofreAbierto;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GetComponent<SpriteRenderer>().sprite = cofreAbierto;
        }
    }
}
