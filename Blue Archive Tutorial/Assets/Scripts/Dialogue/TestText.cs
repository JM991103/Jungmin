using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestText : MonoBehaviour
{
    TextMeshProUGUI cName;
    TextMeshProUGUI cDepartment;
    TextMeshProUGUI dialogueText;

    TalkTypeEffect talk;

    Image Aris;
    Image Yuzu;


    private void Awake()
    {
        Transform trans = transform.GetChild(0);
        cName = trans.GetChild(0).GetComponent<TextMeshProUGUI>();
        cDepartment = trans.GetChild(1).GetComponent<TextMeshProUGUI>();
        dialogueText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        talk = transform.GetChild(1).GetComponent<TalkTypeEffect>();

        Aris = transform.parent.GetChild(1).GetComponent<Image>();
        Yuzu = transform.parent.GetChild(2).GetComponent<Image>();
    }

    int num = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (GameManager.Inst.DialogueDB.Story1[num].name == "아리스")
            {
                Aris.color = GameManager.Inst.TotalCharacter.characters[1].whiteColor;
                Aris.sprite = GameManager.Inst.TotalCharacter.characters[1].faceSprite[GameManager.Inst.DialogueDB.Story1[num].expression];
                Yuzu.color = GameManager.Inst.TotalCharacter.characters[0].grayColor;
                //Aris.color =  GameManager.Inst.Aris.whiteColor;                
                //Yuzu.color = GameManager.Inst.Yuzu.grayColor;
            }
            else if (GameManager.Inst.DialogueDB.Story1[num].name == "유즈")
            {
                Aris.color = GameManager.Inst.TotalCharacter.characters[1].grayColor;
                Yuzu.sprite = GameManager.Inst.TotalCharacter.characters[0].faceSprite[GameManager.Inst.DialogueDB.Story1[num].expression];
                Yuzu.color = GameManager.Inst.TotalCharacter.characters[0].whiteColor;
                //Aris.color = GameManager.Inst.Aris.grayColor;
                //Yuzu.color = GameManager.Inst.Yuzu.whiteColor;
            }

            cName.text = GameManager.Inst.DialogueDB.Story1[num].name;
            cDepartment.text = GameManager.Inst.DialogueDB.Story1[num].department;
            talk.SetMsg(dialogueText.text = GameManager.Inst.DialogueDB.Story1[num].dialog);

            //Aris.sprite = GameManager.Inst.Character.faceSprite[16];
            num++;
        }
        else
        {
            
        }
    }

}
