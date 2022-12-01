using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    /// <summary>
    /// 생성할 프리팹
    /// </summary>
    public GameObject cellPrefab;

    /// <summary>
    /// 보드가 가지는 가로 셀의 길이 (가로 줄의 셀 갯수)
    /// </summary>
    private int width = 16;

    /// <summary>
    /// 보드가 가지는 세로 셀의 길이 (세로 줄의 셀 갯수)
    /// </summary>
    private int height = 16;

    /// <summary>
    /// 셀 한 변의 길이 (셀은 정사격형)
    /// </summary>
    const float Distance = 1.0f;    // 1일 때 카메라 크기 9

    /// <summary>
    /// 이 보드가 가지는 모든 셀
    /// </summary>
    Cell[] cells;

    //private void Start()
    //{
    //    Initialize(width, height, 10);
    //}

    /// <summary>
    /// 이 보드가 가질 모든 셀을 생성하고 배치하는 함수 
    /// </summary>
    public void Initialize(int newWidth, int newHeight, int mineCount)
    {
        ClearCells();

        width = newWidth;
        height = newHeight;

        Vector3 basePos = transform.position;      // 기준 위치 설정 (보드의 위치)

        // 보드의 피봇을 중심으로 셀이 생성되게 하기 위해 설이 생성될 시작점 계산 용도로 구하기
        Vector3 offset = new(-(width - 1) * Distance * 0.5f, (height - 1) * Distance * 0.5f);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                GameObject cellObj = Instantiate(cellPrefab, transform);    // 이 보드를 부모로 삼고 프리팹을 생성한다.
                Cell cell = cellObj.GetComponent<Cell>();                   // 생성한 오브젝트에서 Cell 컴포넌트 찾기
                cell.name = $"Cell_{cell.ID}_{x}_{y}";                      // 오브젝트 이름 지정
                cell.transform.position = basePos + offset + new Vector3(x * Distance, -y * Distance);  // 적절한 위치에 배치
                cells[cell.ID] = cell;                                      // cells 배열에 저장
            }
        }        
    }

    /// <summary>
    /// 보드의 모든 셀을 제거하는 함수
    /// </summary>
    public void ClearCells()
    {
        if (cells != null)      // 기존에 만들어진 셀이 있으면  
        {
            // 이미 만들어진 셀 오브젝트를 모두 삭제하기
            foreach (var cell in cells)
            {
                Destroy(cell.gameObject);
            }
            cells = null;   // 안의 내용을 다 제거했다고 표시
        }
    }
}
