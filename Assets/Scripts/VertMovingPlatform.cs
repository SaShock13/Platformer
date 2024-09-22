using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertMovingPlatform : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    float currentSpeed;
    [SerializeField] Transform start;
    [SerializeField] Transform end;
    [SerializeField] float stopTime;
    bool down = false;


    SliderJoint2D sj;
    Vector3 startPosition;
    JointMotor2D motor;



    private void Awake()
    {
        currentSpeed = moveSpeed;
        sj = GetComponent<SliderJoint2D>();
        transform.position = start.position;
        motor = sj.motor;
        currentSpeed *= -1;
        motor.motorSpeed = currentSpeed;
        sj.motor = motor;
    }

    void Update()
    {
        if ((transform.position.y >= end.position.y&&!down)|(transform.position.y <= start.position.y && down))
        {
            StartCoroutine(ChSpeed());
        }
        //if (transform.position.y <= start.position.y && down)
        //{
        //    StartCoroutine(ChSpeed());
        //    down = false;
        //}
    }

     IEnumerator ChSpeed()
    {
        down = !down;
        float tempSpeed = currentSpeed;
        currentSpeed = 0;
        UpdateSpeed();
        yield return new WaitForSeconds(stopTime);
        currentSpeed = tempSpeed*-1;
        UpdateSpeed();
    }

    void UpdateSpeed()
    {
        motor.motorSpeed = currentSpeed;
        sj.motor = motor;
    }
}
