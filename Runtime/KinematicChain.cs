using System;
using System.Collections.Generic;
using UnityEngine;

namespace OmegaGaze.SpatialKinematics
{
    public class KinematicSegment
    {
        public Transform JointTransform;
        public float Length;
        public JointLimits Limits;
        public Vector3 Position;
        public Quaternion Rotation;

        public KinematicSegment(Transform t, float len, JointLimits limits)
        {
            JointTransform = t;
            Length = len;
            Limits = limits;
            Position = t != null ? t.position : Vector3.zero;
            Rotation = t != null ? t.rotation : Quaternion.identity;
        }
    }

    public class KinematicChain
    {
        public List<KinematicSegment> Segments { get; private set; }
        public float TotalReach { get; private set; }

        public KinematicChain()
        {
            Segments = new List<KinematicSegment>();
            TotalReach = 0f;
        }

        public void AddSegment(Transform t, float length, JointLimits limits)
        {
            Segments.Add(new KinematicSegment(t, length, limits));
            TotalReach += length;
        }

        public void SynchronizeFromTransforms()
        {
            for (int i = 0; i < Segments.Count; i++)
            {
                if (Segments[i].JointTransform != null)
                {
                    Segments[i].Position = Segments[i].JointTransform.position;
                    Segments[i].Rotation = Segments[i].JointTransform.rotation;
                }
            }
        }
    }
}
