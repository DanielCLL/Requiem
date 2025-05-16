using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GetCoin : MonoBehaviour
{
    public AudioSource coinGet;
    public PointsController pointsController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            coinGet.Play();
            if(pointsController != null) pointsController.AddPoints(10);
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            Destroy(this.gameObject,2);
        }
    }
}
