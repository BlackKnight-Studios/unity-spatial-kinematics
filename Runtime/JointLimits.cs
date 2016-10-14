using System;
using UnityEngine;

namespace OmegaGaze.SpatialKinematics
{
    [Serializable]
    public struct JointLimits
    {
        public float MinAngle;
        public float MaxAngle;
        public Vector3 RotationAxis;

        public JointLimits(float min, float max, Vector3 axis)
        {
            MinAngle = min;
            MaxAngle = max;
            RotationAxis = axis.normalized;
        }

        public float Clamp(float angle)
        {
            return Mathf.Clamp(angle, MinAngle, MaxAngle);
        }
    }
}
