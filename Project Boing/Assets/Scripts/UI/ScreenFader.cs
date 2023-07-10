using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{

    [SerializeField] private Image _fader;
    private bool _isBusy;

    public void FadeToBlack(float duration, Action finishedCallBack)
    {
        if(_isBusy) return;
        StartCoroutine(CO_FadeToBlack(duration, finishedCallBack));
    }

    public void FadeFromBlack(float duration, Action finishedCallBack)
    {
        if(_isBusy) return;
        StartCoroutine(CO_FadeFromBlack(duration, finishedCallBack));
    }

    private IEnumerator CO_FadeToBlack(float duration, Action finishedCallBack)
    {
        _isBusy = true;

        while (_fader.color.a < 1)
        {
           _fader.color = new Color(0, 0, 0, _fader.color.a + (Time.deltaTime / duration));
           yield return null; 
        }
        _fader.color = new Color(0, 0, 0, 1);
        _isBusy = false;
        finishedCallBack?.Invoke();
        yield return null;
    }

    private IEnumerator CO_FadeFromBlack(float duration, Action finishedCallBack)
    {
        _isBusy = true;

        while (_fader.color.a > 0)
        {
           _fader.color = new Color(0, 0, 0, _fader.color.a - (Time.deltaTime / duration));
           yield return null; 
        }
        _fader.color = new Color(0, 0, 0, 0);
        _isBusy = false;
        finishedCallBack?.Invoke();
        yield return null;
    }
}
