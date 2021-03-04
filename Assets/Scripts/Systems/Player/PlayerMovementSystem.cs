using UnityEngine;

namespace Primozov.AmongBombs.Systems
{
    public class PlayerMovementSystem
    {
        float speed = 2;
        float speedLimit = 5;
        Vector2 movement = Vector2.zero;

        public PlayerMovementSystem(float speed, float speedLimit)
        {
            this.speed = speed;
            this.speedLimit = speedLimit;
        }

        public void IncreaseSpeed()
        {
            if (speed < speedLimit)
            {
                speed++;
            }       
        }

        public void UpdateMovement(Vector2 movement)
        {
            this.movement = movement;
        }

        public Vector3 GetNewPosition()
        {
            Vector3 mov3 = new Vector3(movement.x, movement.y, 0);
            return mov3 * Time.deltaTime * speed;
        }
    }
}
