using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public GameObject winScreen;
    public float movementSpeed = 1f, turnSpeed = 1f;
    int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = new Vector3();
        velocity = transform.forward * Input.GetAxis("Vertical") * movementSpeed;
        Vector3 angularVelocity = new Vector3();
        angularVelocity.y = Input.GetAxis("Horizontal") * turnSpeed;
        rb.velocity = velocity;
        rb.angularVelocity = angularVelocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        score++;
        if(score == 9){
            winScreen.SetActive(true);
        }
    }
}
