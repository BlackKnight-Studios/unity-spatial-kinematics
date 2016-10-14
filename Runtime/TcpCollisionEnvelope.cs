using System;
using UnityEngine;

namespace OmegaGaze.SpatialKinematics
{
    public class TcpCollisionEnvelope
    {
        public Vector3 TcpOffset;
        public float SafetyRadius;
        public LayerMask FixtureMask;

        public TcpCollisionEnvelope(Vector3 offset, float radius, LayerMask mask)
        {
            TcpOffset = offset;
            SafetyRadius = radius;
            FixtureMask = mask;
        }

        public bool CheckPathClearance(Vector3 origin, Vector3 destination, out RaycastHit hitInfo)
        {
            Vector3 direction = destination - origin;
            float distance = direction.magnitude;

            if (Physics.SphereCast(origin + TcpOffset, SafetyRadius, direction.normalized, out hitInfo, distance, FixtureMask))
            {
                return false; // Collision detected
            }

            return true; // Path clear
        }
    }
}
