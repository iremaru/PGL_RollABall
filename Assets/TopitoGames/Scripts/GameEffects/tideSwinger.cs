using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopitoGames
{
    public class tideSwinger : MonoBehaviour
    {
        #region PROPERTIES
            [SerializeField] float maxElevation;
            [SerializeField] float maxDescent;
            [SerializeField] [Range(0.1f, 2f)] float swingSpeed = 0.5f;

            float initialElevation, currentMaxElevation, currentMaxDescent;
            bool isRising;

        #endregion

        private void Awake() {
            initialElevation = transform.position.y;
            currentMaxElevation = initialElevation + maxElevation;
            currentMaxDescent = initialElevation - maxDescent;
        }
        void Update()
        {
            Swing();
        }

        #region PRIVATE METHODS
            void Swing()
            {
                transform.Translate((GetDirection(transform.position.y) * swingSpeed) * Time.deltaTime, Space.Self);
            }

            Vector3 GetDirection(float currentElevation)
            {
                isRising = !isRising && currentElevation <= currentMaxDescent ||
                    isRising && currentElevation < currentMaxElevation ? true : false;

                if(isRising) return Vector3.up;

                return Vector3.down;
            }
        #endregion
    }
    
}
