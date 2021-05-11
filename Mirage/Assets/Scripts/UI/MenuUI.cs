﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : SingletonPattern<MenuUI>
{
    [Header("Game UI Screens")]
    public GameObject[] gamePanels;

    public bool canPause = true;
    [HideInInspector] public bool isPaused = false;

    [SerializeField] private string formURL;

    protected override void Awake()
    {
        base.Awake();
        Application.targetFrameRate = 60;
        if (!canPause)
        {
            Cursor.visible = true;
        }
    }

    private void Update()
    {
        if (canPause)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPaused)
                {
                    UnpauseGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }
    }

    // Change Scene by entering Scene Name. Make sure it is loaded in the build settings.
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    // Quits game, does not work in play mode in editor
    public void EndGame()
    {
        Debug.Log("Closing Game");
        Application.Quit();
    }

    public void PauseGame()
    {
        Cursor.lockState = CursorLockMode.Confined;
        OpenPanel(0);
        isPaused = true;
        Time.timeScale = 0f;
        Cursor.visible = true;
    }

    public void UnpauseGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        ClosePanel(0);
        isPaused = false;
        Time.timeScale = 1.0f;
        Cursor.visible = false;
    }

    public void RestartScene()
    {
        UnpauseGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OpenPanel(int panelIndex)
    {
        if (panelIndex >= gamePanels.Length || panelIndex < 0)
        {
            return;
        }
        gamePanels[panelIndex].SetActive(true);
    }

    public void ClosePanel(int panelIndex)
    {
        gamePanels[panelIndex].SetActive(false);
    }

    public void OpenFeedbackForm()
    {
        Application.OpenURL(formURL);
    }

}
