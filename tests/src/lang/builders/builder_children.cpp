#include "lang/builders/allocator.hpp"
#include "lang/builders/builder.hpp"
#include "lang/builders/builders.hpp"
#include "core/node.h"
#include "core/nodes/node_type.h"
#include "doctest/doctest.h"

namespace b = cl::lang::builders;

TEST_SUITE_BEGIN("lang/builders/builder_child");

TEST_CASE("Hierarchy temporary") {
    auto m = b::allocator{ malloc, free };
    auto factory = m.source_root("hello.ts")
        << b::children(m.source_root("world.ts"), m.source_root("!.ts"));

    auto root = factory.root_get();
    REQUIRE_UNARY(root);
    REQUIRE_NE(root, factory.parent_get());
    REQUIRE_EQ(2, root->child_count);
    REQUIRE_UNARY(root->payload);
    REQUIRE_EQ(::source_root, root->payload_type);
    REQUIRE_UNARY_FALSE(root->parent);

    auto child = root->children;
    REQUIRE_UNARY(child);
    REQUIRE_EQ(child, factory.parent_get());
    REQUIRE_EQ(0, child->child_count);
    REQUIRE_UNARY_FALSE(child->children);
    REQUIRE_UNARY(child->payload);
    REQUIRE_EQ(::source_root, child->payload_type);
    REQUIRE_EQ(root, child->parent);

    ++child;
    REQUIRE_UNARY(child);
    REQUIRE_EQ(child, factory.parent_get());
    REQUIRE_EQ(0, child->child_count);
    REQUIRE_UNARY_FALSE(child->children);
    REQUIRE_UNARY(child->payload);
    REQUIRE_EQ(::source_root, child->payload_type);
    REQUIRE_EQ(root, child->parent);
}

TEST_SUITE_END();