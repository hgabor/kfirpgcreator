using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace KFIRPG.editor {

	/// <summary>
	/// Wraps an IDictionary in an IList interface.
	/// </summary>
	/// <typeparam name="TKey">The type of keys.</typeparam>
	/// <typeparam name="TValue">The type of values.</typeparam>
	class ListDictionaryAdapter<TKey, TValue>: IBindingList, IList<KeyValuePair<TKey, TValue>>, IDictionary<TKey, TValue> {
		private List<KeyValuePair<TKey, TValue>> list;
		private IDictionary<TKey, TValue> dictionary;

		public ListDictionaryAdapter() {
			this.dictionary = new Dictionary<TKey, TValue>();
			this.list = new List<KeyValuePair<TKey, TValue>>();
		}

		public ListDictionaryAdapter(IDictionary<TKey, TValue> dictionary) {
			this.dictionary = dictionary;
			list = new List<KeyValuePair<TKey, TValue>>(dictionary);
		}

		#region IList Members

		public int Add(object value) {
			dictionary.Add((KeyValuePair<TKey, TValue>)value);
			list.Add((KeyValuePair<TKey, TValue>)value);
			int index = list.IndexOf((KeyValuePair<TKey, TValue>)value);
			OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, index));
			return index;
		}

		public void Clear() {
			dictionary.Clear();
			list.Clear();
			OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
		}

		public bool Contains(object value) {
			return dictionary.Contains((KeyValuePair<TKey, TValue>)value);
		}

		public int IndexOf(object value) {
			return list.IndexOf((KeyValuePair<TKey, TValue>)value);
		}

		public void Insert(int index, object value) {
			dictionary.Add((KeyValuePair<TKey, TValue>)value);
			list.Insert(index, (KeyValuePair<TKey, TValue>)value);
			OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, index));
		}

		public bool IsFixedSize {
			get { return false; }
		}

		public bool IsReadOnly {
			get { return dictionary.IsReadOnly; }
		}

		public void Remove(object value) {
			int index = list.IndexOf((KeyValuePair<TKey, TValue>)value);
			dictionary.Remove((KeyValuePair<TKey, TValue>)value);
			list.Remove((KeyValuePair<TKey, TValue>)value);
			OnListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, index));
		}

		public void RemoveAt(int index) {
			KeyValuePair<TKey, TValue> item = list[index];
			dictionary.Remove(item);
			list.RemoveAt(index);
			OnListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, index));
		}

		public object this[int index] {
			get {
				return list[index];
			}
			set {
				KeyValuePair<TKey, TValue> item = list[index];
				dictionary.Remove(item);
				dictionary.Add((KeyValuePair<TKey, TValue>)value);
				list[index] = (KeyValuePair<TKey, TValue>)value;
				OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, index));
			}
		}

		#endregion

		#region ICollection Members

		public void CopyTo(Array array, int index) {
			Array.Copy(list.ToArray(), 0, array, index, list.Count);
		}

		public int Count {
			get { return dictionary.Count; }
		}

		public bool IsSynchronized {
			get { return false; }
		}

		public object SyncRoot {
			get { return this; }
		}

		#endregion

		#region IEnumerable Members

		public IEnumerator GetEnumerator() {
			return list.GetEnumerator();
		}

		#endregion


		#region IList<KeyValuePair<TKey,TValue>> Members

		public int IndexOf(KeyValuePair<TKey, TValue> item) {
			return list.IndexOf(item);
		}

		public void Insert(int index, KeyValuePair<TKey, TValue> item) {
			dictionary.Add(item);
			list.Insert(index, item);
			OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, index));
		}

		KeyValuePair<TKey, TValue> IList<KeyValuePair<TKey, TValue>>.this[int index] {
			get {
				return list[index];
			}
			set {
				KeyValuePair<TKey, TValue> item = list[index];
				dictionary.Remove(item);
				dictionary.Add(value);
				list[index] = value;
				OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, index));
			}
		}

		#endregion

		#region ICollection<KeyValuePair<TKey,TValue>> Members

		public void Add(KeyValuePair<TKey, TValue> item) {
			dictionary.Add(item);
			list.Add(item);
			int index = list.IndexOf(item);
			OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, index));
		}

		public bool Contains(KeyValuePair<TKey, TValue> item) {
			return dictionary.Contains(item);
		}

		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) {
			dictionary.CopyTo(array, arrayIndex);
		}

		public bool Remove(KeyValuePair<TKey, TValue> item) {
			int index = list.IndexOf(item);
			bool ret = dictionary.Remove(item) && list.Remove(item);
			OnListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, index));
			return ret;
		}

		#endregion

		#region IEnumerable<KeyValuePair<TKey,TValue>> Members

		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() {
			return list.GetEnumerator();
		}

		#endregion

		#region IBidingList Members

		event ListChangedEventHandler ListChanged;
		event ListChangedEventHandler IBindingList.ListChanged {
			add {
				ListChanged += value;
			}
			remove {
				ListChanged -= value;
			}
		}
		void OnListChanged(ListChangedEventArgs e) {
			if (ListChanged != null) {
				ListChanged(this, e);
			}
		}

		bool IBindingList.AllowNew {
			get {
				return true;
			}
		}

		bool IBindingList.AllowEdit {
			get {
				return true;
			}
		}

		bool IBindingList.AllowRemove {
			get {
				return true;
			}
		}

		bool IBindingList.SupportsChangeNotification {
			get {
				return true;
			}
		}

		bool IBindingList.SupportsSearching {
			get {
				return false;
			}
		}

		bool IBindingList.SupportsSorting {
			get {
				return false;
			}
		}

		bool IBindingList.IsSorted {
			get {
				throw new NotSupportedException();
			}
		}

		PropertyDescriptor IBindingList.SortProperty {
			get {
				throw new NotSupportedException();
			}
		}

		ListSortDirection IBindingList.SortDirection {
			get {
				throw new NotSupportedException();
			}
		}

		object IBindingList.AddNew() {
			var newObj = new KeyValuePair<TKey, TValue>(default(TKey), default(TValue));
			return newObj;
		}

		void IBindingList.AddIndex(PropertyDescriptor property) {
			throw new NotSupportedException();
		}

		void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction) {
			throw new NotSupportedException();
		}

		int IBindingList.Find(PropertyDescriptor property, object key) {
			throw new NotSupportedException();
		}

		void IBindingList.RemoveIndex(PropertyDescriptor property) {
			throw new NotSupportedException();
		}

		void IBindingList.RemoveSort() {
			throw new NotSupportedException();
		}

		#endregion

		#region IDictionary<TKey,TValue> Members

		public TValue this[TKey key] {
			get {
				return dictionary[key];
			}
			set {
				int i = list.FindIndex(o => key == null ? o.Key == null : key.Equals(o.Key));
				dictionary[key] = value;
				list[i] = new KeyValuePair<TKey, TValue>(key, value);
				OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, i));
			}
		}

		public ICollection<TKey> Keys {
			get {
				return dictionary.Keys;
			}
		}

		public ICollection<TValue> Values {
			get {
				return dictionary.Values;
			}
		}

		public bool ContainsKey(TKey key) {
			return dictionary.ContainsKey(key);
		}

		public void Add(TKey key, TValue value) {
			this.Add(new KeyValuePair<TKey, TValue>(key, value));
		}

		public bool Remove(TKey key) {
			int i = list.FindIndex(o => key == null ? o.Key == null : key.Equals(o.Key));
			if (i == -1) return false;
			dictionary.Remove(key);
			list.RemoveAt(i);
			OnListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, i));
			return true;
		}

		public bool TryGetValue(TKey key, out TValue value) {
			return dictionary.TryGetValue(key, out value);
		}

		#endregion
	}

}
