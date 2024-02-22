using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClientServer
{
    [Serializable]
    public class ListViewModel
    {
        public int id;

        public float[] color;

        public bool animationType;

        public string name;

        public override string ToString()
        {
            return JsonUtility.ToJson(this, true);
        }
    }
}
