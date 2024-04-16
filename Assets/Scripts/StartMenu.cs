using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Collections;

public class StartMenu : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject optionsMenu;
    public GameObject pauseMenu;

    public GameObject warning;
    public GameObject normalView;
    public GameObject warinigMenu;  

    public GameObject fade;

    public GameObject warningText;
    public UnityEngine.UI.Toggle fullScreenToggle;

    private Action func;

    private void Start()
    {
        if(fullScreenToggle != null)
            fullScreenToggle.isOn = Screen.fullScreen;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1.0f;
            pauseMenu.SetActive(true);
        }
    }

    private void Ask()
    {
        normalView.SetActive(false);
        warning.SetActive(true);
    }

    public void Yes()
    {
        func();
    }

    public void No()
    {
        warinigMenu.SetActive(false);
        normalView.SetActive(true);
    }
    public void Continue()
    {
        if (File.Exists(Application.persistentDataPath + "/saveData.data"))
        {
            FindObjectOfType<GlobalManager>().LoadGame = true;
            StartCoroutine(FadeOut());
        }
        else
        {
            warningText.SetActive(true);
        }
    }

    public void NewGame()
    {
        FindObjectOfType<GlobalManager>().LoadGame = false;
        if (File.Exists(Application.persistentDataPath + "/saveData.data"))
        {
            warinigMenu.SetActive(true);
            startMenu.SetActive(false);
            func = () => {
                File.Delete(Application.persistentDataPath + "/saveData.data");
                StartCoroutine(FadeOut());
            };
        }
        else
        {
            StartCoroutine(FadeOut());
        }
    }

    private IEnumerator FadeOut()
    {
        fade.SetActive(true);
        Animator animator = gameObject.GetComponentInChildren<Animator>();
        animator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length * 2);
        SceneManager.LoadScene("SampleScene");
    }

    public void Options()
    {
        startMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void BackToStart()
    {
        startMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void BackToStartForGameButton()
    {
        Ask();
        func = () => { SceneManager.LoadScene("StartMenu"); };
    }
    public void Reseume()
    {
        pauseMenu.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ExitForGame()
    {
        Ask();
        func = () => { Application.Quit(); };
    }

    public void FullscreenToggle()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
