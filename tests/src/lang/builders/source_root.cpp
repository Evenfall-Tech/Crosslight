#include "lang/builders/source_root.hpp"
#include "lang/builders/allocator.hpp"
#include "lang/builders/builder.hpp"
#include "doctest/doctest.h"

namespace b = cl::lang::builders;

TEST_SUITE_BEGIN("lang/builders/source_root");

TEST_CASE("Single") {
    auto m = b::allocator{ malloc, free };
    auto factory = m.source_root("hello.ts");
}

TEST_CASE("Hierarchy temporary") {
    auto m = b::allocator{ malloc, free };
    auto factory = m.source_root("hello.ts") << m.source_root("world.ts");
}

TEST_CASE("Hierarchy variable") {
    auto m = b::allocator{ malloc, free };
    auto factory = m.source_root("hello.ts") << m.source_root("world.ts");
}

TEST_SUITE_END();
