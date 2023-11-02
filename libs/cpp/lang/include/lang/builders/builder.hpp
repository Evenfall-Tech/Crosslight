#pragma once

#include <cstddef>
#include <type_traits>
#include "lang/builders/allocator.hpp"

struct cl_node;

namespace cl::lang::builders {

class builder;
class builders;

template <typename BuilderT, typename BuilderU>
using e = std::enable_if_t<
    std::is_same_v<std::remove_cv_t<std::remove_reference_t<BuilderT>>, builder> &&
    std::is_same_v<std::remove_cv_t<std::remove_reference_t<BuilderU>>, builder>,
    builder>;

/**
 * @brief Add the child tree to the current tree, setting tail to child.
 * 
 * @param[in] current Current node tree to add child to.
 * @param[in] child Child node tree to add to the current tree.
 * @tparam BuilderT Type of current node tree.
 * @tparam BuilderU Type of child node tree.
 * 
 * @warning @p current and @p child builders are invalidated and shouldn't be used after this call.
 */
template <typename BuilderT, typename BuilderU>
e<BuilderT, BuilderU>
operator <<(BuilderT&& current, BuilderU&& child);

class builder {
public:
    builder(const allocator& m, struct cl_node* root, struct cl_node* parent);
    static builder from_payload(const allocator& m, void* payload, size_t payload_type);
    virtual ~builder();

    builder& operator =(builder&& other) noexcept; // Move assignment
    builder(builder&& other) noexcept; // Move constructor
    builder& operator =(const builder& other) = delete; // Copy assignment
    builder(builder& other) = delete; // Copy constructor

    [[nodiscard]] struct cl_node* root_get() const;
    [[nodiscard]] struct cl_node* parent_get() const;
    [[nodiscard]] const allocator& allocator_get() const;
    void prevent_term();

private:
    struct cl_node* _root;
    struct cl_node* _parent;
    bool _should_destroy;
    allocator _allocator;
};

}
