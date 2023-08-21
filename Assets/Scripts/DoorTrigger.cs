using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] GameObject[] targets;
    // Start is called before the first frame update
    [SerializeField] GameObject otherSide;
    void OnTriggerEnter(Collider other)
    {
        foreach (GameObject target in targets)
        {
            target.SendMessage("Activate");
            StartCoroutine(briefStop());
        }
    }
    void OnTriggerExit(Collider other)
    {
        StartCoroutine(WaitaBit());
    }

    IEnumerator WaitaBit()
    {
        yield return new WaitForSeconds(1);
        foreach (GameObject target in targets)
        {
            target.SendMessage("Deactivate");
        }
    }

    IEnumerator briefStop()
    {
        otherSide.SetActive(false);
        yield return new WaitForSeconds(2);
        otherSide.SetActive(true);
    }    
}
