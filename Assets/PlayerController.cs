using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public GameObject winScreen;
    public Text text;
    public float movementSpeed = 1f, turnSpeed = 1f;
    public int pickupQuota = 3;
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

    IEnumerator FlashMessage(GameObject message) {
        message.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Splash");
    }

    private void OnTriggerEnter(Collider other)
    {
        score++;
        text.text = "Count: " + score.ToString();
        if(score == pickupQuota){
            StartCoroutine(FlashMessage(winScreen));
        }
    }
}
