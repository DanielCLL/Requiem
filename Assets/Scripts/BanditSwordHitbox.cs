using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditSwordHitbox : MonoBehaviour
{
    public GameObject heroKnighGO;

    private GameObject banditParent;
    // Start is called before the first frame update
    void Start()
    {
        banditParent = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EnableHitbox()
    {
        gameObject.SetActive(true);
    }

    public void DisableHitbox()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            heroKnighGO.GetComponent<HeroKnight>().TakeDamage(banditParent.GetComponent<Bandit>().damage);
        }
    }
}
