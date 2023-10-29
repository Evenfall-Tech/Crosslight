#include "lang/builders/source_root.hpp"
#include "lang/builders/allocator.hpp"
#include "lang/builders/builder.hpp"
#include "lang/utils.hpp"
#include "core/node.h"
#include "core/nodes/node_type.h"

namespace b = cl::lang::builders;

b::builder
b::source_root(const b::allocator& m, const char *filename) {
    return m.source_root(filename);
}

b::builder
b::allocator::source_root(const char *filename) const {
    auto* payload = static_cast<struct cl_node_source_root*>(acquire(sizeof(struct cl_node_source_root)));

    if (payload == nullptr) {
        return { *this, nullptr, nullptr };
    }

    payload->file_name = utils::string_duplicate(filename, acquire);

    return builder::from_payload(*this, payload, ::source_root);
}
