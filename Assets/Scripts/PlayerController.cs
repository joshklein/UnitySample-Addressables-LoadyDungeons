﻿using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float m_MovementSpeed = 5.0f;

    [SerializeField]
    private Animator m_AnimatorController;

    private bool m_HasKey = false;

    private Rigidbody m_Rigidbody;

    private int m_VelocityHash = Animator.StringToHash("Velocity");

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void KeyCollected()
    {
        m_HasKey = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Cache the string value
        if (other.CompareTag("Chest"))
        {
            KeyCollected();
        }

        if (other.CompareTag("Door"))
        {
            Debug.Log("Triggered by a door");

            if(m_HasKey)
            {
                Debug.Log("Opened the door");

                GameManager.LevelCompleted();
            }
        }
    }

    void Update()
    {
        float xMovement = m_MovementSpeed * Input.GetAxis("Horizontal");
        float zMovement = m_MovementSpeed * Input.GetAxis("Vertical");

        // TODO: Refactor this
        if(xMovement < 0)
        {
            m_Rigidbody.rotation = Quaternion.Euler(0, -90, 0);
        }
        else if(xMovement > 0)
        {
            m_Rigidbody.rotation = Quaternion.Euler(0, 90, 0);
        }
        else if(zMovement < 0)
        {
            m_Rigidbody.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if(zMovement > 0)
        {
            m_Rigidbody.rotation = Quaternion.Euler(0, 0, 0);
        }

        m_Rigidbody.velocity =  new Vector3(xMovement, 0, zMovement);

        // We use the velocity of the character's rigidbody to drive the animation
        m_AnimatorController.SetFloat(m_VelocityHash, m_Rigidbody.velocity.magnitude);
    }
}
