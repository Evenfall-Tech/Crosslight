#pragma once

#include <vector>
#include <cstddef>
#include <tuple>
#include <typeinfo>
#include <type_traits>
#include <utility>
#include "core/node.h"
#include "lang/language.hpp"
#include "lang/builders/builder.hpp"

namespace cl::lang::builders {

namespace {
    template <typename MaybeBuilderT>
    inline constexpr bool is_builder_v = &typeid(MaybeBuilderT) == &typeid(builder);

    template <bool... t_bs>
    inline constexpr bool is_all_v = (0 + ... + t_bs) == sizeof...(t_bs);

    template <typename... BuilderTs>
    using children_type = std::enable_if_t<
        is_all_v<is_builder_v<BuilderTs>...>,
        std::tuple<BuilderTs&&...>>;
}

template <typename... BuilderTs>
children_type<BuilderTs...> children(BuilderTs&&... children) {
    return {std::forward<BuilderTs>(children)...};
}

namespace {
    template<typename BuilderT, typename T, std::size_t... Index>
    bool check_children_at(BuilderT&& current, T const& tup, std::index_sequence<Index...> const&)
    {
        auto check_child = [&current](auto& child) {
            return child.root_get() == nullptr ||
                   !allocator::equal(current.allocator_get(), child.allocator_get()) ||
                   current.root_get() == nullptr || child.root_get() == nullptr;
        };

        bool ignore[] = {check_child(std::get<Index>(tup))...};
        for (std::size_t i = 0; i < sizeof(ignore); ++i) {
            if (!ignore[i]) {
                return false;
            }
        }

        return true;
    }

    template <typename T>
    struct is_children : std::false_type {};

    template <typename ... BuilderTs>
    struct is_children<std::tuple<BuilderTs...>> :
        std::bool_constant<is_all_v<is_builder_v<BuilderTs>...>>
    {};

    template <typename T>
    struct children_count {};

    template <typename ... Ts, template <typename ...> class HolderT>
    struct children_count<HolderT<Ts...>> :
        std::integral_constant<std::size_t, sizeof...(Ts)>
    {};

    template <typename T>
    inline constexpr bool is_children_v = is_children<T>::value;

    template <typename T>
    inline constexpr std::size_t children_count_v = children_count<T>::value;

    template <typename ChildrenT, std::size_t Index>
    struct cl_node* attach_child_at(const struct cl_node* parent, struct cl_node* nodes, ChildrenT const& children) {
        auto& child = std::get<Index>(children);
        auto* child_node = child.root_get();
        child_node->parent = parent;
        nodes[Index] = *child_node;
        child.root_clear();
        child.allocator_get().release(child_node);
        return child_node;
    }

    template<typename ChildrenT, std::size_t... Index>
    struct cl_node* attach_children_at(struct cl_node* parent, struct cl_node* nodes, ChildrenT const& children, std::index_sequence<Index...> const&)
    {
        struct cl_node* roots[] = {attach_child_at<ChildrenT, Index>(parent, nodes, children)...};
        return roots[children_count_v<ChildrenT> - 1];
    }
}

template <typename BuilderT, typename ChildrenT, typename = std::enable_if_t<is_children_v<ChildrenT>>>
builder operator <<(BuilderT&& current, ChildrenT&& children) {
    const std::size_t child_count = children_count_v<ChildrenT>;
    auto indices = std::make_index_sequence<children_count_v<ChildrenT>>();

    if (check_children_at(current, children, indices)) {
        return { current.allocator_get(), nullptr, nullptr };
    }

    auto* nodes = static_cast<struct cl_node*>(current.allocator_get().acquire(child_count * sizeof(struct cl_node)));

    if (nodes == nullptr) {
        return { current.allocator_get(), nullptr, nullptr };
    }

    auto* parent = current.parent_get();
    auto* root = current.root_get();
    current.root_clear();

    attach_children_at(parent, nodes, children, indices);

    parent->child_count = child_count;
    parent->children = nodes;

    return { current.allocator_get(), root, nodes + child_count - 1 };
}

}