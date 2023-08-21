using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class StartManager : MonoBehaviour
{
    
    public void StartGame()
    {
        SceneManager.LoadScene("Map"); // Load your game scene named "Map"
    }
}

