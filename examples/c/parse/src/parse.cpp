#include <iostream>

#include "lang/plugin.hpp"

int main(int argc, char **argv) {
    cl::lang::plugin lib("./", "cl_lang_ts");

    auto init = lib.get_function<int()>("language_init");
    std::cout << init() << std::endl;

    return 0;
}