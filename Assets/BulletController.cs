using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public ParticleSystem explosion;
    float startTime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime > 5) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "player") {
            if (other.tag == "enemy") {
                part2PlayerController.increment();
                Destroy(other.gameObject);
            }
            explosion.transform.parent = null;
            explosion.Play();
            Destroy(gameObject);
        }
    }
}
