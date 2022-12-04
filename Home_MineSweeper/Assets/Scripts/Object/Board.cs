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

    /// <summary>
    /// 열린 셀에서 표시될 이미지
    /// </summary>
    public Sprite[] openCellImages;

    /// <summary>
    /// OpenCellType으로 이미지를 받아오는 인덱서 (enum)
    /// </summary>
    /// <param name="type">필요한 이미지의 enum타입</param>
    /// <returns>enum타입에 맞는 이미지</returns>
    public Sprite this[OpenCellType type] => openCellImages[(int)type];


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
                cell.ID = y * width + x;                                    // ID 설정 (ID를 통해 위치도 확인 가능)
                cell.Board = this;                                          // 보드 설정
                cell.name = $"Cell_{cell.ID}_{x}_{y}";                      // 오브젝트 이름 지정
                cell.transform.position = basePos + offset + new Vector3(x * Distance, -y * Distance);  // 적절한 위치에 배치
                cells[cell.ID] = cell;                                      // cells 배열에 저장
            }
        }

        // 만들어진 셀에 지뢰를 minCount만큼 설치하기
        int[] ids = new int[cells.Length];
        for (int i = 0; i < cells.Length; i++)
        {
            ids[i] = i;
        }
        Shuffl1e(ids);
        for (int i = 0; i < mineCount; i++)
        {
            cells[ids[i]].SetMine();
        }        
    }

    /// <summary>
    /// 파라메터로 받은 배열 내부의 데이터 순서를 섞는 함수
    /// </summary>
    /// <param name="source">내부 데이터를 섞을 배열</param>
    public void Shuffl1e(int[] source)
    {
        // 피셔 예이츠 알고리즘 적용 하기
        // - 1을 하는 이유 : 배열은 0부터 시작하고 Length는 1부터 숫자를 세기 때문
        int count = source.Length - 1;

        for (int i = 0; i < count; i++) // 배열의 길이 - 1만큼 for문을 돌린다.
        {
            // 배열이 5일때 4개중에 3개중에 2개중에 순차적으로 랜덤으로 한개를 뽑아야 하는데
            // 0 부터 4 + 1 - i를 해서 랜덤으로 숫자를 추출한다.
            int randomIndex = Random.Range(0, count + 1 - i);
            // 마지막인덱스는 5번째 4번째 3번째 순차적으로 줄어 들어야 한다.
            int lastIndex = count - i;
            (source[randomIndex], source[lastIndex]) = (source[lastIndex], source[randomIndex]);    // swap 처리
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
