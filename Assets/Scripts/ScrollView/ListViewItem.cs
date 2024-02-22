using System;
using System.Collections;
using System.Collections.Generic;
using ClientServer;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ListViewItem : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private Button btn;
    [SerializeField] private Image image;
    public ListViewModel data;

    private void Start()
    {
        btn.onClick.AddListener(SetRandomColor);
    }

    public void Setup(ListViewModel data, float waitTime = 0)
    {
        this.data = data;
        gameObject.name = this.data.id.ToString();
        text.text = this.data.id.ToString();
        float[] color = this.data.color;

        float h = color[0] > 1 ? color[0]/360 : color[0];//fix random in mock
        
        image.color = Color.HSVToRGB(h,color[1], color[2]);
    }
    private void SetRandomColor()
    {
        Color newColor = UnityEngine.Random.ColorHSV();
        image.color = newColor;
        Color.RGBToHSV(newColor,out float h, out float s, out float l);
        this.data.color = new float[]{h*360, s, l};
    }
}
