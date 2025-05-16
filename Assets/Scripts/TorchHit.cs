using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchHit : MonoBehaviour
{
    public GameObject hitbox;
    public GameObject monedaBPrefab, monedaPPrefab, monedaOPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == hitbox)
        {
            float valor = Random.Range(0f, 1f);
            if (valor < 0.6f) Instantiate(monedaBPrefab);
            else if (valor < 0.9f) Instantiate(monedaPPrefab);
            else Instantiate(monedaOPrefab);
            Destroy(this.gameObject);
        }
    }
}
