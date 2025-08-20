using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CamereBehaviour : MonoBehaviour
{
    public List<Vector3> cameraSpots = new List<Vector3>();
    public List<Quaternion> cameraRotations = new List<Quaternion>();
    public GameObject playerCamera;
    public int currentCamera = 0;

    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] GameObject mainUI;
    [SerializeField] GameObject HonkButton;
    [SerializeField] TMP_Text roomName;
    static public bool isPaused = false; 
    static public bool canPause = true;
    static public bool isReady = false;

    private List<int> currentCamerasWaiting = new List<int>();

    public void Start()
    {
        playerCamera.transform.position = cameraSpots[currentCamera];
        playerCamera.transform.rotation = cameraRotations[currentCamera];
        SetRoomName(this.gameObject.GetComponent<AnomalyBehaviour>().rooms[currentCamera].GetComponent<RoomBehaviour>().GetRoomName());
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && canPause)
        {
            if(isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if(currentCamera == 7)
        {
            HonkButton.SetActive(true);
        }
        else
        {
            HonkButton.SetActive(false);
        }

        CheckForWaiting();
    }

    public void NextCamera()
    {
        currentCamera++;
        if(currentCamera >= cameraSpots.Count)
        {
            currentCamera = 0;
        }
        playerCamera.transform.position = cameraSpots[currentCamera];
        playerCamera.transform.rotation = cameraRotations[currentCamera];
        SetRoomName(this.gameObject.GetComponent<AnomalyBehaviour>().rooms[currentCamera].GetComponent<RoomBehaviour>().GetRoomName());
    }

    public void PreviousCamera()
    {
        currentCamera--;
        if (currentCamera < 0)
        {
            currentCamera = cameraSpots.Count - 1;
        }
        playerCamera.transform.position = cameraSpots[currentCamera];
        playerCamera.transform.rotation = cameraRotations[currentCamera];
        SetRoomName(this.gameObject.GetComponent<AnomalyBehaviour>().rooms[currentCamera].GetComponent<RoomBehaviour>().GetRoomName());
    }

    public void GoToCamera(int x)
    {
        currentCamera = x;
        playerCamera.transform.position = cameraSpots[currentCamera];
        playerCamera.transform.rotation = cameraRotations[currentCamera];
        SetRoomName(this.gameObject.GetComponent<AnomalyBehaviour>().rooms[currentCamera].GetComponent<RoomBehaviour>().GetRoomName());
    }

    public void CheckForAnomaly()
    {
        if(!this.gameObject.GetComponent<AnomalyBehaviour>().IsChecking)
        {
            this.gameObject.GetComponent<AnomalyBehaviour>().AnomalyCheck(currentCamera);
        }

    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        mainUI.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        mainUI.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void SetRoomName(string name)
    {
        roomName.text = name;
    }

    public void AddToWaitingList(int x)
    {
        currentCamerasWaiting.Add(x);
    }

    private void CheckForWaiting()
    {
        for(int i = 0; i< currentCamerasWaiting.Count;i++)
        {
            if(currentCamerasWaiting[i] == currentCamera)
            {
                currentCamerasWaiting.RemoveAt(i);
                isReady = true;
            }
            else
            {
                isReady = false;
            }
        }
    }
}
