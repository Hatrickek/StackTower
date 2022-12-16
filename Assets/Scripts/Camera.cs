using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private static Transform Target;

    private static float m_MinY;

    private static float m_Offset;

    private static Transform m_Transform;

    private void Start()
    {
        Target = GameObject.Find("Ball").transform;
        m_Transform = transform;
        m_Offset = transform.position.y - Target.position.y;
        m_MinY = 0;
    }

    private void Update()
    {
        if (Target.position.y < m_MinY)
        {
            m_MinY = Target.position.y;
            m_Transform.position = new Vector3(m_Transform.position.x, m_MinY + m_Offset, m_Transform.position.z);
        }
    }

    public static void ResetPosition()
    {
        m_Transform.position = new Vector3(0, 2.24f, -10f);
        m_MinY = 0;
    }
}
