using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private CharacterSprites _playerSprites;

    private Rigidbody2D _rb;
    private SpriteRenderer _sr;

    private Direction _dir;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _dir = Direction.UP;
    }

    private void FixedUpdate()
    {
        // Movements
        _rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * 10f;

        // Set direction depending of velocity
        if (Mathf.Abs(_rb.velocity.x) > Mathf.Abs(_rb.velocity.y))
        {
            if (_rb.velocity.x > 0)
                _dir = Direction.RIGHT;
            else if (_rb.velocity.x < 0)
                _dir = Direction.LEFT;
        }
        else
        {
            if (_rb.velocity.y > 0)
                _dir = Direction.UP;
            else if (_rb.velocity.y < 0)
                _dir = Direction.DOWN;
        }

        // Set sprite direction
        switch (_dir)
        {
            case Direction.UP:
                _sr.sprite = _playerSprites.up;
                break;

            case Direction.DOWN:
                _sr.sprite = _playerSprites.down;
                break;

            case Direction.LEFT:
                _sr.sprite = _playerSprites.left;
                break;

            case Direction.RIGHT:
                _sr.sprite = _playerSprites.right;
                break;
        }
    }

    private enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }
}
