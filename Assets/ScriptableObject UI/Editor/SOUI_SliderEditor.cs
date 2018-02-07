using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

namespace Foliar.UU {

	[CustomEditor(typeof(SOUI_Slider))]
	public class SOUI_SliderEditor : SOUIEditor {

		System.Type[] AllowedTypes = new System.Type[] {
			typeof(System.Decimal),
			typeof(System.Double),
			typeof(System.Single),
			typeof(System.Int16),
			typeof(System.Int32),
			typeof(System.Int64),
			typeof(System.UInt16),
			typeof(System.UInt32),
			typeof(System.UInt64)
		};

		public override void OnInspectorGUI() {
			base.OnInspectorGUI();

			SOUI_Slider myTarget = (SOUI_Slider)target;

			if(myTarget.TargetObject != null) {
				myTarget.UpdateUnitySlider();

				bool IsAllowedType = false;
				System.Type CurrentType = myTarget.TargetField.FieldType;
				for(int i = 0; i < AllowedTypes.Length; i++) {
					if(CurrentType == AllowedTypes[i]) {
						IsAllowedType = true;
						break;
					}
				}			
				if(!IsAllowedType) {
					EditorGUILayout.HelpBox("Warning: Invalid type - should be a numeric type e.g. float, int...", MessageType.Error);
				}

				myTarget.SetUnitySlider();
			}

			
		}
	}
}