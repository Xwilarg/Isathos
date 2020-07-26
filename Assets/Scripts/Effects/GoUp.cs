using UnityEngine;

public class GoUp : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 1f);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.up * .01f);
    }
}
