using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR
using UnityEditor;
#endif

// ���� �׸��� ���� Ÿ�� Ŭ����(�ڵ����� ������ ��������Ʈ�� �������ִ� Ŭ����)
public class RoadTile : Tile
{
    /// <summary>
    /// �ֺ� ��� ��ġ�� RoadTile�� �ִ��� ǥ���ϱ� ���� enum.
    /// </summary>
    [Flags]                 // �� enum�� ��Ʈ�÷��׷� ����ϰڴ�.
    enum AdjTilePosition : byte // 8bit ũ���� enum
    {
        None = 0,       // 0000 0000. �ֺ��� RoadTile�� �ִ�.
        North = 1,      // 0000 0001. ���ʿ� RoadTile�� �ִ�.
        East = 2,       // 0000 0010. ���ʿ� RoadTile�� �ִ�.
        South = 4,      // 0000 0100. ���ʿ� RoadTile�� �ִ�.
        West = 8,       // 0000 1000. ���ʿ� RoadTile�� �ִ�.
        All = North | East | South | West   // 0000 1111. ��� ���⿡ RoadTile�� �ִ�.
    }

    // �� ���� : true / false, ||, &&
    // ��Ʈ ���� : 1 / 0, |(�� �� �ϳ��� 1�̸� 1), &(�� �� 1�̾�� 1)

    /// <summary>
    /// Ÿ���� ��ġ�� �� �ֺ� Ÿ�� ��Ȳ�� ���� �ڵ����� ���õǾ� ������ ��������Ʈ
    /// </summary>
    public Sprite[] sprites;

