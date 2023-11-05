#include "lang/builders/allocator.hpp"
#include "lang/builders/builder.hpp"
#include "core/nodes/node_type.h"

using namespace cl::lang::builders;

bool
allocator::equal(const allocator &left, const allocator &right) {
    return left.acquire == right.acquire && left.release == right.release;
}

builder
allocator::none() const {

    return builder::from_payload(*this, nullptr, ::none);
}