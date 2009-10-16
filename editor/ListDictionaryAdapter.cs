using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace KFIRPG.editor {

	/// <summary>
	/// Wraps an IDictionary in an IList interface.
	/// </summary>
	/// <typeparam name="TKey">The type of keys.</typeparam>
	/// <typeparam name="TValue">The type of values.</typeparam>
	class ListDictionaryAdapter<TKey, TValue>: IList, IList<KeyValuePair<TKey, TValue>> {
		private List<KeyValuePair<TKey, TValue>> list;
		private IDictionary<TKey, TValue> dictionary;

		public ListDictionaryAdapter(IDictionary<TKey, TValue> dictionary) {
			this.dictionary = dictionary;
			list = new List<KeyValuePair<TKey, TValue>>(dictionary);
		}

		#region IList Members

		public int Add(object value) {
			dictionary.Add((KeyValuePair<TKey, TValue>)value);
			list.Add((KeyValuePair<TKey, TValue>)value);
			return list.IndexOf((KeyValuePair<TKey, TValue>)value);
		}

		public void Clear() {
			dictionary.Clear();
			list.Clear();
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
		}

		public bool IsFixedSize {
			get { return false; }
		}

		public bool IsReadOnly {
			get { return dictionary.IsReadOnly; }
		}

		public void Remove(object value) {
			dictionary.Remove((KeyValuePair<TKey, TValue>)value);
			list.Remove((KeyValuePair<TKey, TValue>)value);
		}

		public void RemoveAt(int index) {
			KeyValuePair<TKey, TValue> item = list[index];
			dictionary.Remove(item);
			list.RemoveAt(index);
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
			}
		}

		#endregion

		#region ICollection<KeyValuePair<TKey,TValue>> Members

		public void Add(KeyValuePair<TKey, TValue> item) {
			dictionary.Add(item);
			list.Add(item);
		}

		public bool Contains(KeyValuePair<TKey, TValue> item) {
			return dictionary.Contains(item);
		}

		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) {
			dictionary.CopyTo(array, arrayIndex);
		}

		public bool Remove(KeyValuePair<TKey, TValue> item) {
			return dictionary.Remove(item) && list.Remove(item);
		}

		#endregion

		#region IEnumerable<KeyValuePair<TKey,TValue>> Members

		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() {
			return list.GetEnumerator();
		}

		#endregion
	}
}
