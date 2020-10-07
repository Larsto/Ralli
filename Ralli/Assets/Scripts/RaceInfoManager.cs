using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceInfoManager : MonoBehaviour
{
    public static RaceInfoManager instace;

    public string trackToLoad;
    public CarController racerToUse;
    public int noOfAI;
    public int noOfLaps;

    public bool enterdRace;
    public Sprite trackSprite, racerSprite;

    private void Awake()
    {
        if(instace == null)
        {
            instace = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
       
    }
}
