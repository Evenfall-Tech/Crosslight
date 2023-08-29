set(
    CL_DOTNET_DIR ""
    CACHE PATH "Folder containing the dotnet executable."
)

if(CL_DOTNET_DIR)
    find_program(
        CL_DOTNET_COMMAND dotnet
        HINTS ${CL_DOTNET_DIR}
        DOC "Path to the dotnet program."
    )
else()
    find_program(
        CL_DOTNET_COMMAND dotnet
        DOC "Path to the dotnet program."
    )
endif()

if(CL_DOTNET_COMMAND)
    message(STATUS "Configuring .NET projects...")
    message(VERBOSE "Dotnet executable at ${CL_DOTNET_COMMAND}")
    message(VERBOSE "Compiling for system ${CMAKE_SYSTEM_NAME}")
    message(VERBOSE "Compiling for processor ${CMAKE_SYSTEM_PROCESSOR}")

    if(NOT CL_DOTNET_RID)
        if("${CMAKE_SYSTEM_NAME}" MATCHES "Linux")
            if("${CMAKE_SYSTEM_PROCESSOR}" MATCHES "x86_64")
                set(CL_DOTNET_RID "linux-x64" CACHE STRING "Dotnet target RuntimeIdentifier.")
            elseif("${CMAKE_SYSTEM_PROCESSOR}" MATCHES "x86")
                set(CL_DOTNET_RID "linux-x86" CACHE STRING "Dotnet target RuntimeIdentifier.")
            else()
                message(NOTICE "Unsupported architecture ${CMAKE_SYSTEM_NAME} ${CMAKE_SYSTEM_PROCESSOR}.")
            endif()
        else()
            message(NOTICE "Unsupported architecture ${CMAKE_SYSTEM_NAME} ${CMAKE_SYSTEM_PROCESSOR}.")
        endif()
    endif()

    if(CL_DOTNET_RID)
        add_subdirectory("lang_csharp_ref")
    endif()
else()
    message(STATUS "dotnet executable not found. Specify directory with CL_DOTNET_DIR if required.")
endif()
