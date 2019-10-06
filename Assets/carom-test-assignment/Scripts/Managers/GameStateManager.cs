using System;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] CameraController cameraController;
    [SerializeField] ScoreTracker scoreTracker;
    [SerializeField] InputManagement inputManagement;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject gameMenu;
    [SerializeField] GameObject winMenu;

    public Action onGameStarted; 
    public Action onMainMenuOpened;

    void Start()
    {
        SetInitialState();
        scoreTracker.onGameWon += OpenWinScreen;
    }

    void SetInitialState()
    {
        inputManagement.enabled = false;
        OpenMainMenu();
    }

    public void OpenMainMenu()
    {        
        cameraController.RequestCamera(CameraController.CameraType.Idle);
        mainMenu.SetActive(true);
        gameMenu.SetActive(false);
        winMenu.SetActive(false);
        if(onMainMenuOpened != null) onMainMenuOpened();
    }

    public void StartGame()
    {
        cameraController.RequestCamera(CameraController.CameraType.Play);
        inputManagement.enabled = true;
        mainMenu.SetActive(false);
        gameMenu.SetActive(true);
        if(onGameStarted != null) onGameStarted();
    }

    void OpenWinScreen()
    {
        cameraController.RequestCamera(CameraController.CameraType.Idle);
        inputManagement.enabled = false;
        winMenu.SetActive(true);
        gameMenu.SetActive(false);
    }
}
