using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScare : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject JumpScareImg;
    public AudioSource audioSource;

    void Start()
    {
        JumpScareImg.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            JumpScareImg.SetActive(true);
            audioSource.Play();
            StartCoroutine(DisableImg());
        }
    }


    IEnumerator DisableImg()
    {
        yield return new WaitForSeconds(2);
        JumpScareImg.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
