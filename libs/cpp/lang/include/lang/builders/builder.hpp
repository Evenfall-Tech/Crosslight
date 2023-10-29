#pragma once

#include <cstddef>
#include <type_traits>
#include "lang/language.hpp"

struct cl_node;

namespace cl::lang::builders {

class allocator;
class builder;

/**
 * @brief Add the child tree to the current tree.
 * 
 * @param[in] current Current node tree to add child to.
 * @param[in] child Child node tree to add to the current tree.
 * @tparam BuilderT Type of current node tree.
 * @tparam BuilderU Type of child node tree.
 * 
 * @warning @p current and @p child builders are invalidated and shouldn't be used after this call.
 */
template <typename BuilderT, typename BuilderU>
std::enable_if_t<
    std::is_same_v<std::remove_cv_t<std::remove_reference_t<BuilderT>>, builder> &&
    std::is_same_v<std::remove_cv_t<std::remove_reference_t<BuilderU>>, builder>,
    builder>
operator <<(BuilderT&& current, BuilderU&& child);

class builder {
public:
    class impl; // Use PImpl concept to hide raw alloc and free.

    builder(const allocator& m, struct cl_node* root, struct cl_node* parent);
    builder(const impl* m, struct cl_node* root, struct cl_node* parent);
    static builder from_payload(const allocator& m, void* payload, size_t payload_type);
    virtual ~builder();

    builder& operator =(builder&& other) noexcept; // Move assignment
    builder(builder&& other) noexcept; // Move constructor
    builder& operator =(const builder& other) = delete; // Copy assignment
    builder(builder& other) = delete; // Copy constructor

    struct cl_node* root_get();
    struct cl_node* parent_get();
    const impl* impl_get();
    static bool impl_equal(const impl* left, const impl* right);
    void root_clear();

private:
    struct cl_node* _root;
    struct cl_node* _parent;
    impl* _impl;
};

}
