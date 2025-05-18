using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBall : MonoBehaviour
{
    public  int          damage = 10;

    private GameObject  heroKnight;
    private bool        m_first = true;
    private Rigidbody2D m_rb;
    // Start is called before the first frame update
    void Start()
    {
        heroKnight = GameObject.Find("HeroKnight");
        m_rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (m_first)
        {
            m_first = false;
            if (heroKnight.transform.position.x >= transform.position.x)
                m_rb.AddForce(new Vector2(1,1) * 2f, ForceMode2D.Impulse);
            else
                m_rb.AddForce(new Vector2(-1, 1) * 2f, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            heroKnight.GetComponent<HeroKnight>().TakeDamage(damage);
        Destroy(gameObject);
    }
}
