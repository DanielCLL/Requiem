using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BackgroundMoving : MonoBehaviour
{
    public GameObject backgroundPrefab;

    public bool instanciado, destruido;

    // Start is called before the first frame update
    void Start()
    {
        destruido = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * 0.5f * Time.deltaTime);
        if (transform.position.x < 4.5f && !instanciado)
        {
            Instantiate(backgroundPrefab, new Vector2(transform.position.x + 4.26f, transform.position.y), Quaternion.identity);
            instanciado = true;
        }
        if (transform.position.x < -8 && !destruido)
        {
            Destroy(gameObject);
            destruido = true;
        }
    }
}
