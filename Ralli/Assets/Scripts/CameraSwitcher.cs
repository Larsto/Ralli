using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public static CameraSwitcher instance;

    public GameObject[] cameras;
    private int currentCam;

    public CameraController topdownCam;
    public Cinemachine.CinemachineVirtualCamera cineCam;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            currentCam++;

            if(currentCam >= cameras.Length)
            {
                currentCam = 0;
            }

            for(int i = 0; i < cameras.Length; i++)
            {
                if(i == currentCam)
                {
                    cameras[i].SetActive(true);
                }
                else
                {
                    cameras[i].SetActive(false);
                }
            }
        }
    }

    public void SetTarget(CarController playerCar)
    {
        topdownCam.target = playerCar;
        cineCam.m_Follow = playerCar.transform;
        cineCam.m_LookAt = playerCar.transform;
    }
}
