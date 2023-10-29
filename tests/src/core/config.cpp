#include "core/config.h"
#include "doctest/doctest.h"

TEST_SUITE_BEGIN("core/config");

TEST_CASE("Lifetime") {
    struct cl_config* config = cl_config_init();
    REQUIRE_NE((struct cl_config*)nullptr, config);

    size_t term = cl_config_term(nullptr);
    REQUIRE_EQ(1, term);

    term = cl_config_term(config);
    REQUIRE_EQ(1, term);
}

TEST_CASE("String") {
    struct cl_config* config = cl_config_init();
    REQUIRE_NE((struct cl_config*)nullptr, config);

    SUBCASE("Initial get") {
        const char* str = cl_config_string_get(config, "a");
        REQUIRE_EQ((const char*)nullptr, str);
    }

    SUBCASE("Initial set") {
        size_t set = cl_config_string_set(config, "b", "b");
        REQUIRE_EQ(1, set);
    }

    SUBCASE("Set and get") {
        size_t set = cl_config_string_set(config, "c", "c");
        REQUIRE_EQ(1, set);
        
        const char* str = cl_config_string_get(config, "c");
        REQUIRE_EQ(doctest::String{"c"}, str); // Without conversion doctest compares addresses.
    }

    SUBCASE("Set and set") {
        size_t set = cl_config_string_set(config, "d", "d");
        REQUIRE_EQ(1, set);
        
        const char* str = cl_config_string_get(config, "d");
        REQUIRE_EQ(doctest::String{"d"}, str);

        set = cl_config_string_set(config, "d", "a");
        REQUIRE_EQ(1, set);
        
        str = cl_config_string_get(config, "d");
        REQUIRE_EQ(doctest::String{"a"}, str);
    }

    SUBCASE("Set 0") {
        size_t set = cl_config_string_set(config, "e", nullptr);
        REQUIRE_EQ(1, set);
        
        const char* str = cl_config_string_get(config, "e");
        REQUIRE_EQ((const char*)nullptr, str);
    }

    SUBCASE("Set 0 and set value") {
        size_t set = cl_config_string_set(config, "f", nullptr);
        REQUIRE_EQ(1, set);
        
        const char* str = cl_config_string_get(config, "f");
        REQUIRE_EQ((const char*)nullptr, str);

        set = cl_config_string_set(config, "f", "a");
        REQUIRE_EQ(1, set);
        
        str = cl_config_string_get(config, "f");
        REQUIRE_EQ(doctest::String{"a"}, str);
    }

    SUBCASE("Set value and set 0") {
        size_t set = cl_config_string_set(config, "g", "a");
        REQUIRE_EQ(1, set);
        
        const char* str = cl_config_string_get(config, "g");
        REQUIRE_EQ(doctest::String{"a"}, str);

        set = cl_config_string_set(config, "g", nullptr);
        REQUIRE_EQ(1, set);
        
        str = cl_config_string_get(config, "g");
        REQUIRE_EQ((const char*)nullptr, str);
    }

    SUBCASE("Set 0 and set 0") {
        size_t set = cl_config_string_set(config, "h", nullptr);
        REQUIRE_EQ(1, set);
        
        const char* str = cl_config_string_get(config, "h");
        REQUIRE_EQ((const char*)nullptr, str);

        set = cl_config_string_set(config, "h", nullptr);
        REQUIRE_EQ(1, set);
        
        str = cl_config_string_get(config, "h");
        REQUIRE_EQ((const char*)nullptr, str);
    }

    SUBCASE("Invalid context") {
        size_t set = cl_config_string_set(nullptr, "i", "i");
        REQUIRE_EQ(0, set);
        
        const char* str = cl_config_string_get(nullptr, "i");
        REQUIRE_EQ((const char*)nullptr, str);
    }

    size_t term = cl_config_term(config);
    REQUIRE_EQ(1, term);
}

TEST_SUITE_END();
