using Unity.VisualScripting;
using UnityEngine;

public class PlatformChild : MonoBehaviour
{
    private PlatformParent m_Parent;

    private void Awake()
    {
        m_Parent = GetComponentInParent<PlatformParent>();
    }
    
    private void OnEnable()
    {
        m_Parent = GetComponentInParent<PlatformParent>();
    }

    public void DestroyPlatform()
    {
        m_Parent.DestroyPlatform();
    }
}