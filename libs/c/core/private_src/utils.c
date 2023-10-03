#include "core/utils.h"
#include <stdlib.h>
#include <string.h>

char*
cl_utils_string_duplicate(const char* value) {
    if (value == 0) {
        return 0;
    }

    size_t length = strlen(value);
    char* result = malloc(length + 1);

    if (result == 0) {
        return 0;
    }

    return strcpy(result, value);
}
