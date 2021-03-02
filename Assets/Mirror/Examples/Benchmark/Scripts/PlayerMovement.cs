using UnityEngine;

namespace Mirror.Examples.Benchmark
{
    public class PlayerMovement : NetworkBehaviour
    {
        public float speed = 5;

        private Vector3 dir;

        void Update()
        {
            if (!isLocalPlayer) return;

            float h = 0;
            float v = 0;

            if (Input.GetAxis("Horizontal") > 0)
                h = 1;
            else if (Input.GetAxis("Horizontal") < 0)
                h = -1;

            if (Input.GetAxis("Vertical") > 0)
                v = 1;
            else if (Input.GetAxis("Vertical") < 0)
                v = -1;

            dir = new Vector3(h, v, 0);
        }

        private void FixedUpdate()
        {
            transform.Translate(dir.normalized * (Time.deltaTime * speed));
        }
    }
}
