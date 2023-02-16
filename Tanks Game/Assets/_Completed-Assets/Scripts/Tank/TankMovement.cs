﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Complete
{
    public class TankMovement : MonoBehaviour
    {
        public int m_PlayerNumber = 1;              // Used to identify which tank belongs to which player.  This is set by this tank's manager.
        public float m_Speed = 12f;                 // How fast the tank moves forward and back.
        public float m_TurnSpeed = 180f;            // How fast the tank turns in degrees per second.
        public AudioSource m_MovementAudio;         // Reference to the audio source used to play engine sounds. NB: different to the shooting audio source.
        
        public AudioClip m_EngineIdling;            // Audio to play when the tank isn't moving.
        public AudioClip m_EngineDriving;           // Audio to play when the tank is moving.
        public float m_PitchRange = 0.2f;           // The amount by which the pitch of the engine noises can vary.


        private string m_MovementAxisName;          // The name of the input axis for moving forward and back.
        private string m_TurnAxisName;              // The name of the input axis for turning.
        private Rigidbody m_Rigidbody;              // Reference used to move the tank.
        private float m_MovementInputValue;         // The current value of the movement input.
        private float m_TurnInputValue;             // The current value of the turn input.
        private float m_OriginalPitch;
        private ParticleSystem[] m_particleSystems; // References to all the particles systems used by the Tanks

        //private string PitchParamOfGround = "PitchParamOfGround";


        //JH////

        public AudioMixer audioMainMixer;
        private string pitchParamOfGround = "pitchParamOfGround";
        private string PitchParam = "PitchParam";

        private string groundVol = "groundVol";
        private string movVol = "movVol";

        public AudioSource m_GroundMaterial;
        public AudioClip m_Ground;
        public AudioClip m_Concrete;
        public AudioClip m_ConcreteDriving;

        

        //public AudioSource m_TurningGround;
        //public AudioClip[] m_groundTurning;

        private bool isOnConcrete;

        
        private void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
        }


        private void OnEnable()
        {
            // When the tank is turned on, make sure it's not kinematic.
            m_Rigidbody.isKinematic = false;

            // Also reset the input values.
            m_MovementInputValue = 0f;
            m_TurnInputValue = 0f;

            // We grab all the Particle systems child of that Tank to be able to Stop/Play them on Deactivate/Activate
            // It is needed because we move the Tank when spawning it, and if the Particle System is playing while we do that
            // it "think" it move from (0,0,0) to the spawn point, creating a huge trail of smoke
            m_particleSystems = GetComponentsInChildren<ParticleSystem>();
            for (int i = 0; i < m_particleSystems.Length; ++i)
            {
                m_particleSystems[i].Play();
            }
        }


        private void OnDisable()
        {
            // When the tank is turned off, set it to kinematic so it stops moving.
            m_Rigidbody.isKinematic = true;

            // Stop all particle system so it "reset" it's position to the actual one instead of thinking we moved when spawning
            for (int i = 0; i < m_particleSystems.Length; ++i)
            {
                m_particleSystems[i].Stop();
            }
        }


        private void Start()
        {
            // The axes names are based on player number.
            m_MovementAxisName = "Vertical" + m_PlayerNumber;
            m_TurnAxisName = "Horizontal" + m_PlayerNumber;

            // Store the original pitch of the audio source.
            m_OriginalPitch = m_MovementAudio.pitch;
        }


        private void Update()
        {
            // Store the value of both input axes.
            m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
            m_TurnInputValue = Input.GetAxis(m_TurnAxisName);

            EngineAudio();
            GroundSound();



        }

        
        private void EngineAudio()
        {
            // If there is no input (the tank is stationary)...
            if (Mathf.Abs(m_MovementInputValue) < 0.1f && Mathf.Abs(m_TurnInputValue) < 0.1f)
            {



                // ... and if the audio source is currently playing the driving clip...
                if (m_MovementAudio.clip == m_EngineDriving)
                {
                    // ... change the clip to idling and play it.
                    m_MovementAudio.clip = m_EngineIdling;
                    audioMainMixer.SetFloat(movVol, -7f);

                    //m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                    Invoke("PlayEngine", 0f);
                    audioMainMixer.SetFloat(movVol, -7f);
                    
                }
            }
            else
            {
                // Otherwise if the tank is moving and if the idling clip is currently playing...
                if (m_MovementAudio.clip == m_EngineIdling)
                {
                    // ... change the clip to driving and play.
                    m_MovementAudio.clip = m_EngineDriving;
                    audioMainMixer.SetFloat(movVol, -5f);
                    //m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                    m_MovementAudio.time = Random.Range(0, m_MovementAudio.clip.length);
                    Invoke("PlayEngine", 0f);


                }
            }
        }


        //JH
        private void PlayEngine()
        {
            m_MovementAudio.Play();
        }


        private void FixedUpdate()
        {
            // Adjust the rigidbodies position and orientation in FixedUpdate.
            Move();
            Turn();
            //Debug.Log(m_MovementInputValue);
        }


        private void Move()
        {
            // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
            Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;

            // Apply this movement to the rigidbody's position.
            m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
        }


        private void Turn()
        {
            // Determine the number of degrees to be turned based on the input, speed and time between frames.
            float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;

            // Make this into a rotation in the y axis.
            Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

            // Apply this rotation to the rigidbody's rotation.
            m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Concrete")
            {
                m_MovementAudio.clip = m_ConcreteDriving;
                m_GroundMaterial.clip = m_Concrete;

                isOnConcrete = true;
             
                                
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            m_MovementAudio.clip = m_EngineDriving;
            m_GroundMaterial.clip = m_Ground;
            isOnConcrete = false;

        }
        
        
       
        private void GroundSound()
        {
            if (!m_GroundMaterial.isPlaying)
            {
                if (Mathf.Abs(m_MovementInputValue) > 0.1f)

                {
                    m_GroundMaterial.pitch = Random.Range(0.9f, 1);
                    m_GroundMaterial.time = Random.Range(0, m_GroundMaterial.clip.length);
                    m_GroundMaterial.Play();
                    m_MovementAudio.Play();

                }
                 else if (Mathf.Abs(m_TurnInputValue) > 0.1f)
                {
                    m_GroundMaterial.Play();
                    audioMainMixer.SetFloat(pitchParamOfGround, 1.2f);
                    audioMainMixer.SetFloat(groundVol, -4f);

                    //Debug.Log(m_TurnInputValue);
                }


            }

            if (m_GroundMaterial.isPlaying)
            {
                if (Mathf.Abs(m_MovementInputValue) == 0f && Mathf.Abs(m_TurnInputValue) == 0f)

                {
                    m_GroundMaterial.Stop();

                }
                
                else if (Mathf.Abs(m_TurnInputValue) > 0.1f)
                {
                    audioMainMixer.SetFloat(pitchParamOfGround, 1.2f);
                    audioMainMixer.SetFloat(groundVol, -4f);

                }

                else if(Mathf.Abs(m_TurnInputValue) < 0.1f)
                {
                    audioMainMixer.SetFloat(pitchParamOfGround, 1f);
                    audioMainMixer.SetFloat(groundVol, -7f);

                }


            }
                       
            
            if (isOnConcrete == true)
            {
                if (Mathf.Abs(m_TurnInputValue) > 0.1f)
                {
                    audioMainMixer.SetFloat(pitchParamOfGround, 1.3f);
                    audioMainMixer.SetFloat(groundVol, -4f);

                }

                if (Mathf.Abs(m_MovementInputValue) < 0.1f)
                {
                    m_MovementAudio.Stop();

                }

            }
            
        }

        


    }

    


    
}