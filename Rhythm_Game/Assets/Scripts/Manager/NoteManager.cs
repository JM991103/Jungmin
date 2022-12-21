using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public int bpm = 0;
    double currentTime = 0d;

    [SerializeField]
    Transform noteAppear = null;
    [SerializeField]
    GameObject goNote = null;


    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= 60 / bpm)
        {
            GameObject note = Instantiate(goNote, noteAppear.position, Quaternion.identity);
            note.transform.SetParent(this.transform);
            currentTime = 60d / bpm;
        }
    }
}
