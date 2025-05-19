using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GUIController    keysController;

    private float           timer;
    // Start is called before the first frame update
    void Start()
    {
        keysController = GameObject.Find("GameManager").GetComponent<GUIController>();
        keysController.GetKey();
        timer = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
            transform.Translate(Vector2.up * 0.08f * Time.deltaTime);
        } else
            Destroy(gameObject,1);
    }
}
