using System;
using System.Collections.Generic;
using System.Text;
using KFIRPG.corelib;
using KFIRPG.editor.Undo;

namespace KFIRPG.editor {
	class Project {
		public interface Loadable {
			void Load(Project project);
		}

		public class LoadException: Exception {
			public LoadException(Exception innerException)
			: base("Project could not be loaded! See the inner exception for details.", innerException) { }
		}

		public class CannotRemoveException: Exception {
			public CannotRemoveException(string cause)
			: base(string.Format("Selected object cannot be removed, because it is used by \"{0}\".", cause)) { }
		}

		public readonly UndoStack Undo = new UndoStack();

		public IDictionary<string, SpriteSheet> sheets = new BindingDictionary<string, SpriteSheet>();
		public IDictionary<string, Map> maps = new BindingDictionary<string, Map>();
		public IDictionary<string, Sprite> sprites = new BindingDictionary<string, Sprite>();

		public void RemoveSprite(Sprite s, UndoCommandList list) {
			foreach (var map in maps) {
				foreach (var l in map.Value.layers) {
					foreach (var o in l.objects) {
						if (o != null && o.Sprite == s) {
							throw new CannotRemoveException(o.ToString());
						}
					}
				}
			}
			string name = s.Name;
			list.Add(new UndoCommand(
			             delegate() {
			                 sprites.Remove(name);
			             },
			             delegate() {
			                 sprites.Add(name, s);
			             }
			         ));
		}

		public void RemoveSprite(Sprite s) {
			var l = new UndoCommandList("Remove sprite " + s.Name);
			RemoveSprite(s, l);
			Undo.DoCommand(l);
		}

		public IDictionary<string, Animation> animations = new BindingDictionary<string, Animation>();

		public void RemoveAnimation(Animation a, UndoCommandList list) {
			foreach (var sprite in sprites.Values) {
				if (sprite.animation == a) {
					throw new CannotRemoveException("Sprite " + sprite.Name);
				}
			}
			string name = a.Name;
			list.Add(new UndoCommand(
			             delegate() {
			                 animations.Remove(name);
			             },
			             delegate() {
			                 animations.Add(name, a);
			             }
			         ));
		}

		public void RemoveAnimation(Animation a) {
			var undoComm = new UndoCommandList("Remove animation " + a.Name);
			RemoveAnimation(a, undoComm);
			Undo.DoCommand(undoComm);
		}

		public IDictionary<string, BigFile> musics = new BindingDictionary<string, BigFile>();

		public void RemoveMusic(BigFile music, UndoCommandList list) {
			string name = null;
			foreach (var m in musics) {
				if (m.Value == music) {
					name = m.Key;
				}
			}
			if (name == null) throw new ArgumentException("Music is not in the list!");
			list.Add(new UndoCommand(
			             delegate() {
			                 musics.Remove(name);
			             },
			             delegate() {
			                 musics.Add(name, music);
			             }
			         ));
		}

		public void RemoveMusic(BigFile music) {
			var list = new UndoCommandList("Remove audio file");
			RemoveMusic(music, list);
			Undo.DoCommand(list);
		}

		public List<Script> scripts = new List<Script>();
		public int tileSize;
		public KFIRPG.corelib.Loader loader;
		public string startupMapName;
		public string startupScriptName;
		public string quitScriptName;
		public List<Sprite> party = new List<Sprite>();

		public string scriptvm;
		public int screenWidth;
		public int screenHeight;
		public int startX;
		public int startY;
		public int startLayer;

		byte[] fontFile;
		string fontFileName;
		byte[] windowBorderFile;
		string dialogFile;

		public Project() { }

		private Project(KFIRPG.corelib.Loader loader) {
			this.loader = loader;
			PropertyReader global = loader.GetPropertyReader().Select("global.xml");

			tileSize = global.GetInt("tilesize");
			startupMapName = global.GetString("defaultmap");
			startupScriptName = global.GetString("startscript");
			quitScriptName = global.GetString("quitscript");
			scriptvm = global.GetString("scriptvm");
			screenWidth = global.GetInt("screenwidth");
			screenHeight = global.GetInt("screenheight");

			LoadList("img.list", img => {
			    sheets.Add(img, new SpriteSheet(img, this));
			});
			LoadList("animations.list", anim => {
			    animations.Add(anim, new Animation(anim, this));
			});
			LoadList("sprites.list", sprite => {
			    sprites.Add(sprite, new Sprite(sprite, this));
			});
			LoadList("maps.list", map => {
			    maps.Add(map, new Map(map, this));
			});
			LoadList("scripts.list", script => {
			    if (script.EndsWith("/")) {
			        scripts.Add(new Script(script));
			    }
			    else {
				        scripts.Add(new Script(script, loader.LoadText("scripts/" + script)));
				    }
				});
			LoadList("music.list", music => {
			    musics.Add(music, new BigFile("music/" + music, loader));
			});

			startX = global.GetInt("startx");
			startY = global.GetInt("starty");
			startLayer = global.GetInt("startl");
			foreach (PropertyReader character in global.SelectAll("party/character")) {
				party.Add(sprites[character.GetString("")]);
			}
			foreach (PropertyReader loc in global.SelectAll("locations/location")) {
				string locName = loc.GetString("name");
				int x = loc.GetInt("x");
				int y = loc.GetInt("y");
				int l = loc.GetInt("layer");
				string mapName = loc.GetString("map");
				maps[mapName].layers[l].tiles[x, y].locationName = locName;
			}

			//All the stuff we don't deal with yet
			windowBorderFile = loader.LoadRaw("dialog/windowborder.png");
			dialogFile = loader.LoadText("dialog/dialog.xml");
			fontFileName = loader.GetPropertyReader().Select("dialog/dialog.xml").GetString("font");
			fontFile = loader.LoadRaw("dialog/" + fontFileName);
		}

