using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScare : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject JumpScareImg1;
    public AudioSource audioSource;

    private bool isPlayed = false;

    void Start()
    {
        JumpScareImg1.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(!isPlayed)
            {
                JumpScareImg1.SetActive(true);
                audioSource.Play();
                StartCoroutine(DisableImg());

                isPlayed = true;
            }           
        }
    }


    IEnumerator DisableImg()
    {
        yield return new WaitForSeconds(2);
        JumpScareImg1.SetActive(false);
    }

}
