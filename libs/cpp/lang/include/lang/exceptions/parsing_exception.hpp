#pragma once

#include <exception>
#include <string>
#include "lang/def_visibility.hpp"

namespace cl::lang::exceptions {

class CL_API_OBJ parsing_exception : public std::exception {
public:
    explicit parsing_exception(const std::string& what_arg);
    explicit parsing_exception(const char* what_arg);
    [[nodiscard]] const char* what() const noexcept override;

protected:
    std::string _message;
};

}
