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

namespace KFI_Game_Core.GameObjects {
    class TestLoader : ObjectLoader {
        public string GetAttribute(string id, string attribute) {
            if (id == "nonexistant") throw new ObjectNotFoundException("nonexistant");

            switch (attribute) {
            case "name":
                return "TesztNév";

            case "walkable":
                if (id.Contains("járható"))
                    return "1";
                else
                    return "0";

            case "swimmable":
                if (id.Contains("úszható"))
                    return "1";
                else
                    return "0";

            default:
                throw new AttributeDoesNotExistException(attribute);
            }
        }
        /*public bool AttributeExists(string id, string attribute) {
            if (id == "nonexistant") throw new ObjectNotFoundException(id);
            switch (attribute) {
            case "name":
            case "walkable":
            case "swimmable":
                return true;
            default:
                return false;
            }
        }*/

        public System.IO.Stream GetFile(string id, string filename) {
            if (id == "nonexistant") throw new ObjectNotFoundException("nonexistant");
            throw new NotImplementedException();
        }

        /*public bool FileExists(string id, string filename) {
            throw new Exception("The method or operation is not implemented.");
        }*/
    }
}
