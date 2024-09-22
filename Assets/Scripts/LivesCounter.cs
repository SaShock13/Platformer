using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LivesCounter : MonoBehaviour
{
    int lifes;
    [SerializeField] TMP_Text lifesText;
    [SerializeField] int maxLifes;
    public bool isALive = true;

    private void Awake()
    {
        lifes = maxLifes;
        UpdateLifesText();
    }

    public void DecreseLifes()
    { 
        lifes--;
        if (lifes<=0)
        {
            lifes = 0;
            isALive = false;
        }
        UpdateLifesText() ;
    }

    void UpdateLifesText()
    {
        lifesText.text = lifes.ToString();
    }
    public void IncreseLifes()
    {
        lifes++;
        UpdateLifesText();
    }

}
