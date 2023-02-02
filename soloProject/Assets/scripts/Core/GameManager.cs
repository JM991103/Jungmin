using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    Player player;
    TalkManager talkManager;
    Canvas canvas;

    public Player Player => player;

    protected override void Initialize()
    {
        base.Initialize();
        player = FindObjectOfType<Player>();
        talkManager = FindObjectOfType<TalkManager>();
        canvas = FindObjectOfType<Canvas>();

        OnScenLoad();
    }

    protected override void ManagerDataReset()
    {
        base.ManagerDataReset();

    }

    void OnScenLoad()
    {
        DontDestroyOnLoad(Player);
        DontDestroyOnLoad(canvas);
        DontDestroyOnLoad(talkManager);
    }

}
