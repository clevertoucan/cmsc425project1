using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GenerateBoids : MonoBehaviour
{
    public GameObject boidPrefab, boids;
    public int size = 50;
    // Start is called before the first frame update

    private void OnEnable() {
        if (boids.tag == "Menu") {
            CreateBoids();
        }
    }
    void CreateBoids(){
        for (int i = 0; i < size; i++) {
            Vector3 s = boids.transform.position + Random.insideUnitSphere.normalized * 2;
            Instantiate(boidPrefab, s, Random.rotation, boids.transform);
        }
        foreach(BoidMaster b in BoidSettings.instance.boids) {
            Vector3 s = boids.transform.position + Random.insideUnitSphere.normalized * 2;
            b.transform.position = s;
        }
    }

}
