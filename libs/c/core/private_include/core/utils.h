#pragma once

/**
 * @brief Duplicate a string value.
 * 
 * @param[in] value The string value to duplicate.
 * @return `0` if duplication failed, pointer to duplicate otherwise.
 * 
 * @note The caller is responsible for deallocating the produced duplicate.
 */
char* cl_utils_string_duplicate(const char* value);
