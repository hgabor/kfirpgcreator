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

namespace KFI_Game_Core.GameObjects {
    /// <summary>
    /// Az objektumok innen nyerik a felépítésükhöz szükséges adatokat
    /// </summary>
    interface ObjectLoader {
        /// <summary>
        /// Az objektumhoz tartozó tulajdonságot adja vissza
        /// </summary>
        /// <param name="id">Az objektum azonosítója</param>
        /// <param name="attribute">A kérdéses tulajdonság</param>
        /// <returns>A tulajdonság értéke</returns>
        /// <exception cref="ObjectNotFoundException">Ha az objektum nem létezik</exception>
        /// <exception cref="AttributeDoesNotExistException">Ha a kérdéses tulajdonság nem létezik</exception>
        string GetAttribute(string id, string attribute);
        //bool AttributeExists(string id, string attribute);
        Stream GetFile(string id, string filename);
        //bool FileExists(string id, string filename);
    }
}
