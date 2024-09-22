using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Parallax : MonoBehaviour
{
    [SerializeField] Transform[] layers;
    [SerializeField] float[] coeff;
    int layersAmount;

    private void Awake()
    {
        layersAmount = layers.Length;
    }

    private void Update()
    {
        for (int i = 0; i < layersAmount; i++)
        {
            layers[i].position = transform.position * coeff[i];
        }
    }
}
