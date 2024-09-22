using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] GameObject player;

    [Header("�������� ������ ������������ ������")]
    [SerializeField] Vector3 cameraOffset;

    void Update()
    {

        if (player!=null)
        {
            transform.position = player.transform.position + cameraOffset; 
        }
    }
}
