using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionCube : MonoBehaviour
{
    public GameObject gameManagerGO;
    public float new_mX, new_MX, new_mY, new_MY;
    public bool oneTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameManagerGO.GetComponent<CameraTransition>().changeCamLimits(new_mX, new_MX, new_mY, new_MY);
            if (oneTime) Destroy(gameObject);
        }
    }
}
