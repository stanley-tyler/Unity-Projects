using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour {

    [SerializeField] GameObject deathFX;
    [SerializeField] Transform parent;
    [SerializeField] int hits = 10;



    // Use this for initialization
    void Start () {
        
        Collider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
        
    }


    private void OnParticleCollision(GameObject other)
    {
        GameObject scoreText = GameObject.Find("Text");
        hits--;
        if (hits <= 0)
        {
            KillEnemy();
            scoreText.GetComponent<ScoreBoard>().ScoreHit();
        }
        
    }

    private void KillEnemy()
    {
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);

        fx.transform.parent = parent;
        Destroy(gameObject);
    }
}
