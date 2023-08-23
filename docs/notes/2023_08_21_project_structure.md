## Project Structure

### Build directory structure

The following is the directory structure inside a typical `build` or `out` build destination library.

- `bin` is the directory of important executables and dynamic library files.
- `lib` is the directory of static library files.
- `examples` is the directory of example executables.

### Separate target notes

#### c/core

The core Crosslight interface with definitions for nodes, languages, etc.

> **Note:**
- Defines target property `target_lang` pointing to the language declaration header file.

The code itself is mostly a set of general declarations, including a couple utility preprocessor directives.
Language implementation users must define the declared language functions in each dynamic library.

> **Note:**
- Defines `CL_WINDOWS 1` if the target platform is Windows, `CL_WINDOWS 0` otherwise.
- Defines `CL_UNIX 1` if the target platform is part of the Unix family, `CL_UNIX 0` otherwise.
- Defines `CL_LINUX 1` if the target platform uses the Linux kernel, `CL_LINUX 0` otherwise.
- Defines `CL_MACOS 1` if the target platform is MacOS, `CL_MACOS 0` otherwise. 

#### cpp/lang

The language parsing interface, importing antlr and forwarding it to individual language packages.

> **Note:**
- Defines target property `target_antlr_executable` pointing to the ANTLR4 jar file.
- Defines target property `target_antlr_libraries` pointing to ANTLR4 runtime libraries.
