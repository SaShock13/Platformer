using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindyTrapTrigger : MonoBehaviour
{
    bool inTrigger = false;
    //bool isPressedE = false;
    [SerializeField] GameObject windObj;
    [SerializeField] AreaEffector2D wind;
    [SerializeField] GameObject[] piles;
    int pilesCount;

    private void Awake()
    {
        pilesCount = piles.Length;
    }

    private void Update()
    {
        if (inTrigger && Input.GetKeyDown(KeyCode.E))
        {

            Debug.Log("E");
            StartCoroutine(Wind());
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") )
        {
            Debug.Log("intrigger");
            inTrigger = true;
        }
    }

    IEnumerator Wind()
    {
        for (int i = 0; i < pilesCount; i++)
        {
            piles[i].GetComponent<Collider2D>().isTrigger = false;
            piles[i].GetComponent<Rigidbody2D>().isKinematic = false;
        }

        windObj.SetActive(true);
        Debug.Log("windStarted");
        //wind.enabled = true;
        yield return new WaitForSeconds(5);
        windObj.SetActive(false);
        Debug.Log("windEnded");
        //wind.enabled = false;
    }

}
