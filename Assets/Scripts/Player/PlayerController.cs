using Event;
using Event.Trigger;
using Inventory;
using Other;
using SO;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private CharacterSprites _playerSprites;

        [SerializeField]
        private GameObject _collUp, _collDown, _collLeft, _collRight;

        [SerializeField]
        private GameObject _speakEventIcon;

        [SerializeField]
        private Spell _spell;

        [SerializeField]
        private RectTransform _reloadAttackRect, _reloadDashRect;

        private Rigidbody2D _rb;
        private SpriteRenderer _sr;

        private Direction _dir;
        private EventTrigger _toSpeak;
        public EventTrigger GetEventTrigger() => _toSpeak;
        public Bag Inventory { private set; get; }

        private bool _canMove; // Set for tutorial purpose
        private bool _isCinematic; // Cinematics outside of tutorials
        private bool _isGameOver;

        private float _reloadTimer = 0f; // Time in ms between 2 spells
        private const float _reloadTimerRef = 1f;

        private float _reloadDash = 0f;
        private const float _reloadDashRef = 3f;
        private float _timerDash = 0f;
        private const float _timerDashRef = .2f;

        /// <summary>
        /// ONLY FOR TUTORIAL
        /// </summary>
        public void SetCanMove(bool value)
        {
            _canMove = value;
        }

        public void SetIsCinematic(bool value)
        {
            _isCinematic = value;
        }

        public void Loose()
        {
            _isGameOver = true;
            _sr.sprite = _playerSprites.dead;
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
            _isCinematic = false;
            Inventory = new Bag();
            Inventory.AddItem(ItemID.HUD);
            Inventory.AddItem(ItemID.HOUSE_KEY);
            Inventory.AddItem(ItemID.FOLDED_PAPER);
        }

        private void Update()
        {
            if (_isGameOver)
            {
                if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return))
                    EventManager.S.StartGameOverDiscution();
                return;
            }

            // Reduce timer and display them ingame
            _reloadTimer -= Time.deltaTime;
            _reloadDash -= Time.deltaTime;
            _timerDash -= Time.deltaTime;
            if (_reloadAttackRect != null && _reloadTimer > 0f)
            {
                float val = _reloadTimer * 50 / _reloadTimerRef - 50;
                _reloadAttackRect.sizeDelta = new Vector2(val, 0f);
                _reloadAttackRect.anchoredPosition = new Vector2(val / 2f, 0f);
            }
            if (_reloadDashRect != null && _reloadDash > 0f)
            {
                float val = _reloadDash * 50 / _reloadDashRef - 50;
                _reloadDashRect.sizeDelta = new Vector2(val, 0f);
                _reloadDashRect.anchoredPosition = new Vector2(val / 2f, 0f);
            }

            // Interraction
            if (_canMove)
            {
                if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return)) && _toSpeak != null)
                    EventManager.S.StartEvent(_toSpeak, transform);
                if (!_isCinematic) // Can't open menus during cinematics
                {
                    if (Input.GetKeyDown(KeyCode.I))
                        InventoryPopup.S.ToggleInventory();
                    if (Input.GetMouseButtonDown(0) && _reloadTimer < 0f)
                    {
                        var dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                        dir.z = 0f;
                        dir.Normalize();
                        GameObject go = Instantiate(_spell.ManaBall, transform.position, Quaternion.Euler(new Vector3(0f, 0f, Vector2.SignedAngle(Vector2.up, dir) + 90f)));
                        go.GetComponent<Rigidbody2D>().velocity = dir * 10f;
                        Destroy(go, 10f);
                        _reloadTimer = _reloadTimerRef;
                    }
                    if (Input.GetKeyDown(KeyCode.LeftShift) && _reloadDash < 0f && _timerDash < 0f)
                    {
                        _rb.velocity *= 3f;
                        _reloadDash = _reloadDashRef;
                        _timerDash = _timerDashRef;
                    }
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return))
                    EventManager.S.StartTutorialDiscution();
            }
        }

        private void FixedUpdate()
        {
            if (!_canMove || _isCinematic || _isGameOver)
            {
                _rb.velocity = Vector2.zero;
                return;
            }

            if (_timerDash > 0f) // Can't move player while he is dashing
                return;

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
            _sr.sortingOrder = -(int)(transform.position.y * 100f);

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
}