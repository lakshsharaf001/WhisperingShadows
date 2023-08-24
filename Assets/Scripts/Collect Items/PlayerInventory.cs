
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerInventory : MonoBehaviour
{
    public int NumberOfKeys { get; private set; }
    public int NumberOfBooks { get; private set; }
    
    public UnityEvent<PlayerInventory> OnKeyCollected;
    public UnityEvent<PlayerInventory> OnBookCollected;
    

    private void CheckMissionStatus()
    {
        if (NumberOfKeys >= 4 && NumberOfBooks >= 4)
        {
            MissionSuccess();
        }
    }

    private void MissionSuccess()
    {
        // Load the missionsuccess scene
        SceneManager.LoadScene("MissionSuccess"); // Replace with your scene's name
    }

    public void KeyCollected()
    {
        NumberOfKeys++;
        OnKeyCollected.Invoke(this);
        CheckMissionStatus();
    }

    public void BookCollected()
    {
        NumberOfBooks++;
        OnBookCollected.Invoke(this);
        CheckMissionStatus();
    }

}
