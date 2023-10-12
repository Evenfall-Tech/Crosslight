#pragma once

#include <cstddef>
#include "lang/def_visibility.hpp"

namespace cl::lang {

/**
 * @brief A few utility functions for language implementations.
 */
class CL_API_OBJ utils {
public:
    /**
     * @brief Acquire a copy of a valid string.
     */
    static char *string_duplicate(const char *value, void*(* acquire)(std::size_t));
};

} // namespace cl::lang
