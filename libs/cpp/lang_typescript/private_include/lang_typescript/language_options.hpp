#pragma once

#include <cstddef>

namespace cl::lang::typescript {

using AcquireT = void*(*)(size_t);
using ReleaseT = void(*)(void*);

class language_options {
public:
    AcquireT acquire;
    ReleaseT release;
    bool process_unsupported;
};
}
