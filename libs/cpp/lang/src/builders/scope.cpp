#include "lang/builders/scope.hpp"
#include "lang/builders/allocator.hpp"
#include "lang/builders/builder.hpp"
#include "lang/utils.hpp"
#include "core/node.h"
#include "core/nodes/node_type.h"

namespace b = cl::lang::builders;

b::builder
b::scope(const b::allocator& m, const char *identifier) {
    return m.scope(identifier);
}

b::builder
b::allocator::scope(const char* identifier) const {
    auto* payload = static_cast<struct cl_node_scope*>(acquire(sizeof(struct cl_node_scope)));

    if (payload == nullptr) {
        return { *this, nullptr, nullptr };
    }

    payload->identifier = utils::string_duplicate(identifier, acquire);

    return builder::from_payload(*this, payload, ::scope);
}
