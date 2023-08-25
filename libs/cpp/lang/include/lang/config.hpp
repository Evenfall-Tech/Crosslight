#pragma once

#include <optional>
#include "core/config.h"

namespace cl::lang {

/**
 * @brief Object-oriented key-value pair configuration wrapper.
 * Hierarchial keys supported through `key1/key2`.
 */
class config {
public:
    /**
     * @brief Create a new instance of the config.
     */
    config() {
        _handle = cl_config_new();
    }

    /**
     * @brief Delete an instance of the config.
     */
    ~config() {
        cl_config_delete(_handle);
    }

    /**
     * @brief Get a string value from the config based on a key.
     * 
     * @param[in] key The key to get the value for.
     * @return empty object if no such key found or value is `0`, string value otherwise.
     */
    std::optional<std::string> string_get(const std::string& key) {
        auto result = cl_config_string_get(_handle, key.c_str());

        if (result == 0) {
            return {};
        }

        return result;
    }

    /**
     * @brief Set a string value in the config based on a key.
     * If the key is already present, replace the value.
     * 
     * @param[in] key The key to set the value for.
     * @param[in] value The value to set. Can be 0.
     * @return `false` if setting value for key failed, `true` otherwise.
     */
    bool string_set(const std::string& key, const std::string& value) {
        auto result = cl_config_string_set(_handle, key.c_str(), value.c_str());

        return (bool)result;
    }

private:
    struct cl_config* _handle;
};

}
