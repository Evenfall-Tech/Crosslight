#pragma once

#include "lang/exceptions/parsing_exception.hpp"

namespace cl::lang::exceptions {

class CL_API_OBJ not_implemented_parsing_exception : public parsing_exception {
public:
    explicit not_implemented_parsing_exception(const std::string& what_arg);
    explicit not_implemented_parsing_exception(const char* what_arg);
    [[nodiscard]] const char* what() const noexcept override;

protected:
    std::string _message;
};

}
