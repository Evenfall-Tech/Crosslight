#include "core/config.h"
#include "string.h"
#include "core/utils.h"

struct cl_config_kvp {
    char* key;
    char* value;
};

struct cl_config {
    struct cl_config_kvp* items;
    size_t item_count;
};

struct cl_config*
cl_config_init() {
    struct cl_config* result = malloc(sizeof(struct cl_config));

    if (result == 0) {
        return 0;
    }

    result->items = 0;
    result->item_count = 0;

    return result;
}

size_t
cl_config_term(struct cl_config* config) {
    if (config == 0) {
        return 1;
    }

    if (config->items != 0 && config->item_count > 0) {
        for (size_t i = 0; i < config->item_count; ++i) {
            free(config->items[i].key);
            free(config->items[i].value);
        }

        config->item_count = 0;
        free(config->items);
    }

    free(config);

    return 1;
}

const char*
cl_config_string_get(const struct cl_config* context, const char* key) {
    if (context == 0 || key == 0) {
        return 0;
    }

    // If `context->item_count == 0`, lookup is skipped, so no worries about unallocated item array.
    for (size_t i = 0; i < context->item_count; ++i) {
        if (context->items[i].key == 0) {
            continue;
        }

        int compare = strcmp(key, context->items[i].key);

        if (compare == 0) { // A match was found.
            return context->items[i].value;
        }
        else if (compare > 0) { // The search went over the possible key position.
            return 0;
        }
    }

    return 0;
}

size_t
cl_config_string_set(struct cl_config* context, const char* key, const char* value) {
    if (context == 0 || key == 0) {
        return 0;
    }

    // Step 1: find insert position.

    size_t insert_pos; // Index of newly inserted element.
    char* value_dup; // Allocated duplicate of `key` string.

    // If `context->item_count == 0`, lookup is skipped, so no worries about unallocated item array.
    for (insert_pos = 0; insert_pos < context->item_count; ++insert_pos) {
        // Search for the existing key.
        if (context->items[insert_pos].key == 0) {
            continue; // No key should be `0`, but test for the sake of not segfaulting when it happens.
        }

        int compare = strcmp(key, context->items[insert_pos].key);

        if (compare == 0) { // A match was found.

            value_dup = cl_utils_string_duplicate(value);

            if (value_dup == 0 && value != 0) {
                return 0;
            }
            
            // If a value is present, deallocate it.
            if (context->items[insert_pos].value != 0) {
                free(context->items[insert_pos].value);
            }

            context->items[insert_pos].value = value_dup;

            return 1;
        }
        else if (compare > 0) { // The search went over the possible key position.
            // The new key should be inserted at position `insert_pos`.
            break;
        }
        // If iteration finished, `insert_pos = context->item_count`.
    }

    // Step 2: allocate a new list and move the elements over.

    char* key_dup = cl_utils_string_duplicate(key);

    if (key_dup == 0) {
        return 0;
    }

    value_dup = cl_utils_string_duplicate(value);

    if (value_dup == 0 && value != 0) {
        free(key_dup);

        return 0;
    }

    struct cl_config_kvp* list = malloc((context->item_count + 1) * sizeof(struct cl_config_kvp));

    if (list == 0) {
        free(key_dup);

        if (value_dup != 0) {
            free(value_dup);
        }

        return 0;
    }

    list[insert_pos].key = key_dup;
    list[insert_pos].value = value_dup;

    // If `context->item_count == 0`, copy is skipped, so no worries about unallocated item array.
    if (insert_pos > 0) { // Copy all previous elements of the kvp array.
        memcpy(list, context->items, insert_pos * sizeof(struct cl_config_kvp));
    }

    if (insert_pos < context->item_count) { // Copy all following elements of the kvp array.
        memcpy(list + insert_pos + 1, context->items + insert_pos, (context->item_count - insert_pos) * sizeof(struct cl_config_kvp));
    }

    // Step 3: replace old list with the new one.

    if (context->items != 0) {
        free(context->items);
    }

    context->items = list;
    ++context->item_count;

    return 1;
}
