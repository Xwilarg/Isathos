using UnityEngine;

public class GoDown : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 1f);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.down * .01f);
    }
}
