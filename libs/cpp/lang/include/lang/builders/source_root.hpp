#pragma once

#include "core/nodes/source_root.h"
#include "lang/builders/allocator.hpp"

namespace cl::lang::builders {

class builder;
class allocator;

builder source_root(const allocator& m, const char* filename);

}
