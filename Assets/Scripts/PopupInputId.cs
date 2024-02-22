using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupInputId : MonoBehaviour
{
    public TMP_InputField inputFieldId;
    private Sequence sequence;
    [HideInInspector]
    public bool Opened;

    private void Start()
    {
        Opened = false;
        transform.DOScale(0, 0);
    }

    public void OpenPopup()
    {
        if (!Opened)
        {
            sequence?.Kill();
            sequence = DOTween.Sequence();
            sequence.Append(transform.DOScale(1, 0.3f));
            Opened = true;
        }
    }
    public void ClosePopup()
    {
        if (Opened)
        {
            inputFieldId.text = "";
            sequence?.Kill();
            sequence = DOTween.Sequence();
            sequence.Append(transform.DOScale(0, 0.3f));
            Opened = false;
        }
    }

    public string GetTextInputField()
    {
        return inputFieldId.text;
    }
}
