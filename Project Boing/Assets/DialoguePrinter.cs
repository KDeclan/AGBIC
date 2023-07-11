using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialoguePrinter : MonoBehaviour
{
    public static DialoguePrinter Instance {get; private set; }

    [SerializeField] private TMP_Text _dialogueTextMesh;
    [SerializeField] private GameObject _dialogueTextPanel;

    public void PrintDialogueLine(string lineToPrint, float charSpeed, Action finishedCallback)
    {
       StartCoroutine(CO_PrintDialogueLine(lineToPrint, charSpeed, finishedCallback));
    }
    
    private IEnumerator CO_PrintDialogueLine(string lineToPrint, float charSpeed, Action finishedCallback)
    {
        _dialogueTextMesh.SetText(string.Empty);

        for (int i = 0; i< lineToPrint.Length; i++)
        {
            var character = lineToPrint[i];
            _dialogueTextMesh.SetText(_dialogueTextMesh.text + character);

            yield return new WaitForSeconds(charSpeed);
        }

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

        _dialogueTextMesh.SetText(string.Empty);

        finishedCallback?.Invoke();
        EventBus.Instance.ResumeGameplay();

        yield return null;
    }

    private void Awake() 
    {
        Instance = this;
    }
}
