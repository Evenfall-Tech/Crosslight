#include "lang/exceptions/parsing_exception.hpp"

using namespace cl::lang::exceptions;

parsing_exception::parsing_exception(const std::string& what_arg)
    : _message{what_arg} {}

parsing_exception::parsing_exception(const char* what_arg)
    : _message{what_arg} {}

const char*
parsing_exception::what() const noexcept {
    return _message.c_str();
}
