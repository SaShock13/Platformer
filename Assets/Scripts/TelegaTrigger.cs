using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TelegaTrigger : MonoBehaviour
{
    bool inTrigger = false;
    [SerializeField] WheelJoint2D wheelJoint;

    [SerializeField] TMP_Text text;
    string previousText;
    bool firstTime = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (firstTime)
            {
                previousText = text.text; 
            }
            firstTime = false;
            inTrigger = true;
            text.text = "E - толкнуть телегу"; 
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inTrigger = false;
            text.text = previousText;
        }
    }

    private void Update()
    {
        if (inTrigger&& Input.GetKeyDown(KeyCode.E))           
        {
            wheelJoint.useMotor = true;
            text.text = previousText;
            gameObject.SetActive(false);
        }
    }
}
