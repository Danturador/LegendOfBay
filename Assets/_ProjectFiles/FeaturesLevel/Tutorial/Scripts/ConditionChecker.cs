using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

[Serializable]
public class ConditionChecker : MonoBehaviour
{
	[SerializeField] private GameObject selectedObject;
	[SerializeField] private string selectedClass;
	[SerializeField] private string selectedField;
	[SerializeField] private string selectedProperty;
	[SerializeField] private string selectedOperator;
	[SerializeField] private string comparisonValue;

	public int selectedClassIndex;
	public int selectedFieldIndex;
	public int selectedPropertyIndex;
	public int selectedOperatorIndex;

	private void Awake()
	{
		InitializeValues();
	}

	private void InitializeValues()
	{
		if (selectedObject != null)
		{
			var objectType = selectedObject.GetType();
			var fields = objectType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

			if (selectedFieldIndex >= 0 && selectedFieldIndex < fields.Length)
			{
				selectedField = fields[selectedFieldIndex].Name;
			}

			if (selectedPropertyIndex == 0)
			{
				selectedProperty = "Count";
			}
			else if (selectedPropertyIndex == 1)
			{
				selectedProperty = "Null";
			}

			if (selectedOperatorIndex >= 0 && selectedOperatorIndex < 4)
			{
				var operators = new string[] { "<", ">", "==", "!=" };
				selectedOperator = operators[selectedOperatorIndex];
			}
		}
	}
	public bool CheckConditions()
	{
		if (selectedObject == null) return false;

		Type objectType = Type.GetType(selectedClass);

		if (string.IsNullOrEmpty(selectedClass) || objectType == null || !selectedObject.GetComponent(objectType))
		{
			Debug.LogWarning($"Selected class {selectedClass} does not match the type of selectedObject {selectedObject.GetType().Name}");
			return false;
		}

		Debug.Log($"Looking for field: {selectedField} in {selectedClass}");

		FieldInfo fieldInfo = objectType.GetField(selectedField, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

		if (fieldInfo == null)
		{
			Debug.LogWarning($"Field {selectedField} not found in {selectedClass}");
			return false;
		}

		object inventoryComponent = selectedObject.GetComponent(objectType);
		object fieldValue = fieldInfo.GetValue(inventoryComponent);
		Debug.Log($"Field Value: {fieldValue}");
		int intValue;

		if (selectedProperty == "Count" && fieldValue is IList list)
		{
			intValue = list.Count;
		}
		else
		{
			intValue = Convert.ToInt32(fieldValue);
		}

		int comparisonInt;
		if (!int.TryParse(comparisonValue, out comparisonInt))
		{
			Debug.LogError("Comparison value is not a valid integer.");
			return false;
		}

		bool result = false;
		switch (selectedOperator)
		{
			case "<":
				result = intValue < comparisonInt;
				break;
			case ">":
				result = intValue > comparisonInt;
				break;
			case "==":
				result = intValue == comparisonInt;
				break;
			case "!=":
				result = intValue != comparisonInt;
				break;
		}
		return result;
	}
}

//using UnityEngine;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Reflection;

//[Serializable]
//public class ConditionChecker : MonoBehaviour
//{
//	[SerializeField] private GameObject selectedObject;
//	[SerializeField] private string selectedClass;
//	[SerializeField] private string selectedField;
//	[SerializeField] private string selectedProperty;
//	[SerializeField] private string selectedOperator;
//	[SerializeField] private string comparisonValue;

//	public int selectedClassIndex;
//	public int selectedFieldIndex;
//	public int selectedPropertyIndex;
//	public int selectedOperatorIndex;

//	private void Awake()
//	{
//		InitializeValues();
//	}

//	private void InitializeValues()
//	{
//		if (selectedObject != null)
//		{
//			var objectType = selectedObject.GetType();
//			var fields = objectType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

//			if (selectedFieldIndex >= 0 && selectedFieldIndex < fields.Length)
//			{
//				selectedField = fields[selectedFieldIndex].Name;
//			}

//			if (selectedPropertyIndex == 0)
//			{
//				selectedProperty = "Count";
//			}
//			else if (selectedPropertyIndex == 1)
//			{
//				selectedProperty = "Null";
//			}

//			if (selectedOperatorIndex >= 0 && selectedOperatorIndex < 4)
//			{
//				var operators = new string[] { "<", ">", "==", "!=" };
//				selectedOperator = operators[selectedOperatorIndex];
//			}
//		}
//	}
//	public bool CheckConditions()
//	{
//		if (selectedObject == null) return false;

//		Type objectType = Type.GetType(selectedClass);

//		if (string.IsNullOrEmpty(selectedClass) || objectType == null || !selectedObject.GetComponent(objectType))
//		{
//			Debug.LogWarning($"Selected class {selectedClass} does not match the type of selectedObject {selectedObject.GetType().Name}");
//			return false;
//		}

//		Debug.Log($"Looking for field: {selectedField} in {selectedClass}");

//		if (selectedField == "enabled")
//		{
//			MonoBehaviour componentInstance = selectedObject.GetComponent(objectType) as MonoBehaviour;
//			return componentInstance != null && componentInstance.enabled;
//		}

//		FieldInfo fieldInfo = objectType.GetField(selectedField, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

//		if (fieldInfo == null)
//		{
//			Debug.LogWarning($"Field {selectedField} not found in {selectedClass}");
//			return false;
//		}

//		object inventoryComponent = selectedObject.GetComponent(objectType);
//		object fieldValue = fieldInfo.GetValue(inventoryComponent);
//		Debug.Log($"Field Value: {fieldValue}");
//		int intValue;

//		if (selectedProperty == "Count" && fieldValue is IList list)
//		{
//			intValue = list.Count;
//		}
//		else
//		{
//			intValue = Convert.ToInt32(fieldValue);
//		}

//		int comparisonInt;
//		if (!int.TryParse(comparisonValue, out comparisonInt))
//		{
//			Debug.LogError("Comparison value is not a valid integer.");
//			return false;
//		}

//		bool result = false;
//		switch (selectedOperator)
//		{
//			case "<":
//				result = intValue < comparisonInt;
//				break;
//			case ">":
//				result = intValue > comparisonInt;
//				break;
//			case "==":
//				result = intValue == comparisonInt;
//				break;
//			case "!=":
//				result = intValue != comparisonInt;
//				break;
//		}
//		return result;
//	}
//}