using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[CustomEditor(typeof(NpcDatas))]
public class NpcDatasEditor : Editor
{
    public override void OnInspectorGUI()
    {
        NpcDatas npcDatas = (NpcDatas)target;

        npcDatas.npcName = EditorGUILayout.TextField("NPC Name", npcDatas.npcName);
        npcDatas.default_max_relationship = EditorGUILayout.IntField("Default Max Relationship", npcDatas.default_max_relationship);
        npcDatas.currentRelationship = EditorGUILayout.IntField("Current Relationship", npcDatas.currentRelationship);
        npcDatas.isGift_stamina = EditorGUILayout.Toggle("Is Gift Stamina", npcDatas.isGift_stamina);
        npcDatas.isTotem = EditorGUILayout.Toggle("Is Totem", npcDatas.isTotem);

        if (npcDatas.default_max_relationship == 5)
        {
            npcDatas.isMarry = EditorGUILayout.Toggle("Is Marry", npcDatas.isMarry);
        }

        npcDatas.totem = (Image)EditorGUILayout.ObjectField("Totem", npcDatas.totem, typeof(Image), true);

        if (GUI.changed)
        {
            EditorUtility.SetDirty(npcDatas);
        }
    }
}
