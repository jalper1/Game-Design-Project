using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Vitals;
using Input = UnityEngine.Input;

namespace Custom.Scripts
{
    public class Movement : MonoBehaviour
    {
        public CharacterController2D controller;
        public Animator animator;
        public Combat combat; // Reference to the Combat script

        float horMove = 0f;
        float vertMove = 0f;
        public float runSpeed = 40f;

        bool dash = false;

        public float dashRate = 2f;
        public float nextDashTime = 0f;

        PlayerCharacter playerCharacter;
        public bool canDash = true;

        public AudioSource AudioSource;
        public AudioClip dashSound;
        public AudioClip walkSoundStone;
        public AudioClip walkSoundGrass;

        private void Start()
        {
            playerCharacter = GetComponent<PlayerCharacter>();
        }

        // Update is called once per frame
        void Update()
        {

            horMove = Input.GetAxisRaw("Horizontal") * (runSpeed / 4);
            vertMove = Input.GetAxisRaw("Vertical") * (runSpeed / 4);
            animator.SetFloat("Speed", Mathf.Abs(horMove) + Mathf.Abs(vertMove));

            if (playerCharacter != null)
            {
                if (playerCharacter.EnoughStaminaDash())
                {
                    canDash = true;
                }
                else
                {
                    canDash = false;
                }
            }
            else
            {
                Debug.Log("PLAYER CHARACTER NOT INSTANTIATED");
            }

            if (!combat.IsAttacking() && Time.time >= nextDashTime && canDash)
            {
                if (Input.GetButtonDown("Dash"))
                {
                    AudioSource.PlayOneShot(dashSound);
                    dash = true;
                    nextDashTime = Time.time + 1f / dashRate;

                    playerCharacter.ConsumeStamina(10);
                    VitalsUIBind bindComponent = playerCharacter.staminaBar.GetComponent<VitalsUIBind>();
                    bindComponent.UpdateImage(playerCharacter.stamina.Value, playerCharacter.stamina.MaxValue, false);
                }
            }
        }

        private void FixedUpdate()
        {
            if (combat.IsAttacking()) // Check if not attacking
            {
                horMove = 0f; vertMove = 0f; // Reset movement if attacking
            }
            controller.Move(horMove, vertMove, dash);
            if(SceneManager.GetActiveScene().buildIndex == 1)
            {
                AudioSource.clip = walkSoundStone;
            }
            else
            {
                AudioSource.clip = walkSoundGrass;
            }
            if (horMove != 0 || vertMove != 0)
            {
                if (!AudioSource.isPlaying)
                {
                    AudioSource.pitch = 2;
                    AudioSource.Play();
                }
            }
            else
            {
                AudioSource.Stop();
            }
            dash = false;
        }
    }
}