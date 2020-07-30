// ==================================================
// 文件名：Program.cs
// 创建时间：2020/07/27 12:49
// ==================================================
// 最后修改于：2020/07/30 12:49
// 修改人：jians
// ==================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace ReflectionPractice
{
    public class Student
    {
        public Student()
        {
        }

        public Student(int id, string studentName)
        {
            Id = id;
            StudentName = studentName;
        }

        public int Id { get; set; }

        public string StudentName { get; set; }

        public int Age { get; set; }

        public string this[int id] => StudentName;

        public (string, int) this[int id, string studentName]
        {
            get =>
                (StudentName, Age);
            set
            {
                StudentName = value.Item1;
                Age = value.Item2;
            }
        }
    }

    internal static class Program
    {
        public static void Main(string[] args)
        {
            #region TypeInfo

            //获取类型名，基类，程序集，是否为Public
            var stringType = typeof(string); //System.String
            var stringTypeName = stringType.Name; //String
            var stringTypeBaseType = stringType.BaseType; //System.Object
            var stringTypeAssembly =
                stringType.Assembly; //mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
            var stringTypeIsPublic = stringType.IsPublic; //true

            Console.WriteLine(
                $"{stringType}\n {stringTypeName}\n {stringTypeBaseType}\n {stringTypeAssembly}\n {stringTypeIsPublic}\n");

            #endregion

            #region 获取数组类型

            var intArray = typeof(int).MakeArrayType(); //System.Int32[]
            //创建数组并指定数组维度为3维
            var cubeArray = typeof(int).MakeArrayType(3); //System.Int32[,,]
            //获取数组元素的类型
            var elementType = cubeArray.GetElementType(); //System.Int32
            var cubeArrayRank = cubeArray.GetArrayRank(); //3
            var arrayRank = intArray.GetArrayRank();

            Console.WriteLine(intArray + ";" + cubeArray + ";" + elementType + ";" + cubeArrayRank + ";" + arrayRank);

            #endregion

            //获取嵌套类型
            var nestedTypes = typeof(Program).GetNestedTypes();

            #region 类型名称

            var strBuilderType = typeof(StringBuilder);
            Console.WriteLine(strBuilderType.Namespace); //System.Text
            Console.WriteLine(strBuilderType.Name); //StringBuilder
            Console.WriteLine(strBuilderType.FullName); //System.Text.StringBuilder
            Console.WriteLine(strBuilderType
                .AssemblyQualifiedName); //System.Text.StringBuilder, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089

            #endregion

            #region 泛型类型名称

            //泛型类型名称带有'后缀，后续加上类型参数的数目。

            var dicType1 = typeof(Dictionary<,>);
            Console.WriteLine(dicType1.Name); //Dictionary`2
            Console.WriteLine(dicType1.FullName); //System.Collections.Generic.Dictionary`2

            var dicType2 = typeof(Dictionary<int, string>);
            Console.WriteLine(dicType2.Name); //Dictionary`2
            Console.WriteLine(dicType2
                .FullName); //System.Collections.Generic.Dictionary`2[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089],[System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]

            #endregion

            #region Ref和Out类型

            //ref 和 out 类型带有后缀&
            var outParaType = typeof(bool).GetMethod("TryParse")?.GetParameters()[1].ParameterType;
            Console.WriteLine(outParaType); //System.Boolean&

            #endregion

            #region 实例化类型

            //使用 Activator创建函数
            var studentIns = (Student) Activator.CreateInstance(typeof(Student), 2, "Susy");
            studentIns.Age = 3;

            //通过指定函数构造器创建类型
            var constructorInfo = typeof(Student).GetConstructor(new[] {typeof(int), typeof(string)});
            var studentInsCreatedByCtor = (Student) constructorInfo?.Invoke(new object[] {1, "Ben"});
            if (studentInsCreatedByCtor != null) studentInsCreatedByCtor.Age = 24;

            #endregion

            #region 动态调用成员

            var s = "Hello";
            var propertyInfo = typeof(string).GetProperty("Length");
            var value = propertyInfo != null ? (int?) propertyInfo.GetValue(s) : null;

            //获取索引器
            var property1 = typeof(Student).GetMethod("get_Item",
                new[] {typeof(int), typeof(string)} /*必须指定索引输入参数类型，否则会抛出二义性异常*/);
            var o = ((string, int)) property1.Invoke(studentIns, new object[] {2, "Susy"});

            /***********************************************************************************************************************/
            //带有Set的索引，查找索引方法时除了需要指定索引值，还需要指定写入值类型。
            var property2 = typeof(Student).GetMethod("set_Item",
                new[] {typeof(int),typeof(string),typeof((string,int))} /*必须指定索引输入参数类型，否则会抛出二义性异常*/);
            property2.Invoke(studentIns, new object[] {2, "Susy", ("Andy", 6)});
            /***********************************************************************************************************************/
            
            #endregion
        }

        public class TestNestedClass
        {
        }
    }
}