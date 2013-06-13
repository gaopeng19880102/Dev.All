// ***********************************************************************************
// Created by zbw911 
// �����ڣ�2012��12��18�� 10:44
// 
// �޸��ڣ�2013��02��18�� 18:24
// �ļ�����AsmUtil.cs
// 
// ����и��õĽ����������ʼ���zbw911#gmail.com
// ***********************************************************************************
using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace Dev.Comm
{
    public class AsmUtil
    {
        public static Assembly GetAssemblyFromCurrentDomain(string AName, bool IsLoadAsm)
        {
            Assembly[] asm = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var a in asm)
            {
                if (a.GetName().Name.Equals(AName)) return a;
            }

            if (IsLoadAsm /*&& File.Exists(AName)*/)
            {
                return Assembly.LoadFile(AName);
            }
            else
            {
                return null;
            }
        }


        public static Assembly GetAssemblyFromCurrentDomain(string AName)
        {
            return GetAssemblyFromCurrentDomain(AName, true);
        }

        public static object InvokeMethod(string AName, object[] AParam, object AInstance)
        {
            if (ObjectUtil.IsNull(AInstance)) return null;
            Type type = AInstance.GetType();
            MethodInfo mi = type.GetMethod(AName);
            if (ObjectUtil.IsNull(mi))
            {
                throw new Exception(String.Format("û�ҵ�����", AName));
            }
            //else
            //{
            //    return mi;
            //}

            ParameterInfo[] param = mi.GetParameters();
            if (AParam != null && !param.Length.Equals(AParam.Length))
            {
                throw new Exception(String.Format("û�ҵ�����", AName));
            }
            //else
            //{
            //    return "noParam";
            //}

            return mi.Invoke(AInstance, AParam);
        }

        public static object InvokeMethod(string AAsmName, string AClassName, string AMethodName,
                                          object[] AConstructorParam, object[] AMethodParam, ref object AInstance)
        {
            Assembly asm = GetAssemblyFromCurrentDomain(AAsmName);
            if (ObjectUtil.IsNull(asm))
            {
                throw new Exception(String.Format("������������", AAsmName));
            }
            else
            {
            }

            Type type = asm.GetType(AClassName, false, true);
            if (ObjectUtil.IsNull(type))
            {
                throw new Exception(String.Format("�಻����", AClassName));
            }
            else
            {
                MethodInfo mi = type.GetMethod(AMethodName);
                if (ObjectUtil.IsNull(mi))
                {
                    throw new Exception(String.Format("����������", AMethodName));
                }
                else
                {
                }

                ParameterInfo[] param = mi.GetParameters();
                if (!param.Length.Equals(AMethodParam.Length))
                {
                    throw new Exception(String.Format("������������", AMethodName));
                }
                else
                {
                }

                if (!mi.IsStatic && ObjectUtil.IsNull(AInstance))
                {
                    AInstance = Activator.CreateInstance(type, AConstructorParam);
                }
                else
                {
                }

                return mi.Invoke(AInstance, AMethodParam);
            }
        }

        public static object GetPropertyValue(object obj, string PropertyName, object[] Index)
        {
            PropertyInfo t = obj.GetType().GetProperty(PropertyName);

            if (ObjectUtil.IsNull(t)) return null;


            return t.GetValue(obj, Index);
        }

        public static bool ExistPropertyName(object obj, string PropertyName)
        {
            PropertyInfo t = obj.GetType().GetProperty(PropertyName);
            return t != null;
        }

        public static void SetPropertyValue(object obj, string PropertyName, object Value, object[] Index)
        {
            PropertyInfo t = obj.GetType().GetProperty(PropertyName);

            if (ObjectUtil.IsNull(t)) throw new ArgumentNullException("t");

            object tmp = Convert.ChangeType(Value, t.PropertyType);
            t.SetValue(obj, tmp, Index);
        }

        public static ParameterInfo[] GetMethodParameterInfo()
        {
            return (new StackTrace()).GetFrame(1).GetMethod().GetParameters();
        }

        public static string[] GetMethodParamNames()
        {
            ParameterInfo[] pis = (new StackTrace()).GetFrame(1).GetMethod().GetParameters();
            Array a = Array.CreateInstance(typeof(string), pis.Length);
            for (int i = 0; i < pis.Length; i++)
            {
                a.SetValue(pis[i].Name, i);
            }
            return (string[])a;
        }

        public static string GetMethodParamNamesByFormat()
        {
            string MethodName = (new StackTrace()).GetFrame(1).GetMethod().Name;
            ParameterInfo[] pis = (new StackTrace()).GetFrame(1).GetMethod().GetParameters();
            var result = new StringBuilder();
            result.AppendFormat("{0}|", MethodName);
            for (int i = 0; i < pis.Length; i++)
            {
                result.AppendFormat("{0}={{{1}}}", pis[i].Name, pis[i].Position);
            }
            return result.ToString();
        }

        #region Nested type: ObjectUtil

        public class ObjectUtil
        {
            public static bool IsNull(object AObject)
            {
                return AObject == null;
            }
        }

        #endregion
    }
}