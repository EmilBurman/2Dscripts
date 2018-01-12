using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class Camera2DFollow : MonoBehaviour
    {
        public Transform target;                        // The target of the camera
        [Header("Setup for camera x-axis behavior")]
        public float dampingX = 0.3f;                    // The delay of the camera response to follow, lower is quicker.                       
        public float lookAheadFactorX = 3;               // How much the camera looks ahead after catching up to player.
        public float lookAheadReturnSpeed = 0.5f;       // How fast the camera pans back to the player after looking ahead.
        public float lookAheadMoveThreshold = 0.1f;     // How far the camera is allowed to look ahead, lower is further.

        float m_OffsetZ;                                // The camera offset on the z axis.
        Vector3 m_LastTargetPosition;
        Vector3 m_CurrentVelocity;
        Vector3 m_LookAheadPos;

        // Use this for initialization.
        void Start()
        {
            m_LastTargetPosition = transform.position;
            m_LastTargetPosition = target.position;
            m_OffsetZ = (transform.position - target.position).z;
            transform.parent = null;
        }

        // Update is called on a fixed schedule.
        void FixedUpdate()
        {
            // only update lookahead pos if accelerating or changed direction
            float xMoveDelta = (target.position - m_LastTargetPosition).x;

            bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

            if (updateLookAheadTarget)
            {
                m_LookAheadPos = lookAheadFactorX * Vector3.right * Mathf.Sign(xMoveDelta);
            }
            else
            {
                m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
            }

            Vector3 aheadTargetPos = target.position + m_LookAheadPos + Vector3.forward * m_OffsetZ;
            Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, dampingX);

            transform.position = newPos;

            m_LastTargetPosition = target.position;
        }
    }
}