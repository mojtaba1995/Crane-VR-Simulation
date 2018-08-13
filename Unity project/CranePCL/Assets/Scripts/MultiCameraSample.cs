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

public class MultiCameraSample : MonoBehaviour {

	public OVRCameraRig[]	cameraControllers = new OVRCameraRig[0];
	public int						currentController = 0;

	/// <summary>
	/// Initialize
	/// </summary>
	void Start() {
		UpdateCameraControllers();
	}
	
	/// <summary>
	/// Set the current camera controller
	/// </summary>
	void UpdateCameraControllers() {
		for ( int i = 0; i < cameraControllers.Length; i++ ) {
			if ( cameraControllers[i] == null ) {
				continue;
			}
			cameraControllers[i].gameObject.SetActive( i == currentController );
		}
	}

	/// <summary>
	/// Check input and switch between camera controllers
	/// These samples use the default Unity Input Mappings with the addition of "Right Shoulder" and "Left Shoulder"
	/// </summary>
	void Update() {
		if ( OVRInput.GetDown(OVRInput.RawButton.RShoulder) ) {
			//*************************
			// switch to the next camera
			//*************************
			if ( ++currentController == cameraControllers.Length ) {
				currentController = 0;
			}
			UpdateCameraControllers();
		} else if ( OVRInput.GetDown(OVRInput.RawButton.LShoulder) ) {
			//*************************
			// switch to the previous camera
			//*************************
			if ( --currentController < 0 ) {
				currentController = cameraControllers.Length - 1;
			}
			UpdateCameraControllers();
		}
	}
}
