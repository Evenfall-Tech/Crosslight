#include "lang/builders/builder.hpp"
#include "lang/builders/allocator.hpp"
#include "core/node.h"

namespace b = cl::lang::builders;

class b::builder::impl {
public:
    AcquireT acquire;
    ReleaseT release;
};

b::builder::builder(
    const allocator& m,
    struct cl_node* root,
    struct cl_node* parent)
    : _root{root}, _parent{parent}, _impl{new (static_cast<impl*>(m.acquire(sizeof(impl)))) impl{ m.acquire, m.release }} {}

b::builder::builder(
    const impl* m,
    struct cl_node* root,
    struct cl_node* parent)
    : _root{root}, _parent{parent}, _impl{new (static_cast<impl*>(m->acquire(sizeof(impl)))) impl{ m->acquire, m->release }} {}

b::builder&
b::builder::operator =(b::builder &&other) noexcept {
    // Clear current _root and _impl.
    if (_impl != nullptr) {
        if (!impl_equal(_impl, other._impl)) {
            return *this;
        }

        auto r = _impl->release;

        if (_root != nullptr) {
            if (cl_node_term(_root, 1, r) == 0) {
                return *this;
            }
        }

        r(_impl);
    }

    // _impl was cleared. _root was cleared.
    _impl = new (static_cast<impl*>(other._impl->acquire(sizeof(impl))))
        impl{ other._impl->acquire, other._impl->release };
    _root = other._root;
    _parent = other._parent;
    other._root = nullptr;
    other._parent = nullptr;

    return *this;
}

b::builder::builder(b::builder &&other) noexcept
    : _root{other._root}, _parent{other._parent}, _impl{
        new (static_cast<impl*>(other._impl->acquire(sizeof(impl))))
        impl{ other._impl->acquire, other._impl->release }
    } {
    other._root = nullptr;
}

b::builder::~builder() {
    if (_impl == nullptr) {
        return;
    }

    auto r = _impl->release;

    r(_impl);
    _impl = nullptr;

    if (_root == nullptr) {
        return;
    }

    cl_node_term(_root, 1, r);
    _root = nullptr;
}

template <typename BuilderT, typename BuilderU>
using e = std::enable_if_t<
        std::is_same_v<std::remove_cv_t<std::remove_reference_t<BuilderT>>, b::builder> &&
        std::is_same_v<std::remove_cv_t<std::remove_reference_t<BuilderU>>, b::builder>,
        b::builder>;

template <typename BuilderT, typename BuilderU>
e<BuilderT, BuilderU>
b::operator <<(BuilderT&& current, BuilderU&& child) {
    if (child.root_get() == nullptr ||
        !b::builder::impl_equal(current.impl_get(), child.impl_get()) ||
        current.root_get() == nullptr || child.root_get() == nullptr) {
        return { current.impl_get(), nullptr, nullptr };
    }

    auto* node = child.root_get();
    auto* parent = current.parent_get();
    auto* root = current.root_get();
    // Prevent destructor tree termination.
    child.root_clear();
    current.root_clear();

    node->parent = parent;

    parent->child_count = 1;
    parent->children = node;

    return { current.impl_get(), root, node };
}

template e<b::builder&&, b::builder&&>
b::operator << (b::builder&& current, b::builder&& child);
template e<b::builder&, b::builder&&>
b::operator << (b::builder& current, b::builder&& child);

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

const b::builder::impl*
b::builder::impl_get() {
    return _impl;
}

bool
b::builder::impl_equal(const impl* left, const impl* right) {
    return (left == nullptr && right == nullptr) || (
        left != nullptr &&
        right != nullptr &&
        left->acquire == right->acquire &&
        left->release == right->release);
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
b::builder::root_clear() {
    _root = nullptr;
}
