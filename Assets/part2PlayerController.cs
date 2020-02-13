using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class part2PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public GameObject winScreen, loseScreen;
    public float movementSpeed = 1f, turnSpeed = 1f;
    int score = 0;
    bool playing = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playing)
        {
            Vector3 velocity = new Vector3();
            velocity = transform.forward * Input.GetAxis("Vertical") * movementSpeed;
            Vector3 angularVelocity = new Vector3();
            angularVelocity.y = Input.GetAxis("Horizontal") * turnSpeed;
            rb.velocity = velocity;
            rb.angularVelocity = angularVelocity;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "end"){
            winScreen.SetActive(true);
            playing = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        if(other.tag == "enemy" && playing){
            loseScreen.SetActive(true);
            playing = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
