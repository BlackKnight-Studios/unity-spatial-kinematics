using NUnit.Framework;
using UnityEngine;
using OmegaGaze.SpatialKinematics;

namespace OmegaGaze.SpatialKinematics.Tests
{
    public class KinematicsTests
    {
        [Test]
        public void FabrikSolver_ReachesTarget_WithinSubMillimeterTolerance()
        {
            Vector3[] points = new Vector3[]
            {
                new Vector3(0, 0, 0),
                new Vector3(0, 1, 0),
                new Vector3(0, 2, 0),
                new Vector3(0, 3, 0)
            };
            float[] lengths = new float[] { 1f, 1f, 1f };
            Vector3 target = new Vector3(1.2f, 1.5f, 0.5f);
            Vector3 root = Vector3.zero;

            bool success = FabrikSolver.Solve(points, lengths, target, root, maxIterations: 20, tolerance: 0.0005f);

            Assert.IsTrue(success);
            float finalError = Vector3.Distance(points[3], target);
            Assert.Less(finalError, 0.0005f);
        }
    }
}
