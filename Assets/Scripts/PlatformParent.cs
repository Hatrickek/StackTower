using UnityEngine;

class PlatformParent : MonoBehaviour 
{
    public void DestroyPlatform()
    {
        int childCount = transform.childCount;
        if (childCount > 0)
        {
            for (int i = childCount - 1; i >= 0; i--)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }
}
