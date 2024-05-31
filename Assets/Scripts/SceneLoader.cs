using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public static void LoadMenu() => SceneManager.LoadScene("MainMenu");
    public static void LoadGame() => SceneManager.LoadScene("Game"); 
    
    
}
