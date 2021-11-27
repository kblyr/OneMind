using Swashbuckle.AspNetCore.SwaggerGen;

namespace OneMind;

public static class SwaggerGenOptionsExtensions
{
    public static void UseSchemaIds(this SwaggerGenOptions options)
    {
        options.CustomSchemaIds(type =>
        {
            var schemaIdAttribs = type.GetCustomAttributes(typeof(SchemaIdAttribute), false);

            if (schemaIdAttribs.Any() && schemaIdAttribs[0] is SchemaIdAttribute schemaId)
                return schemaId.SchemaId;

            return type.Name;
        });
    }
}