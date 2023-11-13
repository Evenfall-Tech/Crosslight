#pragma once

#include "lang/language.hpp"

namespace cl::lang {

/**
 * @brief Specifies the processing behavior for unsupported nodes.
 */
enum unsupported_behavior_type {
    /**
     * @brief Generate a runtime-dependent error and terminate the parsing process.
     */
    type_throw = 0,
    /**
     * @brief Pass unsupported nodes, but parse everything else, including their children. May break the syntax.
     */
    type_pass = 1,
    /**
     * @brief Pass unsupported nodes and children. Should keep the syntax correct unless it's an important node.
     */
    type_skip = 2,
};

/**
 * @brief Base type for language-specific parsing options.
 */
class language_options_base {
public:
    /**
     * @brief A delegate to allocate a certain amount of memory.
     */
    AcquireT acquire;
    /**
     * @brief A delegate to free an allocated chunk of memory.
     */
    ReleaseT release;
    /**
     * @brief Whether unsupported nodes and (optionally) their children should be parsed without a payload.
     *
     * On error terminates, potentially causing a memory leak.
     */
    unsupported_behavior_type unsupported_behavior;
};

} // namespace cl::lang
