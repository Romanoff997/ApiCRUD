using ApiCRUD.Models.Client;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Globalization;
using System.Reflection;

namespace ApiCRUD.Services
{
    public static class TypeVisorService
    {
        public static PropertyInfo GetTypeField(string field, Type myType)
        {
            return myType.GetProperty(field);
        }
    }
}
