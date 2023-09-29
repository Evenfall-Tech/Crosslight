#pragma once

#include <string>
#include <optional>
#include "lang/def_visibility.hpp"

struct cl_config;

namespace cl::lang {

/**
 * @brief Object-oriented key-value pair configuration wrapper.
 * Hierarchial keys supported through `key1/key2`.
 */
class CL_API config {
public:
    /**
     * @brief Create a new instance of the config.
     * 
     * @param[in] handle C library config handle.
     * 
     * @warning @p handle must have been created with @ref cl_config_init().
     * The C++ class terminates it inside the destructor.
     */
    config(struct cl_config* handle);

    /**
     * @brief Create a new instance of the config.
     */
    config();

    /**
     * @brief Delete an instance of the config.
     */
    ~config();

    /**
     * @brief Return the C library config handle.
     */
    const struct cl_config* handle_get() const;

    /**
     * @brief Get a string value from the config based on a key.
     * 
     * @param[in] key The key to get the value for.
     * @return empty object if no such key found or value is `0`, string value otherwise.
     */
    std::optional<std::string> string_get(const std::string& key);

    /**
     * @brief Set a string value in the config based on a key.
     * If the key is already present, replace the value.
     * 
     * @param[in] key The key to set the value for.
     * @param[in] value The value to set. Can be 0.
     * @return `false` if setting value for key failed, `true` otherwise.
     */
    bool string_set(const std::string& key, const std::string& value);

private:
    struct cl_config* _handle;
};

} // namespace cl::lang
