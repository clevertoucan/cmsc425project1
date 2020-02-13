using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour{
    Rigidbody rb;
    public float rotateSpeed = 2f, enemyMovementSpeed = 2f;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.angularVelocity = new Vector3(Random.value, Random.value, Random.value) * rotateSpeed;
    }

    void Update(){
        RaycastHit hit;
        if(Physics.Raycast(transform.position, (player.transform.position - transform.position), out hit)){
            if(hit.collider.gameObject.tag == "player"){
                Debug.Log("hit!");
                MoveTowardsPlayer();
            }
        }
    }

    void MoveTowardsPlayer(){
        transform.LookAt(player.transform);
        rb.velocity = (player.transform.position - transform.position).normalized * enemyMovementSpeed;
    }

}
