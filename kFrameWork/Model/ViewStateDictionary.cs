using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

namespace kFrameWork.Model
{
    public class ViewStateDictionary<TKey, TValue>
    {
        StateBag ViewState;
        string PropertyName;
        Dictionary<TKey, TValue> Dic
        {
            get
            {
                if (ViewState[PropertyName] == null)
                    return new Dictionary<TKey, TValue>();
                return ViewState[this.PropertyName] as Dictionary<TKey, TValue>;
            }
            set { ViewState[this.PropertyName] = value; }
        }


        public ViewStateDictionary(StateBag viewState, string propertyName)
        {
            this.ViewState = viewState;
            this.PropertyName = propertyName;
        }

        public TValue GetItem(TKey key)
        {
            return Dic[key];
        }

        public void SetItem(TKey key, TValue value)
        {
            Dictionary<TKey, TValue> Temp = Dic;
            Temp[key] = value;
            Dic = Temp;
        }

        public bool ContainsKey(TKey key)
        {
            return Dic.ContainsKey(key);
        }

        public void AddItem(TKey key, TValue value)
        {
            Dictionary<TKey, TValue> Temp = Dic;
            Temp.Add(key, value);
            Dic = Temp;
        }

        public void RemoveItem(TKey key)
        {
            Dictionary<TKey, TValue> Temp = Dic;
            Temp.Remove(key);
            Dic = Temp;
        }

        public void Clear()
        {
            Dic = new Dictionary<TKey, TValue>();
        }

        public int Count
        {
            get
            {
                return Dic.Count;
            }
        }

        public Dictionary<TKey, TValue> Dictionary
        {
            get
            {
                return Dic;
            }
        }
    }
}
