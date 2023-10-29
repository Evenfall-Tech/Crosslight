#pragma once

#include "lang/language.hpp"

namespace cl::lang::typescript {

using AcquireT = cl::lang::AcquireT;
using ReleaseT = cl::lang::ReleaseT;

class language_options {
public:
    AcquireT acquire;
    ReleaseT release;
    bool process_unsupported;
};
}
