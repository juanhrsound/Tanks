using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Concrete : MonoBehaviour
{
    public AudioSource concrete;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            concrete.Play();

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (concrete.isPlaying)
        {
            concrete.Stop();
        }
    }




}
