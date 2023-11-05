#include <cstring>
#include "lang/builders/allocator.hpp"
#include "lang/builders/builder.hpp"
#include "core/node.h"
#include "core/nodes/node_type.h"
#include "core/nodes/source_root.h"
#include "doctest/doctest.h"

namespace b = cl::lang::builders;

TEST_SUITE_BEGIN("lang/builders/source_root");

TEST_CASE("source_root lifetime") {
    auto m = b::allocator{ malloc, free };
    auto factory = m.source_root("hello.ts");

    auto root = factory.root_get();
    REQUIRE_UNARY(root);
    REQUIRE_EQ(root, factory.parent_get());
    REQUIRE_EQ(0, root->child_count);
    REQUIRE_EQ(nullptr, root->children);
    REQUIRE_UNARY(root->payload);
    REQUIRE_EQ(::source_root, root->payload_type);
    REQUIRE_UNARY_FALSE(root->parent);
    REQUIRE_UNARY_FALSE(strcmp("hello.ts", ((const cl_node_source_root*)root->payload)->file_name));
}

TEST_CASE("source_root value") {
    auto m = b::allocator{ malloc, free };

    SUBCASE("empty") {
        auto factory = m.source_root("");

        auto root = factory.root_get();
        REQUIRE_UNARY(root);
        REQUIRE_EQ(root, factory.parent_get());
        REQUIRE_EQ(0, root->child_count);
        REQUIRE_EQ(nullptr, root->children);
        REQUIRE_UNARY(root->payload);
        REQUIRE_EQ(::source_root, root->payload_type);
        REQUIRE_UNARY_FALSE(root->parent);
        REQUIRE_UNARY_FALSE(strcmp("", ((const cl_node_source_root*)root->payload)->file_name));
    }

    SUBCASE("null") {
        auto factory = m.source_root(nullptr);

        auto root = factory.root_get();
        REQUIRE_UNARY(root);
        REQUIRE_EQ(root, factory.parent_get());
        REQUIRE_EQ(0, root->child_count);
        REQUIRE_EQ(nullptr, root->children);
        REQUIRE_UNARY(root->payload);
        REQUIRE_EQ(::source_root, root->payload_type);
        REQUIRE_UNARY_FALSE(root->parent);
        REQUIRE_EQ(nullptr, ((const cl_node_source_root*)root->payload)->file_name);
    }

    SUBCASE("filled") {
        auto factory = m.source_root("hello.ts");

        auto root = factory.root_get();
        REQUIRE_UNARY(root);
        REQUIRE_EQ(root, factory.parent_get());
        REQUIRE_EQ(0, root->child_count);
        REQUIRE_EQ(nullptr, root->children);
        REQUIRE_UNARY(root->payload);
        REQUIRE_EQ(::source_root, root->payload_type);
        REQUIRE_UNARY_FALSE(root->parent);
        REQUIRE_UNARY_FALSE(strcmp("hello.ts", ((const cl_node_source_root*)root->payload)->file_name));
    }
}

TEST_SUITE_END();
