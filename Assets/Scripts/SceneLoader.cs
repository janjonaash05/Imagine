using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Utility MonoBehaviour for changing between Scenes.
/// </summary>
public class SceneLoader : MonoBehaviour
{
    /// <summary>
    /// Loads MainMenu Scene.
    /// </summary>
    public static void LoadMenu() => SceneManager.LoadScene("MainMenu");

    /// <summary>
    /// Loads Game Scene.
    /// </summary>
    public static void LoadGame() => SceneManager.LoadScene("Game"); 
    
    
}
