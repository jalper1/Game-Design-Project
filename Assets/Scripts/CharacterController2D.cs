using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float m_DashForce = 800f; // Amount of force added when the player dashes.
    [Range(0, .3f)][SerializeField] private float m_MovementSmoothing = .05f; // How much to smooth out the movement

    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true; // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;

    [Header("Events")]
    [Space]
    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }



    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Move(float moveHorizontal, float moveVertical, bool dash)
    {
            // Move the character by finding the target velocity
            Vector2 targetVelocity = new Vector2(moveHorizontal * 10f, moveVertical * 10f);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            // If the input is moving the player right and the player is facing left...
            if (moveHorizontal > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (moveHorizontal < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }

            // Check if the player should dash
            if (dash)
            {
                Dash();
            }
        
    }

    private void Dash()
    {
        // Get the direction of the dash
        Vector2 dashDirection = Vector2.right; // Default dash direction

       
            // Calculate the dash direction based on player's movement and facing direction
            if (Mathf.Abs(m_Rigidbody2D.velocity.x) > 0.01f || Mathf.Abs(m_Rigidbody2D.velocity.y) > 0.01f)
            {
                dashDirection = m_Rigidbody2D.velocity.normalized;
            }
            else
            {
                // If the player is not moving, use the facing direction
                dashDirection = m_FacingRight ? Vector2.right : Vector2.left;
            }

            // Apply a dash force in the calculated direction
            Vector2 dashForce = dashDirection * m_DashForce;
            m_Rigidbody2D.AddForce(dashForce);
        
    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
