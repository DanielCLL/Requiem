using System.Collections;
using System.Collections.Generic;
//using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Sprite       cofreAbierto;
    public GameObject   objeto;

    private bool        abierto;
    private bool        creado;
    private float       timer;
    // Start is called before the first frame update
    void Start()
    {
        abierto = false;
        creado = false;
        timer = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (abierto && !creado)
        {
            if (objeto != null)
                Instantiate(objeto, new Vector3(transform.position.x, transform.position.y + 0.15f, transform.position.z + -0.1f), Quaternion.identity);
            creado = true;
        }
        else if (abierto && creado && timer >= 0)
        {
            
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Input.GetKey(KeyCode.UpArrow) && !abierto)
        {
            GetComponent<SpriteRenderer>().sprite = cofreAbierto;
            abierto = true;
        }
    }
}
