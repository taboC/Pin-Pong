using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagerLoad : MonoBehaviour
{
    
    public void loadScene1()
    {
        PlayerPrefs.SetInt("mode", 1);
        SceneManager.LoadScene("SampleScene"); 
    }
    public void loadScene2()
    {
        PlayerPrefs.SetInt("mode", 2);
        SceneManager.LoadScene("SampleScene"); // Cambia a la escena "Escena2"
    }
}
