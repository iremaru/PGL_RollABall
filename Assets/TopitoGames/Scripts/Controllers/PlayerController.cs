using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody rigidBody;
    [SerializeField] [Range(0.1f, 1f)] float speedPower = 0.5f;
    [SerializeField] [Range(1, 100)] int jumpPower = 1;
    [SerializeField] ForceMode jumpForce = ForceMode.Impulse;
    [Tooltip("When Player is touching this layer, it could jump. Remember to assign the same layer to the game object where you want that Player can to jump.")]
    [SerializeField] LayerMask groundLayer;
    Vector3 startingPosition;

    float direction_LeftRight;
    float direction_FrontBack;
    bool canMove = false;
    bool isOnFloor;

    #region UNITY METHODS
        
        private void Awake() {
            startingPosition = transform.position;
            rigidBody = gameObject.GetComponent<Rigidbody>();
        }
        private void OnEnable() {
            GameManager.getReadyToStart += Init;
            GameManager.gameHasStarted += OnGameStart;
            GameManager.gameIsOver += OnGameOver;
        }
        private void FixedUpdate() {
            if(canMove)
            {
                ForceMode mode = isOnFloor ? ForceMode.VelocityChange : ForceMode.Impulse;
                Vector3 movement = isOnFloor ? new Vector3(direction_LeftRight, 0.0f, direction_FrontBack) : Vector3.down;
                rigidBody.AddForce(movement * speedPower, mode);
            }
        }

        public void OnMove(InputValue movementValue)
        {
            if( CheckIsOnFloor() )
            {
                Vector2 movement = movementValue.Get<Vector2>();
                direction_LeftRight = movement.x;
                direction_FrontBack = movement.y;
            }
        }

        public void OnJump()
        {
            if( canMove && CheckIsOnFloor() )
            {
                rigidBody.AddForce( Vector3.up * jumpPower, jumpForce);
                isOnFloor = false;
            }
        }

        private void OnDisable() {
            GameManager.getReadyToStart -= Init;
            GameManager.gameIsOver -= OnGameOver;
            GameManager.gameHasStarted -= OnGameStart;
        }
    #endregion

    public void Init()
    {
        transform.position = startingPosition;
        canMove = false;
        rigidBody.isKinematic = true;
    }
    public void OnGameStart()
    {
        rigidBody.isKinematic = false;
        canMove = true;
    }
    public void OnGameOver()
    {
        canMove = false;
        rigidBody.isKinematic = true;
    }

    #region PRIVATE METHODS
        
        bool CheckIsOnFloor()
        {
            isOnFloor = Physics.CheckSphere( transform.position, transform.lossyScale.x/2, groundLayer);
        return isOnFloor;
    }
    #endregion

}
