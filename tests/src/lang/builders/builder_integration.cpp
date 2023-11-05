#include <cstring>
#include "lang/builders/allocator.hpp"
#include "lang/builders/builder.hpp"
#include "lang/builders/builders.hpp"
#include "core/node.h"
#include "core/nodes/node_type.h"
#include "core/nodes/source_root.h"
#include "core/nodes/scope.h"
#include "core/nodes/heap_type.h"
#include "doctest/doctest.h"

namespace b = cl::lang::builders;

TEST_SUITE_BEGIN("lang/builders/builder_integration");

TEST_CASE("builder_integration simple") {
    auto m = b::allocator{ malloc, free };

    auto classes = b::children(m.heap_type("Rectangle"), m.heap_type("Circle"));
    auto ns_empty = m.scope("Empty") << m.none(); // to allow an empty namespace instead of a global one.
    auto ns_shapes = m.scope("Shapes") << classes; // node_root points to "Shapes", parent points to "Circle".
    auto factory = m.source_root("Main.cs") << b::children(ns_shapes, ns_empty); // node_root points to "Main.cs", parent points to "Empty".

    auto* node_root = factory.root_get();
    REQUIRE_UNARY(node_root);
    REQUIRE_NE(node_root, factory.parent_get());
    REQUIRE_EQ(2, node_root->child_count);
    REQUIRE_UNARY(node_root->children);
    REQUIRE_UNARY(node_root->payload);
    REQUIRE_EQ(::source_root, node_root->payload_type);
    REQUIRE_UNARY_FALSE(node_root->parent);
    REQUIRE_UNARY_FALSE(strcmp("Main.cs", ((const cl_node_source_root*)node_root->payload)->file_name));

    auto* node_empty = node_root->children + 1;
    REQUIRE_UNARY(node_empty);
    REQUIRE_EQ(node_empty, factory.parent_get());
    REQUIRE_EQ(1, node_empty->child_count);
    REQUIRE_UNARY(node_empty->children);
    REQUIRE_UNARY(node_empty->payload);
    REQUIRE_EQ(::scope, node_empty->payload_type);
    REQUIRE_EQ(node_root, node_empty->parent);
    REQUIRE_UNARY_FALSE(strcmp("Empty", ((const cl_node_scope*)node_empty->payload)->identifier));

    auto* node_empty_child = node_empty->children;
    REQUIRE_UNARY(node_empty_child);
    REQUIRE_NE(node_empty_child, factory.parent_get());
    REQUIRE_EQ(0, node_empty_child->child_count);
    REQUIRE_UNARY_FALSE(node_empty_child->children);
    REQUIRE_UNARY_FALSE(node_empty_child->payload);
    REQUIRE_EQ(::none, node_empty_child->payload_type);
    REQUIRE_EQ(node_empty, node_empty_child->parent);

    auto* node_shapes = node_root->children;
    REQUIRE_UNARY(node_shapes);
    REQUIRE_NE(node_shapes, factory.parent_get());
    REQUIRE_EQ(2, node_shapes->child_count);
    REQUIRE_UNARY(node_shapes->children);
    REQUIRE_UNARY(node_shapes->payload);
    REQUIRE_EQ(::scope, node_shapes->payload_type);
    REQUIRE_EQ(node_root, node_shapes->parent);
    REQUIRE_UNARY_FALSE(strcmp("Shapes", ((const cl_node_scope*)node_shapes->payload)->identifier));

    auto* node_rectangle = node_shapes->children;
    REQUIRE_UNARY(node_rectangle);
    REQUIRE_NE(node_rectangle, factory.parent_get());
    REQUIRE_EQ(0, node_rectangle->child_count);
    REQUIRE_UNARY_FALSE(node_rectangle->children);
    REQUIRE_UNARY(node_rectangle->payload);
    REQUIRE_EQ(::heap_type, node_rectangle->payload_type);
    REQUIRE_EQ(node_shapes, node_rectangle->parent);
    REQUIRE_UNARY_FALSE(strcmp("Rectangle", ((const cl_node_heap_type*)node_rectangle->payload)->identifier));

    auto* node_circle = node_shapes->children + 1;
    REQUIRE_UNARY(node_circle);
    REQUIRE_NE(node_circle, factory.parent_get());
    REQUIRE_EQ(0, node_circle->child_count);
    REQUIRE_UNARY_FALSE(node_circle->children);
    REQUIRE_UNARY(node_circle->payload);
    REQUIRE_EQ(::heap_type, node_circle->payload_type);
    REQUIRE_EQ(node_shapes, node_circle->parent);
    REQUIRE_UNARY_FALSE(strcmp("Circle", ((const cl_node_heap_type*)node_circle->payload)->identifier));
}

TEST_SUITE_END();