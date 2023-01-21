using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Cargo.Core.DataAccessLayer
{
    public static class Mapper
    {
        // DataReader Map To List
        public static List<T> DataReaderMapToList<T>(IDataReader dr)
        {
            List<T> list = new List<T>();

            while (dr.Read())
            {
                T obj = Activator.CreateInstance<T>();

                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (!object.Equals(dr[prop.Name], DBNull.Value))
                    {
                        prop.SetValue(obj, dr[prop.Name], null);
                    }
                }

                list.Add(obj);
            }

            return list;
        }
    }
}