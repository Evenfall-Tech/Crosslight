#include "lang/builders/builder.hpp"
#include "lang/builders/builders.hpp"
#include "lang/builders/allocator.hpp"
#include "lang/language.hpp"
#include "core/node.h"

namespace b = cl::lang::builders;

b::builder::builder(
    const allocator& m,
    struct cl_node* root,
    struct cl_node* parent)
    : _root{root}, _parent{parent},
    _should_destroy{true}, _allocator{m.acquire, m.release} {}

b::builder&
b::builder::operator =(b::builder &&other) noexcept {
    if (!allocator::equal(_allocator, other._allocator)) {
        return *this;
    }
    // Clear current _root.
    if (_should_destroy) {
        auto r = _allocator.release;
        if (cl_node_term(_root, 1, r) == 0) {
            return *this;
        }
    }

    // _root was cleared.
    _root = other._root;
    _parent = other._parent;
    other._should_destroy = false;
    other._parent = nullptr;

    return *this;
}

b::builder::builder(b::builder &&other) noexcept
    : _root{other._root}, _parent{other._parent},
    _should_destroy{true}, _allocator{other._allocator.acquire, other._allocator.release} {
    other._should_destroy = false;
}

b::builder::~builder() {
    if (!_should_destroy) {
        return;
    }

    auto r = _allocator.release;
    cl_node_term(_root, 1, r);
}

template <typename BuilderT, typename BuilderU>
b::e<BuilderT, BuilderU>
b::operator <<(BuilderT&& current, BuilderU&& child) {
    if (child.root_get() == nullptr || current.root_get() == nullptr ||
        !allocator::equal(current.allocator_get(), child.allocator_get())) {
        return { current.allocator_get(), nullptr, nullptr };
    }

    auto* node = child.root_get();
    auto* parent = current.parent_get();
    auto* root = current.root_get();
    // Prevent destructor tree termination.
    child.prevent_term();
    current.prevent_term();

    node->parent = parent;

    parent->child_count = 1;
    parent->children = node;

    return { current.allocator_get(), root, node };
}

template b::builder
b::operator << (b::builder&& current, b::builder&& child);
template b::builder
b::operator << (b::builder& current, b::builder&& child);
template b::builder
b::operator << (b::builder&& current, b::builder& child);
template b::builder
b::operator << (b::builder& current, b::builder& child);

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

struct cl_node*
b::builder::root_get() {
    return _root;
}

struct cl_node*
b::builder::parent_get() {
    return _parent;
}

void
b::builder::prevent_term() {
    _should_destroy = false;
}

const b::allocator&
b::builder::allocator_get() {
    return _allocator;
}
