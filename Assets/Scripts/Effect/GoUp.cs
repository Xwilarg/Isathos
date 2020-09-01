using UnityEngine;

namespace Effect
{
    public class GoUp : MonoBehaviour
    {
        [SerializeField]
        private float timer = 1f;

        private void Start()
        {
            Destroy(gameObject, timer);
        }

        private void FixedUpdate()
        {
            transform.Translate(Vector2.up * .01f);
        }
    }
}