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

using System.IO;

namespace KFI_RPG_Creator.Core {
	/// <summary>
	/// The source of data for object creation
	/// </summary>
	public interface ObjectLoader {
		/// <summary>
		/// Returns the value of an attribute for an object.
		/// </summary>
		/// <param name="id">The ID of the object.</param>
		/// <param name="attribute">The attribute in question.</param>
		/// <returns>The vaue of the attribute.</returns>
		/// <exception cref="ObjectNotFoundException">The object does not exist in the data source.</exception>
		/// <exception cref="AttributeDoesNotExistException">The attrbute is not specified for the object.</exception>
		string GetAttribute(string id, string attribute);
		
		/// <summary>
		/// Returns a System.Stream for a file in the object.
		/// </summary>
		/// <param name="id">The ID of the object.</param>
		/// <param name="filename">The name of the file.</param>
		/// <returns>The stream for the file.</returns>
		/// <exception cref="ObjectNotFoundException">The object does not exist in the data source.</exception>
		/// <exception cref="FileDoesNotExistException">The file does not exist in the object.</exception>
		Stream GetFile(string id, string filename);
		
		/// <summary>
		/// Checks if a file exist in the object.
		/// </summary>
		/// <param name="id">The ID of the object.</param>
		/// <param name="filename">The name of the file.</param>
		/// <returns>True if the file exists, false otherwise.</returns>
		bool FileExists(string id, string filename);
	}
}
