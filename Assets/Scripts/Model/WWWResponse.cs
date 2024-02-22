using System;
using System.Collections.Generic;
using UnityEngine;

namespace ClientServer.WWWResponse
{
    [Serializable]
    public class GetListViewItems
    {
        [SerializeField]
        public ListViewModel[] Items = null;
    }
}