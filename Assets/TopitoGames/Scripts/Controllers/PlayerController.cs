using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] [Range(1, 10)] int speed = 10;

    private float movementX;
    private float movementY;
    bool canMove = false;

    #region UNITY METHODS
        
        private void OnEnable() {
            GameManager.gameIsOver += OnGameOver;
            GameManager.gameIsStarting += OnGameStart;
        }
        private void FixedUpdate() {
            if(canMove)
            {
                Vector3 movement = new Vector3(movementX, 0.0f, movementY);
                rb.AddForce(movement * speed);
            }
        }

        public void OnMove(InputValue movementValue)
        {
            Vector2 movement = movementValue.Get<Vector2>();
            movementX = movement.x;
            movementY = movement.y;
        }

    #endregion

    public void OnGameStart()
    {
        rb.isKinematic = false;
        canMove = true;
    }
    public void OnGameOver()
    {
        canMove = false;
        rb.isKinematic = true;
    }

}
