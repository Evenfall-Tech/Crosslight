#pragma once

#include "core/nodes/access_modifier.h"
#include "lang/builders/allocator.hpp"

namespace cl::lang::builders {

    class builder;
    class allocator;

    builder access_modifier(const allocator& m, enum cl_node_access_modifier_type type);

}
