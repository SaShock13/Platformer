using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BonusCounter : MonoBehaviour
{
    static public int bonusAmount ;
    static public int rareBonusAmount;
    [SerializeField] TMP_Text bonusCounterText;


    private void Start()
    {
        bonusAmount = 0 ;
    }

    /// <summary>
    /// Метод взятия бонусов. Принимает Количество очков и bool переменную , true, если бонус редкий
    /// </summary>
    /// <param name="bonusValue"></param>
    /// <param name="isRare"></param>
    public void TakeABonus(int bonusValue,bool isRare)
    {

        bonusAmount+= bonusValue;
        Debug.Log(bonusAmount);
        UpdateBonusCounterText();
        if (isRare) rareBonusAmount++;
    }

    void UpdateBonusCounterText()
    {
        bonusCounterText.text = bonusAmount.ToString();

    }

}
