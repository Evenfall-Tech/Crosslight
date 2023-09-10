## Project Structure

> **Note:**
- Crosslight is currently only configured for linux, tested under WSL2 and standalone Ubuntu 22.04.

### Build directory structure

The following is the directory structure inside a typical `build` or `out` build destination library.

- `bin` is the directory of important executables and dynamic library files.
- `lib` is the directory of static library files.
- `examples` is the directory of example executables.

### Separate target notes

#### c/core

The core Crosslight interface with definitions for nodes, languages, etc.
All structures from the C libraries need to be allocated with malloc, because users may delete them with free.

> **Note:**
- Defines target property `target_lang` pointing to the language declaration header file.

The code itself is mostly a set of general declarations, including a couple utility preprocessor directives.
Language implementation users must define the declared language functions in each dynamic library.

> **Note:**
- Defines preprocessor `CL_WINDOWS 1` if the target platform is Windows, `CL_WINDOWS 0` otherwise.
- Defines preprocessor `CL_UNIX 1` if the target platform is part of the Unix family, `CL_UNIX 0` otherwise.
- Defines preprocessor `CL_LINUX 1` if the target platform uses the Linux kernel, `CL_LINUX 0` otherwise.
- Defines preprocessor `CL_MACOS 1` if the target platform is MacOS, `CL_MACOS 0` otherwise. 

#### cpp/lang

The language parsing interface, importing antlr and forwarding it to individual language packages.

> **Note:**
- Defines target property `target_antlr_executable` pointing to the ANTLR4 jar file.
- Defines target property `target_antlr_libraries` pointing to ANTLR4 runtime libraries.

#### refs/lang_csharp_ref

C# language reference implementation, dependent on the .NET 7 SDK.

> **Note:**
- Defines variable `CL_DOTNET_DIR` as a hint for finding the dotnet executable path.
- Defines variable `CL_DOTNET_COMMAND` for the dotnet executable path.
- Defines variable `CL_DOTNET_RID` with the target RuntimeIdentifier for the .NET AOT build.
