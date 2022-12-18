using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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
}
