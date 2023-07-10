using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay;
using UnityEngine;
using UnityEngine.UI;

public class YardPanel : MonoBehaviour
{
    [SerializeField] private Text levelText;
    [SerializeField] private Image fillImage;

    public Button yardButton;
    public Image yardIcon;
    public Text yardCostText;
    public Text ticketCostText;
    public Text yardTitle;

    public void UpdateLevelOnYard(Yard yard)
    {
        //List<Animal> animals = yard.GetAnimals();
        int currentYardLevel = yard.GetLevel();
        int nextLevel = yard.GetNextLevel();
        if (nextLevel == -1)
        {
            levelText.text = "MAX LEVEL";
            return;
        }

        levelText.text = $"{currentYardLevel} / {nextLevel}";
        fillImage.fillAmount = (float)currentYardLevel / nextLevel;
    }
}
