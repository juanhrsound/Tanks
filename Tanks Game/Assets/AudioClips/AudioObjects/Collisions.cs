using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;



[CreateAssetMenu (fileName = "Collisions", menuName = "Audio/Collisions")]
public class Collisions : ScriptableObject
{
    public Vector2 volume = new Vector2(0f, 1f); 
    public Vector2 pitch = new Vector2(0.8f, 1f);

    public AudioClip[] debrisClip;
   

}



