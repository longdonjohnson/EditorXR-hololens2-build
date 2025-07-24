using NUnit.Framework;
using Unity.EditorXR.Core;
using Unity.EditorXR.Input;
using UnityEngine;

namespace Unity.EditorXR.Tests.Unit
{
    public class HandTrackingTest
    {
        [Test]
        public void HandTrackingTestScene()
        {
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.AddComponent<EnableXR>();
            cube.AddComponent<HoloLens2InputToEvents>();

            var inputToEvents = cube.GetComponent<HoloLens2InputToEvents>();
            Assert.IsNotNull(inputToEvents);

            // In a real test, we would simulate hand tracking input and verify that the cube moves correctly.
            // For now, we'll just check that the scripts can be added to a GameObject without errors.
        }
    }
}
