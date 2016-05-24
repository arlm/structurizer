﻿using EnsureThat;
using Structurizer.Schemas.MemberAccessors;

namespace Structurizer.Schemas.Builders
{
    public class AutoStructureSchemaBuilder : IStructureSchemaBuilder
    {
        public IDataTypeConverter DataTypeConverter { get; set; }

        public AutoStructureSchemaBuilder()
        {
            DataTypeConverter = new DataTypeConverter();
        }

        public virtual IStructureSchema CreateSchema(IStructureType structureType)
        {
            Ensure.That(structureType, "structureType").IsNotNull();

            var indexAccessors = GetIndexAccessors(structureType);
            if (indexAccessors == null || indexAccessors.Length < 1)
                throw new StructurizerException(string.Format(StructurizerExceptionMessages.AutoSchemaBuilder_MissingIndexableMembers, structureType.Name));

            return new StructureSchema(structureType, indexAccessors);
        }

        protected virtual IIndexAccessor[] GetIndexAccessors(IStructureType structureType)
        {
            var accessors = new IIndexAccessor[structureType.IndexableProperties.Length];

            for (var i = 0; i < accessors.Length; i++)
            {
                var property = structureType.IndexableProperties[i];
                accessors[i] = new IndexAccessor(property, DataTypeConverter.Convert(property));
            }

            return accessors;
        }
    }
}