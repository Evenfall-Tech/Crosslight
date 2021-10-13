# Crosslight Roadmap

## Alpha

### V0.1

The initial version must have all Crosslight nodes required to map ILSpy nodes.
This includes, but is not limited to, Class/Struct, Function/Method, Operator,
Value, Reference, Attribute etc.

The viewer must be able to correctly display all available nodes, including
node data representation (type, title or content) and icon.

Crosslight intermediate language should have a function to save a node tree to
a human-readable text file and to an optimized compressed binary file. This
should be a debugging feature, not meant for further interpretation or
compilation.

*POC*:
- Create 2 files that contain all possible CIL input language features.
- Convert CIL input to Crosslight node tree.
- Convert Crosslight node tree to Serialized output files on disk.
- Convert Serialized input files on disk to Crosslight node tree.
- Convert Crosslight node tree to Viewer display.

### V0.2

This version must have a fully functioning C# input/output language using Antlr.
Conversion of `C# -> Crosslight -> C#` must not break or change code, with the
exception of formatting. Together with CIL input decompilation can be achieved.

*POC*:
- Create 2 files that contain all possible CSharp input language features.
- Convert CSharp input to Crosslight node tree.
- Convert Crosslight node tree to Serialized output files on disk.
- Convert Serialized input files on disk to Crosslight node tree.
- Convert Crosslight node tree to Viewer display.
- Convert Crosslight node tree to CSharp output.
- Convert CIL input to Crosslight node tree.
- Convert Crosslight node tree to CSharp output.

### V0.3

This version covers intermediate language transformation and modification. The
idea is to have custom transformer languages, including a language that operates
on human-readable config files, that can change the Crosslight node tree to
achieve different results, like data model mapping or code generation.

*POC*:
- Create a sample project with a back-end and a front-end in C#.
- Create a config that maps back-end endpoints to front-end requests and creates
Data Transfer Objects for the front-end.
- Convert back-end CSharp input to Crosslight node tree.
- Apply transformers.
- Convert Crosslight node tree to CSharp output.

### V0.4

This version introduces the foundation to add other input/output languages. The
choice of language is yet to be made, but simplicity and small standard library
is key for easy mapping.

*POC*:
- Create a sample project with a back-end in C# and a front-end in [X].
- Create a config that maps back-end endpoints to front-end requests and creates
Data Transfer Objects for the front-end.
- Convert back-end CSharp input to Crosslight node tree.
- Apply transformers.
- Convert Crosslight node tree to [X] output.

### V0.5

This is a maintenance version of Crosslight to create mappings for the standard
libraries of supported languages. The idea is to define a Crosslight standard
library that will have all the features of supported ones and create a config to
correctly map standard library calls of languages into Crosslight SL and back.

*POC*:
- For each supported language create a project that uses all common standard
library features and a test project that checks if code is correct.
- Convert these projects into Crosslight node trees.
- Apply transformers.
- Convert Crosslight node trees back into original and other languages.
- Test converted code and observe correct behavior.

### V0.6

This version caters towards future usage of Crosslight. All code must be edited
with the goal of better readability and documentation. Static documentation
should be generated and explain the usage of Crosslight in plain terms that are
understandable to a beginner. Add sections that describe installing, using,
modifying and contributing to Crosslight.

Moreover, since the code will be used by external users, create different tests
for the Crosslight project itself and for the supported languages. These tests
must be then run on each major code change, so they should be added to push
actions on `dev` and `master` branches of Git. Performance, integration and
stress testing should also be performed.

*POC*:
- Run tests.
- Create a website (possibly on GitHub Pages or similar resource) to store
Crosslight documentation.
- Call in external users to use the project and gather feedback.
- Modify the project according to the feedback.
- Run tests.

## Beta

### V0.7

This version is centered around rewriting language projects to be more
independent from the surrounding ecosystems, including CIL input language.

External dependencies should be either swapped for more cross-platform and
cross-language ones or rewritten into custom solutions. The main hook on Antlr
will most likely remain, but is subject to change in case a better solution
comes up.

After this point, Crosslight itself should be able to be transformed into other
languages that support the dependencies.

*POC*:
- Choose a target language [X].
- Convert Crosslight project into Crosslight node tree.
- Apply transformers.
- Convert Crosslight node tree to output language [X].
- Run existing POCs and tests to verify correct behavior.

### V0.8 and further

These versions focus on the real-world usage of Crosslight. Possible
applications of the project should be gathered and a direction should be chosen,
for example, back-end to front-end mapping, UI library conversion, game engine
scripting, etc. ASP.NET, Entity Framework, Godot, WPF and Avalonia may be good
candidates.

*POC*: to be chosen based on research.

## Release

### V1.0

In short, the Crosslight project should be a performant, tested and actually
used solution that is able to fulfill all feature requests and fix bug issues.