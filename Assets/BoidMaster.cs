using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidMaster : MonoBehaviour {

    public GameObject overrideTarget;
    BoidSettings settings;

    void Start(){
        settings = BoidSettings.instance;
        settings.boids.Add(this);
        
    }

    // Update is called once per frame
    void Update(){
        Vector3 separate = new Vector3(), align = new Vector3(), boidTarget = new Vector3();
        Vector3 sum = new Vector3();
        int num = 0;
        float min = float.MaxValue;
        foreach (BoidMaster b in settings.boids) {
            Vector3 distance = b.transform.position - transform.position;
            
            if (b != this && distance.sqrMagnitude < settings.viewDistance * settings.viewDistance && Vector3.Angle(transform.up, distance.normalized) < settings.viewAngle) {
                num++;
                sum += b.transform.position - transform.position;
                if (distance.sqrMagnitude < min) {
                    min = distance.sqrMagnitude;
                    separate = transform.position - b.transform.position;
                }
                align += b.transform.up;
            }
        }

        Vector3 center = num == 0? Vector3.zero: sum / num;
        align = num == 0 ? Vector3.zero : align / num;
        
        Vector3 turn = Vector3.RotateTowards(transform.up, center.normalized, settings.stepTurn * settings.cohesionWeight, 0);
        turn += Vector3.RotateTowards(transform.up, separate.normalized, settings.stepTurn* settings.separationWeight / (min / settings.viewDistance), 0);
        turn += Vector3.RotateTowards(transform.up, align.normalized, settings.stepTurn * settings.alignmentWeight, 0);
        if (overrideTarget != null) {
            boidTarget = overrideTarget.transform.position - transform.position;
            boidTarget = boidTarget.normalized;
            turn += Vector3.RotateTowards(transform.up, boidTarget, settings.stepTurn * settings.overrideWeight, 0);
        } else if (settings.boidTarget != null) {
            boidTarget = settings.boidTarget.transform.position - transform.position;
            boidTarget = boidTarget.normalized;
            turn += Vector3.RotateTowards(transform.up, boidTarget, settings.stepTurn * settings.targetWeight, 0);
        }
        Vector3 clear = Vector3.zero;
        float dist = PathIsBlocked();
        if (dist > 0) {
            clear = -transform.up;
            foreach (Vector3 v in settings.dirArray) {
                Vector3 dir = transform.TransformDirection(v);
                if (!Physics.Raycast(transform.position, dir, settings.viewDistance)) {
                    clear = dir;
                    break;
                }
            }
            clear = Vector3.RotateTowards(transform.up, clear.normalized, settings.stepTurn * settings.collisionWeight * 1 / ( dist * dist / settings.viewDistance * settings.viewDistance ), 0);
            if (dist < settings.collisionThreshhold) {
                turn = clear;
            } else {
                turn += clear;
            }
            
        }
        transform.up = turn;
        float speed = settings.defaultSpeed;
        if (overrideTarget != null) {
            speed = Mathf.Clamp01(( transform.position - overrideTarget.transform.position).sqrMagnitude / ( settings.targetDistanceModifier * settings.viewDistance * settings.viewDistance ));
            speed = Mathf.Lerp(settings.minSpeed, settings.maxSpeed, speed);
        } else if (settings.boidTarget != null) {
            speed = Mathf.Clamp01(( transform.position - settings.boidTarget.transform.position ).sqrMagnitude / ( settings.targetDistanceModifier * settings.viewDistance * settings.viewDistance ));
            speed = Mathf.Lerp(settings.minSpeed, settings.maxSpeed, speed);
        }
        GetComponent<Rigidbody>().velocity = transform.up * speed;
        Vector3 sepLine = transform.position + separate * settings.separationWeight / min, 
            alignLine = transform.position + align.normalized * settings.alignmentWeight, 
            centerLine = transform.position + center.normalized * settings.cohesionWeight,
            targetLine = transform.position + boidTarget.normalized * settings.targetWeight,
            collisionLine = transform.position + clear.normalized * settings.collisionWeight;
        Debug.DrawLine(transform.position, sepLine, Color.red);
        Debug.DrawLine(transform.position, alignLine, Color.green);
        Debug.DrawLine(transform.position, centerLine, Color.blue);
        Debug.DrawLine(transform.position, targetLine, Color.magenta);
        Debug.DrawLine(transform.position, collisionLine, Color.yellow);
    }

    float PathIsBlocked() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.up, out hit, settings.viewDistance)){
            return hit.distance;
        }
        return -1;
        
    }

}
