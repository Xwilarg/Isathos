using UnityEngine;

namespace Attack
{
    public class ThrowingSpell : MonoBehaviour
    {
        [SerializeField]
        private GameObject _explosion;

        public void OnCollisionEnter2D(Collision2D collision)
        {
            Destroy(gameObject);
            Destroy(Instantiate(_explosion, transform.position, Quaternion.identity), 1.5f);
        }
    }
}
