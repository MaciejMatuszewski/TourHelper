using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Reflection;
using TourHelper.Base.Exception;
using TourHelper.Base.Model.Entity;
using TourHelper.Base.Repository;

namespace TourHelper.Repository
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : BaseModel, new()
    {
        private string connectionString =
                "Data Source=127.0.0.1;" +
                //"Data Source=192.168.0.162;" +
                "Initial Catalog=TourHelper;" +
                "User id=sa;" +
                "Password=!P@ssw0rd;";

        public TEntity Get(int id)
        {
            return ExecuteSelectCommand($"SELECT * FROM [dbo].[{typeof(TEntity).Name}] WHERE Id = {id}").SingleOrDefault();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return ExecuteSelectCommand($"SELECT * FROM [dbo].[{typeof(TEntity).Name}]");
        }

        public int Update(TEntity entityModel)
        {
            int entityId = 0;
            CultureInfo cultureInfo = new CultureInfo("pl-PL");
            cultureInfo.NumberFormat.NumberDecimalSeparator = ".";

            string statement = $"UPDATE [dbo].[{typeof(TEntity).Name}] SET";

            Type type = entityModel.GetType();
            IList<PropertyInfo> properties = new List<PropertyInfo>(type.GetProperties());

            foreach (PropertyInfo property in properties)
            {
                var propertyValue = property.GetValue(entityModel, null);

                if (property.Name == nameof(entityModel.Id))
                {
                    entityId = (int)propertyValue;
                }
                else if (property.Name == nameof(entityModel.CreatedOn))
                {
                    continue;
                }
                else
                {
                    if ((IsNullable(property.PropertyType) || property.PropertyType.Name == typeof(string).Name) && propertyValue == null)
                    {
                        statement += $" {property.Name} = NULL,";
                    }
                    else if (property.PropertyType.Name == typeof(string).Name || GetNullableType(property).Name == typeof(DateTime).Name)
                    {
                        statement += $" {property.Name} = '{propertyValue}',";
                    }
                    else
                    {
                        statement += $" {property.Name} = {Convert.ToString(property.GetValue(entityModel, BindingFlags.GetProperty, null, null, cultureInfo), cultureInfo)},";
                    }
                }
            }

            statement = statement.TrimEnd(',');
            statement += $" WHERE Id = {entityId}";

            return ExecuteNonQuery(statement);
        }

        public int UpdateRange(IEnumerable<TEntity> entitiesModel)
        {
            int rowAffected = 0;
            foreach (var entityModel in entitiesModel)
            {
                rowAffected += Update(entityModel);
            }
            return rowAffected;
        }

        public TEntity Insert(TEntity entityModel)
        {
            string statement = $"INSERT INTO [dbo].[{typeof(TEntity).Name}] ";

            string entityFieldsSubStatement = "(";
            string entityValuesSubStatement = "(";

            Type type = entityModel.GetType();
            IList<PropertyInfo> properties = new List<PropertyInfo>(type.GetProperties());
            CultureInfo cultureInfo = new CultureInfo("pl-PL");
            cultureInfo.NumberFormat.NumberDecimalSeparator = ".";

            foreach (PropertyInfo property in properties)
            {
                if (property.Name == nameof(entityModel.CreatedOn))
                {
                    entityFieldsSubStatement += $"{property.Name},";
                    entityValuesSubStatement += $"'{DateTime.Now}',";

                }
                else if (property.Name != nameof(entityModel.Id))
                {
                    entityFieldsSubStatement += $"{property.Name},";
                    if (property.GetValue(entityModel, null) == null)
                    {
                        entityValuesSubStatement += $"NULL,";

                    }
                    else if (property.PropertyType.Name == typeof(string).Name
                        || property.PropertyType.Name == typeof(DateTime).Name
                        || GetNullableType(property).Name == typeof(DateTime).Name)
                    {
                        entityValuesSubStatement += $"'{Convert.ToString(property.GetValue(entityModel, BindingFlags.GetProperty, null, null, cultureInfo), cultureInfo)}',";
                    }
                    else
                    {
                        entityValuesSubStatement += $"{Convert.ToString(property.GetValue(entityModel, BindingFlags.GetProperty, null, null, cultureInfo), cultureInfo)},";
                    }
                }
            }

            entityFieldsSubStatement = entityFieldsSubStatement.TrimEnd(',');
            entityFieldsSubStatement += ")";
            entityValuesSubStatement = entityValuesSubStatement.TrimEnd(',');
            entityValuesSubStatement += ")";

            statement += $"{entityFieldsSubStatement} VALUES {entityValuesSubStatement}";

            int rowsAffected = ExecuteNonQuery(statement);

            if (rowsAffected > 1)
            {
                throw new RepositoryException($"Insert statement affected {rowsAffected} rows");
            }

            var addedEntity = ExecuteSelectCommand($"SELECT TOP 1 * FROM [dbo].[{typeof(TEntity).Name}] ORDER BY Id DESC").SingleOrDefault();

            return addedEntity;
        }

        public int Delete(TEntity entityModel)
        {
            string statement = $"DELETE FROM [dbo].[{typeof(TEntity).Name}] WHERE Id = {entityModel.Id}";

            return ExecuteNonQuery(statement);
        }


        public int DeleteRange(IEnumerable<TEntity> entitiesModel)
        {
            int rowAffected = 0;
            foreach (var entityModel in entitiesModel)
            {
                rowAffected += Delete(entityModel);
            }
            return rowAffected;
        }


        protected IEnumerable<TEntity> ExecuteSelectCommand(string statement)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = statement;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection;

            var x = System.Net.Dns.GetHostName();

            sqlConnection.Open();


            reader = cmd.ExecuteReader();

            List<TEntity> entities = new List<TEntity>();

            while (reader.Read())
            {
                TEntity entity = new TEntity();
                Type type = entity.GetType();
                IList<PropertyInfo> properties = new List<PropertyInfo>(type.GetProperties());

                foreach (PropertyInfo property in properties)
                {
                    var entityValue = reader.GetValue(reader.GetOrdinal(property.Name));
                    if (entityValue.GetType().Name == typeof(DBNull).Name)
                    {
                        if (IsNullable(property.PropertyType) || property.PropertyType.Name == typeof(string).Name)
                        {
                            property.SetValue(entity, null, null);
                        }
                        else
                        {
                            throw new RepositoryException($"Could not map null value to not nullable property {entity.GetType().Name}.{property.Name}");
                        }
                    }
                    else
                    {
                        property.SetValue(entity, entityValue, null);
                    }
                }

                entities.Add(entity);
            }

            sqlConnection.Close();

            return entities;
        }

        protected int ExecuteNonQuery(string statement)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            int rowsAffected;

            cmd.CommandText = statement;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection;

            sqlConnection.Open();

            rowsAffected = cmd.ExecuteNonQuery();

            return rowsAffected;
        }

        private bool IsNullable(Type type) => type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);

        private Type GetNullableType(PropertyInfo propertyInfo)
        {
            var propertyType = propertyInfo.PropertyType;
            if (propertyType.IsGenericType &&
                propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                propertyType = propertyType.GetGenericArguments()[0];
            }
            return propertyType;
        }
    }
}
