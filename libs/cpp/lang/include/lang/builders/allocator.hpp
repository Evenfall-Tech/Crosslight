#pragma once

#include "core/nodes/access_modifier.h"
#include "lang/language.hpp"

namespace cl::lang::builders {

class builder;

class allocator {
public:
    AcquireT acquire;
    ReleaseT release;

    [[nodiscard]] builder none() const;
    builder source_root(const char* filename) const;
    builder scope(const char* identifier) const;
    builder heap_type(const char* identifier) const;
    builder access_modifier(enum cl_node_access_modifier_type type) const;

    static bool equal(const allocator& left, const allocator& right);
};

}