using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CutScene : MonoBehaviour
{
    GameObject player;
    [SerializeField] Animator cutSceneAnimator;
    [SerializeField] GameObject cutPanel;
    [SerializeField] CinemachineVirtualCamera mainVCam;
    [SerializeField] CinemachineVirtualCamera cutCCam;
    [SerializeField] TMP_Text text;
    bool isAnyKey=false;
    bool inDialog = false;

    private void Update()
    {
        if (Input.anyKeyDown && inDialog)
        {
            Debug.Log("AnykEY");
            isAnyKey = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.gameObject;
            StartCoroutine(nameof(CutSceneAction));
        }
    }

    IEnumerator CutSceneAction()
    {
        player.GetComponent<PlayerInput>().enabled = false;
        mainVCam.enabled = false;
        cutCCam.enabled = true;
        yield return new WaitForSeconds(2);
        cutPanel.SetActive(true);
        cutSceneAnimator.SetBool("cutStarted", true);
        yield return new WaitForSeconds(1);
        text.enabled = true;
        inDialog = true;
        while (!isAnyKey)
        {            
            yield return new WaitForSeconds(0.1f);
        }        
        text.enabled = false;
        inDialog = false;
        cutSceneAnimator.SetBool("cutStarted", false);
        yield return new WaitForSeconds(1);
        cutPanel.SetActive(false);
        cutCCam.enabled = false;
        mainVCam.enabled = true;
        player.GetComponent<PlayerInput>().enabled = true;
        Destroy(gameObject);
    }
        
}
