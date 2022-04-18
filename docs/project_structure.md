# Crosslight

## Principles

The Crosslight framework works with mutable node trees representing pieces of code.
It is plugin-based, where any input, output or intermediate language can access
the currently processed node tree, files on disk, in-memory data, etc.

The framework is designed to be easy to operate as a Crosslight developer extending it,
a system programmer invoking it in code or a generalist programmer using the GUI.

Every plugin for the framework can contain one or more input, output or intermediate
languages. Although multiple languages can be contained in the same project or
even class, it is considered a good practice to keep at most one input and one
output language in a plugin separated into different classes.

Input languages transform other forms of code or data into the Crosslight node tree.
Output languages transform Crosslight node trees into code in other languages,
visualize it, print the tree structure, etc.
Intermediate languages focus on transforming the tree. This can be used for
optimization, code generation, merging of partial members, etc.

The processing is done in a form of a pipeline, where every language plugin in
the chain is processing the tree one after another.

The Crosslight node tree is validated after every step (except for the output)
to catch errors and report them. Validation plugins can also modify the tree
to fix small errors or add metadata.

A logging system is used to store analytics, performance metrics and other
useful information.

When an error occurs, the whole pipeline is aborted and the processing stops.
It should be noted, however, that the generated artifacts, such as files, stay
on disk. If the next build is successful, they will likely be replaced with
the newer versions.