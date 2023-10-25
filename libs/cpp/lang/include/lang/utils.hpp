#pragma once

#include <cstddef>
#include "lang/def_visibility.hpp"
#include "lang/language.hpp"

namespace cl::lang {

/**
 * @brief A few utility functions for language implementations.
 */
class CL_API_OBJ utils {
public:
    /**
     * @brief Acquire a copy of a valid string.
     *
     * @param[in] value The value to duplicate.
     * @param[in] acquire Memory allocator function.
     */
    static char *string_duplicate(const char *value, AcquireT acquire);
};

} // namespace cl::lang
