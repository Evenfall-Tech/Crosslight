# Crosslight

A language translation / interpretation engine for cross-language compilation / execution. Run Crosslight, select languages for input and output, specify input and output files and see the magic!

## About

Crosslight is developed as a plugin-based translation / interpretation environment. The initial goal of this repository was to translate C# code into C++ to use in time-dependent applications (with hopes of speeding up the runtime). After the rewrite, the repository is configured for typescript->C# conversion, but this will be expanded in the future.

> **Note:**
> To be done.

### Projects

> **Note:**
> To be done.

## Installation and Usage

The dependencies vary from project to project:
- For Crosslight core, the library containing primary structure definitions, latest `git >=2.40` and `CMake >=3.14` are required.
- For Crosslight lang, the library containing common declarations for language implementations in C++, `Java >=11` is required as a transient dependency for parser code generation with ANTLRv4. OpenJDK is recommended.
- For Crosslight csharp, the library containing the C# bindings for Crosslight, `.NET >= 8` is required. Dotnet SDK is recommended.
- For Crosslight lang_typescript, the typescript language implementation, no addiitonal dependencies are required.
- For Crosslight lang_csharp_ref, the reference implementations of the C# language, the native build tools for the target platform are required. For an example with Visual Studio, on Windows 11 Arm64 `MSVC >=v143 ARM64/ARM64EC build tools` are needed, on x64 `MSVC >=v143 x64/x86 build tools` are needed, etc.
- For Crosslight ast example, `zlib` and `llvm >=14` is required, including the IR generation component.

> **Note:**
> To be done.

## Showcase

> **Note:**
> To be done.

## Benchmarks

> **Note:**
> To be done.

## Contributing

> **Note:**
> To be done.
