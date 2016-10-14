using System;
using UnityEngine;

namespace OmegaGaze.SpatialKinematics
{
    /// <summary>
    /// Forward And Backward Reaching Inverse Kinematics (FABRIK)
    /// Provides linear O(n) convergence with sub-millimeter precision.
    /// </summary>
    public static class FabrikSolver
    {
        public static bool Solve(
            Vector3[] positions,
            float[] lengths,
            Vector3 target,
            Vector3 rootOrigin,
            int maxIterations = 15,
            float tolerance = 0.001f)
        {
            int n = positions.Length;
            if (n < 2) return false;

            float totalLength = 0f;
            for (int i = 0; i < lengths.Length; i++) totalLength += lengths[i];

            // Case 1: Target unreachable
            if (Vector3.Distance(rootOrigin, target) >= totalLength)
            {
                for (int i = 0; i < n - 1; i++)
                {
                    float r = Vector3.Distance(target, positions[i]);
                    float lambda = lengths[i] / r;
                    positions[i + 1] = (1f - lambda) * positions[i] + lambda * target;
                }
                return false;
            }

            // Case 2: Target reachable, iterative backward and forward pass
            int iteration = 0;
            float diff = Vector3.Distance(positions[n - 1], target);

            while (diff > tolerance && iteration < maxIterations)
            {
                // Stage 1: Backward pass (End-effector to Base)
                positions[n - 1] = target;
                for (int i = n - 2; i >= 0; i--)
                {
                    float r = Vector3.Distance(positions[i + 1], positions[i]);
                    float lambda = lengths[i] / r;
                    positions[i] = (1f - lambda) * positions[i + 1] + lambda * positions[i];
                }

                // Stage 2: Forward pass (Base to End-effector)
                positions[0] = rootOrigin;
                for (int i = 0; i < n - 1; i++)
                {
                    float r = Vector3.Distance(positions[i + 1], positions[i]);
                    float lambda = lengths[i] / r;
                    positions[i + 1] = (1f - lambda) * positions[i] + lambda * positions[i + 1];
                }

                diff = Vector3.Distance(positions[n - 1], target);
                iteration++;
            }

            return diff <= tolerance;
        }
    }
}
