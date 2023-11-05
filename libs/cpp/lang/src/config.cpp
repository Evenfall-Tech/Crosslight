#include "lang/config.hpp"
#include "core/config.h"

using namespace cl::lang;

config::config(struct cl_config* handle) :
    _handle(handle) {}

config::config() {
    _handle = cl_config_init();
}

config::~config() {
    cl_config_term(_handle);
}

const struct cl_config*
config::handle_get() const {
    return _handle;
}

std::optional<std::string>
config::string_get(const std::string& key) {
    auto result = cl_config_string_get(_handle, key.c_str());

    if (result == nullptr) {
        return {};
    }

    return result;
}

bool
config::string_set(const std::string& key, const std::string& value) {
    auto result = cl_config_string_set(_handle, key.c_str(), value.c_str());

    return (bool)result;
}
