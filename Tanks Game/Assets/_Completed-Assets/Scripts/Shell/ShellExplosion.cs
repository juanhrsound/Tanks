using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

namespace Complete
{
    public class ShellExplosion : MonoBehaviour
    {
        public LayerMask m_TankMask;                        // Used to filter what the explosion affects, this should be set to "Players".
        public ParticleSystem m_ExplosionParticles;         // Reference to the particles that will play on explosion.
       
        public float m_MaxDamage = 100f;                    // The amount of damage done if the explosion is centred on a tank.
        public float m_ExplosionForce = 1000f;              // The amount of force added to a tank at the centre of the explosion.
        public float m_MaxLifeTime = 2f;                    // The time in seconds before the shell is removed.
        public float m_ExplosionRadius = 5f;                // The maximum distance away from the explosion tanks can be and are still affected.


        //JH---------
        //public AudioSource m_ExplosionAudio;                // Reference to the audio that will play on explosion.
        //public AudioClip m_Woosh;
        //public AudioClip[] m_ExplosionClip;
        public Collisions debrisSound;





        private void Start ()
        {
            // If it isn't destroyed by then, destroy the shell after it's lifetime.
            Destroy (gameObject, m_MaxLifeTime);
            


        }

        private void Update()
        {
            //Debug.DrawRay(this.transform.position, Vector3.down * 2f, Color.red);
            //Debug.DrawRay(this.transform.position, Vector3.back * 2f, Color.red);
            //TouchingTheGround();



        }
        /*
        private void TouchingTheGround()
        {
            if (!hasPlayedWoosh && (Physics.Raycast(this.transform.position, Vector3.down, 1.5f, groundMask)
                || Physics.Raycast(this.transform.position, Vector3.back, 1.5f, groundMask)))
            {
                Debug.Log("TOUCH!");
                m_ExplosionAudio.PlayOneShot(m_Woosh);
                hasPlayedWoosh = true;
            }
            else if (hasPlayedWoosh && !(Physics.Raycast(this.transform.position, Vector3.down, 1.5f, groundMask)
                || Physics.Raycast(this.transform.position, Vector3.back, 1.5f, groundMask)))
            {
                // reset the flag if the condition is no longer met
                hasPlayedWoosh = false;
            }

        }*/

        private void OnTriggerEnter (Collider other)
        {        

            // Collect all the colliders in a sphere from the shell's current position to a radius of the explosion radius.
            Collider[] colliders = Physics.OverlapSphere(transform.position, m_ExplosionRadius, m_TankMask);

            // Go through all the colliders...
            for (int i = 0; i < colliders.Length; i++)
            {
                // ... and find their rigidbody.
                Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();

                // If they don't have a rigidbody, go on to the next collider.
                if (!targetRigidbody)
                    continue;

                // Add an explosion force.
                targetRigidbody.AddExplosionForce(m_ExplosionForce, transform.position, m_ExplosionRadius);

                // Find the TankHealth script associated with the rigidbody.
                TankHealth targetHealth = targetRigidbody.GetComponent<TankHealth>();

                // If there is no TankHealth script attached to the gameobject, go on to the next collider.
                if (!targetHealth)
                    continue;

                // Calculate the amount of damage the target should take based on it's distance from the shell.
                float damage = CalculateDamage(targetRigidbody.position);

                // Deal this damage to the tank.
                targetHealth.TakeDamage(damage);
            }

            // Unparent the particles from the shell.
            m_ExplosionParticles.transform.parent = null;

            // Play the particle system.
            //m_ExplosionParticles.Play();
            //Explode();

            // Play the explosion sound effect.

            //m_ExplosionAudio.Play();

            //m_ExplosionAudio.Play();           

            //debrisSound.audioSource.Play();

            PlayRandomDebrisClip();
            //Explode();

            // Once the particles have finished, destroy the gameobject they are on.
            ParticleSystem.MainModule mainModule = m_ExplosionParticles.main;
            Destroy(m_ExplosionParticles.gameObject, mainModule.duration);

            // Destroy the shell.
            Destroy(gameObject);


        }

        void PlayRandomDebrisClip()
        {
            GameObject audioObject = new GameObject();
            AudioSource audioSource = audioObject.AddComponent<AudioSource>();


            audioSource.volume = Random.Range(debrisSound.volume.x, debrisSound.volume.y);
            audioSource.pitch = Random.Range(debrisSound.pitch.x, debrisSound.pitch.y);

            audioSource.PlayOneShot(debrisSound.debrisClip[Random.Range(0, debrisSound.debrisClip.Length)]);

            Destroy(audioObject, debrisSound.debrisClip.Length);
        }





        private float CalculateDamage (Vector3 targetPosition)
        {
            // Create a vector from the shell to the target.
            Vector3 explosionToTarget = targetPosition - transform.position;

            // Calculate the distance from the shell to the target.
            float explosionDistance = explosionToTarget.magnitude;

            // Calculate the proportion of the maximum distance (the explosionRadius) the target is away.
            float relativeDistance = (m_ExplosionRadius - explosionDistance) / m_ExplosionRadius;

            // Calculate damage as this proportion of the maximum possible damage.
            float damage = relativeDistance * m_MaxDamage;

            // Make sure that the minimum damage is always 0.
            damage = Mathf.Max (0f, damage);

            return damage;

            

        }



    }
}