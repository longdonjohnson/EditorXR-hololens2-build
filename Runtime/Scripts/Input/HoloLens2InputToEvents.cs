#if ENABLE_WINMD_SUPPORT
using Windows.UI.Input.Spatial;
#endif
using UnityEngine;
using UnityEngine.XR;

namespace Unity.EditorXR.Input
{
    sealed class HoloLens2InputToEvents : BaseVRInputToEvents
    {
        protected override string DeviceName
        {
            get { return "HoloLens"; }
        }

#if ENABLE_WINMD_SUPPORT
        SpatialInteractionManager m_InteractionManager;

        void Start()
        {
            m_InteractionManager = SpatialInteractionManager.GetForCurrentView();
        }
#endif

        void SendTrackingEvents(VRInputDevice.Handedness hand, int deviceIndex)
        {
#if ENABLE_WINMD_SUPPORT
            var handState = m_InteractionManager.GetDetectedSourcesAtTimestamp(PerceptionTimestampHelper.FromHistoricalTargetTime(DateTimeOffset.Now)).FirstOrDefault(s => s.Source.Handedness == (hand == VRInputDevice.Handedness.Left ? SpatialInteractionSourceHandedness.Left : SpatialInteractionSourceHandedness.Right));
            if (handState != null)
            {
                var pose = handState.SourcePose;
                var position = pose.Position;
                var rotation = pose.Rotation;

                var inputEvent = InputSystem.CreateEvent<VREvent>();
                inputEvent.deviceType = typeof(VRInputDevice);
                inputEvent.deviceIndex = deviceIndex;
                inputEvent.localPosition = new Vector3((float)position.X, (float)position.Y, (float)position.Z);
                inputEvent.localRotation = new Quaternion((float)rotation.X, (float)rotation.Y, (float)rotation.Z, (float)rotation.W);

                InputSystem.QueueEvent(inputEvent);
            }
#else
            var node = hand == VRInputDevice.Handedness.Left ? XRNode.LeftHand : XRNode.RightHand;
            var localPosition = InputTracking.GetLocalPosition(node);
            var localRotation = InputTracking.GetLocalRotation(node);

            var inputEvent = InputSystem.CreateEvent<VREvent>();
            inputEvent.deviceType = typeof(VRInputDevice);
            inputEvent.deviceIndex = deviceIndex;
            inputEvent.localPosition = localPosition;
            inputEvent.localRotation = localRotation;

            InputSystem.QueueEvent(inputEvent);
#endif
        }
    }
}
