using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Concrete : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "player")
        {
            FindObjectOfType<TankMovement>().m_MovementAudio.Play();

        }
    }





}
