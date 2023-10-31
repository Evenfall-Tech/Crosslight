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

    template<typename BuilderT, typename ChildrenT>
    bool check_children(BuilderT&& current, const ChildrenT& tup)
    {
        return check_children_at(current, tup, std::make_index_sequence<children_count_v<ChildrenT>>());
    }
}

template <typename BuilderT, typename ChildrenT, typename = std::enable_if_t<is_children_v<ChildrenT>>>
builder operator <<(BuilderT&& current, ChildrenT&& children) {
    const std::size_t child_count = children_count_v<ChildrenT>;

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

    for (std::size_t i = 0; i < child_count; ++i) {
        auto* child_node = children[i].root_get();
        nodes[i] = *child_node;
    }

    parent->child_count = child_count;
    parent->children = nodes;*/
}

}