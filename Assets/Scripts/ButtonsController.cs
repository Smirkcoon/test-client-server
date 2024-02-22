using System;
using UnityEngine;
using UnityEngine.UI;
using HttpRequest;
using ClientServer;
using ClientServer.WWWResponse;
using TMPro;

public class ButtonsController : MonoBehaviour
{
    public HttpManager httpManager;
    public TMP_Text responseText;
    public PopupInputId popupInputId;
    private Action resultButtons;
    public ScroollViewController ScroollViewController;
    public void OnClickCreate()
    {
        ScroollViewController.AddNewItem();
        /*httpManager.SendPacket<GetListViewItems>(ePacketType.POST, res =>
        {
        }, inputFieldId.text);*/
    }

    public void OnClickDelete()
    {
        popupInputId.OpenPopup();
        httpManager.SendPacket<GetListViewItems>(ePacketType.DELETE, res => {  });
        if(!string.IsNullOrEmpty(popupInputId.GetTextInputField()))
            ScroollViewController.RemoveButtonById(GetIntInputField());
    }

    public void OnClickUpdate()
    {
        popupInputId.OpenPopup();
        //httpManager.SendPacket<GetListViewItems>(ePacketType.PUT, res => {  });
    }

    public void OnClickRefresh()
    {
        if (popupInputId.Opened)
        {
            httpManager.SendPacket<GetListViewItems>(ePacketType.GET,
                res => { ScroollViewController.AddItemWithData(res.Items); }, popupInputId.GetTextInputField());
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
