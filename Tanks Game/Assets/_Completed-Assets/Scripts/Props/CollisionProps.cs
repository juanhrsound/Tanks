using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionProps : MonoBehaviour
{
    public Collisions debrisSound;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            AudioComponent();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Shells")
        {
            AudioComponent();

        }
    }
    private void AudioComponent()
    {
        GameObject audioObject = new GameObject();
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(debrisSound.debrisClip[Random.Range(0, debrisSound.debrisClip.Length)]);
        audioSource.volume = Random.Range(debrisSound.volume.x, debrisSound.volume.y);
        audioSource.pitch = Random.Range(debrisSound.pitch.x, debrisSound.pitch.y);
        Destroy(audioObject, debrisSound.debrisClip.Length);

    }


}
