using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(RoadTile))]
public class RoadTileEditor : Editor
{
    // ������ �Ǹ� �ڵ����� Ȱ��ȭ
    // target : ���õ� ����Ƽ ������Ʈ
    RoadTile roadTile;

    void OnEnable()
    {
        roadTile = target as RoadTile;  // ĳ���� �õ��ؼ� ������ ���� �ִ´�.
    }

    /// <summary>
    /// �ν�����â���� �ʿ� ������ �׸��� �Լ�
    /// </summary>
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();  // �⺻������ �׸��� ���� ��� �׸�

        if(roadTile != null && roadTile.sprites != null)        // RoadTile�� �ְ� ��������Ʈ�� ������
        {
            Texture2D texture;
            EditorGUILayout.LabelField("Sprites Preview");      // ���� �ֱ�
            GUILayout.BeginHorizontal();                        // �������� �׸��� ����
            foreach(var sprite in roadTile.sprites)             // roadTile.sprites���� �ϳ��� ó��
            {
                texture = AssetPreview.GetAssetPreview(sprite); // ��������Ʈ�� �ؽ��ķ� �ٲٰ�
                GUILayout.Label("", GUILayout.Height(64), GUILayout.Width(64)); // ũ�� ���
                GUI.DrawTexture(GUILayoutUtility.GetLastRect(), texture);       // ũ�� �������� �ؽ��� �׸���
            }
            GUILayout.EndHorizontal();                          // �������� �׸��� ���� ������
        }
    }
}

#endif