using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace KFIRPG.editor {
	class MRU: IEnumerable<string> {
		List<string> items;
		int maxItems;
		string fileName;

		public MRU(int maxItems, string fileName) {
			this.maxItems = maxItems;
			this.fileName = fileName;
			if (!File.Exists(fileName)) {
				items = new List<string>();
			}
			else {
				items = new List<string>(File.ReadAllLines(fileName));
			}
		}

		private void Save() {
			File.WriteAllLines(fileName, items.ToArray());
		}

		public void Add(string item) {
			if (items.Contains(item)) {
				items.Remove(item);
			}
			items.Insert(0, item);
			if (items.Count > maxItems) items.RemoveAt(items.Count - 1);
			Save();
		}

		#region IEnumerable<string> Members

		public IEnumerator<string> GetEnumerator() {
			return items.GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
			return items.GetEnumerator();
		}

		#endregion
	}
}
