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

    template <std::size_t Index, typename T>
    struct children_node {
        inline
        constexpr auto & operator()(std::integral_constant<std::size_t, Index>) noexcept
        { return _data; }

        inline
        constexpr auto & operator()(std::integral_constant<std::size_t, Index>) const noexcept
        { return _data; }

        inline constexpr T get(std::integral_constant<std::size_t, Index>) noexcept
        { return std::forward<T>(_data); }

        std::conditional_t<
            std::is_lvalue_reference_v<T>,
            std::remove_reference_t<T> &,
            std::remove_reference_t<T>
        > _data;
    };

    template <typename ... Ts>
    struct children_base : Ts... {
        using Ts::operator()...;
        using Ts::get...;
    };

    template <typename BuilderT>
    using child_type = std::conditional_t<
        std::is_lvalue_reference_v<BuilderT>,
        builder &,
        builder &&
    >;

    template <typename ... Ts, std::size_t ... Indexes>
    constexpr children_base<children_node<Indexes, child_type<Ts>>...>
    get_children_base(
        std::index_sequence<Indexes...> = std::make_index_sequence<sizeof...(Ts)>{}) noexcept;

    template <typename ... Ts>
    using children_base_t = decltype(get_children_base<Ts...>(std::make_index_sequence<sizeof...(Ts)>{}));

    template <typename ... Ts>
    struct children_type : children_base_t<Ts...> {
        using base = children_base_t<Ts...>;
        using base::operator();

        constexpr explicit children_type(Ts && ... data) :
            base{std::forward<Ts>(data)...} {}

        template <std::size_t Index>
        constexpr auto & at() const noexcept
        { return base::operator()(std::integral_constant<std::size_t, Index>{}); }

        template <std::size_t Index>
        constexpr auto && get() noexcept
        { return base::get(std::integral_constant<std::size_t, Index>{}); }
    };

    template <typename ... Ts>
    children_type(Ts && ...) -> children_type<Ts&&...>;

    template <typename MaybeBuilderT>
    inline constexpr bool is_builder_v = &typeid(MaybeBuilderT) == &typeid(builder);

    template <bool... BooleanTs>
    inline constexpr bool is_all_v = (0 + ... + BooleanTs) == sizeof...(BooleanTs);
}

template <typename... BuilderTs>
children_type<BuilderTs...> children(BuilderTs&&... children) {
    (..., children.prevent_term());
    return children_type<BuilderTs...>{std::forward<BuilderTs>(children)...};
}

namespace {
    template <std::size_t Index, typename ChildrenT>
    constexpr auto && child_get(ChildrenT && children) noexcept {
        return children.template get<Index>();
    }

    template<typename BuilderT, typename ChildrenT, std::size_t... Index>
    bool check_children_at(BuilderT&& current, ChildrenT&& children, std::index_sequence<Index...> const&) {
        auto child_valid = [&current](auto&& child) {
            return child.root_get() == nullptr || current.root_get() == nullptr ||
                   !allocator::equal(current.allocator_get(), child.allocator_get());
        };

        return (0 + ... + child_valid(child_get<Index>(children)));
    }

    template <typename T>
    struct is_children : std::false_type {};

    template <typename ... BuilderTs>
    struct is_children<std::tuple<BuilderTs...>> :
        std::bool_constant<is_all_v<is_builder_v<BuilderTs>...>> {};

    template <typename ... BuilderTs>
    struct is_children<std::tuple<BuilderTs...>&> :
        std::bool_constant<is_all_v<is_builder_v<BuilderTs>...>> {};

    template <typename ... BuilderTs>
    struct is_children<children_type<BuilderTs...>> :
        std::bool_constant<is_all_v<is_builder_v<BuilderTs>...>> {};

    template <typename ... BuilderTs>
    struct is_children<children_type<BuilderTs...> &> :
        std::bool_constant<is_all_v<is_builder_v<BuilderTs>...>> {};

    template <typename T>
    struct children_count {};

    template <typename ... Ts, template <typename ...> typename HolderT>
    struct children_count<HolderT<Ts...>> :
        std::integral_constant<std::size_t, sizeof...(Ts)> {};

    template <typename ... Ts, template <typename ...> typename HolderT>
    struct children_count<HolderT<Ts...>&> :
        std::integral_constant<std::size_t, sizeof...(Ts)> {};

    template <typename T>
    inline constexpr bool is_children_v = is_children<T>::value;

    template <typename T>
    inline constexpr std::size_t children_count_v = children_count<T>::value;

    template <typename ChildrenT, std::size_t Index>
    void attach_child_at(const struct cl_node* parent, struct cl_node* nodes, ChildrenT&& children) {
        auto&& child = child_get<Index>(children);
        auto* child_node = child.root_get();
        child_node->parent = parent;
        auto child_count = child_node->child_count;

        // Reattach children of children to the newly acquired array memory.
        auto* child_children = (struct cl_node*)child_node->children;

        if (child_children != nullptr && child_count > 0) {
            for (size_t i = 0; i < child_count; ++i) {
                (child_children + i)->parent = nodes + Index;
            }
        }

        nodes[Index] = *child_node;
        child.prevent_term();
        child.allocator_get().release(child_node);
    }

    template<typename ChildrenT, std::size_t... Index>
    void attach_children_at(
        struct cl_node* parent,
        struct cl_node* nodes,
        ChildrenT&& children,
        std::index_sequence<Index...> const&) {
        (..., attach_child_at<ChildrenT, Index>(parent, nodes, children));
    }
}

template <typename BuilderT, typename ChildrenT, typename = std::enable_if_t<is_children_v<ChildrenT>>>
builder operator <<(BuilderT&& current, ChildrenT&& children) {
    constexpr std::size_t child_count = children_count_v<ChildrenT>;
    constexpr auto indices = std::make_index_sequence<children_count_v<ChildrenT>>();

    if (check_children_at(current, children, indices)) {
        return { current.allocator_get(), nullptr, nullptr };
    }

    auto* nodes = static_cast<struct cl_node*>(current.allocator_get().acquire(child_count * sizeof(struct cl_node)));

    if (nodes == nullptr) {
        return { current.allocator_get(), nullptr, nullptr };
    }

    auto* parent = current.parent_get();
    auto* root = current.root_get();
    current.prevent_term();

    attach_children_at(parent, nodes, children, indices);

    parent->child_count = child_count;
    parent->children = nodes;

    return { current.allocator_get(), root, nodes + child_count - 1 };
}

}
