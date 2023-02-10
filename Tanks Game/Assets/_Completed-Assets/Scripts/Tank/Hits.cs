using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Hits : MonoBehaviour
{
    public GameObject gameObject;
    //Colliders // Crash
    public AudioSource m_hits;
    
    public AudioClip[] m_Trees;
    public AudioClip[] m_Columns;
    public AudioClip[] m_Pumpjack;
    public AudioClip[] m_PalmTrees;    
    public AudioClip[] m_Radar;    
    public AudioClip[] m_OilStorage;
    public AudioClip[] m_Buildings;
    public AudioClip[] m_TankRuin;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "TankRuin")
        {            
            m_hits.PlayOneShot(m_TankRuin[Random.Range(0, m_TankRuin.Length)]);
            AudioParameters();
        }
        if (collision.gameObject.tag == "Tree")
        {
            AudioParameters();

            m_hits.PlayOneShot(m_Trees[Random.Range(0, m_Trees.Length)]);
        }
        if (collision.gameObject.tag == "Column")
        {
            m_hits.PlayOneShot(m_Columns[Random.Range(0, m_Columns.Length)]);
            AudioParameters();

        }
        if (collision.gameObject.tag == "Pumpjack")
        {
            m_hits.PlayOneShot(m_Pumpjack[Random.Range(0, m_Pumpjack.Length)]);
            AudioParameters();

        }
        if (collision.gameObject.tag == "PalmTree")
        {
            AudioParameters();
            m_hits.PlayOneShot(m_PalmTrees[Random.Range(0, m_PalmTrees.Length)]);

        }
        if (collision.gameObject.tag == "Radar")
        {
            m_hits.PlayOneShot(m_Radar[Random.Range(0, m_Radar.Length)]);
            AudioParameters();

        }
        if (collision.gameObject.tag == "OilStorage")
        {
            m_hits.PlayOneShot(m_OilStorage[Random.Range(0, m_OilStorage.Length)]);
            AudioParameters();

        }
        if (collision.gameObject.tag == "Buildings")
        {
            m_hits.PlayOneShot(m_Buildings[Random.Range(0, m_Buildings.Length)]);
            AudioParameters();

        }

    }

    private void AudioParameters()
    {
        m_hits.pitch = Random.Range(0.9f, 1);
        m_hits.Play();
    }


}
