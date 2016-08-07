using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pgc.Model.Enums
{
    [PersianTitle("نوع تغییرات عملیاتی")]
    public enum BranchFinanceLogActionType
    {
        [PersianTitle("ایجاد درخواست")]
        BranchOrderAgentInserting 								= 1,
        [PersianTitle("ویرایش درخواست (شعبه)")]
        BranchOrderAgentEditing 								= 2,
        [PersianTitle("ویرایش درخواست (مدیر)")]
        BranchOrderAdminEditing 								= 3,
        [PersianTitle("تایید درخواست")]
        BranchOrderAdminConfirmation 							= 4,
        [PersianTitle("ابطال درخواست")]
        BranchOrderAdminCanclation 								= 5,
        [PersianTitle("به جریان انداختن درخواست")]
        BranchOrderAdminRollingBackCanclation 					= 6,
        [PersianTitle("بستن درخواست")]
        BranchOrderAdminFinalization 							= 7,
        [PersianTitle("تغییر وضعیت ارسالی درخواست")]
        BranchOrderAdminShipmentUpdate 							= 8,
        [PersianTitle("حذف درخواست (شعبه)")]
        BranchOrderAgentDelete 									= 9,
        [PersianTitle("به جریان انداختن درخواست")]
        BranchOrderAdminRollingBackFinalized 					= 10,
        [PersianTitle("حذف درخواست (مدیر)")]
        BranchOrderAdminDelete 									= 11,


        [PersianTitle("ایجاد کسری درخواست")]
        BranchLackOrderAgentInserting 							= 12,
        [PersianTitle("ویرایش کسری درخواست (شعبه)")]
        BranchLackOrderAgentEditing 							= 13,
        [PersianTitle("ویرایش کسری درخواست (مدیر)")]
        BranchLackOrderAdminEditing 							= 14,
        [PersianTitle("تایید کسری درخواست")]
        BranchLackOrderAdminConfirmation 						= 15,
        [PersianTitle("ابطال کسری درخواست")]
        BranchLackOrderAdminCanclation 							= 16,
        [PersianTitle("به جریان انداختن کسری درخواست")]
        BranchLackOrderAdminRollingBackCanclation 				= 17,
        [PersianTitle("حذف کسری درخواست (شعبه)")]
        BranchLackOrderAgentDelete 								= 18,
        [PersianTitle("حذف کسری درخواست (مدیر)")]
        BranchLackOrderAdminDelete 								= 19,

	

        [PersianTitle("ایجاد مرجوعی")]
        BranchReturnOrderAgentInserting 						= 20,
        [PersianTitle("ویرایش مرجوعی (شعبه)")]
        BranchReturnOrderAgentEditing 							= 21,
        [PersianTitle("ویرایش مرجوعی (مدیر)")]
        BranchReturnOrderAdminEditing 							= 22,
        [PersianTitle("تایید مرجوعی")]
        BranchReturnOrderAdminConfirmation 						= 23,
        [PersianTitle("ابطال مرجوعی")]
        BranchReturnOrderAdminCanclation 						= 24,
        [PersianTitle("به جریان انداختن مرجوعی")]
        BranchReturnOrderAdminRollingBackCanclation 			= 25,
        [PersianTitle("بستن مرجوعی")]
        BranchReturnOrderAdminFinalization 						= 26,
        [PersianTitle("حذف مرجوعی (شعبه)")]
        BranchReturnOrderAgentDelete 							= 27,
        [PersianTitle("به جریان انداختن مرجوعی")]
        BranchReturnOrderAdminRollingBackFinalized 				= 28,
        [PersianTitle("حذف مرجوعی (مدیر)")]
        BranchReturnOrderAdminDelete 							= 29,
        [PersianTitle("ویرایش درخواست قبل از تایید مدیر(شعبه)")]
        BranchOrderEditBeforConfirm                             = 30,
        [PersianTitle("ویرایش درخواست قبل از تایید مدیر(شعبه مرکزی)")]
        BranchOrderAdminEditBeforConfirm                        =31
    }
}
    
