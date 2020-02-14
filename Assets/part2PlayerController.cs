using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class part2PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public GameObject winScreen, loseScreen, bulletPrefab;
    public Text text; 
    public float movementSpeed = 1f, turnSpeed = 1f, bulletSpeed = 15f, jumpForce = 200f;
    public int score = 0;
    public int scoreQuota = 3;
    bool playing = true;
    private static part2PlayerController instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        rb = GetComponent<Rigidbody>();
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        if (playing)
        {
            transform.position += transform.forward * Input.GetAxis("Vertical") * movementSpeed;
            Vector3 angularVelocity = new Vector3();
            angularVelocity.y = Input.GetAxis("Horizontal") * turnSpeed;
            rb.angularVelocity = angularVelocity;

        }
    }

    private void Update() {

        if (Input.GetButtonDown("Fire1")) {
            GameObject bullet = Instantiate(bulletPrefab, null);
            bullet.transform.position = transform.position;
            Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();
            bulletRB.velocity = transform.forward * bulletSpeed;
        }

        if (Input.GetButtonDown("Jump")) {
            rb.AddForce(new Vector3(0, jumpForce, 0));
        }
    }

    IEnumerator FlashMessage(GameObject message) {
        message.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Splash");
    }

    public static void increment() {
        instance.score++;
        instance.text.text = "Count: " + instance.score.ToString();
        if (instance.score >= instance.scoreQuota) {
            instance.playing = false;
            instance.rb.velocity = Vector3.zero;
            instance.rb.angularVelocity = Vector3.zero;
            instance.StartCoroutine(instance.FlashMessage(instance.winScreen));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "end") {
            playing = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            StartCoroutine(FlashMessage(winScreen));
        }
        if(other.tag == "enemy" && playing){
            playing = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            StartCoroutine(FlashMessage(loseScreen));
        }
    }
}
