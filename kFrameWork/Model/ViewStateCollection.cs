using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

namespace kFrameWork.Model
{
    public class ViewStateCollection<TValue>
    {
        StateBag ViewState;
        string PropertyName;
        List<TValue> Collection
        {
            get
            {
                if (ViewState[PropertyName] == null)
                    return new List<TValue>();
                return ViewState[this.PropertyName] as List<TValue>;
            }
            set { ViewState[this.PropertyName] = value; }
        }

        public ViewStateCollection(StateBag viewState, string propertyName)
        {
            this.ViewState = viewState;
            this.PropertyName = propertyName;
        }

        public TValue GetItem(int index)
        {
            return Collection[index];
        }

        public void SetItem(int index, TValue value)
        {
            List<TValue> Temp = Collection;
            Temp[index] = value;
            Collection = Temp;
        }

        public bool Contains(TValue value)
        {
            return Collection.Contains(value);
        }

        public void AddItem(TValue value)
        {
            List<TValue> Temp = Collection;
            Temp.Add(value);
            Collection = Temp;
        }

        public void Insert(int index, TValue value)
        {
            List<TValue> Temp = Collection;
            Temp.Insert(index, value);
            Collection = Temp;
        }

        public void RemoveItem(TValue value)
        {
            List<TValue> Temp = Collection;
            Temp.Remove(value);
            Collection = Temp;
        }

        public void RemoveAt(int index)
        {
            List<TValue> Temp = Collection;
            Temp.RemoveAt(index);
            Collection = Temp;
        }

        public void Clear()
        {
            Collection = new System.Collections.Generic.List<TValue>();
        }

        public int Count
        {
            get
            {
                return Collection.Count;
            }
        }

        public List<TValue> List
        {
            get
            {
                return Collection;
            }
        }
    }
}
