using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace KFIRPG.editor {
	class BindingDictionary<TKey, TValue>: BindingList<KeyValuePair<TKey, TValue>>, IDictionary<TKey, TValue> {
		public EqualityComparer<TKey> comp = EqualityComparer<TKey>.Default;

		TValue IDictionary<TKey, TValue>.this[TKey key] {
			get {
				if (key == null) throw new ArgumentNullException();
				for (int i = 0; i < this.Count; ++i) {
					if (comp.Equals(this[i].Key, key)) {
						return this[i].Value;
					}
				}
				throw new KeyNotFoundException();
			}
			set {
				if (key == null) throw new ArgumentNullException();
				for (int i = 0; i < this.Count; ++i) {
					if (comp.Equals(this[i].Key, key)) {
						this[i] = new KeyValuePair<TKey, TValue>(key, value);
						return;
					}
				}
				this.Add(key, value);
			}
		}

		public ICollection<TKey> Keys {
			get {
				var list = new List<TKey>();
				foreach (var kvp in this) {
					list.Add(kvp.Key);
				}
				return list;
			}
		}

		public ICollection<TValue> Values {
			get {
				var list = new List<TValue>();
				foreach (var kvp in this) {
					list.Add(kvp.Value);
				}
				return list;
			}
		}

		public bool IsReadOnly {
			get {
				return false;
			}
		}

		public bool ContainsKey(TKey key) {
			if (key == null) throw new ArgumentNullException();
			foreach (var kvp in this) {
				if (comp.Equals(kvp.Key, key)) return true;
			}
			return false;
		}

		public void Add(TKey key, TValue value) {
			if (key == null) throw new ArgumentNullException();
			foreach (var kvp in this) {
				if (comp.Equals(kvp.Key, key)) throw new ArgumentException("Key already exists in the Dictionary");
			}
			this.Add(new KeyValuePair<TKey, TValue>(key, value));
		}

		public bool Remove(TKey key) {
			if (key == null) throw new ArgumentNullException();
			for (int i = 0; i < this.Count; ++i) {
				if (comp.Equals(this[i].Key, key)) {
					this.RemoveAt(i);
					return true;
				}
			}
			return false;
		}

		public bool TryGetValue(TKey key, out TValue value) {
			if (key == null) throw new ArgumentNullException();
			if (ContainsKey(key)) {
				value = ((IDictionary<TKey, TValue>)this)[key];
				return true;
			}
			else {
				value = default(TValue);
				return false;
			}
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return base.GetEnumerator();
		}
	}
}
