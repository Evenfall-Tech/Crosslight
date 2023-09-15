/**
 * @file core/message.h
 * @brief The logging message API.
 * 
 * @author Mykola Morozov
 * @copyright (c) 2023 Evenfall-Tech
 *
 * This library is released under MPL-2.0 <https://github.com/Evenfall-Tech/Crosslight/blob/master/LICENSE> license.
 */

#pragma once

#include <stdlib.h>

/**
 * @brief Logging message severity level.
 */
enum cl_message_level {
    trace = 0, /**< Most detailed messages. May contain sensitive data. */
    debug = 1, /**< Used for interactive investigation during development. */
    info = 2, /**< Track the general flow of the application.. */
    warning = 3, /**< Highlight abnormal or unexpected event in the application flow. */
    error = 4, /**< Highlight when the current flow is stopped due to a failure. */
    critical = 5, /**< Describe an unrecoverable application or system crash. */
};

/**
 * @brief Logging message.
 */
struct cl_message {
    enum cl_message_level level; /**< Severity of the message. */
    const char* text; /**< Message text with a terminator character at the end. */
};
