using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GlobalManager : MonoBehaviour
{
    public static GlobalManager Instance;
    public bool activeSaveSystem = true;

    [HideInInspector]
    public bool LoadGame = false;
    [HideInInspector]
    public bool playTutorial = true;

    private bool sceneLoaded = false;
    [HideInInspector]
    public int warnings = 0;
    [HideInInspector]
    public double weeklyIncome = 0;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (!sceneLoaded)
        {
            if (SceneManager.GetActiveScene().name == "SampleScene")
            {
                sceneLoaded = true;
                StartCoroutine(FadeIn());
            }
        }
    }

    private IEnumerator FadeIn()
    {
        Animator animator = Camera.main.GetComponentInChildren<Animator>();
        animator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        GameObject.Find("Main Camera/Canvas/OpenScene").SetActive(false);
    }
    public void EndGame()
    {
        SceneManager.LoadScene("EndScene");
        File.Delete(Application.persistentDataPath + "/saveData.data");
    }
}
