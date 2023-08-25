#pragma once

#include <stdlib.h>
#include "core/message.h"

/**
 * @brief Function execution result. An alternative for error codes and exceptions.
 */
struct cl_result {
    size_t code; /**< Return code, posix/win32/http. */
    const void* data; /**< Data, produced by the function. */
    const struct cl_message* messages; /**< Messages, produced during function execution. */
    size_t message_count; /**< Number of supplementary messages. */
};
