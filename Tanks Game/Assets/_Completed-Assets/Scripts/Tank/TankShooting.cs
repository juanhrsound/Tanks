using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

namespace Complete
{
    public class TankShooting : MonoBehaviour
    {
        public int m_PlayerNumber = 1;              // Used to identify the different players.
        public Rigidbody m_Shell;                   // Prefab of the shell.
        public Transform m_FireTransform;           // A child of the tank where the shells are spawned.
        public Slider m_AimSlider;                  // A child of the tank that displays the current launch force.
        public AudioSource m_ShootingAudio;         // Reference to the audio source used to play the shooting audio. NB: different to the movement audio source.
        //public AudioClip m_ChargingClip;            // Audio that plays when each shot is charging up.

        //JH ---- 
        public AudioMixer audioMixer;
        public AudioClip [] m_FireClip;

        private float volumeMin = 0.4f;
        private float volumeMax = 0.5f;
        private float pitchMin = 0.9f;
        private float pitchMax = 1f;
        private float lowPassMin = 1589f;
        private float lowPassMax = 2140f;
        private float decayTimeMin = 15.77f;
        private float decayTimeMax = 16.77f;                          
        
        private string decayTime = "decayTime";
        private string lowPassShooting = "lowPassShooting";
        private string shiftShoots = "shiftShoots";
        private string roomShootingReverb = "roomShootingReverb";
        private string reflectDelay = "reflectDelay";
               

        //JH ----


        public float m_MinLaunchForce = 15f;        // The force given to the shell if the fire button is not held.
        public float m_MaxLaunchForce = 30f;        // The force given to the shell if the fire button is held for the max charge time.
        public float m_MaxChargeTime = 0.75f;       // How long the shell can charge for before it is fired at max force.


        private string m_FireButton;                // The input axis that is used for launching shells.
        private float m_CurrentLaunchForce;         // The force that will be given to the shell when the fire button is released.
        private float m_ChargeSpeed;                // How fast the launch force increases, based on the max charge time.
        private bool m_Fired;                       // Whether or not the shell has been launched with this button press.


        private void OnEnable()
        {
            // When the tank is turned on, reset the launch force and the UI
            m_CurrentLaunchForce = m_MinLaunchForce;
            m_AimSlider.value = m_MinLaunchForce;
        }


        private void Start()
        {
            // The fire axis is based on the player number.
            m_FireButton = "Fire" + m_PlayerNumber;

            // The rate that the launch force charges up is the range of possible forces by the max charge time.
            m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;
        }


        private void Update ()
        {
            // The slider should have a default value of the minimum launch force.
            m_AimSlider.value = m_MinLaunchForce;

            // If the max force has been exceeded and the shell hasn't yet been launched...
            if (m_CurrentLaunchForce >= m_MaxLaunchForce && !m_Fired)
            {
                // ... use the max force and launch the shell.
                m_CurrentLaunchForce = m_MaxLaunchForce;
                Fire();
            }
            // Otherwise, if the fire button has just started being pressed...
            else if (Input.GetButtonDown(m_FireButton))
            {
                // ... reset the fired flag and reset the launch force.
                m_Fired = false;
                m_CurrentLaunchForce = m_MinLaunchForce;

                // Change the clip to the charging clip and start it playing.
                //m_ShootingAudio.clip = m_ChargingClip;
                m_ShootingAudio.Play();
            }
            // Otherwise, if the fire button is being held and the shell hasn't been launched yet...
            else if (Input.GetButton(m_FireButton) && !m_Fired)
            {
                // Increment the launch force and update the slider.
                m_CurrentLaunchForce += m_ChargeSpeed * Time.deltaTime;

                m_AimSlider.value = m_CurrentLaunchForce;

            }


            // Otherwise, if the fire button is released and the shell hasn't been launched yet...
            else if (Input.GetButtonUp(m_FireButton) && !m_Fired)
            {
                // ... launch the shell.
                Fire();
            }
        }


        private void Fire ()
        {
            // Set the fired flag so only Fire is only called once.

            m_Fired = true;

            // Create an instance of the shell and store a reference to it's rigidbody.
            Rigidbody shellInstance =
                Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

            // Set the shell's velocity to the launch force in the fire position's forward direction.
            shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward;

            // Change the clip to the firing clip and play it.
            m_ShootingAudio.PlayOneShot(m_FireClip[Random.Range(0, m_FireClip.Length)]);

            
            if (m_CurrentLaunchForce < 18f)
            {

                m_ShootingAudio.volume = Random.Range(0.70f, 0.75f);
                audioMixer.SetFloat("shiftShoots", Random.Range(0.95f, 1f));
                audioMixer.SetFloat("lowPassShooting", Random.Range(130f, 150f));
                //Reverb Parameters
                audioMixer.SetFloat("roomShootingReverb", Random.Range(-4788f, -4288f));
                audioMixer.SetFloat("reflectDelay", Random.Range(0.2f, 0.25f));
            }
            
            if (m_CurrentLaunchForce > 18f)
            {
                m_ShootingAudio.volume = Random.Range(0.99f, 1f);
                audioMixer.SetFloat("shiftShoots", Random.Range(0.62f, 0.70f));
                audioMixer.SetFloat("lowPassShooting", 10f);
                //Reverb Parameters
                audioMixer.SetFloat("roomShootingReverb", Random.Range(-800,-850));
                audioMixer.SetFloat("reflectDelay", Random.Range(0.28f, 0.3f));

            }

            Debug.Log(m_AimSlider.value);
           
            // Reset the launch force.  This is a precaution in case of missing button events.
            m_CurrentLaunchForce = m_MinLaunchForce;
        }
    }
}