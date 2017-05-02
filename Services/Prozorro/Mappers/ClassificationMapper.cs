using Kapitalist.Data.Models.DTO;
using Kapitalist.Data.Models.Interfaces;
using Kapitalist.Data.Store.Models;
using System;
using Rest = Kapitalist.Core.OpenProcurement.Models;

namespace Kapitalist.Services.Prozorro.Mappers
{
    public static class ClassificationMapper
    {
        public static ClassificationDTO ToDTO(this IClassification source)
        {
            return source == null
                ? null
                : new ClassificationDTO(source);
        }

        public static Rest.Classification ToRest(this IClassification source)
        {
            return source == null
                ? null
                : new Rest.Classification(source);
        }

        public static T ToDraft<T>(this ClassificationDTO source)
            where T : class, IClassification
        {
            if (source == null)
                return null;
            return (T)Activator.CreateInstance(typeof(T), source);
        }

        public static ClassificationDTO ToDTO(this DraftClassification source)
        {
            return source == null
                ? null
                : new ClassificationDTO(source);
        }
    }
}