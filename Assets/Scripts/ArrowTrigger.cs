using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrigger : MonoBehaviour
{
    [Header("������ ������")]
    [SerializeField] GameObject arrow;

    [Header("�������� ������ ������������ ��������")]
    [SerializeField] Vector3 offsetArrow;

    Vector3 instanceLocalTransform;
    GameObject arrowInstance;
    bool alreadyExist = false;

    private void Awake()
    {
        instanceLocalTransform = transform.position + offsetArrow;
        //arrow.GetComponent<TargetJoint2D>().target = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player")&& alreadyExist ==false)
        {
            alreadyExist=true;
            arrowInstance = Instantiate(arrow, instanceLocalTransform, new Quaternion(0, 0, 0.516832173f, 0.856086791f));
            arrowInstance.GetComponent<TargetJoint2D>().target = transform.position;
            StartCoroutine(DeleteObject());
        }
    }

    /// <summary>
    /// ������� ������ ����� ������� ����� ��������
    /// </summary>
    /// <returns></returns>
    IEnumerator DeleteObject()
    {
        yield return new WaitForSeconds(1);


        Destroy(arrowInstance);
        alreadyExist = false;
    }
}
