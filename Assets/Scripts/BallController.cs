using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float JumpSpeed = 5;
    public float ClickingSpeed = 10;
    public ParticleSystem ContactParticle;
    [SerializeField] private GameSO m_GameSO;
    private Rigidbody m_Rigidbody;

    private bool m_IsClicking;

    private static Transform m_Transform;

    void Awake()
    {
        m_Transform = transform;
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_IsClicking = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            m_IsClicking = false;
        }

        if (m_IsClicking)
        {
            m_Rigidbody.velocity = Vector3.up * -ClickingSpeed;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (m_GameSO.State != GameSO.GameState.Alive)
            return;

        if (m_IsClicking)
        {
            if (other.gameObject.CompareTag("Bad"))
            {
                m_GameSO.State = GameSO.GameState.Dead;
            }
            else if (other.gameObject.CompareTag("Good"))
            {
                other.gameObject.GetComponent<PlatformChild>().DestroyPlatform();
                m_GameSO.AddScore();
                ContactParticle.Play();
            }
        }

        else
        {
            m_Rigidbody.velocity = Vector3.up * JumpSpeed;
        }

        if (other.gameObject.CompareTag("End"))
        {
            m_GameSO.AddLevel();
        }
    }

    public static void ResetPosition()
    {
        m_Transform.position = new Vector3(0f, 0.93f, -1.59200001f);
    }
}
