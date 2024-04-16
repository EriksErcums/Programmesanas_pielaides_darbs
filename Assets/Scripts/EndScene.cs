using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
    public void End()
    {
        File.Delete(Application.persistentDataPath + "/saveData.data");
        SceneManager.LoadScene("StartMenu");
    }
}
