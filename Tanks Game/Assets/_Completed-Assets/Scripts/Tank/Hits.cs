using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Hits : MonoBehaviour
{
    public GameObject gameObject;
    public GameObject shell;

    public AudioSource m_hits;
    
    public AudioClip[] m_Trees;
    public AudioClip[] m_Columns;
    public AudioClip[] m_Pumpjack;
    public AudioClip[] m_PalmTrees;    
    public AudioClip[] m_Radar;    
    public AudioClip[] m_OilStorage;
    public AudioClip[] m_Buildings;
    public AudioClip[] m_TankRuin;


    private void Awake()
    {
        shell = gameObject.GetComponent<GameObject>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Tree")
        {
            m_hits.PlayOneShot(m_Trees[Random.Range(0, m_Trees.Length)]);
            RandomPitch();
        }
      
        if (collision.gameObject.tag == "Column")
        {
            m_hits.PlayOneShot(m_Columns[Random.Range(0, m_Columns.Length)]);


        }
        if (collision.gameObject.tag == "Pumpjack")
        {
            m_hits.PlayOneShot(m_Pumpjack[Random.Range(0, m_Pumpjack.Length)]);
            RandomPitch();


        }
        if (collision.gameObject.tag == "PalmTree")
        {
            m_hits.PlayOneShot(m_PalmTrees[Random.Range(0, m_PalmTrees.Length)]);
            RandomPitch();


        }
        if (collision.gameObject.tag == "Radar")
        {

            m_hits.PlayOneShot(m_Radar[Random.Range(0, m_Radar.Length)]);
            RandomPitch();


        }
        if (collision.gameObject.tag == "OilStorage")
        {
            m_hits.PlayOneShot(m_OilStorage[Random.Range(0, m_OilStorage.Length)]);
            RandomPitch();


        }
        if (collision.gameObject.tag == "Buildings")
        {
            m_hits.PlayOneShot(m_Buildings[Random.Range(0, m_Buildings.Length)]);
            RandomPitch();


        }

    }

    private void RandomPitch()
    {
        m_hits.pitch = Random.Range(0.9f, 1);
       
    }


}
