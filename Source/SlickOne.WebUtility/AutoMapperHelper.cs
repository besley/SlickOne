using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace SlickOne.WebUtility
{
    /// <summary>
    /// AutoMapper帮助类
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TTarget"></typeparam>
    public class AutoMapperHelper<TSource, TTarget>
        where TSource: class
        where TTarget: class
    {
        /// <summary>
        /// 静态类型转换
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static TTarget AutoConvert(TSource entity)
        {
            AutoMapper.Mapper.CreateMap<TSource, TTarget>();
            return AutoMapper.Mapper.Map<TSource, TTarget>(entity);
        }

        /// <summary>
        /// 动态类型转换
        /// </summary>
        /// <param name="expando">动态类型对象</param>
        /// <returns></returns>
        public static TTarget AutoConvertDynamic(object expando)
        {
            var entity = Activator.CreateInstance<TTarget>();
            var properties = expando as IDictionary<string, object>;
            if (properties == null)
                return entity;

            foreach (var item in properties)
            {
                var propertyInfo = entity.GetType().GetProperty(item.Key);
                if (propertyInfo != null)
                {
                    if (item.Value == DBNull.Value)
                        propertyInfo.SetValue(entity, null, null);
                    else
                        propertyInfo.SetValue(entity, item.Value, null);
                }
            }
            return entity;
        }
    }
}
