using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Item))]
public class ItemEditor : Editor
{
    SerializedProperty typeProperty;
    SerializedProperty productProperty;
    SerializedProperty sellableProperty;
    SerializedProperty priceProperty;

    private void OnEnable()
    {
        // Get references to properties
        typeProperty = serializedObject.FindProperty("type");
        productProperty = serializedObject.FindProperty("plantdata");
        sellableProperty = serializedObject.FindProperty("sellable");
        priceProperty = serializedObject.FindProperty("price");
    }

    public override void OnInspectorGUI()
    {
        // Update the serialized object
        serializedObject.Update();

        // Draw default properties, excluding Product and price
        DrawPropertiesExcluding(serializedObject, "plantdata", "price");

        // Conditionally draw Product if the ItemType is Seed
        if ((ItemType)typeProperty.enumValueIndex == ItemType.Seed)
        {
            EditorGUILayout.PropertyField(productProperty);
        }

        // Conditionally draw price if sellable is true
        EditorGUILayout.PropertyField(sellableProperty);
        if (sellableProperty.boolValue)
        {
            EditorGUILayout.PropertyField(priceProperty);
        }

        // Apply changes to the serialized object
        serializedObject.ApplyModifiedProperties();
    }
}