    /// <summary>
    /// Ÿ���� �׷��� �� �ڵ����� ȣ���� �Ǵ� �Լ�
    /// (Ÿ���� Ÿ�ϸʿ� ��ġ�Ǹ� Ÿ�Ͽ��� ������ ��������Ʈ�� �׸��µ� �� �� �ڵ����� ȣ���)
    /// (���� ǥ���� ��������Ʈ�� �°� �ٽ� �׸���� ��ȣ�� ������ ����)
    /// </summary>
    /// <param name="position">Ÿ�ϸʿ��� Ÿ���� �׷����� ��ġ</param>
    /// <param name="tilemap">�� Ÿ���� �׷��� Ÿ�� ��</param>
    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        for (int y = -1; y < 2; y++)
        {
            for (int x = -1; x < 2; x++)
            {
                Vector3Int location = new(position.x + x, position.y + y, position.z);  // �ֺ� 8������ ��ġ

                if (HasThisTile(tilemap,position))  // �ֺ� Ÿ���� ���� ������ Ȯ��
                {
                    tilemap.RefreshTile(location);  // ���� ������ �����Ѵ�.
                }
            }
        }
    }

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        AdjTilePosition mask = AdjTilePosition.None;
        // mask�� �ֺ� Ÿ���� ��Ȳ�� ����ϱ�
        // ex) ���ʿ� RoadTile�� ������ mask���� AdjTilePosition.North�� ���� �Ѵ�.
        // �ϵ��ʿ� RoadTile�� ������ mask���� (AdjTilePosition.North | AdjTilePosition.East)�� ���� �Ѵ�.

        //if(HasThisTile(tilemap, position + new Vector3Int(0,1,0)))
        //{
        //    //mask = mask | AdjTilePosition.North;
        //    mask |= AdjTilePosition.North;
        //}

        mask |= HasThisTile(tilemap, position + new Vector3Int(0, 1, 0)) ? AdjTilePosition.North : 0;
        mask |= HasThisTile(tilemap, position + new Vector3Int(1, 0, 0)) ? AdjTilePosition.East : 0;
        mask |= HasThisTile(tilemap, position + new Vector3Int(0, -1, 0)) ? AdjTilePosition.South : 0;
        mask |= HasThisTile(tilemap, position + new Vector3Int(-1, 0, 0)) ? AdjTilePosition.West : 0;

        // mask���� ���� � ��������Ʈ�� ������ ������ ����
        int index = GetIndex(mask);
        if (index > -1)
        {
            tileData.sprite = sprites[index];               // index��°�� ��������Ʈ�� ����
            tileData.color = Color.white;                   // ������ �⺻ ���
            Matrix4x4 m = tileData.transform;               // transform ��Ʈ���� �޾ƿͼ�
            // mask ���� ���� �󸶸�ŭ ȸ�� ��ų �������� ����
            m.SetTRS(Vector3.zero, GetRotation(mask), Vector3.one); // ����� ȸ����� ��Ʈ���� ����

            tileData.transform = m;                         // ��Ʈ���� ������ ������ ����
            tileData.flags = TileFlags.LockTransform;       // �ٸ� Ÿ���� ȸ���� ���� ���ϵ���
            tileData.colliderType = ColliderType.None;      // ���̴ϱ� �ö��̴� ����

        }
        else
        {
            Debug.Log("���� : �߸��� �ε���");
        }

    }

    /// <summary>
    /// mask ������ ���� � ��������Ʈ�� ����� �������� �����ؼ� �����ִ� �Լ�
    /// </summary>
    /// <param name="mask">�ֺ� Ÿ�� ��Ȳ Ȯ�ο� ����ũ</param>
    /// <returns>�׷��� ��������Ʈ�� �ε���</returns>
    int GetIndex(AdjTilePosition mask)
    {
        int index = -1;
        switch(mask)
        {
            case AdjTilePosition.None:
                index = 0;
                break;
            case AdjTilePosition.South | AdjTilePosition.West:
            case AdjTilePosition.West | AdjTilePosition.North:
            case AdjTilePosition.North | AdjTilePosition.East:
            case AdjTilePosition.East | AdjTilePosition.South:
                index = 1;  // ���� ����� ��������Ʈ
                break;
            case AdjTilePosition.North:
            case AdjTilePosition.East:
            case AdjTilePosition.South:
            case AdjTilePosition.West:
            case AdjTilePosition.North | AdjTilePosition.South:
            case AdjTilePosition.East | AdjTilePosition.West:
                index = 2;  // |�� ����� ��������Ʈ
                break;

            case AdjTilePosition.All & ~AdjTilePosition.North:  // 0000 1111 & 1111 1110 = 0000 1110
            case AdjTilePosition.All & ~AdjTilePosition.East:
            case AdjTilePosition.All & ~AdjTilePosition.South:
            case AdjTilePosition.All & ~AdjTilePosition.West:
                index = 3;  // ���� ����� ��������Ʈ
                break;
            case AdjTilePosition.All:
                index = 4;
                break;
        }
        return index;
    }

    Quaternion GetRotation(AdjTilePosition mask)
    {
        Quaternion rotate = Quaternion.identity;
        switch(mask)
        {
            case AdjTilePosition.North | AdjTilePosition.West:  // ���� ������
            case AdjTilePosition.East:                          // |�� ������
            case AdjTilePosition.West:
            case AdjTilePosition.East | AdjTilePosition.West:
            case AdjTilePosition.All & ~AdjTilePosition.West:   // ���� ������
                rotate = Quaternion.Euler(0, 0, -90);
                break;
            case AdjTilePosition.North | AdjTilePosition.East:  // ���� ������
            case AdjTilePosition.All & ~AdjTilePosition.North:  // ���� ������
                rotate = Quaternion.Euler(0, 0, -180);
                break;
            case AdjTilePosition.East | AdjTilePosition.South:  // ���� ������
            case AdjTilePosition.All & ~AdjTilePosition.East:   // ���� ������
                rotate = Quaternion.Euler(0, 0, -270);
                break;
        }

        return rotate;
    }


    /// <summary>
    /// Ÿ�ϸʿ��� ������ ��ġ�� ���� ������ Ÿ������ Ȯ��
    /// </summary>
    /// <param name="tilemap">Ȯ���� Ÿ�ϸ�</param>
    /// <param name="position">Ȯ���� ��ġ</param>
    /// <returns>true�� ���� ������ Ÿ���̴�. false�� �ٸ� ������ Ÿ���̴�.</returns>
    bool HasThisTile(ITilemap tilemap, Vector3Int position)
    {
        // Ÿ�ϸʿ��� Ÿ���� ������ �� ���� ������ Ȯ��
        return tilemap.GetTile(position) == this;
    }

#if UNITY_EDITOR
    [MenuItem("Assets/Create/2D/Tiles/RoadTile")]
    public static void CreateRoadTile()
    {
        string path = EditorUtility.SaveFilePanelInProject( // ���� ����� �� ����
            "Save Road Tile",   // ����
            "New Road Tile",    // ������ �⺻ �̸�
            "Asset",            // ������ Ȯ����
            "Save Road Tile",   // ��¿� �޼���
            "Assets");          // �⺻���� ������ ����

        if (path != "")
        {
            AssetDatabase.CreateAsset(CreateInstance<RoadTile>(), path);    // ������ ������ ��ο� RoadTile ������ �ϳ� ����
        }
    }
#endif
}
