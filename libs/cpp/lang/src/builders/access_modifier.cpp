#include "lang/builders/access_modifier.hpp"
#include "lang/builders/allocator.hpp"
#include "lang/builders/builder.hpp"
#include "core/nodes/node_type.h"

namespace b = cl::lang::builders;

b::builder
b::access_modifier(const b::allocator& m, enum cl_node_access_modifier_type type) {
    return m.access_modifier(type);
}

b::builder
b::allocator::access_modifier(enum cl_node_access_modifier_type type) const {
    auto* payload = static_cast<struct cl_node_access_modifier*>(acquire(sizeof(struct cl_node_access_modifier)));

    if (payload == nullptr) {
        return { *this, nullptr, nullptr };
    }

    payload->type = type;

    return builder::from_payload(*this, payload, ::access_modifier);
}
