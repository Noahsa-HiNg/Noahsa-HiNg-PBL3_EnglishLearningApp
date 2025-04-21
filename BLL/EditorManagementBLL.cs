using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
namespace BLL
{
    public class EditorManagementBLL:EditorValidator
    {
        EditorAccess editorAccess = new EditorAccess();
        public string[] AddEditor(Editor editor, Account account)
        {
            string[] ResultACC = new string[5];
            string[] information = { account.Username, account.Password, editor.Name, editor.Phone,editor.Email };
            MyDelegate[] methods = { ChecklogicUsername, CheckLogicPassWord, ChecklogicName, ChecklogicPhone, ChecklogicEmail };
            for (int i = 0; i < ResultACC.Length; i++)
            {
                ResultACC[i] = "valid_true";
            }
            for (int i = 0; i < information.Length; i++)
            {
                if (methods[i](information[i]) != "valid_true")
                {
                    ResultACC[i] = methods[i](information[i]);
                }
            }
            return ResultACC;
        }
        public string DeleteEditor(Editor editor)
        {
            string result = editorAccess.DeleteDataEditor(editor);
            return result;
        }
        public (Editor,Account) ShowInforEditor(Editor editor)
        {
            return editorAccess.ShowDataInforEditor(editor.ID);
        }
        public string ChangePermiss(Editor editor)
        {
            return editorAccess.ChangeDataPermiss(editor);
        }
    }
}
