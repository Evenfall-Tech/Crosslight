## access
1. **internal** - visible inside *ProjectNode*
2. **public** - visible everywhere
3. **private** - visible only within body of same parent *EntityNode*
4. **protected** - visible within *InheritedTypeNode* and its inherited children
5. **readonly** - *EntityNode* object may be assigned only once during parent *EntityNode* definition
6. **protected internal** - parses to **public** as fallback + custom 'protected internal'
7. **private protected** - parses to **public** as fallback + custom 'private protected'
8. **custom** - for all access modifiers falling outside of existing ones

## inheritance control
1. **abstract** - some content of *CompoundTypeNode* may be declaration-only
2. **virtual** - some content of *CompoundTypeNode* may be overridden
3. **sealed** - some content of *CompoundTypeNode* (or it self) may not be overridden
4. **new** - some content of *CompoundTypeNode* hides parent declaration
5. **override** - some content of *CompoundTypeNode* re-defines parent declaration/definition
6. **static** - some content of *CompoundTypeNode* belongs to type definition rather than instance

## conversion type
1. **implicit** - *CompoundTypeNode* automatically casts to specified type
2. **explicit** - *CompoundTypeNode* must be manually casted to specified type

## parallelism
1. **async** - *BaseFunctionNode* runs potentially long-running work without blocking the caller's thread in asynchronous context

## optimizations
1. **volatile** - *ValueNode* may be modified in any thread disregarding thread safety so no optimizations may be made
2. **unsafe** - operations involve low-level memory access and modification
3. **extern** - *CompoundTypeNode* content is defined elsewhere

## parameter passing
1. **in** - parameter must be passed by reference and mustn't be assigned in *BaseFunctionNode* body
2. **out** - parameter must be assigned in *BaseFunctionNode* body before exiting
3. **ref** - parameter must be passed by reference and may be assigned in *BaseFunctionNode* body
4. **params** - a variable number of parameters with a type declared with this modifier may be passed to *BaseFunctionNode*