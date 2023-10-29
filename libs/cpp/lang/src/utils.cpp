#include "lang/utils.hpp"
#include <cstring>

using namespace cl::lang;

char*
utils::string_duplicate(const char *value, AcquireT acquire) {
    if (value == nullptr) {
        return nullptr;
    }

    size_t length = strlen(value);
    auto* result = static_cast<char *>(acquire(length + 1));

    if (result == nullptr) {
        return nullptr;
    }

    result[length] = 0;
    return strcpy(result, value);
}
