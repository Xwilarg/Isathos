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
    private EventTrigger _toSpeak;

    private bool _canMove;

    public void SetCanMove(bool value)
    {
        _canMove = value;
    }

    public static PlayerController S;

    public void EnterEventTrigger(EventTrigger e)
    {
        _speakEventIcon.SetActive(true);
        _toSpeak = e;
    }

    public void ExitEventTrigger()
    {
        // That happens after taking a door but while being in a tutorial
        // When that's the case, we want to keep the tutorial popup
        if (!_canMove)
            return;

        _speakEventIcon.SetActive(false);
        _toSpeak = null;
        EventManager.S.Clear();
        DialoguePopup.S.Close();
    }

    private void Awake()
    {
        S = this;
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _dir = Direction.UP;
        _toSpeak = null;
        _canMove = true;
    }

    private void Update()
    {
        // Interraction
        if (_canMove)
        {
            if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return)) && _toSpeak != null)
                EventManager.S.StartEvent(_toSpeak, transform);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return))
                EventManager.S.StartTutorialDiscution();
        }
    }

    private void FixedUpdate()
    {
        if (!_canMove)
        {
            _rb.velocity = Vector2.zero;
            return;
        }

        // Movements
        _rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * 5f;

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
}
