// Copyright (C) 2007 Gábor Halász
// 
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.

using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using ICSharpCode.SharpZipLib.Zip;

namespace KFI_RPG_Creator.Core {
	/// <summary>
	/// Betöltő, amely merevlemezről tölti be a szükséges adatokat
	/// </summary>
	class ObjectLoader_File: ObjectLoader {
		const string AttributesFileName = "data.xml";
		
		class Data {
			internal Dictionary<string,string> attributes = new Dictionary<string,string>();
			internal Dictionary<string,MemoryStream> files = new Dictionary<string,MemoryStream>();
		}

		void LoadFromFile(string id) {
			string fileName = id.Replace('.', Path.DirectorySeparatorChar)+".kfiobject";
			Data data = new Data();
			using (ZipInputStream zs = new ZipInputStream(File.OpenRead(fileName))) {
				ZipEntry entry;
				while ((entry = zs.GetNextEntry()) != null) {
					MemoryStream m = new MemoryStream();
					int size = 2048;
					byte[] buffer = new byte[size];
					while ((size = zs.Read(buffer, 0, size)) > 0) {
						m.Write(buffer, 0, size);
					}
					m.Position = 0;
					data.files.Add(entry.Name, m);
				}
			}
			if (data.files.ContainsKey(AttributesFileName)) {
				XmlDocument doc = new XmlDocument();
				doc.Load(data.files[AttributesFileName]);
				XmlElement root = doc.DocumentElement;
				foreach(XmlNode element in root.ChildNodes) {
					data.attributes.Add(element.Name, element.InnerText);
				}
			}
			/*foreach (KeyValuePair<string, MemoryStream> streams in data.files) {
				if (streams.Key == AttributesFileName) {
					XmlDocument doc = new XmlDocument();
					doc.Load(streams.Value);
					XmlElement root = doc.DocumentElement;
					foreach(XmlNode element in root.ChildNodes) {
						data.attributes.Add(element.Name, element.InnerText);
					}
				}
			}*/
			dataList.Add(id, data);
		}
		
		Dictionary<string, Data> dataList = new Dictionary<string, Data>();

		void TryToLoad(string id) {
			try {
				LoadFromFile(id);
			}
			catch(FileNotFoundException ex) {
				throw new ObjectNotFoundException(id, ex);
			}
		}
		
		public string GetAttribute(string id, string attribute) {
			if (!dataList.ContainsKey(id)) {
				TryToLoad(id);
			}
			if (!(dataList[id].attributes.ContainsKey(attribute))) {
				throw new AttributeDoesNotExistException(attribute);
			}
			return dataList[id].attributes[attribute];
		}

		public Stream GetFile(string id, string fileName) {
			if (!dataList.ContainsKey(id)) {
				TryToLoad(id);
			}
			if (!(dataList[id].files.ContainsKey(fileName))) {
				throw new FileDoesNotExistException(fileName);
			}
			dataList[id].files[fileName].Position = 0;
			return dataList[id].files[fileName];
		}
	}
}
