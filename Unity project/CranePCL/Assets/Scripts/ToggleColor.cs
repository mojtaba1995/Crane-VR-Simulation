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

public class ToggleColor : MonoBehaviour
{
	public enum Colors
	{
		Red,
		Green,
		Blue
	};

	public Colors ObjectColor = Colors.Red;

	public void RestoreState()
	{
		ObjectColor = (Colors)PlayerPrefs.GetInt(name + "_ObjectColor");
		GetComponent<Renderer>().sharedMaterial = SaveStateSample.instance.Materials[(int)ObjectColor];
	}

	public void SaveState()
	{
		PlayerPrefs.SetInt(name + "_ObjectColor", (int)ObjectColor);
	}

 	public void OnClick()
	{
		int color = (int)ObjectColor;
		color++;
		if (color > (int)Colors.Blue)
		{
			color = (int)Colors.Red;
		}
		
		ObjectColor = (Colors)color;
		GetComponent<Renderer>().sharedMaterial = SaveStateSample.instance.Materials[(int)ObjectColor];
	}
}
