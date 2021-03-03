using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

namespace Primozov.AmongBombs
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] float speed = 2;
        [SerializeField] float speedLimit = 5;
        [SerializeField] GameObject rotationRef;
        [SerializeField] public bool invertAxis;

        [Header("Inputs")]
        [SerializeField] KeyCode keyUp;
        [SerializeField] KeyCode keyLeft;
        [SerializeField] KeyCode keyDown;
        [SerializeField] KeyCode keyRight;

        [Header("Events")]
        [SerializeField] UnityEvent<Vector2> onPlayerMove;
        [SerializeField] UnityEvent onPlayerSpeedIncrease;

        Vector2 movement;

        void Update()
        {
            GetInputs();
        }

        public void IncreaseSpeed()
        {
            if (speed < speedLimit)
            {
                speed++;
                onPlayerSpeedIncrease.Invoke();
            }
                
        }

        private void GetInputs()
        {
            movement = Vector2.zero;
            if (Input.GetKey(keyUp))
                movement += Vector2.up;
            if (Input.GetKey(keyLeft))
                movement += Vector2.left;
            if (Input.GetKey(keyDown))
                movement += Vector2.down;
            if (Input.GetKey(keyRight))
                movement += Vector2.right;
        }

        private void FixedUpdate()
        {
            if (invertAxis)
            {
                movement *= -1;
            }

            onPlayerMove.Invoke(movement);
            RotateRefByMovement(movement);
            Vector3 mov3 = new Vector3(movement.x, movement.y, 0);
            transform.position += mov3 * Time.deltaTime * speed;
        }

        private void RotateRefByMovement(Vector2 movement)
        {
            if(movement.x > 0)
            {
                rotationRef.transform.rotation = Quaternion.Euler(0, 0, 0);
            }else if(movement.x < 0)
            {
                rotationRef.transform.rotation = Quaternion.Euler(0, 0, 180);
            }else if(movement.y > 0)
            {
                rotationRef.transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            else if (movement.y < 0)
            {
                rotationRef.transform.rotation = Quaternion.Euler(0, 0, -90);
            }
        }

        public void InvertAxis(float seconds)
        {
            StartCoroutine(InvertedAxisPeriod(seconds));
        }

        IEnumerator InvertedAxisPeriod(float seconds)
        {
            invertAxis = true;
            yield return new WaitForSeconds(seconds);
            invertAxis = false;
        }
    }
}