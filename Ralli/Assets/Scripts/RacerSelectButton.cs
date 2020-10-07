using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RacerSelectButton : MonoBehaviour
{
    public Image racerImage;

    public CarController racerToSet;

    public void SelectRacer()
    {
        RaceInfoManager.instace.racerToUse = racerToSet;
        RaceInfoManager.instace.racerSprite = racerImage.sprite;

        MainMenu.instance.racerSelectImage.sprite = racerImage.sprite;

        MainMenu.instance.CloseRacerSelect();
    }
}