		private void LoadList(string listName, Action<string> addFunction) {
			foreach (string str in loader.LoadText(listName).Split('\n')) {
				string strTrimmed = str.Trim();
				if (strTrimmed == "") continue;
				addFunction(strTrimmed);
			}
		}

		public static Project FromFiles(string path) {
			try {
				return new Project(new KFIRPG.corelib.FileLoader(path));
			}
			catch (System.IO.FileNotFoundException ex) {
				throw new LoadException(ex);
			}
			catch (KFIRPG.corelib.Game.SettingsException ex) {
				throw new LoadException(ex);
			}
		}

		public void Save(Saver saver) {
			//Global.xml
			PropertyWriter pGlobal = saver.CreatePropertyFile("global.xml");

			pGlobal.Set("scriptvm", scriptvm);
			pGlobal.Set("startscript", startupScriptName);
			pGlobal.Set("quitscript", quitScriptName);
			pGlobal.Set("defaultmap", startupMapName);
			pGlobal.Set("tilesize", tileSize);
			pGlobal.Set("startx", startX);
			pGlobal.Set("starty", startY);
			pGlobal.Set("startl", startLayer);
			pGlobal.Set("screenwidth", screenWidth);
			pGlobal.Set("screenheight", screenHeight);

			PropertyWriter pParty = pGlobal.Create("party");
			foreach (Sprite sp in party) {
				pParty.Set("character", sp.Name);
			}

			//Needed later
			PropertyWriter pLocations = pGlobal.Create("locations");

			//Images
			List<string> imageList = new List<string>();
			foreach (KeyValuePair<string, SpriteSheet> sheet in this.sheets) {
				imageList.Add(sheet.Key);
				saver.Save("img/" + sheet.Key + ".png", sheet.Value.sheet);
				PropertyWriter pImg = saver.CreatePropertyFile("img/" + sheet.Key + ".xml");

				pImg.Set("width", sheet.Value.spriteWidth);
				pImg.Set("height", sheet.Value.spriteHeight);
				pImg.Set("x", sheet.Value.x);
				pImg.Set("y", sheet.Value.y);
			}
			saver.Save("img.list", string.Join("\n", imageList.ToArray()));

			//Sprites
			List<string> spriteList = new List<string>();
			foreach (KeyValuePair<string, Sprite> sprite in this.sprites) {
				spriteList.Add(sprite.Key);
				PropertyWriter pSprite = saver.CreatePropertyFile("sprites/" + sprite.Value.Name + ".xml");
				pSprite.Set("animation", sprite.Value.animation.Name);
				pSprite.Set("speed", sprite.Value.speed);
				pSprite.Set("noclip", sprite.Value.noclip);
				PropertyWriter exts = pSprite.Create("exts");
				foreach (KeyValuePair<string, string> kvp in sprite.Value.ext) {
					PropertyWriter ext = exts.Create("ext");
					ext.Set("key", kvp.Key);
					ext.Set("value", kvp.Value);
				}
			}
			saver.Save("sprites.list", string.Join("\n", spriteList.ToArray()));

			//Maps
			List<string> mapList = new List<string>();
			foreach (KeyValuePair<string, Map> map in this.maps) {
				mapList.Add(map.Key);
				PropertyWriter pInfo = saver.CreatePropertyFile("maps/" + map.Key + "/info.xml");
				pInfo.Set("layers", map.Value.layers.Count);
				pInfo.Set("width", map.Value.width);
				pInfo.Set("height", map.Value.height);

				PropertyWriter pObjects = saver.CreatePropertyFile("maps/" + map.Key + "/objects.xml");
				PropertyWriter pOnStep = saver.CreatePropertyFile("maps/" + map.Key + "/onstep.xml");

				for (int l = 0; l < map.Value.layers.Count; ++l) {
					Map.Layer layer = map.Value.layers[l];
					List<string> tileList = new List<string>();
					List<string> passList = new List<string>();
					for (int j = 0; j < map.Value.height; ++j) {
						string[] tileLine = new string[map.Value.width];
						string[] passLine = new string[map.Value.width];
						for (int i = 0; i < map.Value.width; ++i) {
							tileLine[i] = layer.tiles[i, j].gfx.Id.ToString();
							passLine[i] = layer.tiles[i, j].passable ? "1" : "0";
							if (!string.IsNullOrEmpty(layer.tiles[i, j].onStep)) {
								PropertyWriter pEvent = pOnStep.Create("event");
								pEvent.Set("x", i);
								pEvent.Set("y", j);
								pEvent.Set("layer", l);
								pEvent.Set("script", layer.tiles[i, j].onStep);
							}
							if (!string.IsNullOrEmpty(layer.tiles[i, j].locationName)) {
								string locName = layer.tiles[i, j].locationName;
								PropertyWriter location = pLocations.Create("location");
								location.Set("name", locName);
								location.Set("x", i);
								location.Set("y", j);
								location.Set("layer", l);
								location.Set("map", map.Key);
							}
							if (layer.objects[i, j] != null) {
								Map.Obj obj = layer.objects[i, j];
								PropertyWriter pObject = pObjects.Create("object");
								pObject.Set("x", i);
								pObject.Set("y", j);
								pObject.Set("layer", l);
								pObject.Set("sprite", obj.Sprite.Name);
								pObject.Set("action", obj.actionScript);
								pObject.Set("movement", obj.movementAIScript);
								pObject.Set("collide", obj.collideScript);
							}
						}
						tileList.Add(string.Join(" ", tileLine));
						passList.Add(string.Join(" ", passLine));
					}
					saver.Save(string.Format("maps/{0}/layers/tiles.{1}", map.Key, l), string.Join("\n", tileList.ToArray()));
					saver.Save(string.Format("maps/{0}/layers/passability.{1}", map.Key, l), string.Join("\n", passList.ToArray()));
					saver.Save(string.Format("maps/{0}/layers/name.{1}", map.Key, l), layer.name);
				}

				PropertyWriter pLadders = saver.CreatePropertyFile("maps/" + map.Key + "/ladders.xml");
				for (int i = 0; i < map.Value.width; ++i) {
					for (int j = 0; j < map.Value.height; ++j) {
						Map.Ladder ladder = map.Value.ladders[i, j];
						if (ladder != null) {
							PropertyWriter pLadder = pLadders.Create("ladder");
							pLadder.Set("x", i);
							pLadder.Set("y", j);
							pLadder.Set("base", map.Value.layers.IndexOf(ladder.baseLayer));
							pLadder.Set("top", map.Value.layers.IndexOf(ladder.topLayer));
						}
					}
				}
			}
			saver.Save("maps.list", string.Join("\n", mapList.ToArray()));

			//Scripts
			List<string> scriptList = new List<string>();
			foreach (Script script in scripts) {
				if (script.IsFolder) {
					scriptList.Add(script.Name + "/");
				}
				else {
					scriptList.Add(script.Name);
					saver.Save("scripts/" + script.Name, script.Text);
				}
			}
			saver.Save("scripts.list", string.Join("\n", scriptList.ToArray()));

			//Animations
			List<string> animationList = new List<string>();
			foreach (KeyValuePair<string, Animation> animKvp in animations) {
				animationList.Add(animKvp.Key);
				Animation anim = animKvp.Value;
				PropertyWriter pAnim = saver.CreatePropertyFile("animations/" + animKvp.Key + ".xml");
				pAnim.Set("sheet", anim.sheet.Name);
				foreach (KeyValuePair<string, Animation.Group> groupKvp in anim.groups) {
					PropertyWriter pGroup = pAnim.Create("group");
					pGroup.Set("name", groupKvp.Key);
					foreach (Animation.Frame frame in groupKvp.Value.frames) {
						PropertyWriter pFrame = pGroup.Create("frame");
						pFrame.Set("sheetid", frame.sheetId);
						pFrame.Set("time", frame.time);
					}
				}
			}
			saver.Save("animations.list", string.Join("\n", animationList.ToArray()));

			//Musics
			List<string> musicList = new List<string>();
			foreach (var musicKvp in musics) {
				musicList.Add(musicKvp.Key);
				if (musicKvp.Value.NeedsSaving || !saver.Exists("music/" + musicKvp.Key)) {
					saver.Save("music/" + musicKvp.Key, musicKvp.Value.Content);
				}
			}
			saver.Save("music.list", string.Join("\n", musicList.ToArray()));

			//All the stuff we don't deal with yet
			saver.Save("dialog/windowborder.png", windowBorderFile);
			saver.Save("dialog/dialog.xml", dialogFile);
			saver.Save("dialog/" + fontFileName, fontFile);
		}
	}
}
