
**KFI RPG Creator** is a tool for creating JRPG style games (think Final Fantasy VI). It is a work-in-progress, don't expect it to be done (or even usable) in the near future.
It contains an "editor" part (to create the game) and a "runner" part (so you don't have to redistribute the whole editor to play the game).

## Why?

Most "game maker" suites' premise is that you don't have to know programming to make your game. They achieve this by limiting your possibilities, sometimes arbitrarily (e.g. you cannot change the way battle works, item types are predefined etc.)
Even if you can work around some of these limitations, it usually involves a lot of repetitive code.

There are some good ones, but none of them are free nor open source.

Therefore I'm writing my own code for my upcoming game.

## Features

* Map editor with an intuitive interface
* Animation editor
* Most of the actions/events are scripted using Lua, only the very basics are hardcoded
* Support for all popular image/audio formats using SDL
* Gamepad support
* **No sprite/tile editor**. Use a proper image editor for spriting.

## Requirements:

* editor: .NET 4.0 (Windows only for the moment)
* runner: .NET 4.0 or latest mono (Windows and Linux)

Linux requires to map your windows .dlls to .so files.

## Known issues

* Editor does not compile.
* Building requires external libraries which are not included.
* The lua prepackaged with Tao requires msvcr80.dll, which might not be present on a modern system. Rebuilding lua with MinGW fixes the problem.
* The Tao framework is not mantained anymore.
* Build instuctions do not exist yet.

## License

MIT, see [COPYING].
