#pragma once

#include <vector>
#include <cstddef>
#include <tuple>
#include <typeinfo>
#include <type_traits>
#include <utility>
#include "lang/language.hpp"
#include "lang/builders/builder.hpp"

struct cl_node;

namespace cl::lang::builders {

namespace {
    template <typename MaybeBuilderT>
    inline constexpr bool is_builder_v = &typeid(MaybeBuilderT) == &typeid(builder);

    template <bool... t_bs>
    inline constexpr bool is_all_v = (0 + ... + t_bs) == sizeof...(t_bs);

    template <typename... BuildersT>
    using children_type = std::enable_if_t<
        is_all_v<is_builder_v<BuildersT>...>,
        std::tuple<
            std::conditional_t<
                std::is_lvalue_reference_v<BuildersT>,
                builder &,
                builder
            >...>>;
}

template <typename... BuildersT>
children_type<BuildersT...> children(BuildersT&&... children) {
    return {std::forward<BuildersT>(children)...};
}

namespace {
    template<typename BuilderT, typename T, std::size_t... Index>
    bool check_children_at(BuilderT&& current, T const& tup, std::index_sequence<Index...> const&)
    {
        auto check_child = [current](auto child) {
            return child.root_get() == nullptr ||
                   !allocator::equal(current.allocator_get(), child.allocator_get()) ||
                   current.root_get() == nullptr || child.root_get() == nullptr;
        };

        bool ignore[] = {check_child(std::get<Index>(tup))...};
        for (size_t i = 0; i < sizeof(ignore); ++i) {
            if (!ignore[i]) {
                return false;
            }
        }

        return true;
    }

    template<typename BuilderT, typename... BuildersU>
    bool check_children(BuilderT&& current, children_type<BuildersU...> const& tup)
    {
        return check_children_at(current, tup, std::make_index_sequence<sizeof...(BuildersU)>());
    }
}

template <typename BuilderT, typename... BuildersU>
builder operator <<(BuilderT&& current, children_type<BuildersU...>&& children) {
    const size_t child_count = sizeof...(BuildersU);


    if (check_children(current, children)) {
        return { current.allocator_get(), nullptr, nullptr };
    }
    return { current.allocator_get(), nullptr, nullptr };

    /*auto* nodes = static_cast<struct cl_node*>(current.impl_get()->acquire(child_count * sizeof(struct node)));

    if (nodes == nullptr) {
        return { current.impl_get(), nullptr, nullptr };
    }

    auto* parent = current.parent_get();
    auto* root = current.root_get();
    current.root_clear();

    for (size_t i = 0; i < child_count; ++i) {
        auto* child_node = children[i].root_get();
        nodes[i] = *child_node;
    }

    parent->child_count = child_count;
    parent->children = nodes;*/
}

}