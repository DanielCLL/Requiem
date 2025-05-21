using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed;
    public float lifeTime = 2f;

    private GameObject heroKnightGO;
    private int m_facingDirection;
    // Start is called before the first frame update
    void Start()
    {
        heroKnightGO = GameObject.Find("HeroKnight");
        m_facingDirection = heroKnightGO.GetComponent<HeroKnight>().GetFacingDirection();
        if (heroKnightGO.GetComponent<HeroKnight>().GetFacingDirection() == 1)
            GetComponent<SpriteRenderer>().flipX = true;
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
            speed = -speed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        if (lifeTime > 0)
            lifeTime -= Time.deltaTime;
        else
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Scenario") || collision.CompareTag("Enemy"))
            Destroy(gameObject);
    }
}
