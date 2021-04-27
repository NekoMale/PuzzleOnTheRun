using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    [SerializeField] float _moveSpeed = 3.5f;
    [SerializeField] float _jumpForce = 3.5f;

    [Header("Only for debug purpose")]
    [SerializeField] [ReadOnly] bool _isJumping = false;
    [SerializeField] [ReadOnly] bool _jump = false;
    [SerializeField] [ReadOnly] bool _isStopped = false;
    [SerializeField] [ReadOnly] float _currentMoveSpeed = 0f;
    [SerializeField] [ReadOnly] float _currentJumpForce = 0f;

    Rigidbody2D _myRigidBody = null;

    #region Public Movement Methods
    /// <summary>
    /// Set current move speed to 0 in order to stop it
    /// </summary>
    public void Stop() {
        _isStopped = true;
        _isJumping = false;
        _myRigidBody.velocity = Vector2.zero;
    }

    /// <summary>
    /// Set current move speed to original move speed in order to start movement again
    /// </summary>
    public void ResumeMovement() {
        _isStopped = false;
    }

    /// <summary>
    /// Multiply current move speed for multiplier.
    /// If you need to divide move speed you can pass (1/divisor)
    /// </summary>
    /// <param name="multiplier">Value that multiply _currentMoveSpeed</param>
    public void MultiplyMoveSpeed(float multiplier) {
        _currentMoveSpeed *= multiplier;
        _myRigidBody.velocity = new Vector2(_currentMoveSpeed, _myRigidBody.velocity.y);
    }

    /// <summary>
    /// Increase current move speed to this value.
    /// If you need to subtract move speed you can pass (- amount)
    /// </summary>
    /// <param name="amount">Value that change _currentMoveSpeed</param>
    public void ChangeMoveSpeed(float amount) {
        _currentMoveSpeed += amount;
        _myRigidBody.velocity = new Vector2(_currentMoveSpeed, _myRigidBody.velocity.y);
    }

    public void ResetMovement() {
        _currentMoveSpeed = _moveSpeed;
    }
    #endregion

    #region Public Jump Methods
    /// <summary>
    /// Multiply current jump force for multiplier.
    /// If you need to divide jump force you can pass (1/divisor)
    /// </summary>
    /// <param name="multiplier">Value that multiply _currentJumpForce</param>
    public void MultiplyJumpForce(float multiplier) {
        _currentJumpForce *= multiplier;
    }

    /// <summary>
    /// Increase current jump force to this value.
    /// If you need to subtract jump force you can pass (- amount)
    /// </summary>
    /// <param name="amount">Value that change _currentJumpForce</param>    
    public void ChangeJumpForce(float amount) {
        _currentJumpForce += amount;
    }

    /// <summary>
    /// Order to movement to jump
    /// </summary>
    public void Jump() {
        _jump = true;
    }

    public void ResetJump() {
        _currentJumpForce = _jumpForce;
    }
    #endregion

    #region Unity Methods
    private void Awake() {
        _myRigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        _currentMoveSpeed = _moveSpeed;
        _currentJumpForce = _jumpForce;
    }

    private void Update() {
        if (_isStopped) return;

        if (!Input.GetKeyDown(KeyCode.Space)) return;
        if (_isJumping) return;
        Jump();
    }

    private void FixedUpdate() {
        if (_isStopped) return;

        if (!_isJumping) {
            Debug.Log(_myRigidBody.velocity);
            _myRigidBody.velocity = new Vector2(_currentMoveSpeed, _myRigidBody.velocity.y);
        }

        if (_jump) {
            _isJumping = true;
            _jump = false;
            _myRigidBody.velocity = new Vector2(_currentMoveSpeed, 0);
            //ResetMovement(); //Aggiunto altrimenti non funziona il salto fra due piattaforme
            _myRigidBody.AddForce(new Vector2(0, _currentJumpForce), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        _isJumping = false;
    }
    #endregion
}
