using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeHitbox : MonoBehaviour
{
    private GameObject parentEnemy;
    // Start is called before the first frame update
    void Start()
    {
        parentEnemy = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (parentEnemy.GetComponent<Slime>().GetIsDead())
        {
            GetComponent<BoxCollider2D>().enabled = false;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SwordHitbox"))
        {
            parentEnemy.GetComponent<Slime>().TakeDamage(10);
        }
    }
}
