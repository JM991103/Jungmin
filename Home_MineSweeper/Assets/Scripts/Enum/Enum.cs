public enum OpenCellType
{
    Empty = 0,      // �ֺ��� ���ڰ� �ִ�.
    Number1,        // �ֺ��� ���ڰ� 1�� �ִ�.
    Number2,
    Number3,
    Number4,
    Number5,
    Number6,
    Number7,
    Number8,
    Mine_NotFound,  // �� ã�� ����
    Mine_Explosion, // ���� ����
    Mine_Mistake    // ���ڰ� �ƴѵ� ���ڶ�� ǥ���� ���
}

public enum CloseCellType
{
    Close = 0,
    Close_Press,
    Question,
    Question_Press,
    Flag
}
