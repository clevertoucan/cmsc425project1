using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidSettings : MonoBehaviour{
    public HashSet<BoidMaster> boids = new HashSet<BoidMaster>();
    public GameObject boidTarget;
    public float viewAngle = 120, viewDistance = 50, separationWeight = 1f, cohesionWeight = 1f,
        alignmentWeight = 1f, stepTurn = 0.1f, targetWeight = 1f, collisionWeight = 3f, collisionThreshhold = .5f, minSpeed = .5f, maxSpeed = 10f, defaultSpeed = 5f, targetDistanceModifier = 2f, overrideWeight = 1f;
    public int samples = 50;

    private void OnDestroy() {
        boids.Clear();
    }

    public Vector3[] dirArray;
    Vector3[] CreateDirectionArray(int sampleSize) {
        Vector3[] dirArray = new Vector3[sampleSize];
        float offset = 2f / samples;
        float increment = Mathf.PI * ( 3 - Mathf.Sqrt(5) );
        for (int i = 0; i < sampleSize; i++) {
            float y = ( ( i * offset ) - 1 ) + ( offset / 2 );
            float r = Mathf.Sqrt(1 - Mathf.Pow(y, 2));

            float phi = ( ( i + 1 ) % samples ) * increment;

            float x = Mathf.Cos(phi) * r;
            float z = Mathf.Sin(phi) * r;

            dirArray[i] = -new Vector3(x, y, z);
        }
        return dirArray;
    }


    private void Awake() {
        instance = this;
        Physics.queriesHitTriggers = true;
        dirArray = CreateDirectionArray(samples);
    }

    public static BoidSettings instance;

}
