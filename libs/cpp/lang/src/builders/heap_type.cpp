#include "lang/builders/heap_type.hpp"
#include "lang/builders/allocator.hpp"
#include "lang/builders/builder.hpp"
#include "lang/utils.hpp"
#include "core/node.h"
#include "core/nodes/node_type.h"

namespace b = cl::lang::builders;

b::builder
b::heap_type(const b::allocator& m, const char *identifier) {
    return m.heap_type(identifier);
}

b::builder
b::allocator::heap_type(const char* identifier) const {
    auto* payload = static_cast<struct cl_node_heap_type*>(acquire(sizeof(struct cl_node_heap_type)));

    if (payload == nullptr) {
        return { *this, nullptr, nullptr };
    }

    payload->identifier = utils::string_duplicate(identifier, acquire);

    return builder::from_payload(*this, payload, ::heap_type);
}
