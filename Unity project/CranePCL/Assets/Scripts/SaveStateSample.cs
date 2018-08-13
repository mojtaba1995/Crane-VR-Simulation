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
using System.Collections;

public class SaveStateSample : MonoBehaviour {
	public static SaveStateSample instance = null;

	public Material[] Materials;

	void Awake() {
		instance = this;
	}

	// Use this for initialization
	void Start () {
		RestoreState();
	}

	void OnApplicationQuit() {
		SaveState();
	}

	void OnApplicationPause( bool pauseState ) {
		if ( pauseState == true )
		{
			SaveState();
		}
	}

	void SaveState()
	{
		Debug.Log( "Saving state..." );
		ToggleColor[] objects = FindObjectsOfType( typeof( ToggleColor ) ) as ToggleColor[];
		foreach( ToggleColor obj in objects )
		{
			obj.SaveState();
		}
		
		PlayerPrefs.SetInt( "HasSaveState", 1 );
		PlayerPrefs.Save();
	}

	void RestoreState()
	{
		if ( PlayerPrefs.GetInt( "HasSaveState" ) != 0 )
		{
			Debug.Log( "Restoring state..." );
			ToggleColor[] objects = FindObjectsOfType( typeof( ToggleColor ) ) as ToggleColor[];
			foreach( ToggleColor obj in objects )
			{
				obj.RestoreState();
			}
		}
		else
		{
			Debug.Log( "No saved state." );
		}
	}
}
