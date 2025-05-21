using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject lockGO;
    public AudioSource doorOpen;

    private GameObject gameManager;
    private bool open;
    private float maxY;    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        maxY = transform.position.y + 0.8f;
        open = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (open && maxY - transform.position.y > 0)
            transform.Translate(Vector2.up * 0.5f * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !open)
        {
            if (gameManager.GetComponent<GUIController>().GetKeys() > 0)
            {
                gameManager.GetComponent<GUIController>().UseKey();
                open = true;
                Destroy(lockGO);
                doorOpen.Play();
            }
        }
    }
}
