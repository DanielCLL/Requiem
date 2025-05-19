using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GetCoin : MonoBehaviour
{
    public GameObject coinPrefab;
    public int value;

    private AudioSource[] audioManager;
    private GUIController pointsController;

    // Start is called before the first frame update
    void Start()
    {
        pointsController = GameObject.Find("GameManager").GetComponent<GUIController>();
        audioManager = GameObject.Find("AudioManager").GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            audioManager[0].Play();
            if(pointsController != null) pointsController.AddPoints(value);
            coinPrefab.GetComponent<BoxCollider2D>().enabled = false;
            coinPrefab.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(coinPrefab);
        }
    }
}
