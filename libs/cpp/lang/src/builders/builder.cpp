#include "lang/builders/builder.hpp"
#include "lang/builders/allocator.hpp"
#include "core/node.h"

namespace b = cl::lang::builders;

class b::builder::impl {
public:
    AcquireT acquire;
    ReleaseT release;
};

b::builder::builder(const allocator& m, struct cl_node *root, struct cl_node *parent)
    : _root{root}, _parent{parent}, _impl{new (static_cast<impl*>(m.acquire(sizeof(impl)))) impl{ m.acquire, m.release }} {}

b::builder::~builder() {
    if (_root == nullptr) {
        return;
    }

    cl_node_term(_root, 1, _impl->release);
    _root = nullptr;

    auto r = _impl->release;
    r(_impl);
    _impl = nullptr;
}

b::builder&
b::operator<<(builder& current, builder&& child) {
    if (child._root == nullptr ||
        child._impl->release != current._impl->release ||
        child._impl->acquire != current._impl->acquire) {
        return current;
    }

    auto* node = child._root;
    child._root = nullptr; // Prevent destructor tree termination.

    node->parent = current._parent;

    current._parent->child_count = 1;
    current._parent->children = node;

    current._parent = node;

    return current;
}

b::builder&
b::operator<<(builder&& current, builder&& child) {
    if (child._root == nullptr ||
        child._impl->release != current._impl->release ||
        child._impl->acquire != current._impl->acquire) {
        return current;
    }

    auto* node = child._root;
    child._root = nullptr; // Prevent destructor tree termination.

    node->parent = current._parent;

    current._parent->child_count = 1;
    current._parent->children = node;

    current._parent = node;

    return current;
}

b::builder
b::builder::from_payload(const allocator &m, void *payload, size_t payload_type) {
    auto* node = static_cast<struct cl_node*>(m.acquire(sizeof(struct cl_node)));

    if (node == nullptr) {
        return { m, nullptr, nullptr };
    }

    node->children = nullptr;
    node->child_count = 0;
    node->parent = nullptr;
    node->payload_type = payload_type;
    node->payload = payload;

    return { m, node, node };
}
