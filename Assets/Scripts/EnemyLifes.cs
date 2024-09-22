using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyLifes : MonoBehaviour
{
    int lifes;
    [SerializeField] int maxLifes;
    public bool isALive = true;

    private void Awake()
    {
        lifes = maxLifes;
    }

    public void DecreseLifes()
    {
        lifes--;
        if (lifes <= 0)
        {
            lifes = 0;
            isALive = false;
        }
    }
}
