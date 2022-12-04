using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_Cell : TestBase
{
    protected override void Test1(InputAction.CallbackContext _)
    {
        Board board = GameManager.Inst.Board;
        board.Initialize(GameManager.Inst.boardWidth, GameManager.Inst.boardHeight, GameManager.Inst.mineCount);
    }

    protected override void Test2(InputAction.CallbackContext _)
    {
        int[] array = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        Board board = GameManager.Inst.Board;
        board.Shuffl1e(array);

        string output = "Array : ";
        foreach (var num in array)
        {
            output += $"{num}, ";
        }
        output += "��";
        Debug.Log(output);
    }

    protected override void Test3(InputAction.CallbackContext _)
    {
        float startTime = Time.realtimeSinceStartup;    // ���������� �ð��� üũ�ϴ� �Լ�

        Board board = GameManager.Inst.Board;   
        int[,] result = new int[10,10];                 // ��� ����� ���� �迭

        for (int i = 0; i < 1000000; i++)               // 100���� ����
        {
            int[] array = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }; // ���� �迭
            board.Shuffl1e(array);                          // Shuffle ����

            for (int j = 0; j < array.Length; j++)          // ���� ��� �����ϱ�
            {
                // �ش� �Ǵ� ��ġ�� 1����. �� : array[j], �� : j
                result[array[j], j]++;
            }
        }
        string output = "";                                 // ���� ��� ����ϱ�
        for (int y = 0; y < 10; y++)
        {
            output += $"���� {y} :";
            for (int x = 0; x < 10; x++)
            {
                output += $"{result[y,x]} ";
            }
            output += " ";
        }
        Debug.Log(output);
        Debug.Log($"��� �ð� : {Time.realtimeSinceStartupAsDouble - startTime}");
    }
}
