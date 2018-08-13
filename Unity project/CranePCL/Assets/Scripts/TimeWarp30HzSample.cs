/************************************************************************************

Copyright   :   Copyright 2014 Oculus VR, LLC. All Rights reserved.

Licensed under the Oculus VR Rift SDK License Version 3.3 (the "License");
you may not use the Oculus VR Rift SDK except in compliance with the License,
which is provided at the time of installation or download, or which
otherwise accompanies this software in either electronic or hard copy form.

You may obtain a copy of the License at

http://www.oculus.com/licenses/LICENSE-3.3

Unless required by applicable law or agreed to in writing, the Oculus VR SDK
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

************************************************************************************/

using UnityEngine;
using System.Collections;					// required for Coroutines
using System.Runtime.InteropServices;		// required for DllImport

/// <summary>
/// Sets the minimum vsync count in VRAPI to allow TimeWarp sub-stepping, where the same frame is
/// warped multiple times during slow synthesis.
/// \warning OVR_TW_SetMinimumVsyncs is deprecated and will be removed in the near future.
/// </summary>
public class TimeWarp30HzSample : MonoBehaviour
{
    public enum VsyncMode
    {
        VSYNC_60FPS = 1,
        VSYNC_30FPS = 2,
        VSYNC_20FPS = 3
    }

	public OVRInput.RawButton button = OVRInput.RawButton.Back;

	public VsyncMode			                targetFrameRate = VsyncMode.VSYNC_30FPS;

	public Material								fps60Material = null;
	public Material								fps30Material = null;
	public Material								fps20Material = null;

	private Renderer							targetFpsRenderer = null;

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start()
	{
		targetFpsRenderer = GetComponentInChildren<Renderer>();
#if (UNITY_ANDROID && !UNITY_EDITOR)
		// delay one frame because OVRCameraController initializes TimeWarp in Start()
		Invoke("UpdateVSyncMode", 0.01666f);
#endif
	}
	
	/// <summary>
	/// Set the Time Warp vsync rate
	/// </summary>
	void UpdateVSyncMode()
	{
		// This is initialized in OVRCameraController.Start() to VSYNC_60FPS, however, you can override the setting
		// as desired to support Time Warp VSync at 60/30/20 FPS frame rates for consistency or power savings.
		// Time Warp at 30 FPS will also help app that can't consistently perform at 60 FPS
		OVRManager.instance.vsyncCount = (int)targetFrameRate;

		// swap the material on the fps quad renderer
		switch(targetFrameRate)
		{
		case VsyncMode.VSYNC_20FPS:
			targetFpsRenderer.sharedMaterial = fps20Material;
			//Application.targetFrameRate = 20;
			break;
		case VsyncMode.VSYNC_30FPS:
			targetFpsRenderer.sharedMaterial = fps30Material;
			//Application.targetFrameRate = 30;
			break;
		case VsyncMode.VSYNC_60FPS:
			targetFpsRenderer.sharedMaterial = fps60Material;
			//Application.targetFrameRate = 60;
			break;
		}
	}

	/// <summary>
	/// Check input and switch between target frame rates
	/// These samples use the default Unity Input Mappings with the addition of "Right Shoulder" and "Left Shoulder"
	/// </summary>
	void Update()
	{
		if (OVRInput.GetDown(button))
		{
			//*************************
			// switch to the next mode
			//*************************
			switch(targetFrameRate)
			{
			case VsyncMode.VSYNC_20FPS:
				targetFrameRate = VsyncMode.VSYNC_30FPS;
				break;
			case VsyncMode.VSYNC_30FPS:
				targetFrameRate = VsyncMode.VSYNC_60FPS;
				break;
			case VsyncMode.VSYNC_60FPS:
				targetFrameRate = VsyncMode.VSYNC_20FPS;
				break;
			}
			UpdateVSyncMode();
		}
	}
}
