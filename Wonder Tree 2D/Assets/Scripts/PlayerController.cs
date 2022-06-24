using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum Life { Alive, Death}
    public Life lifeState = Life.Alive;

    public bool facingRight = true;
    public float runSpeed = 10;
    public float jumpForce = 7;
    public float groundCheckerRange = 0.2f;
    public LayerMask groundLayers;

    [SerializeField] Rigidbody2D _playerRB;
    [SerializeField] Animator _playerAnimator;
    [SerializeField] Transform _groundChecker;

    float _horizontalInput = 0f;
    [SerializeField] bool _grounded = false;
    [SerializeField] bool _running = false;

    // Start is called before the first frame update
    void OnEnable()
    {
        _playerRB = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponentInChildren<Animator>();

        lifeState = Life.Alive;
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeState == Life.Alive)
        {
            JumpPlayer();
            _horizontalInput = Input.GetAxisRaw("Horizontal") * runSpeed;
            GroundChecker();
        }
        else
        {
            Die();
        }

    }

    private void FixedUpdate()
    {
        if (lifeState == Life.Alive)
        {
            MovePlayer();
        }
    }

    void MovePlayer()
    {
        if (_horizontalInput != 0)
        {
            _running = true;
            transform.Translate(Vector2.right * _horizontalInput * Time.fixedDeltaTime);
            FlipFacePlayer();
            _playerAnimator.SetBool("Grounded", _grounded);
            _playerAnimator.SetBool("Running", _running);
        }
        else
        {
            _running = false;
            _playerAnimator.SetBool("Grounded", _grounded);
            _playerAnimator.SetBool("Running", _running);
        }
    }

    void FlipFacePlayer()
    {
        Vector3 localScale = transform.localScale;

        if (_horizontalInput > 0)
        {
            facingRight = true;
        }
        else if (_horizontalInput < 0)
        {
            facingRight = false;
        }

        if ((facingRight && localScale.x < 0) || (!facingRight && localScale.x > 0))
        {
            localScale.x *= -1;
        }

        // update the scale
        transform.localScale = localScale;
    }


    void JumpPlayer()
    {
        if (_grounded && Input.GetButtonDown("Jump"))
        {
            _playerRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            _grounded = false;
            _playerAnimator.SetBool("Grounded", !_grounded);
        }
        else
        {
            _playerAnimator.SetBool("Grounded", _grounded);
        }
    }

    void Die()
    {
        if (lifeState == Life.Death)
        {
            Debug.Log("Player has Died!");
            Destroy(gameObject);
        }
    }

    private void GroundChecker()
    {
        Collider2D floor = Physics2D.OverlapCircle(_groundChecker.position, groundCheckerRange, groundLayers);
        if (floor)
        {
            if (floor.CompareTag("Ground"))
            {
                _grounded = true;
            }
            else
            {
                _grounded = false;
            }
        }
        else
        {
            _grounded = false;
        }
    }

    private void OnDrawGizmos()
    {
        if (_groundChecker == null)
            return;

        Gizmos.DrawWireSphere(_groundChecker.position, groundCheckerRange);
    }
}
