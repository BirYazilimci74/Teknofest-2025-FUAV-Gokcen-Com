cmake_minimum_required(VERSION 3.5)
project(gokcen_com_domain)

find_package(ament_cmake REQUIRED)

add_custom_target(run ALL)

add_custom_command(
    COMMAND
    dotnet build ${CMAKE_CURRENT_SOURCE_DIR}
    TARGET run POST_BUILD
    WORKING_DIRECTORY ${CMAKE_CURRENT_SOURCE_DIR}
)

install(FILES
    ${CMAKE_CURRENT_SOURCE_DIR}/bin/Debug/net8.0/GokcenCom.Domain.dll
    DESTINATION lib/GokcenCom.Domain
)

ament_package()