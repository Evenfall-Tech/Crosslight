#include "lang/builders/allocator.hpp"

using namespace cl::lang::builders;

bool
allocator::equal(const allocator &left, const allocator &right) {
    return left.acquire == right.acquire && left.release == right.release;
}