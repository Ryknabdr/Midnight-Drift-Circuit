    using UnityEngine;

    public class SimpleNPC : MonoBehaviour
    {
        [Header("Waypoints")]
        public Transform[] waypoints;

        [Header("NPC Setting")]
        public float speed = 10f;
        public float turnSpeed = 12f;
        public float reachDistance = 2f;

        private int currentWaypoint = 0;
        private float startY;

        void Start()
        {
            startY = transform.position.y;
        }

        void Update()
        {
            if (waypoints == null || waypoints.Length == 0)
                return;

            Transform target = waypoints[currentWaypoint];

            Vector3 targetPos = target.position;
            targetPos.y = startY;

            Vector3 direction = targetPos - transform.position;

            if (direction.magnitude < reachDistance)
            {
                currentWaypoint++;

                if (currentWaypoint >= waypoints.Length)
                    currentWaypoint = 0;

                return;
            }

            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                turnSpeed * Time.deltaTime
            );

            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPos,
                speed * Time.deltaTime
            );

            // Kunci tinggi NPC supaya tidak tenggelam
            Vector3 pos = transform.position;
            pos.y = startY;
            transform.position = pos;
        }
    }