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

    auto init = lib.get_function<int()>("language_init");
    std::cout << init() << std::endl;

    return 0;
}