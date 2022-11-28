using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템을 생성만 하는 클래스. 팩토리 디자인 패턴
/// 개발을 하다보면 어디서 부터 어디까지 아이템을 생성하는지 모르기 때문에
/// 생성하는 부분을 한곳에 다 모아 놓기위해 만들어 놓음
/// </summary>
public class ItemFactory
{ 
    // 함수의 구성요소 : 이름, 파라메터, 리턴 값, 바디(코드)
    // overloading(오버로딩) : 이름은 같은데 파라메터가 다른 함수를 만드는 것
    // overriding(오버라이딩) : 이름, 파라메터, 리턴값이 같은 함수를 만드는 것 

    static int itemCount = 0;   // 생성된 아이탬의 총 갯수. 아이템 생성 아이디의 역할도 함.

    //static : 정적 생성된 메모리 주소의 변화가 없다.new를 하지 않아도 접근이 가능해 진다.
    /// <summary>
    /// ItemIDCode로 아이템 생성
    /// </summary>
    /// <param name="code">생성할 아이템 코드</param>
    /// <returns>생성 결과</returns>
    public static GameObject MakeItem(ItemIDCode code)
    {
        GameObject obj = new GameObject();  // new를 하게 되면 빈 오브젝트가 생성된다.

        Item item = obj.AddComponent<Item>();           // Item 컴포넌트 추가하기
        item.data = GameManager.Inst.ItemData[code];

        string[] itemName = item.data.name.Split("_");      // 00_아이템   Split("_")를 하면 _를 기준으로 앞뒤로 분할 해준다.
        obj.name = $"{itemName[1]}_{itemCount++}";          // {itemName[1]} 까지만 하게 되면 중복된 이름이 나오기 때문에 갯수를 표시해주는 변수를 사용한다.
        obj.layer = LayerMask.NameToLayer("Item");          // 레이어를 Item으로 설정해 준다.

        CircleCollider2D cc = obj.AddComponent<CircleCollider2D>(); // 컬라이더 추가
        cc.isTrigger = true;
        cc.radius = 0.5f;

        return obj;
    }

    /// <summary>
    /// 아이템 코드를 이용해 특정 위치에 아이템을 생성하는 함수
    /// </summary>
    /// <param name="code">생성할 아이템 코드</param>
    /// <param name="position">생성할 위치</param>
    /// <param name="randomNoise">위치에 랜덤성을 더할지 여부, true면 약간의 랜덤성을 더한다. 기본값 = flase</param>
    /// <returns>생성된 아이템</returns>
    public static GameObject MakeItem(ItemIDCode code, Vector3 position, bool randomNoise = false)
    {
        GameObject obj = MakeItem(code);    // 만들고
        if (randomNoise)                    // 위치에 랜덤성을 더하면
        {
            // insideUnitCircle : 반지름 1짜리 원을 그리고 원 안에 랜덤 좌표값을 돌려준다.
            Vector2 noise = Random.insideUnitCircle * 0.5f; // 반지름 0.5인 원의 안쪽에서 랜덤한 위치 구함
            position.x = noise.x;           // 구한 랜덤한 위치를 파라메터로 받은 기존 위치에 추가
            position.y = noise.y;   
        }
        obj.transform.position = position;  // 위치지정
        return obj;
    }

    /// <summary>
    /// 아이템 코드를 이용해서 아이템을 한번에 여러개 생성하는 함수 
    /// </summary>
    /// <param name="code">생성할 아이템의 아이템 코드</param>
    /// <param name="count">생성할 갯수</param>
    /// <returns>생성된 아이템들이 담긴 배열</returns>
    public static GameObject[] MakeItems(ItemIDCode code, int count)
    {
        GameObject[] objs = new GameObject[count];  // 배열 만들고
        for (int i = 0; i < count; i++)     
        {
            objs[i] = MakeItem(code);               // count만큼 반복해서 아이템 생성
        }
        return objs;                                // 생성한 것 리턴
    }

