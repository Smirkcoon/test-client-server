using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ClientServer;
using UnityEngine;

public class ScroollViewController : MonoBehaviour
{
    public Dictionary<int,ListViewItem> Items = new Dictionary<int, ListViewItem>();

    [SerializeField] private Transform content;
    [SerializeField] private ListViewItem prefab;

    public ListViewItem[] GetList()
    {
        return Items.Values.ToArray();
    }
    
    public void AddNewItem()
    {
        var newItem = Instantiate(prefab, content);

        for (int i = 0; i < Items.Keys.Count; i++)
        {
            if (!Items.Keys.Contains(i))
            {
                Items.Add(i, newItem);
                newItem.SetupNewItem(i);
                return;
            }
        }

        int id = -1;
        if (Items.Count > 0)
            id = Items.Keys.Max() + 1;
        else
            id = 0;
        
        Items.Add(id, newItem);
        newItem.SetupNewItem(id);
    }
    
    public void AddItemWithData(ListViewModel[] itemsModels)
    {
        for (int i = 0; i < itemsModels.Length; i++)
        {
            var newItem = Instantiate(prefab, content);
            Items.Add(itemsModels[i].id, newItem);
            newItem.Setup(itemsModels[i],i/10);
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
