﻿using Fireasy.Common.Serialization;

namespace Fireasy.Redis
{
    /// <summary>
    /// 序列化器。
    /// </summary>
    public class RedisSerializer
    {
        /// <summary>
        /// 序列化对象。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual string Serialize<T>(T obj)
        {
            var option = new JsonSerializeOption();
            option.Converters.Add(new FullDateTimeJsonConverter());
            var serializer = new JsonSerializer(option);
            return serializer.Serialize(obj);
        }

        /// <summary>
        /// 反序列化。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public virtual T Deserialize<T>(string str)
        {
            var option = new JsonSerializeOption();
            option.Converters.Add(new FullDateTimeJsonConverter());
            var serializer = new JsonSerializer(option);
            return serializer.Deserialize<T>(str);
        }
    }
}
