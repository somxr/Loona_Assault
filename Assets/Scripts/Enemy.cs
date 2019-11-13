using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject deathFX;
    [SerializeField] Transform spawnedAtRuntime;
    [SerializeField] int scorePerHit = 10;
    ScoreBoard scoreBoard;
    [SerializeField] int hits = 10;

    void Start()
    {
        AddBoxCollider();
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void AddBoxCollider()
    {
        Collider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (hits < 1)
        {
            KillEnemy();
        }
    }

    private void ProcessHit()
    {
        scoreBoard.ScoreHit(scorePerHit);
        hits--;
    }

    private void KillEnemy()
    {
        Instantiate(deathFX, transform.position, Quaternion.identity, spawnedAtRuntime);
        Destroy(gameObject);
    }
}
