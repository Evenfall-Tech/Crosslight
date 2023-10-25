#pragma once

#include <cstddef>
#include "lang/language.hpp"

struct cl_node;

namespace cl::lang::builders {

class allocator;
class builder;

builder& operator <<(builder& current, builder&& child);
builder& operator <<(builder&& current, builder&& child);

class builder {
public:
    builder(const allocator& m, struct cl_node* root, struct cl_node* parent);
    static builder from_payload(const allocator& m, void* payload, size_t payload_type);
    virtual ~builder();

    friend builder& cl::lang::builders::operator <<(builder& current, builder&& child);
    friend builder& cl::lang::builders::operator <<(builder&& current, builder&& child);

private:
    class impl; // Use PImpl concept to hide raw alloc and free.

    cl_node* _root;
    cl_node* _parent;
    impl* _impl;
};

}
