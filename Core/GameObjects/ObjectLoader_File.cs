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

namespace KFI_Game_Core.GameObjects {
    /// <summary>
    /// Betöltõ, amely merevlemezrõl tölti be a szükséges adatokat
    /// </summary>
    class ObjectLoader_File: ObjectLoader {
        //TODO: Finish ObjectLoader_File
        class Data {
        }

        Data LoadFromFile(string id) {
            throw new NotImplementedException();
        }

        public string GetAttribute(string id, string attribute) {
            throw new NotImplementedException();
        }
        public bool AttributeExists(string id, string attribute) {
            throw new NotImplementedException();
        }

        public Stream GetFile(string id, string filename) {
            throw new NotImplementedException();
        }
        public bool FileExists(string id, string filename) {
            throw new NotImplementedException();
        }
    }
}
