#include <iostream>

#include <memory>
#include "lang/config.hpp"
#include "lang/plugin.hpp"

int main(int argc, char **argv) {
    auto config = std::make_unique<cl::lang::config>();

    config->string_set("b", "value");

    //std::cout << config->string_get("a").value_or("") << std::endl;
    //std::cout << config->string_get("b").value_or("") << std::endl;

    cl::lang::plugin lib("./", "cl_lang_typescript");

    auto init = lib.get_function<void*(const struct cl_config*)>("language_init");
    auto term = lib.get_function<std::size_t(const void*)>("language_term");

    void* lang = init(config->handle_get());
    std::cout << lang << std::endl;
    term(lang);

    return 0;
}