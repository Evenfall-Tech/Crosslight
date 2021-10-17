# Node Rules

1. **Do not** create util node types to include some field, create an interface instead.
2. **Do not** put child nodes inside the constructor, put only simple (.NET) types there like string, int, etc. Children can be added later to the corresponding field.
