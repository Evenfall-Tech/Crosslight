#include "lang/exceptions/not_supported_parsing_exception.hpp"

using namespace cl::lang::exceptions;

not_supported_parsing_exception::not_supported_parsing_exception(const std::string& what_arg)
    : parsing_exception{what_arg} {}

not_supported_parsing_exception::not_supported_parsing_exception(const char* what_arg)
    : parsing_exception{what_arg} {}

const char*
not_supported_parsing_exception::what() const noexcept {
    return _message.c_str();
}
