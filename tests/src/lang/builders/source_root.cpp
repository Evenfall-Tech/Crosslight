#include "lang/builders/source_root.hpp"
#include "lang/builders/allocator.hpp"
#include "lang/builders/builder.hpp"
#include "core/node.h"
#include "core/nodes/node_type.h"
#include "doctest/doctest.h"

namespace b = cl::lang::builders;

TEST_SUITE_BEGIN("lang/builders/source_root");

TEST_CASE("Single") {
    auto m = b::allocator{ malloc, free };
    auto factory = m.source_root("hello.ts");

    auto root = factory.root_get();
    REQUIRE_UNARY(root);
    REQUIRE_EQ(root, factory.parent_get());
    REQUIRE_EQ(0, root->child_count);
    REQUIRE_UNARY_FALSE(root->children);
    REQUIRE_UNARY(root->payload);
    REQUIRE_UNARY(root->payload_type);
    REQUIRE_UNARY_FALSE(root->parent);
}

TEST_CASE("Hierarchy temporary") {
    auto m = b::allocator{ malloc, free };
    auto factory = m.source_root("hello.ts") << m.source_root("world.ts");

    auto root = factory.root_get();
    REQUIRE_UNARY(root);
    REQUIRE_NE(root, factory.parent_get());
    REQUIRE_EQ(1, root->child_count);
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
}

TEST_CASE("Hierarchy variable") {
    auto m = b::allocator{ malloc, free };
    auto a = m.source_root("hello.ts");
    auto factory = a << m.source_root("world.ts");

    auto root = factory.root_get();
    REQUIRE_UNARY(root);
    REQUIRE_NE(root, factory.parent_get());
    REQUIRE_EQ(1, root->child_count);
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
}

TEST_CASE("Hierarchy variable rewrite") {
    auto m = b::allocator{ malloc, free };
    auto factory = m.source_root("hello.ts");
    factory = factory << m.source_root("world.ts");

    auto root = factory.root_get();
    REQUIRE_UNARY(root);
    REQUIRE_NE(root, factory.parent_get());
    REQUIRE_EQ(1, root->child_count);
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
}

TEST_CASE("Hierarchy variable rewrite reverse") {
    auto m = b::allocator{ malloc, free };
    auto factory = m.source_root("world.ts");
    factory = m.source_root("hello.ts") << factory;

    auto root = factory.root_get();
    REQUIRE_UNARY(root);
    REQUIRE_NE(root, factory.parent_get());
    REQUIRE_EQ(1, root->child_count);
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
}

TEST_CASE("Hierarchy both variables rewrite") {
    auto m = b::allocator{ malloc, free };
    auto factory = m.source_root("world.ts");
    auto a = m.source_root("hello.ts");
    factory = a << factory;

    auto root = factory.root_get();
    REQUIRE_UNARY(root);
    REQUIRE_NE(root, factory.parent_get());
    REQUIRE_EQ(1, root->child_count);
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
}

TEST_CASE("Hierarchy temporary nested temporary") {
    auto m = b::allocator{ malloc, free };
    auto factory = m.source_root("hello.ts") << m.source_root("world.ts") << m.source_root("!.ts");

    auto root = factory.root_get();
    REQUIRE_UNARY(root);
    REQUIRE_NE(root, factory.parent_get());
    REQUIRE_EQ(1, root->child_count);
    REQUIRE_UNARY(root->payload);
    REQUIRE_EQ(::source_root, root->payload_type);
    REQUIRE_UNARY_FALSE(root->parent);

    auto child = root->children;
    REQUIRE_UNARY(child);
    REQUIRE_NE(child, factory.parent_get());
    REQUIRE_EQ(1, child->child_count);
    REQUIRE_UNARY(child->payload);
    REQUIRE_EQ(::source_root, child->payload_type);
    REQUIRE_EQ(root, child->parent);

    child = child->children;
    REQUIRE_UNARY(child);
    REQUIRE_EQ(child, factory.parent_get());
    REQUIRE_EQ(0, child->child_count);
    REQUIRE_UNARY_FALSE(child->children);
    REQUIRE_UNARY(child->payload);
    REQUIRE_EQ(::source_root, child->payload_type);
    REQUIRE_EQ(root->children, child->parent);
}

TEST_CASE("Hierarchy variable nested temporary") {
    auto m = b::allocator{ malloc, free };
    auto factory = m.source_root("hello.ts");
    factory = factory << m.source_root("world.ts") << m.source_root("!.ts");

    auto root = factory.root_get();
    REQUIRE_UNARY(root);
    REQUIRE_NE(root, factory.parent_get());
    REQUIRE_EQ(1, root->child_count);
    REQUIRE_UNARY(root->payload);
    REQUIRE_EQ(::source_root, root->payload_type);
    REQUIRE_UNARY_FALSE(root->parent);

    auto child = root->children;
    REQUIRE_UNARY(child);
    REQUIRE_NE(child, factory.parent_get());
    REQUIRE_EQ(1, child->child_count);
    REQUIRE_UNARY(child->payload);
    REQUIRE_EQ(::source_root, child->payload_type);
    REQUIRE_EQ(root, child->parent);

    child = child->children;
    REQUIRE_UNARY(child);
    REQUIRE_EQ(child, factory.parent_get());
    REQUIRE_EQ(0, child->child_count);
    REQUIRE_UNARY_FALSE(child->children);
    REQUIRE_UNARY(child->payload);
    REQUIRE_EQ(::source_root, child->payload_type);
    REQUIRE_EQ(root->children, child->parent);
}

TEST_CASE("Hierarchy variable nested variable") {
    auto m = b::allocator{ malloc, free };
    auto factory = m.source_root("hello.ts");
    factory = factory << m.source_root("world.ts");
    factory = factory << m.source_root("!.ts");

    auto root = factory.root_get();
    REQUIRE_UNARY(root);
    REQUIRE_NE(root, factory.parent_get());
    REQUIRE_EQ(1, root->child_count);
    REQUIRE_UNARY(root->payload);
    REQUIRE_EQ(::source_root, root->payload_type);
    REQUIRE_UNARY_FALSE(root->parent);

    auto child = root->children;
    REQUIRE_UNARY(child);
    REQUIRE_NE(child, factory.parent_get());
    REQUIRE_EQ(1, child->child_count);
    REQUIRE_UNARY(child->payload);
    REQUIRE_EQ(::source_root, child->payload_type);
    REQUIRE_EQ(root, child->parent);

    child = child->children;
    REQUIRE_UNARY(child);
    REQUIRE_EQ(child, factory.parent_get());
    REQUIRE_EQ(0, child->child_count);
    REQUIRE_UNARY_FALSE(child->children);
    REQUIRE_UNARY(child->payload);
    REQUIRE_EQ(::source_root, child->payload_type);
    REQUIRE_EQ(root->children, child->parent);
}

TEST_SUITE_END();
