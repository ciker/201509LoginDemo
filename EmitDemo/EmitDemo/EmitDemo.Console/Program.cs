using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;

namespace EmitDemo.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            User user = new User();
            user.ID = 1;
            user.UserName = "aa";
            user.Mobile = "123123123123";
            user.Email = "aaa@qq.com";
            user.Address = "abcabcabc";

            var uc = getUserInstance();

        }

        static User getUserInstance()
        {
            var type = typeof(User);

            DynamicMethod instDynamicMethod = new DynamicMethod("", type, null);

            ILGenerator ilGenerator = instDynamicMethod.GetILGenerator();

            var constuctor = typeof(User).GetConstructors(BindingFlags.Instance | BindingFlags.Public)[0];
            //ilGenerator.BeginExceptionBlock();
            LocalBuilder result = ilGenerator.DeclareLocal(type);
            ilGenerator.Emit(OpCodes.Nop);

            ilGenerator.Emit(OpCodes.Newobj, constuctor);
            ilGenerator.Emit(OpCodes.Stloc_0, result);


            ilGenerator.Emit(OpCodes.Ldloc_0);
            //ilGenerator.Emit(OpCodes.Ldc_I4, 0x463);
            ilGenerator.Emit(OpCodes.Ldc_I4, 1123);
            ilGenerator.Emit(OpCodes.Callvirt, type.GetMethod("set_ID"));

            ilGenerator.Emit(OpCodes.Nop);

            ilGenerator.Emit(OpCodes.Ldloc_0);
            ilGenerator.Emit(OpCodes.Ldstr, "aa");
            ilGenerator.Emit(OpCodes.Callvirt, type.GetMethod("set_UserName"));

            ilGenerator.Emit(OpCodes.Nop);

            ilGenerator.Emit(OpCodes.Ldloc_0);
            ilGenerator.Emit(OpCodes.Ldstr, "123123123123");
            ilGenerator.Emit(OpCodes.Callvirt, type.GetMethod("set_Mobile"));

            ilGenerator.Emit(OpCodes.Nop);

            ilGenerator.Emit(OpCodes.Ldloc_0);
            ilGenerator.Emit(OpCodes.Ldstr, "aaa@qq.com");
            ilGenerator.Emit(OpCodes.Callvirt, type.GetMethod("set_Email"));

            ilGenerator.Emit(OpCodes.Nop);

            ilGenerator.Emit(OpCodes.Ldloc_0);
            ilGenerator.Emit(OpCodes.Ldstr, "abcabcabc");
            ilGenerator.Emit(OpCodes.Callvirt, type.GetMethod("set_Address"));

            ilGenerator.Emit(OpCodes.Nop);

            ilGenerator.Emit(OpCodes.Ldloc_0, result);

            ilGenerator.Emit(OpCodes.Ret);


            var fun = (Func<User>)instDynamicMethod.CreateDelegate(typeof(Func<User>));
            return fun.Invoke();
        }

        static T GetEmitByT<T>()
        {
            var type = typeof(T);
            var instanceDynamicMethod = new DynamicMethod("instance", type, null);

            var ilGenerator = instanceDynamicMethod.GetILGenerator();
            var constuctor = typeof(User).GetConstructors(BindingFlags.Instance | BindingFlags.Public)[0];
            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Default | BindingFlags.Public);

            var result = ilGenerator.DeclareLocal(type);
            ilGenerator.Emit(OpCodes.Nop);
            ilGenerator.Emit(OpCodes.Newobj, constuctor);
            ilGenerator.Emit(OpCodes.Stloc_0, result);

            foreach (var property in properties)
            {

                //:TODO need  to judge the type of the value



                ilGenerator.Emit(OpCodes.Ldloc_0);
                //ilGenerator.Emit(OpCodes.Ldc_I4, 0x463);
                ilGenerator.Emit(OpCodes.Ldc_I4, 1123);
                ilGenerator.Emit(OpCodes.Callvirt, type.GetMethod("set_ID"));

                ilGenerator.Emit(OpCodes.Nop);

                ilGenerator.Emit(OpCodes.Ldloc_0);
                ilGenerator.Emit(OpCodes.Ldstr, "aa");
                ilGenerator.Emit(OpCodes.Callvirt, type.GetMethod("set_UserName"));

                ilGenerator.Emit(OpCodes.Nop);

                ilGenerator.Emit(OpCodes.Ldloc_0);
                ilGenerator.Emit(OpCodes.Ldstr, "123123123123");
                ilGenerator.Emit(OpCodes.Callvirt, type.GetMethod("set_Mobile"));

                ilGenerator.Emit(OpCodes.Nop);

                ilGenerator.Emit(OpCodes.Ldloc_0);
                ilGenerator.Emit(OpCodes.Ldstr, "aaa@qq.com");
                ilGenerator.Emit(OpCodes.Callvirt, type.GetMethod("set_Email"));

                ilGenerator.Emit(OpCodes.Nop);

                ilGenerator.Emit(OpCodes.Ldloc_0);
                ilGenerator.Emit(OpCodes.Ldstr, "abcabcabc");
                ilGenerator.Emit(OpCodes.Callvirt, type.GetMethod("set_Address"));

                ilGenerator.Emit(OpCodes.Nop);

                ilGenerator.Emit(OpCodes.Ldloc_0, result);

                ilGenerator.Emit(OpCodes.Ret);
            }
            var fun = (Func<T>)instanceDynamicMethod.CreateDelegate(typeof(Func<T>));
            return fun.Invoke();
        }
    }
}
