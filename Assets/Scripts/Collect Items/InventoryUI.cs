using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    private TextMeshProUGUI Dtext;
    // Start is called before the first frame update
    void Start()
    {
        Dtext = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateKeyText(PlayerInventory playerInventory)
    {
        Dtext.text = playerInventory.NumberOfKeys.ToString();
    }

    public void UpdateBookText(PlayerInventory playerInventory)
    {
        Dtext.text = playerInventory.NumberOfBooks.ToString();
    }
}
