using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] float offset;
    [SerializeField] float speed;
    SliderJoint2D sj;
    Vector3 startPosition;
    JointMotor2D motor;

    private void Awake()
    {
        sj = GetComponent<SliderJoint2D>();
        startPosition = transform.position;
        motor = sj.motor;
        speed *= -1;
        ChangeSpeed(speed);
    }

    void Update()
    {
        if (Mathf.Abs( transform.position.x - startPosition.x )>= offset ) 
        {
            speed *= -1;
            startPosition = transform.position;
            ChangeSpeed(speed);
        }
    }

    void ChangeSpeed(float speed)
    {
        motor.motorSpeed = speed;
        sj.motor = motor;
    }
}
