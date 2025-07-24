using UnityEngine;
using UnityEngine.XR.Management;

namespace Unity.EditorXR.Core
{
    public class EnableXR : MonoBehaviour
    {
        void Start()
        {
            if (!XRGeneralSettings.Instance.Manager.isInitializationComplete)
            {
                XRGeneralSettings.Instance.Manager.InitializeLoaderSync();
                XRGeneralSettings.Instance.Manager.StartSubsystems();
            }

#if ENABLE_WINMD_SUPPORT
            UnityEngine.XR.WSA.HolographicSettings.IsContentProtectionEnabled = false;
            UnityEngine.XR.WSA.HolographicSettings.ReprojectionMode = UnityEngine.XR.WSA.HolographicReprojectionMode.OrientationAndPosition;
#endif
        }
    }
}
