using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public static MainMenu instance;

    public GameObject raceSetupPanle, trackSelectPanel, racerSelectPanel;

    public Image trackSelectImage, racerSelectImage;
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        if (RaceInfoManager.instace.enterdRace)
        {
            trackSelectImage.sprite = RaceInfoManager.instace.trackSprite;
            racerSelectImage.sprite = RaceInfoManager.instace.racerSprite;

            OpenRaceSetup();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        RaceInfoManager.instace.enterdRace = true;
        SceneManager.LoadScene(RaceInfoManager.instace.trackToLoad);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game quit");
    }

    public void OpenRaceSetup()
    {
        raceSetupPanle.SetActive(true);
    }

    public void CloseRaceSetup()
    {
        raceSetupPanle.SetActive(false);
    }

    public void OpenTrackSelect()
    {
        trackSelectPanel.SetActive(true);
        CloseRaceSetup();
    }

    public void CloseTrackSelect()
    {
        trackSelectPanel.SetActive(false);
        OpenRaceSetup();
    }

    public void OpenRacerSelect()
    {
        racerSelectPanel.SetActive(true);
        CloseRaceSetup();
    }

    public void CloseRacerSelect()
    {
        racerSelectPanel.SetActive(false);
        OpenRaceSetup();
    }

}
