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

    public void UpdateLevelOnYard(Yard yard)
    {
        //List<Animal> animals = yard.GetAnimals();
        int currentYardLevel = yard.GetLevel();
        int nextLevel = GetNextYardLevel(yard);
        if (nextLevel == -1)
        {
            levelText.text = "MAX LEVEL";
            return;
        }

        levelText.text = $"{currentYardLevel} / {nextLevel}";
        fillImage.fillAmount = (float)currentYardLevel / nextLevel;
    }

    private int GetNextYardLevel(Yard yard)
    {
        int currentAnimalIndex = yard.GetCurrentAnimalIndex();
        int nextAnimalIndex = currentAnimalIndex + 1;
        if (yard.GetAnimals().Count > nextAnimalIndex)
        {
            return yard.GetAnimals()[nextAnimalIndex].level;
        }
        
        print("Animals.Count = " + yard.GetAnimals().Count);
        
        return -1;
    }
}
