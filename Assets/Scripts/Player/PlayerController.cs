using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private CharacterSprites _playerSprites;

    [SerializeField]
    private GameObject _collUp, _collDown, _collLeft, _collRight;

    [SerializeField]
    private GameObject _speakEventIcon;

    private Rigidbody2D _rb;
    private SpriteRenderer _sr;

    private Direction _dir;

    public void EnterEventTrigger(EventTrigger _)
    {
        _speakEventIcon.SetActive(true);
    }

    public void ExitEventTrigger()
    {
        _speakEventIcon.SetActive(false);
    }

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
                _collUp.SetActive(true);
                _collDown.SetActive(false);
                _collLeft.SetActive(false);
                _collRight.SetActive(false);
                break;

            case Direction.DOWN:
                _sr.sprite = _playerSprites.down;
                _collUp.SetActive(false);
                _collDown.SetActive(true);
                _collLeft.SetActive(false);
                _collRight.SetActive(false);
                break;

            case Direction.LEFT:
                _sr.sprite = _playerSprites.left;
                _collUp.SetActive(false);
                _collDown.SetActive(false);
                _collLeft.SetActive(true);
                _collRight.SetActive(false);
                break;

            case Direction.RIGHT:
                _sr.sprite = _playerSprites.right;
                _collUp.SetActive(false);
                _collDown.SetActive(false);
                _collLeft.SetActive(false);
                _collRight.SetActive(true);
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
