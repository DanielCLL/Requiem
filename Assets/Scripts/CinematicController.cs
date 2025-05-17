using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicController : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject mainHero;

    private Animator heroAnimator;
    private Rigidbody2D heroRB;
    private bool cinematic0;
    private float timer;
    private Color black = Color.black;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera.transform.position = new Vector3(-23.56f, 8.3f, -10f);
        timer = 0;
        cinematic0 = true;
        heroRB = mainHero.GetComponent<Rigidbody2D>();
        heroAnimator = heroRB.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cinematic0)
        {
            heroAnimator.SetInteger("AnimState", 1);
            timer += Time.deltaTime;
            if (timer < 2.5f)
            {
                heroRB.velocity = new Vector2(1 * 1.5f, heroRB.velocity.y);
            } else
            {
                heroAnimator.SetInteger("AnimState", 0);
            }

            if (mainHero.transform.position.y < 6)
            {
                mainHero.transform.position = new Vector3(-12.0975f, -2.53f, 0f);
                mainCamera.transform.position = new Vector3(-12.0975f, -3.6635f, -10f);
                cinematic0 = false;
                timer = 0;
            }
        }
    }
}
