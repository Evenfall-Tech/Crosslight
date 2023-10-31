#pragma once

#include "lang/language.hpp"

namespace cl::lang::builders {

class builder;

class allocator {
public:
    AcquireT acquire;
    ReleaseT release;

    builder source_root(const char* filename) const;
    builder scope(const char* identifier) const;
    builder heap_type(const char* identifier) const;

    static bool equal(const allocator& left, const allocator& right);
};

}