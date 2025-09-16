cmake_minimum_required(VERSION 3.20)
project(box2d_unity_shim C)

# Box2D 소스를 서브프로젝트로 추가 (경로는 환경에 맞게 수정)
set(BOX2D_ROOT "${CMAKE_CURRENT_LIST_DIR}/../box2d-main")
add_subdirectory("${BOX2D_ROOT}" "${CMAKE_BINARY_DIR}/box2d_build")

add_library(box2d_unity SHARED box2d_unity_shim.c)
target_include_directories(box2d_unity PRIVATE "${BOX2D_ROOT}/include")
target_link_libraries(box2d_unity PRIVATE box2d)
set_target_properties(box2d_unity PROPERTIES OUTPUT_NAME "box2d_unity")
