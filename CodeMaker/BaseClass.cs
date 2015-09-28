// Decompiled with JetBrains decompiler
// Type: CodeMaker.BaseClass
// Assembly: CodeMaker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2C24D03B-1DFB-4ABE-A5BB-5B82050459A6
// Assembly location: D:\langben6.1狼奔代码生成器\langben6.1\CodeMaker.exe

using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CodeMaker
{
  public class BaseClass
  {
    public static string m_PdmINI = Application.StartupPath + "\\Pdm.ini";
    public static string m_DempDirectory = Application.StartupPath;
    public static string m_Pdm = string.Empty;
    public static string m_RootDirectory = string.Empty;
    public static List<Reference> m_SelfJoinClass = new List<Reference>();
    public static string m_DALnamespace = "^DAL^";
    public static string m_BLLnamespace = "^BLL^";
    public static string m_IBLLnamespace = "^IBLL^";
    public static List<string> m_refNotIdClass = new List<string>();
    public string m_ReplaceClassName = "^ReplaceClassName^";
    public string m_ReplaceClassCode = "^ReplaceClassCode^";
    public string m_ReplaceAttribute = "^ReplaceAttribute^";
    public string m_RadioButtonListChecked = "^m_RadioButtonListChecked^";
    public string m_ReplaceJavascript = "^ReplaceJavascript^";
    public string m_Id = "^m_Id^";
    public string m_Name = "^m_Name^";
    public string m_String = "^string^";
    public string m_Sort = "^m_Sort^";
    public StringBuilder m_Content = new StringBuilder();
    public string m_Application = "^m_Application^";
    public string m_Views = "/Views";
    public string m_DAL = "DAL";
    public string m_BLL = "BLL";
    public string m_IBLL = "IBLL";
    public string m_App = "App";
    public string m_CreateEditorFor = "        \r\n            <div class=@editor-label@>\r\n                <%: Html.LabelFor(model => model.^ReplaceAttribute^) %>：\r\n            </div>\r\n            <div class=@editor-field@>\r\n                <%=Html.DropDownListFor(model => model.^ReplaceAttribute^,Models.SysFieldModels.GetSysField(@^ReplaceClassCode^@,@^ReplaceAttribute^@),@请选择@)%>  \r\n            </div>";
    public string m_CreateRadioButtonFor = "        \r\n            <div class=@editor-label@>\r\n                <%: Html.LabelFor(model => model.^ReplaceAttribute^) %>：\r\n            </div>\r\n            <div class=@editor-field@>\r\n             <%: Html.RadioButtonListFor(model => model.^ReplaceAttribute^,Models.SysFieldModels.GetSysField(@^ReplaceClassCode^@,@^ReplaceAttribute^@),^m_RadioButtonListChecked^) %>\r\n            </div>";
    public string m_LianDong = "        \r\n            <div class=@editor-label@>\r\n                <%: Html.LabelFor(model => model.^ReplaceAttribute^) %>：\r\n            </div>\r\n            <div class=@editor-field@>\r\n                <select id=@^ReplaceAttribute^@ name=@^ReplaceAttribute^@>\r\n                </select>              \r\n            </div>";
    public string m_LianDongEditorFor = "        \r\n            <div class=@editor-label@>\r\n                <%: Html.LabelFor(model => model.^ReplaceAttribute^) %>：\r\n            </div>\r\n            <div class=@editor-field@>\r\n                <%=Html.DropDownListFor(model => model.^ReplaceAttribute^,Models.SysFieldModels.GetSysField(@^ReplaceClassCode^@,@^ReplaceAttribute^@,Model.^MyParent^),@请选择@)%>  \r\n            </div>";
    public string m_TextAreaFor = "\r\n            <br style='clear: both;' />\r\n            <div class='editor-label'>\r\n                <%: Html.LabelFor(model => model.^ReplaceAttribute^) %>：\r\n            </div>\r\n            <div class='textarea-box'>\r\n                <%: Html.TextAreaFor(model => model.^ReplaceAttribute^) %>\r\n                <%: Html.ValidationMessageFor(model => model.^ReplaceAttribute^) %>\r\n            </div>";
    public string m_EditorFor = "     \r\n            <div class=@editor-label@>\r\n                <%: Html.LabelFor(model => model.^ReplaceAttribute^) %>：\r\n            </div>\r\n            <div class=@editor-field@>\r\n                <%: Html.EditorFor(model => model.^ReplaceAttribute^) %>\r\n                <%: Html.ValidationMessageFor(model => model.^ReplaceAttribute^) %>\r\n            </div>";
    public string m_PickTime = "\r\n                                       <script src='<%: Url.Content('~/Res/My97DatePicker/WdatePicker.js') %>' type='text/javascript'></script>\r\n                                         ".Replace('\'', '"');
    public string m_EditorForInt = "     \r\n            <div class=*editor-label*>\r\n                <%: Html.LabelFor(model => model.^ReplaceAttribute^) %>：\r\n            </div>\r\n            <div class=*editor-field*>\r\n                <%: Html.TextBoxFor(model => model.^ReplaceAttribute^, new {  onkeyup = *isInt(this)*, @class=*text-box single-line* })%>\r\n                <%: Html.ValidationMessageFor(model => model.^ReplaceAttribute^) %>\r\n            </div>";
    public string m_EditorForDate = "     \r\n            <div class=*editor-label*>\r\n                <%: Html.LabelFor(model => model.^ReplaceAttribute^) %>：\r\n            </div>\r\n            <div class=*editor-field*>\r\n                <%: Html.TextBox(*^ReplaceAttribute^*, ((Model != null && Model.^ReplaceAttribute^ != null) ? Model.^ReplaceAttribute^.ToString().AsDateTime().ToString(*yyyy-MM-dd*) : **), new { style = *width: 244px;*, @class=*easyui-datebox* })%>\r\n                <%: Html.ValidationMessageFor(model => model.^ReplaceAttribute^) %>\r\n            </div>";
    public string m_PickTimeCrea = "^m_PickTimeCrea^";
    public string m_UpLoaderScript = "\r\n    <link href=@../../Res/jquery.uploadify-v2.1.4/uploadify.css@ rel=@stylesheet@ type=@text/css@ />\r\n    <script src=@../../Res/jquery.uploadify-v2.1.4/jquery.uploadify.v2.1.4.min.js@ type=@text/javascript@></script>\r\n    <script src=@../../Res/jquery.uploadify-v2.1.4/swfobject.js@ type=@text/javascript@></script>     \r\n";
    public string m_UpLodeJs = " \r\n                $('#file_upload').uploadify({\r\n                    'uploader': '../../Res/jquery.uploadify-v2.1.4/uploadify.swf',\r\n                    'script': '../../Res/jquery.uploadify-v2.1.4/FileUploader.ashx',\r\n                    'cancelImg': '../../Res/jquery.uploadify-v2.1.4/cancel.png',\r\n                    'folder': '/up',\r\n                    'queueID': 'fileQueue',\r\n                    'auto': true,\r\n                    'multi': true,\r\n                    'simUploadLimit': 5,                \r\n                    'onComplete': function (event, queueId, fileObj, response, data) {\r\n                         if (response == '0') {\r\n                         alert('上传失败，请检查文件是否存在或者网络是否通畅');\r\n                         return;\r\n                    }\r\n                    var content = @@; //需要添加的内容\r\n                    var hidden = document.getElementById('^ReplaceAttribute^'); //获取隐藏的控件\r\n                    hidden.value += @^@ + response + @&@ + fileObj.name;//为隐藏控件赋值\r\n                    content += '<table  id=@' + response + '@ class=@deleteStyle@><tr><td><img src=@../../../Images/deleteimge.png@ title=@点击删除@  alt=@删除@    onclick=@ deleteTable(' + @'@ + response + @',@ + @'@ + '^ReplaceAttribute^' + @'@ + ');@ /></td><td>' + fileObj.name + '</td></tr></table>';\r\n                     if (content!=null) {   \r\n                         $(@#up@).append(content); \r\n                    }\r\n                           \r\n                    }                \r\n                });\r\n";
    public string m_StringCreate = "^string?^";
  }
}
