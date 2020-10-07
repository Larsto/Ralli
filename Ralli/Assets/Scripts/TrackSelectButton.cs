using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackSelectButton : MonoBehaviour
{
    public string trackSelectName;
    public Image trackImage;

    public int raceLaps = 4;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SelectTrack()
    {
        RaceInfoManager.instace.trackToLoad = trackSelectName;
        RaceInfoManager.instace.noOfLaps = raceLaps;
        RaceInfoManager.instace.trackSprite = trackImage.sprite;

        MainMenu.instance.trackSelectImage.sprite = trackImage.sprite;

        MainMenu.instance.CloseTrackSelect();
    }
}
