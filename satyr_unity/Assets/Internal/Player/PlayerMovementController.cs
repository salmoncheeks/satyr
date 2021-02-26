using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private CharacterController _controller;
    [SerializeField] private Animator _animator;

    private Vector3 _playerVelocity;
    private bool _groundedPlayer;
    private bool _walkingPlayer;
    private const float PLAYER_SPEED = 2.0f;
    private const float JUMP_HEIGHT = 1.0f;
    private const float GRAVITY_VALUE = -9.81f;

    private void Update()
    {
        _groundedPlayer = _controller.isGrounded;
        
        if (_groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }

        var move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _controller.Move(move * Time.deltaTime * PLAYER_SPEED);

        if (move.magnitude > 0.01f && !_walkingPlayer)
        {
            _walkingPlayer = true;
            _animator.SetBool("Walking", true);
            _animator.SetBool("Idle", false);
        }
        else if (move.magnitude < 0.01f && _walkingPlayer)
        {
            _walkingPlayer = false;
            _animator.SetBool("Walking", false);
            _animator.SetBool("Idle", true);
        }

        if (move.x < -0.1)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if(move.x > 0.1)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);

        }

        if (Input.GetButtonDown("Jump") && _groundedPlayer)
        {
            _playerVelocity.y += Mathf.Sqrt(JUMP_HEIGHT * -3.0f * GRAVITY_VALUE);
        }

        _playerVelocity.y += GRAVITY_VALUE * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);
    }
}
