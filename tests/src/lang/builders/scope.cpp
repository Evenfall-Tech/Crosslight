#include <cstring>
#include "lang/builders/allocator.hpp"
#include "lang/builders/builder.hpp"
#include "core/node.h"
#include "core/nodes/node_type.h"
#include "core/nodes/scope.h"
#include "doctest/doctest.h"

namespace b = cl::lang::builders;

TEST_SUITE_BEGIN("lang/builders/scope");

TEST_CASE("scope lifetime") {
    auto m = b::allocator{ malloc, free };
    auto factory = m.scope("hello.world");

    auto root = factory.root_get();
    REQUIRE_UNARY(root);
    REQUIRE_EQ(root, factory.parent_get());
    REQUIRE_EQ(0, root->child_count);
    REQUIRE_EQ(nullptr, root->children);
    REQUIRE_UNARY(root->payload);
    REQUIRE_EQ(::scope, root->payload_type);
    REQUIRE_UNARY_FALSE(root->parent);
    REQUIRE_UNARY_FALSE(strcmp("hello.world", ((const cl_node_scope*)root->payload)->identifier));
}

TEST_CASE("scope value") {
    auto m = b::allocator{ malloc, free };

    SUBCASE("empty") {
        auto factory = m.scope("");

        auto root = factory.root_get();
        REQUIRE_UNARY(root);
        REQUIRE_EQ(root, factory.parent_get());
        REQUIRE_EQ(0, root->child_count);
        REQUIRE_EQ(nullptr, root->children);
        REQUIRE_UNARY(root->payload);
        REQUIRE_EQ(::scope, root->payload_type);
        REQUIRE_UNARY_FALSE(root->parent);
        REQUIRE_UNARY_FALSE(strcmp("", ((const cl_node_scope*)root->payload)->identifier));
    }

    SUBCASE("null") {
        auto factory = m.scope(nullptr);

        auto root = factory.root_get();
        REQUIRE_UNARY(root);
        REQUIRE_EQ(root, factory.parent_get());
        REQUIRE_EQ(0, root->child_count);
        REQUIRE_EQ(nullptr, root->children);
        REQUIRE_UNARY(root->payload);
        REQUIRE_EQ(::scope, root->payload_type);
        REQUIRE_UNARY_FALSE(root->parent);
        REQUIRE_EQ(nullptr, ((const cl_node_scope*)root->payload)->identifier);
    }

    SUBCASE("filled") {
        auto factory = m.scope("hello.world");

        auto root = factory.root_get();
        REQUIRE_UNARY(root);
        REQUIRE_EQ(root, factory.parent_get());
        REQUIRE_EQ(0, root->child_count);
        REQUIRE_EQ(nullptr, root->children);
        REQUIRE_UNARY(root->payload);
        REQUIRE_EQ(::scope, root->payload_type);
        REQUIRE_UNARY_FALSE(root->parent);
        REQUIRE_UNARY_FALSE(strcmp("hello.world", ((const cl_node_scope*)root->payload)->identifier));
    }
}

TEST_SUITE_END();
