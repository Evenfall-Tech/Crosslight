## Project Structure

> **Note:**
- Crosslight is currently only configured for linux, tested under WSL2 and standalone Ubuntu 22.04.

### Build directory structure

The following is the directory structure inside a typical `build` or `out` build destination library.

- `/bin` is the directory of important executables and dynamic library files.
- `/bin/plugins` is the directory with language implementations and possibly other plugins.
- `/lib` is the directory of static and shared library files.
- `/examples` is the directory of example executables.

### IDE directory structure

Modern IDEs that support wrapping projects or targets in folders are supported by Crosslight.
When building Crosslight itself, the targets are distributed between `Runtime`, `Plugins`, `Examples`, and `Tests`.
When building as a submodule, the folder names are wrapped with `Crosslight <Folder>`.

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

#### csharp/common

The C# wrapper for the core library, for use with other language implementations in C#.

> **Note:**
- The native pointer-based constructors for most of the `Core` types assume the pointers to be valid.

#### refs/lang_csharp_ref

C# language reference implementation, dependent on the .NET 7 SDK. For methods using `AcquireDelegate` and `ReleaseDelegate`,
it is strongly encouraged to pass their instances, as the defaults may not handle exceptions.

> **Note:**
- Defines variable `CL_DOTNET_DIR` as a hint for finding the dotnet executable path.
- Defines variable `CL_DOTNET_COMMAND` for the dotnet executable path.
- Defines variable `CL_DOTNET_RID` with the target RuntimeIdentifier for the .NET AOT build.

#### tests

The tests cover the functionality of the framework using unit testing with [doctest](https://github.com/doctest/doctest).

> **Note:**
- Although doctest does not differentiate between arguments, the assertions are written in the `(expected, actual)` format.
- The naming of test suites corresponds to the tested file or object using full path, e.g., `core/config`.
- The naming of test cases and subcases corresponds to individual use cases for the object.
- For testing C++ libraries, `nullptr` can be used where applicable. For C libraries, `(mytype*)0` is used.
- For testing C++ libraries, appropriate checks have to use the `<LEVEL>_NOTHROW(expression)` format from [here](https://github.com/doctest/doctest/blob/master/doc/markdown/assertions.md#exceptions).
