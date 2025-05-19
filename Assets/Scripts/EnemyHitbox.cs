using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    public GameObject   parentEnemy;
    // Start is called before the first frame update
    void Start()
    {
        parentEnemy = GetComponentInParent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (parentEnemy.GetComponent<Slime>().GetIsDead())
        {
            GetComponent<BoxCollider2D>().enabled = false;
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
