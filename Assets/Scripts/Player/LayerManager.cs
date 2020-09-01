using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class LayerManager : MonoBehaviour
    {
        private List<SpriteRenderer> _sprites;
        private Transform _playerTransform;

        private void Start()
        {
            _sprites = new List<SpriteRenderer>();
            _playerTransform = transform.parent;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            SpriteRenderer _sr = collision.GetComponent<SpriteRenderer>();
            if (_sr != null)
                _sprites.Add(_sr);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            SpriteRenderer _sr = collision.GetComponent<SpriteRenderer>();
            if (_sr != null)
                _sprites.Remove(_sr);
        }

        private void FixedUpdate()
        {
            foreach (var sr in _sprites)
            {
                sr.sortingOrder = sr.transform.position.y < _playerTransform.position.y ? 1 : -1;
            }
        }
    }
}