## Project Structure

### cpp

#### lang

The language parsing interface, importing antlr and forwarding it to individual language packages.

> **Note:**
> Defines target property `target_antlr_executable` pointing to the ANTLR4 jar file.
> Defines target property `target_antlr_dirs` pointing to a list of include directories for ANTLR4 runtime.
