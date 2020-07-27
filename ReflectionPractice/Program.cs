using System;

namespace ReflectionPractice
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            #region TypeInfo
            //获取类型名，基类，程序集，是否为Public
            var stringType = typeof(string); //System.String
            var stringTypeName = stringType.Name; //String
            var stringTypeBaseType = stringType.BaseType; //System.Object
            var stringTypeAssembly = stringType.Assembly; //mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
            var stringTypeIsPublic = stringType.IsPublic; //true

            Console.WriteLine($"{stringType}\n {stringTypeName}\n {stringTypeBaseType}\n {stringTypeAssembly}\n {stringTypeIsPublic}\n");
            #endregion

            #region 获取数组类型
            var intArray = typeof(int).MakeArrayType();  //System.Int32[]
            //创建数组并指定数组维度为3维
            var cubeArray = typeof(int).MakeArrayType(3);  //System.Int32[,,]
            //获取数组元素的类型
            
            
            Console.WriteLine(intArray+";"+cubeArray);
            #endregion
            
            



        }
    }
}