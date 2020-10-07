using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceManager : MonoBehaviour
{
    public static RaceManager instance;

    public Checkpoint[] allCheckpoints;

    public int totalLaps;

    public CarController playerCar;
    public List<CarController> allAICars = new List<CarController>();
    public int playerPosition;
    public float timeBetweenPosCheck = .2f;
    private float posCheckCounter;

    public float aiDefaultSpeed = 30f, playerDefaultSpeed = 30f, rubberbandSpeedMod = 3.5f, rubBandAccel = .5f;

    public bool isStarting;
    public float timeBetweenStartCount = 1f;
    private float startCounter;
    public int countDownCurrent = 3;

    public int playerStartPosition, aiNumberToSpawn;
    public Transform[] startPoints;
    public List<CarController> carsToSpawn = new List<CarController>();

    public bool raceCompleted;

    public string raceCompletedScene;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        totalLaps = RaceInfoManager.instace.noOfLaps;
        aiNumberToSpawn = RaceInfoManager.instace.noOfAI;
        for (int i = 0; i < allCheckpoints.Length; i++)
        {
            allCheckpoints[i].cpNumber = i;
        }
        isStarting = true;
        startCounter = timeBetweenStartCount;

        UIManager.instance.countDownText.text = countDownCurrent + "!";

        playerStartPosition = Random.Range(0, aiNumberToSpawn + 1);

        playerCar = Instantiate(RaceInfoManager.instace.racerToUse, startPoints[playerStartPosition].position, startPoints[playerStartPosition].rotation);
        playerCar.isAI = false;
        playerCar.GetComponent<AudioListener>().enabled = true;

        CameraSwitcher.instance.SetTarget(playerCar);

        //playerCar.transform.position = startPoints[playerStartPosition].position;
        //playerCar.theRB.transform.position = startPoints[playerStartPosition].position;

        for(int i = 0; i < aiNumberToSpawn + 1; i++)
        {
            if(i != playerStartPosition)
            {
                int selectedCar = Random.Range(0, carsToSpawn.Count);

                allAICars.Add(Instantiate(carsToSpawn[selectedCar], startPoints[i].position, startPoints[i].rotation));

                if(carsToSpawn.Count >= aiNumberToSpawn - i)
                {
                    carsToSpawn.RemoveAt(selectedCar);
                }
                
            }
        }

        UIManager.instance.positionText.text = (playerStartPosition + 1) + "/" + (allAICars.Count + 1);

    }

    // Update is called once per frame
    void Update()
    {
        if (isStarting)
        {
            startCounter -= Time.deltaTime;
            if(startCounter <= 0)
            {
                countDownCurrent--;
                startCounter = timeBetweenStartCount;

                UIManager.instance.countDownText.text = countDownCurrent + "!";

                if (countDownCurrent == 0)
                {
                    isStarting = false;
                    UIManager.instance.countDownText.gameObject.SetActive(false);
                    UIManager.instance.goText.gameObject.SetActive(true);
                }
            }
        }
        else
        {
        
        posCheckCounter -= Time.deltaTime;
            if (posCheckCounter <= 0)
            {
                playerPosition = 1;

                foreach (CarController aiCar in allAICars)
                {
                    if (aiCar.currentLap > playerCar.currentLap)
                    {
                        playerPosition++;
                    }
                    else if (aiCar.currentLap == playerCar.currentLap)
                    {
                        if (aiCar.nextCheckpoint > playerCar.nextCheckpoint)
                        {
                            playerPosition++;
                        }
                        else if (aiCar.nextCheckpoint == playerCar.nextCheckpoint)
                        {
                            if (Vector3.Distance(aiCar.transform.position, allCheckpoints[aiCar.nextCheckpoint].transform.position) < Vector3.Distance(playerCar.transform.position, allCheckpoints[aiCar.nextCheckpoint].transform.position))
                            {
                                playerPosition++;
                            }
                        }
                    }
                }
                posCheckCounter = timeBetweenPosCheck;

                UIManager.instance.positionText.text = playerPosition + "/" + (allAICars.Count + 1);
            }
        }

        // manage rubber banding
        if(playerPosition == 1)
        {
            foreach(CarController aiCar in allAICars)
            {
                playerCar.maxSpeed = Mathf.MoveTowards(aiCar.maxSpeed, aiDefaultSpeed - rubberbandSpeedMod, rubBandAccel * Time.deltaTime);
            }
            playerCar.maxSpeed = Mathf.MoveTowards(playerCar.maxSpeed, playerDefaultSpeed - rubberbandSpeedMod, rubBandAccel * Time.deltaTime);
        }
        else
        {
            foreach (CarController aiCar in allAICars)
            {
                playerCar.maxSpeed = Mathf.MoveTowards(aiCar.maxSpeed, aiDefaultSpeed - (rubberbandSpeedMod * ((float)playerPosition / ((float)allAICars.Count + 1))), rubBandAccel * Time.deltaTime);
            }
            playerCar.maxSpeed = Mathf.MoveTowards(playerCar.maxSpeed, playerDefaultSpeed + (rubberbandSpeedMod * ((float)playerPosition / ((float)allAICars.Count + 1))), rubBandAccel * Time.deltaTime);
        }
    }
    public void FinishRace()
    {
        raceCompleted = true;

        switch (playerPosition)
        {
            case 1:
                UIManager.instance.raceResultText.text = "You finished 1st";
                break;
            case 2:
                UIManager.instance.raceResultText.text = "You finished 2nd";
                break;
            case 4:
                UIManager.instance.raceResultText.text = "You finished 3rd";
                break;
            default:
                UIManager.instance.raceResultText.text = "You finished " + playerPosition + "th";
                break;
        }

        UIManager.instance.resultScreen.SetActive(true);
    }

    public void ExitRace()
    {
        SceneManager.LoadScene(raceCompletedScene);
    }
}
