using UnityEngine;

public class AnimateAndDestroy : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 0.2f);
    }
}