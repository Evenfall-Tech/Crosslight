#include "lang_ecmascript/visitor.hpp"
#include <algorithm>
#include <iostream>
#include <optional>
#include "lang/utils.hpp"
#include "lang/exceptions/not_implemented_parsing_exception.hpp"
#include "lang/exceptions/not_supported_parsing_exception.hpp"
#include "core/node.h"
#include "core/nodes/node_type.h"
#include "core/nodes/source_root.h"
#include "core/nodes/scope.h"
#include "core/nodes/heap_type.h"

using namespace cl::lang::ecmascript;
using namespace cl::lang::exceptions;
using list = std::list<struct cl_node*>;

template <typename T>
CL_ALWAYS_INLINE static T* acquire_entity(cl::lang::AcquireT acquire) {
    return static_cast<T*>(acquire(sizeof(T)));
}

visitor::visitor(const language_options& options)
        : _options{options},
          _root{nullptr},
          _parent{nullptr} {
}

struct cl_node*
visitor::get_root() {
    return _root;
}
