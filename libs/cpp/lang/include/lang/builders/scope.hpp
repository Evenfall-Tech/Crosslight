#pragma once

#include "core/nodes/scope.h"

namespace cl::lang::builders {

class builder;
class allocator;

builder scope(const allocator& m, const char* identifier);

}