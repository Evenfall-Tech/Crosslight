#include "lang_ecmascript/ParserEsBase.hpp"
#include "ParserEs.h"

using namespace antlr4;
using namespace cl::lang::ecmascript;

ParserEsBase::ParserEsBase(TokenStream *input)
        : Parser(input) {
}

size_t
ParserEsBase::getNextTokenType() {
    auto token = _input->LT(1);

    if (token) {
        return token->getType();
    }

    return 0;
}

size_t
ParserEsBase::getPrevTokenType() {
    auto token = _input->LT(-1);

    if (token) {
        return token->getType();
    }

    return 0;
}

bool
ParserEsBase::nextTextEq(const char *str) {
    auto token = _input->LT(1);

    if (token) {
        return token->getText() == str;
    }

    return false;
}

bool
ParserEsBase::prevTextEq(const char *str) {
    auto token = _input->LT(-1);

    if (token) {
        return token->getText() == str;
    }

    return false;
}

bool
ParserEsBase::isValidExpressionStatement() {
    auto token = _input->LT(1);

    if (token) {
        auto type = token->getType();

        // Check for `async [no LineTerminator here] function` separately.
        if (type == ParserEs::AsyncKeyword) {
            auto index = token->getTokenIndex();
            auto size = _input->size();
            Token* maybe_line_terminator = nullptr;
            size_t i = index;

            for (; i < size; ++i) {
                maybe_line_terminator = _input->get(i);

                if (!maybe_line_terminator) {
                    continue;
                }

                if (maybe_line_terminator->getChannel() != Token::HIDDEN_CHANNEL) {
                    break;
                }

                if (maybe_line_terminator->getType() == ParserEs::LineTerminator) {
                    return true;
                }
            }

            // Now `i` points to the first non-hidden token.
            return !maybe_line_terminator || maybe_line_terminator->getType() != ParserEs::FunctionKeyword;
        }

        return
            type != ParserEs::LeftBracesLiteral &&
            type != ParserEs::FunctionKeyword &&
            type != ParserEs::ClassKeyword &&
            (type != ParserEs::LetKeyword || !_input->LT(2) || _input->LT(2)->getType() != ParserEs::LeftBracketsLiteral);
    }

    return false;
}

bool
ParserEsBase::isNoLineTerminatorAfterTerminal(size_t terminal_type) {
    if (!_input) {
        return true;
    }

    auto current_index = _input->index();
    auto current_size = _input->size();

    Token* token = nullptr;
    Token* maybe_line_terminator = nullptr;

    // Find arrow punctuator.
    for (size_t i = current_index; i < current_size; ++i) {
        token = _input->get(i);

        if (token && token->getType() == terminal_type) {
            // Traverse forward, while we have hidden channel.
            for (size_t j = i + 1; j < current_size; ++j) {
                maybe_line_terminator = _input->get(j);

                if (!maybe_line_terminator) {
                    continue;
                }

                if (maybe_line_terminator->getChannel() != Token::HIDDEN_CHANNEL) {
                    break;
                }

                if (maybe_line_terminator->getType() == ParserEs::LineTerminator) {
                    return false;
                }
            }

            return true;
        }
    }

    return true;
}

bool
ParserEsBase::isNoLineTerminatorBeforeTerminal(size_t terminal_type) {
    if (!_input) {
        return true;
    }

    auto current_index = _input->index();
    auto current_size = _input->size();

    Token* token = nullptr;
    Token* maybe_line_terminator = nullptr;

    // Find arrow punctuator.
    for (size_t i = current_index; i < current_size; ++i) {
        token = _input->get(i);

        if (token && token->getType() == terminal_type) {
            // Traverse back, while we have hidden channel.
            for (size_t j = i - 1; j >= current_index; --j) {
                maybe_line_terminator = _input->get(j);

                if (!maybe_line_terminator) {
                    continue;
                }

                if (maybe_line_terminator->getChannel() != Token::HIDDEN_CHANNEL) {
                    break;
                }

                if (maybe_line_terminator->getType() == ParserEs::LineTerminator) {
                    return false;
                }
            }

            return true;
        }
    }

    return true;
}

bool
ParserEsBase::isNoLineTerminatorArrowFunction() {
    if (!_input) {
        return true;
    }

    auto current_index = _input->index();
    auto current_size = _input->size();

    Token* token = nullptr;
    Token* maybe_line_terminator = nullptr;

    // Find arrow punctuator.
    for (size_t i = current_index; i < current_size; ++i) {
        token = _input->get(i);

        if (token && token->getType() == ParserEs::ArrowPunctuator) {
            // Traverse back, while we have hidden channel.
            for (size_t j = i - 1; j >= current_index; --j) {
                maybe_line_terminator = _input->get(j);

                if (!maybe_line_terminator) {
                    continue;
                }

                if (maybe_line_terminator->getChannel() != Token::HIDDEN_CHANNEL) {
                    break;
                }

                if (maybe_line_terminator->getType() == ParserEs::LineTerminator) {
                    return false;
                }
            }

            return true;
        }
    }

    return true;
}
