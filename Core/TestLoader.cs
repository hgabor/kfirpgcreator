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

namespace KFI_RPG_Creator.Core {
	public class TestLoader : ObjectLoader {
		public string GetAttribute(string id, string attribute) {
			if (id == "nonexistant") throw new ObjectNotFoundException("nonexistant");

			switch (attribute) {
				
			case "ethereal":
				if (id.Contains("ethereal"))
					return "true";
				else
					return "false";
			
			case "collision.size":
				return "50";
				
			case "collision.type":
				return (id.Contains("cylinder"))
					? "cylinder"
					: "box";
				
			case "collision.height":
				return "70";

			default:
				throw new AttributeDoesNotExistException(attribute);
			}
		}

		public System.IO.Stream GetFile(string id, string filename) {
			if (id == "nonexistant") throw new ObjectNotFoundException("nonexistant");
			if (filename == "nonexistant") throw new FileDoesNotExistException("nonexistant");
			return new System.IO.MemoryStream();
		}

		
		public bool FileExists(string id, string filename) {
			if (id == "nonexistant") throw new ObjectNotFoundException("nonexistant");
			return (filename != "nonexistant");
		}
	}
}
