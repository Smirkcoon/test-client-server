using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ClientServer;
using DG.Tweening;
using UnityEngine;

public class ScrollViewController : MonoBehaviour
{
    public Dictionary<int,ListViewItem> Items = new Dictionary<int, ListViewItem>();

    [SerializeField] private Transform content;
    [SerializeField] private ListViewItem prefab;

    public ListViewItem[] GetList()
    {
        return Items.Values.ToArray();
    }

    public void AddItemWithData(ListViewModel[] itemsModels)
    {
        for (int i = 0; i < itemsModels.Length; i++)
        {
            int x = i;
            if (Items.Keys.Contains(itemsModels[i].id))
                continue;

            Sequence s = DOTween.Sequence();
            s.AppendInterval(i* 1.0f/10);
            s.OnComplete(() =>
            {
                var newItem = Instantiate(prefab, content);
                Items.Add(itemsModels[x].id, newItem);
                newItem.Setup(itemsModels[x]);
                newItem.transform.DOScale(1, 0.2f);
            });
        }
    }

    public void RemoveButtonById(int id)
    {
        if (id >= 0 && Items.Keys.Contains(id))
        {
            Destroy(Items[id].gameObject);
            Items.Remove(id);
        }
        else
            Debug.LogError("Remove Button Not Correct id");
    }
}
