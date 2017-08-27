//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using FlyDoc.Model;
//using FlyDoc.Lib;
//using FlyDoc.ViewModel;
//using System.Windows.Forms;
//using FlyDoc.Forms;

//namespace FlyDoc.Decor
//{
   

//    public class DecorForm : MainForm
//    {
//        public void btnOnOff(int WhichSection)
//        {
//            switch (WhichSection)
//            {
//                case 1:           
//                    setAppModeButtonEnable(btnNew, enableNotes);
//                    setAppModeButtonEnable(btnCopy, enableNotes);
//                    setAppModeButtonEnable(btnEdit, enableNotes);
//                    setAppModeButtonEnable(btnDelete, enableNotes);
//                    datePickerStart.Enabled = true;
//                    datePickerEnd.Enabled = true;
//                    tbxFindDocNumber.Enabled = true;
//                    chkCEO.Enabled = true;
//                    // если дир то видим все отделы, или принудительный фильтр по отделу
//                    if (enableApprDir) { }
//                    else
//                    {
//                       cbDepartmentFilter.SelectedValue = _currentDepId.ToString();// придумать как вытянуть номер отдела без таблицы конфиг!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
//                       cbDepartmentFilter.Enabled = false;
//                        setAppModeButtonEnable(btnDeleteDepartmentFilter, false);
//                    }
//                    datePickerStart.Enabled = true;
//                    datePickerEnd.Enabled = true;
//                    break;
//                case 2:
//                    setAppModeButtonEnable(btnNew, enableSchedule);
//                    setAppModeButtonEnable(btnCopy, enableSchedule);
//                    setAppModeButtonEnable(btnEdit, enableSchedule);
//                    setAppModeButtonEnable(btnDelete, enableSchedule);
//                    datePickerStart.Enabled = true;
//                    datePickerEnd.Enabled = true;
//                    tbxFindDocNumber.Enabled = false;
//                    chkCEO.Enabled = true;
//                    // если дир то видим все отделы, или принудительный фильтр по отделу
//                    if (enableApprDir) { }
//                    else
//                    {
//                        cbDepartmentFilter.SelectedValue = _currentDepId.ToString();// придумать как вытянуть номер отдела без таблицы конфиг!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
//                        cbDepartmentFilter.Enabled = false;
//                        setAppModeButtonEnable(btnDeleteDepartmentFilter, false);
//                    }
//                    datePickerStart.Enabled = true;
//                    datePickerEnd.Enabled = true;
//                    break;
//                case 3:
//                    //Выключаем лишние кнопки
//                    //setAppModeButtonEnable(btnNew, false);
//                    //setAppModeButtonEnable(btnCopy, false);
//                    //setAppModeButtonEnable(btnEdit, false);
//                    //setAppModeButtonEnable(btnDelete, false);
//                    //datePickerStart.Enabled = false;
//                    //datePickerEnd.Enabled = false;
//                    //tbxFindDocNumber.Enabled = false;
//                    //cbDepartmentFilter.SelectedIndex = 0;
//                    //setAppModeButtonEnable(btnDeleteDepartmentFilter, true);
//                    //chkCEO.Enabled = false;
//                    break;
//                case 4:
//                    setAppModeButtonEnable(btnNew, enablePhone);
//                    setAppModeButtonEnable(btnCopy, enablePhone);
//                    setAppModeButtonEnable(btnEdit, enablePhone);
//                    setAppModeButtonEnable(btnDelete, enablePhone);
//                    datePickerStart.Enabled = false;
//                    datePickerEnd.Enabled = false;
//                    tbxFindDocNumber.Enabled = false;
//                    setAppModeButtonEnable(btnDeleteDepartmentFilter, true);
//                    chkCEO.Enabled = false;
//                    cbDepartmentFilter.SelectedIndex = 0;
//                    break;
//                case 5:
//                    setAppModeButtonEnable(btnNew, enableConfig);
//                    setAppModeButtonEnable(btnCopy, enableConfig);
//                    setAppModeButtonEnable(btnEdit, enableConfig);
//                    setAppModeButtonEnable(btnDelete, enableConfig);
//                    datePickerStart.Enabled = false;
//                    datePickerEnd.Enabled = false;
//                    tbxFindDocNumber.Enabled = true;
//                    setAppModeButtonEnable(btnDeleteDepartmentFilter, true);
//                    chkCEO.Enabled = false;
//                    cbDepartmentFilter.SelectedIndex = 0;
//                    break;
//                default:
//                    break;
//            }
//        }
//        public void setAppModeButtonEnable(Button appModeButton, bool enable)
//        {
//            appModeButton.Enabled = enable;
//            toolTip1.SetToolTip(appModeButton, appModeButton.Text);

//        }
        
//    }
//}
