#pragma once

#include "core/nodes/heap_type.h"

namespace cl::lang::builders {

class builder;
class allocator;

builder heap_type(const allocator& m, const char* identifier);

}