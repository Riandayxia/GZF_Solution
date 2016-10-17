using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
namespace Quest.Framework
{
    public class MappingBase<TEntity> : EntityTypeConfiguration<TEntity>, IEntityMapper
        where TEntity : class,IEntity
    {
        public MappingBase()
        {
            this.Map(m => m.ToTable(typeof(TEntity).Name));
        }

        public void RegistTo(ConfigurationRegistrar configurationRegistrar)
        {
            configurationRegistrar.Add(this);
        }
    }
}
