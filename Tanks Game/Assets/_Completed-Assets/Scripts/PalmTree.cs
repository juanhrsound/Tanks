using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalmTree : MonoBehaviour
{
    public Collisions debrisSound;
    
    private void Awake()
    {/*
        GameObject[] palmTrees = GameObject.FindGameObjectsWithTag("PalmTree");
    
        foreach (GameObject palmTree in palmTrees)
        {
            palmTree.AddComponent<PalmTree>().debrisSound = debrisSound;
        }*/
    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Find all objects with the "PalmTree" tag
            GameObject[] palmTrees = GameObject.FindGameObjectsWithTag("PalmTree");

            // Play debris sound on all objects with the "PalmTree" tag
            foreach (GameObject palmTree in palmTrees)
            {
                GameObject audioObject = new GameObject();
                AudioSource audioSource = audioObject.AddComponent<AudioSource>();
                audioSource.PlayOneShot(debrisSound.debrisClip[Random.Range(0, debrisSound.debrisClip.Length)]);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Shells"))
        {
            // Find all objects with the "PalmTree" tag
            GameObject[] palmTrees = GameObject.FindGameObjectsWithTag("PalmTree");

            // Play debris sound on all objects with the "PalmTree" tag
            foreach (GameObject palmTree in palmTrees)
            {
                GameObject audioObject = new GameObject();
                AudioSource audioSource = audioObject.AddComponent<AudioSource>();
                audioSource.PlayOneShot(debrisSound.debrisClip[Random.Range(0, debrisSound.debrisClip.Length)]);
            }
        }
    }





}