    /// <summary>
    /// 코드를 이용해 특정 위치에 아이템을 한번에 여러개 생성하는 함수
    /// </summary>
    /// <param name="code">생성할 아이템의 아이템 코드</param>
    /// <param name="count">생성할 갯수</param>
    /// <param name="position">생성할 기준 위치</param>
    /// <param name="randomNoise">위치에 랜덤성을 더할지 여부. true면 약간의 랜덤성을 더한다. 기본값은 false</param>
    /// <returns></returns>
    public static GameObject[] MakeItems(ItemIDCode code, int count ,Vector3 position, bool randomNoise = false)
    {
        GameObject[] objs = new GameObject[count];  // 배열 만들고
        for (int i = 0; i < count; i++)
        {
            objs[i] = MakeItem(code, position, randomNoise);               // count만큼 반복해서 아이템 생성, 위치와 노이즈도 적용
        }
        return objs;                                // 생성한 것 리턴
    }


    /// <summary>
    /// 아이템 id로 아이템 생성
    /// </summary>
    /// <param name="id">생성할 아이템</param>
    /// <returns>생성한 아이템</returns>
    public static GameObject MakeItem(int id)
    {
        if (id < 0)
        {
            return null;
        }
        return MakeItem((ItemIDCode)id);
    }

    /// <summary>
    /// 아이템 아이디를 이용해 특정 위치에 아이템을 생성하는 함수
    /// </summary>
    /// <param name="id">생성할 아이템 아이디</param>
    /// <param name="position">생성할 위치</param>
    /// <returns>생성된 아이템</returns>
    public static GameObject MakeItem(int id, Vector3 position, bool randomNoise = false)
    {
        GameObject obj = MakeItem(id);      // 만들고
        if (randomNoise)                    // 위치에 랜덤성을 더하면
        {
            // insideUnitCircle : 반지름 1짜리 원을 그리고 원 안에 랜덤 좌표값을 돌려준다.
            Vector2 noise = Random.insideUnitCircle * 0.5f; // 반지름 0.5인 원의 안쪽에서 랜덤한 위치 구함
            position.x = noise.x;           // 구한 랜덤한 위치를 파라메터로 받은 기존 위치에 추가
            position.y = noise.y;
        }
        obj.transform.position = position;  // 위치지정
        return obj;
    }

    /// <summary>
    /// 아이템 코드를 이용해서 아이템을 한번에 여러개 생성하는 함수 
    /// </summary>
    /// <param name="id">생성할 아이템의 아이템 아이디</param>
    /// <param name="count">생성할 갯수</param>
    /// <returns>생성된 아이템들이 담긴 배열</returns>
    public static GameObject[] MakeItems(int id, int count)
    {
        GameObject[] objs = new GameObject[count];  // 배열 만들고
        for (int i = 0; i < count; i++)
        {
            objs[i] = MakeItem(id);               // count만큼 반복해서 아이템 생성
        }
        return objs;                                // 생성한 것 리턴
    }

    /// <summary>
    /// 코드를 이용해 특정 위치에 아이템을 한번에 여러개 생성하는 함수
    /// </summary>
    /// <param name="id">생성할 아이템의 아이템 아이디</param>
    /// <param name="count">생성할 갯수</param>
    /// <param name="position">생성할 기준 위치</param>
    /// <param name="randomNoise">위치에 랜덤성을 더할지 여부. true면 약간의 랜덤성을 더한다. 기본값은 false</param>
    /// <returns></returns>
    public static GameObject[] MakeItems(int id, int count, Vector3 position, bool randomNoise = false)
    {
        GameObject[] objs = new GameObject[count];  // 배열 만들고
        for (int i = 0; i < count; i++)
        {
            objs[i] = MakeItem(id, position, randomNoise);               // count만큼 반복해서 아이템 생성, 위치와 노이즈도 적용
        }
        return objs;                                // 생성한 것 리턴
    }

}
