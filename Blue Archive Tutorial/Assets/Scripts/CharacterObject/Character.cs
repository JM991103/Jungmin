using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// menuName = "CharacterObject" 우클릭 했을때 메뉴 이름
// fileName = "Character" 파일을 생성 했을때 오브젝트의 이름
[CreateAssetMenu(menuName = "CharacterObject", fileName = "Character", order = 0)]
public class Character : ScriptableObject
{
    public string charName;
    public string department;

    public Color grayColor;
    public Color whiteColor;

    public Sprite[] faceSprite;
}
