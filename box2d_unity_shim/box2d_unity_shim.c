#include "../box2d-main/include/box2d/box2d.h"
#ifdef _WIN32
  #define B2U_API __declspec(dllexport)
#else
  #define B2U_API
#endif
#ifdef __cplusplus
extern "C" {
#endif

B2U_API b2WorldId b2u_CreateWorld(float gx, float gy) {
  b2WorldDef def = b2DefaultWorldDef();
  def.gravity = (b2Vec2){gx, gy};
  return b2CreateWorld(&def);
}

B2U_API void b2u_WorldStep(b2WorldId w, float dt, int subSteps) {
  b2World_Step(w, dt, subSteps);
}

B2U_API b2BodyId b2u_CreateBody(b2WorldId w) {
  b2BodyDef def = b2DefaultBodyDef();
  def.type = b2_dynamicBody;
  b2BodyId body_id = b2CreateBody(w, &def);

  b2Polygon box = b2MakeOffsetBox(.5f, .5f, (b2Vec2){0,0}, b2Rot_identity);

  b2ShapeDef shape_def = b2DefaultShapeDef();
  shape_def.density = 1.f;
  b2ShapeId shape_id = b2CreatePolygonShape(body_id, &shape_def, &box);
  return body_id;
}

B2U_API void b2u_GetBody(b2BodyId b, float* out_x, float* out_y, float* angle) {
  b2Vec2 pos = b2Body_GetPosition(b);
  *out_x = pos.x;
  *out_y = pos.y;

  b2Rot rot = b2Body_GetRotation(b);
  *angle = b2Rot_GetAngle(rot) * (180.0f / 3.14159265358979323846f);
}

#ifdef __cplusplus
}
#endif
