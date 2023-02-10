using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Hits : MonoBehaviour
{
    public GameObject gameObject;
    //Colliders // Crash
    public AudioSource hits;
    
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
            hits.PlayOneShot(m_TankRuin[Random.Range(0, m_TankRuin.Length)]);
        }
        if (collision.gameObject.tag == "Tree")
        {
            hits.PlayOneShot(m_Trees[Random.Range(0, m_Trees.Length)]);
        }
        if (collision.gameObject.tag == "Column")
        {
            hits.PlayOneShot(m_Columns[Random.Range(0, m_Columns.Length)]);
        }
        if (collision.gameObject.tag == "Pumpjack")
        {
            hits.PlayOneShot(m_Pumpjack[Random.Range(0, m_Pumpjack.Length)]);
        }
        if (collision.gameObject.tag == "PalmTree")
        {
            hits.PlayOneShot(m_PalmTrees[Random.Range(0, m_PalmTrees.Length)]);
        }
        if (collision.gameObject.tag == "Radar")
        {
            hits.PlayOneShot(m_Radar[Random.Range(0, m_Radar.Length)]);
        }
        if (collision.gameObject.tag == "OilStorage")
        {
            hits.PlayOneShot(m_OilStorage[Random.Range(0, m_OilStorage.Length)]);
        }
        if (collision.gameObject.tag == "Buildings")
        {
            hits.PlayOneShot(m_Buildings[Random.Range(0, m_Buildings.Length)]);
        }
    
    }


}
