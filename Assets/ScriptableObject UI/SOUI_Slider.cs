using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;

namespace Foliar.UU {
	/// <summary>
	/// Allows direct manipulation of a ScriptableObject field that has a numeric type
	/// </summary>
	[RequireComponent(typeof(Slider))]
	public class SOUI_Slider : SOUI {
		Slider _MySlider;
		/// <summary>
		/// Unity Slider component
		/// </summary>
		private Slider MySlider {
			get {
				if(_MySlider == null) _MySlider = GetComponent<Slider>();

				return _MySlider;
			} set {
				_MySlider = value;
			}
		}

		/// <summary>
		/// Whether the target field is a whole value type or not (short, int, long, ushort, uint, ulong)
		/// </summary>
		public bool IsWholeValue;

		private void Awake() {
			MySlider = GetComponent<Slider>();

			//Add listener
			MySlider.onValueChanged.AddListener(delegate { SliderValueChanged(MySlider.value); });
		}

		private void Update() {
			SetUnitySlider();
		}

		/// <summary>
		/// Sets the target field to the supplied value (This method is supplied to the Unity Slider component's OnValueChanged event)
		/// </summary>
		/// <param name="value"></param>
		void SliderValueChanged(float value) {
			if(IsWholeValue) {
				TargetField.SetValue(TargetObject, Mathf.RoundToInt(value));
			} else {
				TargetField.SetValue(TargetObject, value);
			}
		}

		/// <summary>
		/// Makes the Unity Slider component's min value and whole numbers setting reflect the source type
		/// </summary>
		public void UpdateUnitySlider() {
			System.Type type = TargetField.FieldType;
			if(type == typeof(ushort) || type == typeof(uint) || type == typeof(ulong)) {
				if(MySlider.minValue < 0) MySlider.minValue = 0;
			}
			if(type == typeof(float) || type == typeof(double)) {
				IsWholeValue = false;
				MySlider.wholeNumbers = false;
			} else {
				IsWholeValue = true;
				MySlider.wholeNumbers = true;
			}
			SetUnitySlider();
		}

		/// <summary>
		/// Sets the value of the Unity slider to the value held in the source ScriptableObject field
		/// </summary>
		public void SetUnitySlider() {
			if(IsWholeValue) {
				MySlider.value = (int)TargetField.GetValue(TargetObject);
			} else {
				MySlider.value = (float)TargetField.GetValue(TargetObject);
			}
			
		}
	
	}


}