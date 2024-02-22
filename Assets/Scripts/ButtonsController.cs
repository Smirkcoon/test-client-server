using System;
using UnityEngine;
using UnityEngine.UI;
using HttpRequest;
using ClientServer;
using ClientServer.WWWResponse;
using TMPro;
using UnityEngine.Serialization;

public class ButtonsController : MonoBehaviour
{
    public HttpManager httpManager;
    public TMP_Text responseText;
    public PopupInputId popupInputId;
    private Action resultButtons;
    public ScrollViewController scrollViewController;
    public void OnClickCreate()
    {
        popupInputId.ClosePopup();
        httpManager.SendPacket<GetListViewItems>(ePacketType.POST, res =>
        {
            scrollViewController.AddItemWithData(res.Items);
        });
    }

    public void OnClickDelete()
    {
        popupInputId.OpenPopup();
        httpManager.SendPacket<GetListViewItems>(ePacketType.DELETE, res => {  });
        if(!string.IsNullOrEmpty(popupInputId.GetTextInputField()))
            scrollViewController.RemoveButtonById(GetIntInputField());
    }

    public void OnClickUpdate()
    {
        if (popupInputId.Opened)
        {
            httpManager.SendPacket<GetListViewItems>(ePacketType.PUT, res =>
                {
                    Debug.Log("PUT " + JsonUtility.ToJson(res, true));
                },
                popupInputId.GetTextInputField());
        }
        popupInputId.OpenPopup();
    }

    public void OnClickRefresh()
    {
        if (popupInputId.Opened)
        {
            httpManager.SendPacket<GetListViewItems>(ePacketType.GET,
                res =>
                {
                    scrollViewController.AddItemWithData(res.Items);
                    Debug.Log("Refresh " + JsonUtility.ToJson(res, true));
                }, popupInputId.GetTextInputField());
        }
        popupInputId.OpenPopup();
    }

    private void SetTextStatus(ListViewItem[] buttons)
    {
        responseText.text = buttons.Length.ToString();
    }

    private int GetIntInputField()
    {
        int i = -1;
        int.TryParse(popupInputId.GetTextInputField(), out i);
        return i;
    }
}
