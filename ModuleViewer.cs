#region ● Namespace declaration

using Bizentro.AppFramework.MESLibrary;
using Bizentro.AppFramework.UI.Common;
using Bizentro.AppFramework.UI.Common.Exceptions;
using Bizentro.AppFramework.UI.Controls;
using Bizentro.AppFramework.UI.Module;
using Bizentro.AppFramework.UI.Variables;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Microsoft.Practices.CompositeUI.SmartParts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web.Services.Protocols;
using System.Windows.Forms;

#endregion

namespace Bizentro.App.UI.PIM.PIR101M1_CKO134
{
    [SmartPart]
    public partial class ModuleViewer : ViewBase
    {

        #region ▶ 1. Declaration part

        #region ■ 1.1 Program information
        /// <TemplateVersion>0.0.1.0</TemplateVersion>
        /// <NameSpace>①namespace</NameSpace>
        /// <Module>②module name</Module>
        /// <Class>③class name</Class>
        /// <Desc>④
        ///   작업지시관리 프로그램 
        /// </Desc>
        /// <History>⑤
        ///   <FirstCreated>
        ///     <history name="Eunjeong" Date="2016-04-28">Make …</history>
        ///   </FirstCreated>
        ///   <Lastmodified>
        ///     <history name="LGE"  Date="2016-06-23"> 작업지시정보를 등록, 수정, 삭제 </history>
        ///     <history name="modifier"  Date="modified date"> contents </history>
        ///     <history name="modifier"  Date="modified date"> contents </history>
        ///   </Lastmodified>
        /// </History>
        /// <Remarks>⑥
        ///   <remark name="modifier"  Date="modified date">… </remark>
        ///   <remark name="modifier"  Date="modified date">… </remark>
        /// </Remarks>

        #endregion

        #region ■ 1.2. Class global constants (common)

        #endregion

        #region ■ 1.3. Class global variables (common)

        private tdsPIR101M1 cqtdsPIR101M1 = new tdsPIR101M1();
        private ControlHelper ctrlHelper = new ControlHelper();
        private DataTable orderTypeTable = new DataTable();
        private DataTable statusTypeTable = new DataTable();
        private DataTable laborClassTable = new DataTable();
        private DataSet conditionDataSet = new DataSet();

        #endregion

        #region ■ 1.4 Class global constants (grid)

        #endregion

        #region ■ 1.5 Class global variables (grid)

        #endregion

        #endregion

        #region ▶ 2. Initialization part

        #region ■ 2.1 Constructor(common)

        public ModuleViewer()
        {
            InitializeComponent();
        }

        #endregion

        #region ■ 2.2 Form_Load(common)

        protected override void Form_Load()
        {
            uniBase.UData.SetWorkingDataSet(this.cqtdsPIR101M1);
            uniBase.UCommon.SetViewType(enumDef.ViewType.T02_Multi);

            uniBase.UCommon.LoadInfTB19029(enumDef.FormType.Input, enumDef.ModuleInformation.Common);   // Load company numeric format. I: Input Program, *: All Module
            this.LoadCustomInfTB19029();                                                                // Load custom numeric format
        }

        protected override void Form_Load_Completed()
        {
            dtDateTerm.uniFromValue = DateTime.Now.AddDays(-7);
            dtDateTerm.uniToValue = DateTime.Now;
            this.uniGrid1.GridStyle = enumDef.GridStyle.AppendDetail;
        }

        #endregion

        #region ■ 2.3 Initializatize local global variables

        protected override void InitLocalVariables()
        {
            // init Dataset Row : change your code
            cqtdsPIR101M1.Clear();

            uniBase.UCommon.SetToolBarMulti(enumDef.ToolBitMulti.CopyRow, false);
        }

        #endregion

        #region ■ 2.4 Set local global default variables

        protected override void SetLocalDefaultValue()
        {
            //-- Toolbar 설정 로직 ---
            //uniBase.UCommon.SetToolBarAll(false);
            //uniBase.UCommon.SetToolBarCommonAll(true);
            uniBase.UCommon.SetToolBarCommon(enumDef.ToolBitCommon.Save, false);
            //uniBase.UCommon.SetToolBarSingle(enumDef.ToolBitSingle.New, true);
            uniBase.UCommon.SetToolBarMulti(enumDef.ToolBitMulti.InsertRow | enumDef.ToolBitMulti.CopyRow | enumDef.ToolBitMulti.DeleteRow | enumDef.ToolBitMulti.Cancel, false);

            #region 공장(조회조건 콤보) 기본값 설정
            if (string.IsNullOrWhiteSpace(CommonVariable.gPlant))
            {
                string strSql = @"SELECT TOP 1 DivisionID FROM System.Division (nolock) ORDER BY DivisionID ASC";

                try
                {
                    using (Bizentro.AppFramework.DataBridge.uniCommand iuniCommand = uniBase.UDatabase.GetSqlStringCommand(strSql))
                    {
                        DataSet dsPlant = uniBase.UDatabase.ExecuteDataSet(iuniCommand).Copy();

                        if (dsPlant != null && dsPlant.Tables.Count > 0 && dsPlant.Tables[0].Rows.Count > 0)
                        {
                            cbDivisionID.Value = dsPlant.Tables[0].Rows[0]["DivisionID"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    bool reThrow = ExceptionControler.AutoProcessException(ex);
                    if (reThrow)
                        throw;
                }
            }
            else
            {
                cbDivisionID.Value = CommonVariable.gPlant;
            }
            #endregion 공장(조회조건 콤보) 기본값 설정
            return;
        }

        #endregion

        #region ■ 2.5 Gathering combo data(GatheringComboData)

        protected override void GatheringComboData()
        {
            string strSql = @"SELECT CodeID Code, CodeName Name FROM Information.CommonCodeDetail (nolock) WHERE CodeTypeID = 'MANUFACTURE_ORDER_TYPE';
                                SELECT DivisionID Code, DivisionName Name From System.Division (nolock);
                                SELECT b.Status Code, b.StatusName Name from System.CommonStatus (nolock)a join System.CommonStatusDetail (nolock)b on a.ObjectName = b.ObjectName where a.ObjectName = 'Manufacture.ManufactureOrder'";

            try
            {
                using (Bizentro.AppFramework.DataBridge.uniCommand iuniCommand = uniBase.UDatabase.GetSqlStringCommand(strSql))
                {
                    conditionDataSet = uniBase.UDatabase.ExecuteDataSet(iuniCommand).Copy();
                }
            }
            catch (Exception ex)
            {
                bool reThrow = ExceptionControler.AutoProcessException(ex);
                if (reThrow)
                    throw;
            }
            finally
            {
                //if (iqtdsTypedDataSet != null) iqtdsTypedDataSet.Dispose();
                //if (iqtdsICondition != null) iqtdsICondition.Dispose();
            }

            statusTypeTable = conditionDataSet.Tables[2];

            //statusTypeTable.Columns.Add("Code");
            //statusTypeTable.Columns.Add("Name");

            DataRow dr = statusTypeTable.NewRow();
            statusTypeTable.Rows.InsertAt(dr, 0);
            //statusTypeTable.Rows.Add(dr);
            //statusTypeTable.Rows.Add(new string []{"-1","대기"});
            //statusTypeTable.Rows.Add(new string[] { "0", "확정" });
            //statusTypeTable.Rows.Add(new string[] { "1", "진행" });
            //statusTypeTable.Rows.Add(new string[] { "2", "완료" });
            //statusTypeTable.Rows.Add(new string[] { "8", "중단" });
            //statusTypeTable.Rows.Add(new string[] { "9", "취소" });

            orderTypeTable = conditionDataSet.Tables[0];
            dr = orderTypeTable.NewRow();
            orderTypeTable.Rows.InsertAt(dr, 0);
            cbDivisionID.SetDataBinding(conditionDataSet, conditionDataSet.Tables[1].TableName);
            cbDivisionID.ValueMember = conditionDataSet.Tables[1].Columns[0].ColumnName;
            cbDivisionID.DisplayMember = conditionDataSet.Tables[1].Columns[1].ColumnName;
            cbOrderType.SetDataBinding(conditionDataSet, conditionDataSet.Tables[0].TableName);
            cbOrderType.ValueMember = conditionDataSet.Tables[0].Columns[0].ColumnName;
            cbOrderType.DisplayMember = conditionDataSet.Tables[0].Columns[1].ColumnName;
            cbStatus.SetDataBinding(conditionDataSet, conditionDataSet.Tables[2].TableName);
            cbStatus.ValueMember = conditionDataSet.Tables[2].Columns[0].ColumnName;
            cbStatus.DisplayMember = conditionDataSet.Tables[2].Columns[1].ColumnName;
            uniBase.UCommon.ChangeFieldLockAttribute(cbDivisionID, enumDef.FieldLockAttribute.Required);
            cbDivisionID.FieldType = enumDef.FieldType.NotNull;
        }

        #endregion

        #region ■ 2.6 Define user defined numeric info

        public void LoadCustomInfTB19029()
        {
            #region User Define Numeric Format Data Setting  ☆
            base.viewTB19029.ggUserDefined6.DecPoint = 0;
            base.viewTB19029.ggUserDefined6.Integeral = 15;

            base.viewTB19029.ggUserDefined7.DecPoint = 4;
            base.viewTB19029.ggUserDefined7.Integeral = 19;

            base.viewTB19029.ggUserDefined8.DecPoint = 4;
            base.viewTB19029.ggUserDefined8.Integeral = 20;

            base.viewTB19029.ggUserDefined9.DecPoint = 2;
            base.viewTB19029.ggUserDefined9.Integeral = 9;

            #endregion
        }

        #endregion

        #endregion

        #region ▶ 3. Grid method part

        #region ■ 3.1 Initialize Grid (InitSpreadSheet)

        private void InitSpreadSheet()
        {
            #region ■■ 3.1.1 Pre-setting grid information

            #region -- [uniGrid1] ProductOrder --
            tdsPIR101M1.B_ProductOrderDataTable uniGridTB1 = cqtdsPIR101M1.B_ProductOrder;
            CheckEditorDataFilter chkFilter = new CheckEditorDataFilter("1", "0");
            //this.uniGrid1.SSSetFloat(uniGridTB1.ProductOrderNumberColumn.ColumnName, "ProductOrderNumber", viewTB19029.ggUserDefined6, enumDef.FieldType.Primary, enumDef.HAlign.Right, true, enumDef.PosZero.Default, 0, int.MaxValue);
            this.uniGrid1.SSSetEdit(uniGridTB1.ProductOrderNumberColumn.ColumnName, "ProductOrderNumber", 120, enumDef.FieldType.Primary, enumDef.CharCase.Default, false, enumDef.HAlign.Left, 80);
            this.uniGrid1.SSSetCombo(uniGridTB1.DivisionIDColumn.ColumnName, "DivisionID", 130, conditionDataSet.Tables[1], enumDef.FieldType.ReadOnly, enumDef.HAlign.Left, false, false);
            this.uniGrid1.SSSetCombo(uniGridTB1.StatusColumn.ColumnName, "Status", 120, statusTypeTable, enumDef.FieldType.ReadOnly, enumDef.HAlign.Left, false, false);
            this.uniGrid1.SSSetDate(uniGridTB1.ManufactureOrderDateColumn.ColumnName, "ManufactureOrderDate", 120, enumDef.FieldType.ReadOnly, "YYYY-MM-DD", enumDef.HAlign.Center);
            this.uniGrid1.SSSetCombo(uniGridTB1.ManufactureOrderTypeColumn.ColumnName, "ManufactureOrderType", 120, conditionDataSet.Tables[0], enumDef.FieldType.NotNull, enumDef.HAlign.Left, false, false);
            this.uniGrid1.SSSetEdit(uniGridTB1.ShiftTypeColumn.ColumnName, "ShiftType", 120, enumDef.FieldType.NotNull, enumDef.CharCase.Default, true, enumDef.HAlign.Left, 1);
            this.uniGrid1.SSSetEdit(uniGridTB1.ShiftTypeNameColumn.ColumnName, "ShiftTypeName", 120, enumDef.FieldType.ReadOnly, enumDef.CharCase.Default, false, enumDef.HAlign.Left, 255);
            this.uniGrid1.SSSetEdit(uniGridTB1.ModelDescriptionColumn.ColumnName, "ModelDescription", 120, enumDef.FieldType.ReadOnly, enumDef.CharCase.Default, false, enumDef.HAlign.Left, 255);
            this.uniGrid1.SSSetEdit(uniGridTB1.ProductIDColumn.ColumnName, "ProductID", 120, enumDef.FieldType.ReadOnly, enumDef.CharCase.Default, true, enumDef.HAlign.Left, 80);
            this.uniGrid1.SSSetEdit(uniGridTB1.ProductNameColumn.ColumnName, "ProductName", 120, enumDef.FieldType.ReadOnly, enumDef.CharCase.Default, false, enumDef.HAlign.Left, 255);
            this.uniGrid1.SSSetEdit(uniGridTB1.SpecColumn.ColumnName, "Spec", 120, enumDef.FieldType.ReadOnly, enumDef.CharCase.Default, false, enumDef.HAlign.Left, 255);
            this.uniGrid1.SSSetFloat(uniGridTB1.OrderQuantityColumn.ColumnName, "OrderQuantity", viewTB19029.ggUserDefined6, enumDef.FieldType.NotNull, enumDef.HAlign.Right, true, enumDef.PosZero.Default, 0, int.MaxValue);
            this.uniGrid1.SSSetFloat(uniGridTB1.ResultQuantityColumn.ColumnName, "ResultQuantity", viewTB19029.ggUserDefined6, enumDef.FieldType.ReadOnly, enumDef.HAlign.Right, true, enumDef.PosZero.Default, 0, int.MaxValue);
            this.uniGrid1.SSSetFloat(uniGridTB1.RestQuantityColumn.ColumnName, "RestQuantity", viewTB19029.ggUserDefined6, enumDef.FieldType.ReadOnly, enumDef.HAlign.Right, true, enumDef.PosZero.Default, 0, int.MaxValue);
            this.uniGrid1.SSSetEdit(uniGridTB1.LotNumberColumn.ColumnName, "LotNumber", 120, enumDef.FieldType.Default, enumDef.CharCase.Default, false, enumDef.HAlign.Left, 80);
            this.uniGrid1.SSSetEdit(uniGridTB1.TrackingNumberColumn.ColumnName, "TrackingNumber", 120, enumDef.FieldType.Default, enumDef.CharCase.Default, false, enumDef.HAlign.Left, 80);
            this.uniGrid1.SSSetEdit(uniGridTB1.UserDefinedLotNumberColumn.ColumnName, "UserDefinedLotNumber", 120, enumDef.FieldType.Default, enumDef.CharCase.Default, false, enumDef.HAlign.Left, 80);
            this.uniGrid1.SSSetDate(uniGridTB1.StartDateColumn.ColumnName, "StartDate", 120, enumDef.FieldType.NotNull, "YYYY-MM-DD", enumDef.HAlign.Center);
            this.uniGrid1.SSSetDate(uniGridTB1.StartDateTimeColumn.ColumnName, "StartDateTime", 150, enumDef.FieldType.ReadOnly, "", enumDef.HAlign.Center);
            this.uniGrid1.SSSetDate(uniGridTB1.DueDateColumn.ColumnName, "DueDate", 120, enumDef.FieldType.NotNull, "YYYY-MM-DD", enumDef.HAlign.Center);
            this.uniGrid1.SSSetDate(uniGridTB1.FinishDateTimeColumn.ColumnName, "FinishDateTime", 150, enumDef.FieldType.ReadOnly, "", enumDef.HAlign.Center);
            this.uniGrid1.SSSetEdit(uniGridTB1.RequestDivisionIDColumn.ColumnName, "RequestDivisionID", 120, enumDef.FieldType.Default, enumDef.CharCase.Default, false, enumDef.HAlign.Left, 2);
            this.uniGrid1.SSSetDate(uniGridTB1.RequestDateColumn.ColumnName, "RequestDate", 150, enumDef.FieldType.Default, "", enumDef.HAlign.Center);
            this.uniGrid1.SSSetFloat(uniGridTB1.RequestSequenceColumn.ColumnName, "RequestSequence", viewTB19029.ggUserDefined6, enumDef.FieldType.Default, enumDef.HAlign.Right, true, enumDef.PosZero.Default, 0, int.MaxValue);
            this.uniGrid1.SSSetCheck(uniGridTB1.isOutsourcingColumn.ColumnName, "isOutsourcing", 90, enumDef.FieldType.NotNull, chkFilter, "", enumDef.HAlign.Center, enumDef.HAlign.Center);
            this.uniGrid1.SSSetCheck(uniGridTB1.isMultiRoutingColumn.ColumnName, "isMultiRouting", 90, enumDef.FieldType.NotNull, chkFilter, "", enumDef.HAlign.Center, enumDef.HAlign.Center);
            this.uniGrid1.SSSetEdit(uniGridTB1.EquipmentIDColumn.ColumnName, "EquipmentID", 120, enumDef.FieldType.Default, enumDef.CharCase.Default, false, enumDef.HAlign.Left, 40);
            this.uniGrid1.SSSetEdit(uniGridTB1.EquipmentNameColumn.ColumnName, "EquipmentName", 120, enumDef.FieldType.Default, enumDef.CharCase.Default, true, enumDef.HAlign.Left, 255);
            this.uniGrid1.SSSetEdit(uniGridTB1.MoldIDColumn.ColumnName, "MoldID", 120, enumDef.FieldType.Default, enumDef.CharCase.Default, false, enumDef.HAlign.Left, 40);
            this.uniGrid1.SSSetEdit(uniGridTB1.MoldNameColumn.ColumnName, "MoldName", 120, enumDef.FieldType.Default, enumDef.CharCase.Default, true, enumDef.HAlign.Left, 255);
            this.uniGrid1.SSSetFloat(uniGridTB1.CavityColumn.ColumnName, "Cavity", viewTB19029.ggUserDefined9, enumDef.FieldType.Default, enumDef.HAlign.Right, true, enumDef.PosZero.Default, 0, int.MaxValue);
            this.uniGrid1.SSSetEdit(uniGridTB1.WarehouseIDColumn.ColumnName, "WarehouseID", 120, enumDef.FieldType.Default, enumDef.CharCase.Default, false, enumDef.HAlign.Left, 8);
            this.uniGrid1.SSSetEdit(uniGridTB1.WarehouseNameColumn.ColumnName, "WarehouseName", 120, enumDef.FieldType.Default, enumDef.CharCase.Default, true, enumDef.HAlign.Left, 255);
            this.uniGrid1.SSSetEdit(uniGridTB1.StoreLocationIDColumn.ColumnName, "StoreLocationID", 120, enumDef.FieldType.Default, enumDef.CharCase.Default, false, enumDef.HAlign.Left, 8);
            this.uniGrid1.SSSetEdit(uniGridTB1.StoreLocationNameColumn.ColumnName, "StoreLocationName", 120, enumDef.FieldType.Default, enumDef.CharCase.Default, true, enumDef.HAlign.Left, 255);
            this.uniGrid1.SSSetEdit(uniGridTB1.LineIDColumn.ColumnName, "LineID", 120, enumDef.FieldType.ReadOnly, enumDef.CharCase.Default, false, enumDef.HAlign.Left, 8);
            this.uniGrid1.SSSetEdit(uniGridTB1.LineNameColumn.ColumnName, "LineName", 120, enumDef.FieldType.ReadOnly, enumDef.CharCase.Default, false, enumDef.HAlign.Left, 255);
            this.uniGrid1.SSSetEdit(uniGridTB1.WorkCenterIDColumn.ColumnName, "WorkCenterID", 120, enumDef.FieldType.Default, enumDef.CharCase.Default, false, enumDef.HAlign.Left, 8);
            this.uniGrid1.SSSetEdit(uniGridTB1.WorkCenterNameColumn.ColumnName, "WorkCenterName", 120, enumDef.FieldType.Default, enumDef.CharCase.Default, true, enumDef.HAlign.Left, 255);
            this.uniGrid1.SSSetEdit(uniGridTB1.PartnerIDColumn.ColumnName, "PartnerID", 120, enumDef.FieldType.Default, enumDef.CharCase.Default, false, enumDef.HAlign.Left, 8);
            this.uniGrid1.SSSetEdit(uniGridTB1.PartnerNameColumn.ColumnName, "PartnerName", 120, enumDef.FieldType.Default, enumDef.CharCase.Default, true, enumDef.HAlign.Left, 255);
            this.uniGrid1.SSSetEdit(uniGridTB1.MinimumLaborClassColumn.ColumnName, "MinimumLaborClass", 120, enumDef.FieldType.Default, enumDef.CharCase.Default, false, enumDef.HAlign.Left, 8);
            this.uniGrid1.SSSetEdit(uniGridTB1.MinimumLaborClassNameColumn.ColumnName, "MinimumLaborClassName", 120, enumDef.FieldType.Default, enumDef.CharCase.Default, true, enumDef.HAlign.Left, 255);
            this.uniGrid1.SSSetEdit(uniGridTB1.ProjectNumberColumn.ColumnName, "ProjectNumber", 120, enumDef.FieldType.Default, enumDef.CharCase.Default, false, enumDef.HAlign.Left, 80);
            this.uniGrid1.SSSetFloat(uniGridTB1.ProjectNumberSequenceColumn.ColumnName, "ProjectNumberSequence", viewTB19029.ggUserDefined6, enumDef.FieldType.Default, enumDef.HAlign.Right, true, enumDef.PosZero.Default, 0, int.MaxValue);
            this.uniGrid1.SSSetEdit(uniGridTB1.SL_CDColumn.ColumnName, "SL_CD", 120, enumDef.FieldType.ReadOnly, enumDef.CharCase.Default, false, enumDef.HAlign.Left, 255);
            this.uniGrid1.SSSetEdit(uniGridTB1.BIN_CDColumn.ColumnName, "BIN_CD", 120, enumDef.FieldType.ReadOnly, enumDef.CharCase.Default, false, enumDef.HAlign.Left, 255);
            this.uniGrid1.SSSetCheck(uniGridTB1.isClosedColumn.ColumnName, "isClosed", 160, enumDef.FieldType.ReadOnly, chkFilter, "", enumDef.HAlign.Center, enumDef.HAlign.Center);
            this.uniGrid1.SSSetEdit(uniGridTB1.ManufactureOrderDescriptionColumn.ColumnName, "ManufactureOrderDescription", 120, enumDef.FieldType.Default, enumDef.CharCase.Default, false, enumDef.HAlign.Left, 8000);
            this.uniGrid1.SSSetEdit(uniGridTB1.RemarksColumn.ColumnName, "Remarks", 120, enumDef.FieldType.Default, enumDef.CharCase.Default, false, enumDef.HAlign.Left, 8000);
            this.uniGrid1.SSSetEdit(uniGridTB1.RegDateTimeColumn.ColumnName, "RegDateTime", 120, enumDef.FieldType.Default, enumDef.CharCase.Default, false, enumDef.HAlign.Left, 255);
            this.uniGrid1.SSSetEdit(uniGridTB1.RegUserIDColumn.ColumnName, "RegUserID", 120, enumDef.FieldType.Default, enumDef.CharCase.Default, false, enumDef.HAlign.Left, 255);
            this.uniGrid1.SSSetEdit(uniGridTB1.UpdateDateTimeColumn.ColumnName, "UpdateDateTime", 120, enumDef.FieldType.Default, enumDef.CharCase.Default, false, enumDef.HAlign.Left, 255);
            this.uniGrid1.SSSetEdit(uniGridTB1.UpdateUserIDColumn.ColumnName, "UpdateUserID", 120, enumDef.FieldType.Default, enumDef.CharCase.Default, false, enumDef.HAlign.Left, 255);
            #endregion

            #region -- [uniGrid2] WorkLot --
            tdsPIR101M1.B_WorkLotDataTable uniGridTB2 = cqtdsPIR101M1.B_WorkLot;
            this.uniGrid2.SSSetEdit(uniGridTB2.LotNumberColumn.ColumnName, "LotNumber", 120, enumDef.FieldType.ReadOnly, enumDef.CharCase.Default, false, enumDef.HAlign.Left, 255);
            this.uniGrid2.SSSetEdit(uniGridTB2.ProductOrderNumberColumn.ColumnName, "ProductOrderNumber", 120, enumDef.FieldType.ReadOnly, enumDef.CharCase.Default, false, enumDef.HAlign.Left, 80);
            this.uniGrid2.SSSetEdit(uniGridTB2.ProductIDColumn.ColumnName, "ProductID", 100, enumDef.FieldType.ReadOnly, enumDef.CharCase.Default, false, enumDef.HAlign.Left, 80);
            this.uniGrid2.SSSetEdit(uniGridTB2.ProductNameColumn.ColumnName, "ProductName", 120, enumDef.FieldType.ReadOnly, enumDef.CharCase.Default, false, enumDef.HAlign.Left, 255);
            this.uniGrid2.SSSetEdit(uniGridTB2.SpecColumn.ColumnName, "Spec", 120, enumDef.FieldType.ReadOnly, enumDef.CharCase.Default, false, enumDef.HAlign.Left, 255);
            this.uniGrid2.SSSetEdit(uniGridTB2.WorkCenterIDColumn.ColumnName, "WorkCenterID", 40, enumDef.FieldType.ReadOnly, enumDef.CharCase.Default, false, enumDef.HAlign.Left, 20);
            this.uniGrid2.SSSetEdit(uniGridTB2.LineIDColumn.ColumnName, "LineID", 40, enumDef.FieldType.ReadOnly, enumDef.CharCase.Default, false, enumDef.HAlign.Left, 20);
            this.uniGrid2.SSSetEdit(uniGridTB2.RoutingIDColumn.ColumnName, "RoutingID", 40, enumDef.FieldType.ReadOnly, enumDef.CharCase.Default, false, enumDef.HAlign.Left, 20);
            this.uniGrid2.SSSetFloat(uniGridTB2.OrderQuantityColumn.ColumnName, "OrderQuantity", viewTB19029.ggUserDefined6, enumDef.FieldType.ReadOnly, enumDef.HAlign.Right, true, enumDef.PosZero.Default, 0, int.MaxValue);
            this.uniGrid2.SSSetFloat(uniGridTB2.QuantityColumn.ColumnName, "Quantity", viewTB19029.ggUserDefined6, enumDef.FieldType.ReadOnly, enumDef.HAlign.Right, true, enumDef.PosZero.Default, 0, int.MaxValue);
            this.uniGrid2.SSSetFloat(uniGridTB2.GoodQuantityColumn.ColumnName, "GoodQuantity", viewTB19029.ggUserDefined6, enumDef.FieldType.ReadOnly, enumDef.HAlign.Right, true, enumDef.PosZero.Default, 0, int.MaxValue);
            this.uniGrid2.SSSetFloat(uniGridTB2.BadQuantityColumn.ColumnName, "BadQuantity", viewTB19029.ggUserDefined6, enumDef.FieldType.ReadOnly, enumDef.HAlign.Right, true, enumDef.PosZero.Default, 0, int.MaxValue);
            this.uniGrid2.SSSetFloat(uniGridTB2.RestQuantityColumn.ColumnName, "RestQuantity", viewTB19029.ggUserDefined6, enumDef.FieldType.ReadOnly, enumDef.HAlign.Right, true, enumDef.PosZero.Default, 0, int.MaxValue);
            this.uniGrid2.SSSetEdit(uniGridTB2.LocationIDColumn.ColumnName, "LocationID", 40, enumDef.FieldType.ReadOnly, enumDef.CharCase.Default, false, enumDef.HAlign.Left, 20);
            this.uniGrid2.SSSetEdit(uniGridTB2.WarehouseIDColumn.ColumnName, "WarehouseID", 40, enumDef.FieldType.ReadOnly, enumDef.CharCase.Default, false, enumDef.HAlign.Left, 20);
            this.uniGrid2.SSSetEdit(uniGridTB2.EquipmentIDColumn.ColumnName, "EquipmentID", 80, enumDef.FieldType.ReadOnly, enumDef.CharCase.Default, false, enumDef.HAlign.Left, 40);
            this.uniGrid2.SSSetEdit(uniGridTB2.MoldIDColumn.ColumnName, "MoldID", 80, enumDef.FieldType.ReadOnly, enumDef.CharCase.Default, false, enumDef.HAlign.Left, 40);
            this.uniGrid2.SSSetFloat(uniGridTB2.CavityColumn.ColumnName, "Cavity", viewTB19029.ggUserDefined9, enumDef.FieldType.Default, enumDef.HAlign.Right, true, enumDef.PosZero.Default, 0, int.MaxValue);
            this.uniGrid2.SSSetCheck(uniGridTB2.isPrintColumn.ColumnName, "isPrint", 160, enumDef.FieldType.ReadOnly, chkFilter, "", enumDef.HAlign.Center, enumDef.HAlign.Center);
            this.uniGrid2.SSSetFloat(uniGridTB2.PrintCountColumn.ColumnName, "PrintCount", viewTB19029.ggUserDefined6, enumDef.FieldType.Default, enumDef.HAlign.Right, true, enumDef.PosZero.Default, 0, int.MaxValue);
            this.uniGrid2.SSSetEdit(uniGridTB2.RemarksColumn.ColumnName, "Remarks", 120, enumDef.FieldType.ReadOnly, enumDef.CharCase.Default, false, enumDef.HAlign.Left, 80);
            this.uniGrid2.SSSetEdit(uniGridTB2.LabelPrintRemarksColumn.ColumnName, "LabelPrintRemarks", 120, enumDef.FieldType.ReadOnly, enumDef.CharCase.Default, false, enumDef.HAlign.Left, 255);
            this.uniGrid2.SSSetEdit(uniGridTB2.RegDateTimeColumn.ColumnName, "RegDateTime", 120, enumDef.FieldType.Default, enumDef.CharCase.Default, false, enumDef.HAlign.Left, 255);
            this.uniGrid2.SSSetEdit(uniGridTB2.RegUserIDColumn.ColumnName, "RegUserID", 120, enumDef.FieldType.Default, enumDef.CharCase.Default, false, enumDef.HAlign.Left, 255);
            this.uniGrid2.SSSetEdit(uniGridTB2.UpdateDateTimeColumn.ColumnName, "UpdateDateTime", 120, enumDef.FieldType.Default, enumDef.CharCase.Default, false, enumDef.HAlign.Left, 255);
            this.uniGrid2.SSSetEdit(uniGridTB2.UpdateUserIDColumn.ColumnName, "UpdateUserID", 120, enumDef.FieldType.Default, enumDef.CharCase.Default, false, enumDef.HAlign.Left, 255);
            #endregion

            #endregion

            #region ■■ 3.1.2 Formatting grid information

            this.uniGrid1.ShowHeaderCheck = false;
            this.uniGrid2.ShowHeaderCheck = false;
            this.uniGrid1.InitializeGrid(enumDef.IsOutlookGroupBy.No, enumDef.IsSearch.Yes);
            this.uniGrid2.InitializeGrid(enumDef.IsOutlookGroupBy.No, enumDef.IsSearch.Yes);

            #endregion

            #region ■■ 3.1.3 Setting etc grid

            // Hidden Column Setting
            #region -- [uniGrid1] ProductOrder --
            this.uniGrid1.SSSetColHidden(uniGridTB1.LotNumberColumn.ColumnName);
            this.uniGrid1.SSSetColHidden(uniGridTB1.TrackingNumberColumn.ColumnName);
            this.uniGrid1.SSSetColHidden(uniGridTB1.UserDefinedLotNumberColumn.ColumnName);
            this.uniGrid1.SSSetColHidden(uniGridTB1.RequestDivisionIDColumn.ColumnName);
            this.uniGrid1.SSSetColHidden(uniGridTB1.RequestDateColumn.ColumnName);
            this.uniGrid1.SSSetColHidden(uniGridTB1.RequestSequenceColumn.ColumnName);
            this.uniGrid1.SSSetColHidden(uniGridTB1.isMultiRoutingColumn.ColumnName);
            this.uniGrid1.SSSetColHidden(uniGridTB1.EquipmentIDColumn.ColumnName);
            this.uniGrid1.SSSetColHidden(uniGridTB1.EquipmentNameColumn.ColumnName);
            this.uniGrid1.SSSetColHidden(uniGridTB1.MoldIDColumn.ColumnName);
            this.uniGrid1.SSSetColHidden(uniGridTB1.MoldNameColumn.ColumnName);
            this.uniGrid1.SSSetColHidden(uniGridTB1.WarehouseIDColumn.ColumnName);
            this.uniGrid1.SSSetColHidden(uniGridTB1.WarehouseNameColumn.ColumnName);
            this.uniGrid1.SSSetColHidden(uniGridTB1.StoreLocationIDColumn.ColumnName);
            this.uniGrid1.SSSetColHidden(uniGridTB1.StoreLocationNameColumn.ColumnName);
            this.uniGrid1.SSSetColHidden(uniGridTB1.WorkCenterIDColumn.ColumnName);
            this.uniGrid1.SSSetColHidden(uniGridTB1.WorkCenterNameColumn.ColumnName);
            this.uniGrid1.SSSetColHidden(uniGridTB1.PartnerIDColumn.ColumnName);
            this.uniGrid1.SSSetColHidden(uniGridTB1.PartnerNameColumn.ColumnName);
            this.uniGrid1.SSSetColHidden(uniGridTB1.MinimumLaborClassColumn.ColumnName);
            this.uniGrid1.SSSetColHidden(uniGridTB1.MinimumLaborClassNameColumn.ColumnName);
            this.uniGrid1.SSSetColHidden(uniGridTB1.ProjectNumberColumn.ColumnName);
            this.uniGrid1.SSSetColHidden(uniGridTB1.ProjectNumberSequenceColumn.ColumnName);
            this.uniGrid1.SSSetColHidden(uniGridTB1.SL_CDColumn.ColumnName);
            this.uniGrid1.SSSetColHidden(uniGridTB1.BIN_CDColumn.ColumnName);
            this.uniGrid1.SSSetColHidden(uniGridTB1.RegUserIDColumn.ColumnName);
            this.uniGrid1.SSSetColHidden(uniGridTB1.RegDateTimeColumn.ColumnName);
            this.uniGrid1.SSSetColHidden(uniGridTB1.UpdateUserIDColumn.ColumnName);
            this.uniGrid1.SSSetColHidden(uniGridTB1.UpdateDateTimeColumn.ColumnName);
            #endregion

            #region -- [uniGrid2] WorkLot --
            this.uniGrid2.SSSetColHidden(uniGridTB2.ProductOrderNumberColumn.ColumnName);
            this.uniGrid2.SSSetColHidden(uniGridTB2.WorkCenterIDColumn.ColumnName);
            this.uniGrid2.SSSetColHidden(uniGridTB2.LineIDColumn.ColumnName);
            this.uniGrid2.SSSetColHidden(uniGridTB2.RoutingIDColumn.ColumnName);
            this.uniGrid2.SSSetColHidden(uniGridTB2.LocationIDColumn.ColumnName);
            this.uniGrid2.SSSetColHidden(uniGridTB2.WarehouseIDColumn.ColumnName);
            this.uniGrid2.SSSetColHidden(uniGridTB2.EquipmentIDColumn.ColumnName);
            this.uniGrid2.SSSetColHidden(uniGridTB2.MoldIDColumn.ColumnName);
            this.uniGrid2.SSSetColHidden(uniGridTB2.CavityColumn.ColumnName);
            this.uniGrid2.SSSetColHidden(uniGridTB2.isPrintColumn.ColumnName);
            this.uniGrid2.SSSetColHidden(uniGridTB2.PrintCountColumn.ColumnName);
            this.uniGrid2.SSSetColHidden(uniGridTB2.RemarksColumn.ColumnName);
            this.uniGrid2.SSSetColHidden(uniGridTB2.RegUserIDColumn.ColumnName);
            this.uniGrid2.SSSetColHidden(uniGridTB2.RegDateTimeColumn.ColumnName);
            this.uniGrid2.SSSetColHidden(uniGridTB2.UpdateUserIDColumn.ColumnName);
            this.uniGrid2.SSSetColHidden(uniGridTB2.UpdateDateTimeColumn.ColumnName);
            #endregion

            #endregion
        }

        #endregion

        #region ■ 3.2 InitData

        private void InitData()
        {
            // TO-DO: 컨트롤을 초기화(또는 초기값)할때 할일 
            // SetDefaultVal과의 차이점은 전자는 Form_Load 시점에 콘트롤에 초기값을 세팅하는것이고
            // 후자는 특정 시점(조회후 또는 행추가후 등 특정이벤트)에서 초기값을 셋팅한다.
        }

        #endregion

        #region ■ 3.3 SetSpreadColor

        private void SetSpreadColor(int pvStartRow, int pvEndRow)
        {
            // TO-DO: InsertRow후 그리드 컬러 변경
            //uniGrid1.SSSetProtected(gridCol.LastNum, pvStartRow, pvEndRow);
        }

        #endregion

        #region ■ 3.4 InitControlBinding

        protected override void InitControlBinding()
        {
            // Grid binding with global dataset variable.
            this.InitSpreadSheet();
            this.uniGrid1.uniGridSetDataBinding(this.cqtdsPIR101M1.B_ProductOrder);
            this.uniGrid2.uniGridSetDataBinding(this.cqtdsPIR101M1.B_WorkLot);
            string[] masterKey = { this.cqtdsPIR101M1.B_ProductOrder.ProductOrderNumberColumn.ColumnName };
            string[] detailKey = { this.cqtdsPIR101M1.B_WorkLot.ProductOrderNumberColumn.ColumnName };
            //string[] masterKey = { this.cqtdsPIR101M1.B_ProductOrder.LotNumberColumn.ColumnName };
            //string[] detailKey = { this.cqtdsPIR101M1.B_WorkLot.LotNumberColumn.ColumnName };

            this.uniGrid1.RegisterDetailGrid(this.uniGrid2, masterKey, detailKey);
        }

        #endregion

        #endregion

        #region ▶ 4. Toolbar method part

        #region ■ 4.1 Common Fnction group

        #region ■■ 4.1.1 OnFncQuery(old:FncQuery)

        protected override bool OnFncQuery()
        {
            //TO-DO : code business oriented logic
            return DBQuery();
        }

        #endregion

        #region ■■ 4.1.2 OnFncSave(old:FncSave)

        protected override bool OnFncSave()
        {
            //TO-DO : code business oriented logic
            return DBSave();
        }

        #endregion

        #endregion

        #region ■ 4.2 Single Fnction group

        #region ■■ 4.2.1 OnFncNew(old:FncNew)

        protected override bool OnFncNew()
        {
            //TO-DO : code business oriented logic
            return true;
        }

        #endregion

        #region ■■ 4.2.2 OnFncDelete(old:FncDelete)

        protected override bool OnFncDelete()
        {
            //TO-DO : code business oriented logic
            return true;
        }

        #endregion

        #region ■■ 4.2.3 OnFncCopy(old:FncCopy)

        protected override bool OnFncCopy()
        {
            //TO-DO : code business oriented logic
            return true;
        }

        #endregion

        #region ■■ 4.2.4 OnFncFirst(No implementation)

        #endregion

        #region ■■ 4.2.5 OnFncPrev(old:FncPrev)

        protected override bool OnFncPrev()
        {
            //TO-DO : code business oriented logic
            return true;
        }

        #endregion

        #region ■■ 4.2.6 OnFncNext(old:FncNext)

        protected override bool OnFncNext()
        {
            //TO-DO : code business oriented logic
            return true;
        }

        #endregion

        #region ■■ 4.2.7 OnFncLast(No implementation)

        #endregion

        #endregion

        #region ■ 4.3 Grid Fnction group

        #region ■■ 4.3.1 OnFncInsertRow(old:FncInsertRow)

        protected override bool OnFncInsertRow()
        {
            if (ActiveGrid == uniGrid1)
            {
                if (uniGrid1.ActiveRow == null)
                {
                    return false;
                }

                if (cbDivisionID.SelectedItem == null)
                {
                    uniBase.UMessage.DisplayMessageBox("970029", MessageBoxButtons.OK, new string[] { lblDivision.Text });
                    uniGrid1.ActiveRow.Delete();
                    return false;
                }

                uniGrid1.ActiveRow.Cells["DivisionID"].Value = cbDivisionID.SelectedItem.DataValue.ToString();
                uniGrid1.ActiveRow.Cells["ManufactureOrderDate"].Value = DateTime.Now;
                uniGrid1.ActiveRow.Cells["StartDate"].Value = DateTime.Now;
                uniGrid1.ActiveRow.Cells["DueDate"].Value = DateTime.Now;
                uniGrid1.ActiveRow.Cells["Status"].Value = "-1";
                uniGrid1.ActiveRow.Cells["isOutsourcing"].Value = "0";
                uniGrid1.ActiveRow.Cells["isMultiRouting"].Value = "0";
                uniGrid1.ActiveRow.Cells["isClosed"].Value = "0";
            }
            else if (ActiveGrid == uniGrid2)
            {
                if (uniGrid2.ActiveRow == null)
                {
                    return false;
                }

                ActiveGrid.editUndo();
            }

            return true;
        }

        #endregion

        #region ■■ 4.3.2 OnFncDeleteRow(old:FncDeleteRow)

        protected override bool OnFncDeleteRow()
        {
            //TO-DO : code business oriented logic
            if (ActiveGrid == uniGrid1)
            {
                if (int.Parse(ActiveGrid.ActiveRow.Cells["Status"].Value.ToString()) > -1)
                {
                    //uniBase.UMessage.DisplayMessageBox("DT9999", MessageBoxButtons.OK, "확정, 작업중, 작업완료상태는 수정할 수 없습니다.");
                    uniGrid1.editUndo();
                    return false;
                }
            }
            return true;
        }

        #endregion

        #region ■■ 4.3.3 OnFncCancel(old:FncCancel)

        protected override bool OnFncCancel()
        {
            //TO-DO : code business oriented logic
            return true;
        }

        #endregion

        #region ■■ 4.3.4 OnFncCopyRow(old:FncCopy)

        protected override bool OnFncCopyRow()
        {
            //TO-DO : code business oriented logic
            return true;
        }

        #endregion

        #endregion

        #region ■ 4.4 Db function group

        #region ■■ 4.4.1 DBQuery(Common)

        private bool DBQuery()
        {
            try
            {
                string strProcName = "Manufacture.RetrieveProductOrder";

                using (Bizentro.AppFramework.DataBridge.uniCommand iuniCommand = uniBase.UDatabase.GetStoredProcCommand(strProcName))
                {
                    uniBase.UDatabase.AddInParameter(iuniCommand, "@DivisionID", SqlDbType.NVarChar, cbDivisionID.SelectedItem.DataValue.ToString());
                    uniBase.UDatabase.AddInParameter(iuniCommand, "@ManufactureOrderDateFrom", SqlDbType.Date, dtDateTerm.uniFromValue);
                    uniBase.UDatabase.AddInParameter(iuniCommand, "@ManufactureOrderDateTo", SqlDbType.Date, dtDateTerm.uniToValue);
                    uniBase.UDatabase.AddInParameter(iuniCommand, "@ManufactureOrderType", SqlDbType.NVarChar, cbOrderType.SelectedItem == null ? string.Empty : cbOrderType.SelectedItem.DataValue.ToString());
                    uniBase.UDatabase.AddInParameter(iuniCommand, "@LineID", SqlDbType.NVarChar, popLine.CodeValue == null ? string.Empty : popLine.CodeValue);
                    uniBase.UDatabase.AddInParameter(iuniCommand, "@ModelID", SqlDbType.NVarChar, popModel.CodeValue == null ? string.Empty : popModel.CodeValue);
                    uniBase.UDatabase.AddInParameter(iuniCommand, "@ProductID", SqlDbType.NVarChar, popProductID.CodeValue == null ? string.Empty : popProductID.CodeValue);
                    uniBase.UDatabase.AddInParameter(iuniCommand, "@Status", SqlDbType.Int, cbStatus.SelectedItem == null ? DBNull.Value : cbStatus.SelectedItem.DataValue);
                    uniBase.UDatabase.AddInParameter(iuniCommand, "@ManagedNumber", SqlDbType.NVarChar, txtProductOrderNumber.Text);

                    using (DataSet dsResult = uniBase.UDatabase.ExecuteDataSet(iuniCommand))
                    {
                        if (dsResult.Tables[0].Rows.Count < 1)
                        {
                            uniBase.UMessage.DisplayMessageBox("900014", MessageBoxButtons.OK);
                            this.Focus();
                        }
                        else
                        {
                            cqtdsPIR101M1.Clear();
                            cqtdsPIR101M1.B_ProductOrder.Merge(dsResult.Tables[0], false, MissingSchemaAction.Ignore);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                bool reThrow = ExceptionControler.AutoProcessException(ex);
                if (reThrow)
                    throw;
                return false;
            }
            finally
            {
                //if (iqtdsTypedDataSet != null) iqtdsTypedDataSet.Dispose();
                //if (iqtdsICondition != null) iqtdsICondition.Dispose();
            }

            return true;
        }

        private bool DBQuery2(params string[] strParameters)
        {
            try
            {
                string strProcName = "Production.RetrieveWorkLotByProductOrderNumber";

                using (Bizentro.AppFramework.DataBridge.uniCommand iuniCommand = uniBase.UDatabase.GetStoredProcCommand(strProcName))
                {
                    uniBase.UDatabase.AddInParameter(iuniCommand, "@ProductOrderNumber", SqlDbType.NVarChar, 16, strParameters[0]);
                    //uniBase.UDatabase.AddInParameter(iuniCommand, "@LotNumber", SqlDbType.NVarChar, 255, strParameters[1]);

                    using (DataSet dsResult = uniBase.UDatabase.ExecuteDataSet(iuniCommand))
                    {
                        if (dsResult.Tables[0].Rows.Count < 1)
                        {
                            this.Focus();
                        }
                        else
                        {
                            cqtdsPIR101M1.B_WorkLot.Merge(dsResult.Tables[0], false, MissingSchemaAction.Ignore);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                bool reThrow = ExceptionControler.AutoProcessException(ex);
                if (reThrow)
                    throw;
                return false;
            }
            finally
            {
                //if (iqtdsTypedDataSet != null) iqtdsTypedDataSet.Dispose();
                //if (iqtdsICondition != null) iqtdsICondition.Dispose();
            }

            return true;
        }

        #endregion

        #region ■■ 4.4.2 DBDelete(Single)

        private bool DBDelete()
        {
            //TO-DO : code business oriented logic
            return true;
        }

        #endregion

        #region ■■ 4.4.3 DBSave(Common)

        private bool DBSave()
        {
            this.uniGrid1.UpdateData();

            DataTable chgDt = null;

            try
            {
                chgDt = new DataTable();
                chgDt.Merge(new tdsPIR101M1.B_ProductOrderDataTable(), false, MissingSchemaAction.Add);
                chgDt.Merge(cqtdsPIR101M1.B_ProductOrder, false, MissingSchemaAction.Ignore);

                DataView dv = chgDt.DefaultView;
                dv.RowFilter = "cud_char = '' or cud_char is null";

                foreach (DataRowView rw in dv)
                {
                    rw.Delete();
                }

                chgDt.AcceptChanges();
                dv.RowFilter = string.Empty;

                if (chgDt == null) chgDt.Rows.Add(chgDt.NewRow());
                chgDt.Columns.Remove("DivisionID");
                chgDt.Columns.Remove("ErpOrderQuantity");
                chgDt.Columns.Remove("ShiftTypeName");
                chgDt.Columns.Remove("ModelDescription");
                chgDt.Columns.Remove("EquipmentName");
                chgDt.Columns.Remove("MoldName");
                chgDt.Columns.Remove("WarehouseName");
                chgDt.Columns.Remove("StoreLocationName");
                chgDt.Columns.Remove("LineName");
                chgDt.Columns.Remove("WorkCenterName");
                chgDt.Columns.Remove("PartnerName");
                chgDt.Columns.Remove("MinimumLaborClassName");
                chgDt.Columns.Remove("RestQuantity");
                chgDt.Columns.Remove("ProductName");
                chgDt.Columns.Remove("Spec");
                chgDt.Columns.Remove("SL_CD");
                chgDt.Columns.Remove("BIN_CD");
                chgDt.Columns.Remove("isClosed");
                chgDt.Columns.Remove("RegDateTime");
                chgDt.Columns.Remove("RegUserID");
                chgDt.Columns.Remove("UpdateDateTime");
                chgDt.Columns.Remove("UpdateUserID");

                string strProcName = "Manufacture.SaveManufactureOrder";

                using (Bizentro.AppFramework.DataBridge.uniCommand iuniCommand = uniBase.UDatabase.GetStoredProcCommand(strProcName))
                {
                    uniBase.UDatabase.AddInParameter(iuniCommand, "@DivisionID", SqlDbType.NVarChar, 2, uniGrid1.ActiveRow.Cells["DivisionID"].Value.ToString());
                    uniBase.UDatabase.AddInParameter(iuniCommand, "@ManufactureOrder", SqlDbType.Structured, chgDt);
                    uniBase.UDatabase.AddInParameter(iuniCommand, "@UserID", SqlDbType.NVarChar, 40, CommonVariable.gUsrID);
                    uniBase.UDatabase.AddOutParameter(iuniCommand, "@MessageCode", SqlDbType.NVarChar, 6);
                    uniBase.UDatabase.AddOutParameter(iuniCommand, "@Message", SqlDbType.NVarChar, 200);
                    uniBase.UDatabase.AddReturnParameter(iuniCommand, "RETURN_VALUE", SqlDbType.Int, 9);

                    uniBase.UDatabase.ExecuteNonQuery(iuniCommand, false);

                    int iReturn = (int)uniBase.UDatabase.GetParameterValue(iuniCommand, "RETURN_VALUE");

                    if (iReturn < 0)
                    {
                        string sMsgCd = uniBase.UDatabase.GetParameterValue(iuniCommand, "@MessageCode") as string;
                        string sMessage = uniBase.UDatabase.GetParameterValue(iuniCommand, "@Message") as string;
                        //int iPosition = (int)uniBase.UDatabase.GetParameterValue(iuniCommand, "@ERR_POS");

                        if (string.IsNullOrEmpty(sMsgCd)) sMsgCd = "DT9999";

                        uniBase.UMessage.DisplayMessageBox(sMsgCd, MessageBoxButtons.OK, sMessage);

                        uniGrid1.Focus();
                        return false;
                    }
                }

                if (uniGrid1.DataSource != null) ((DataTable)uniGrid1.DataSource).Rows.Clear();
            }
            catch (Exception ex)
            {
                bool reThrow = ExceptionControler.AutoProcessException(ex);
                if (reThrow)
                    throw;
                return false;
            }
            finally
            {
                if (chgDt != null) chgDt.Dispose();
            }

            return true;
        }

        #endregion

        #endregion

        #endregion

        #region ▶ 5. Event method part

        #region ■ 5.1 Single control event implementation group

        private void cbDivisionID_SelectionChanged(object sender, EventArgs e)
        {
            cqtdsPIR101M1.B_ProductOrder.Clear();
            this.OnFncQuery();
        }

        #endregion

        #region ■ 5.2 Grid   control event implementation group

        #region ■■ 5.2.1 ButtonClicked >>> ClickCellButton

        /// <summary>
        /// Cell 내의 버튼을 클릭했을때의 일련작업들을 수행합니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uniGrid1_ClickCellButton(object sender, CellEventArgs e)
        {

        }

        #endregion ■■ ButtonClicked >>> ClickCellButton

        #region ■■ 5.2.2 Change >>> CellChange

        /// <summary>
        /// fpSpread의 Change 이벤트는 UltraGrid의 BeforeExitEditMode 또는 AfterExitEditMode 이벤트로 대체됩니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uniGrid1_BeforeExitEditMode(object sender, Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs e)
        {

        }

        private void uniGrid1_AfterExitEditMode(object sender, EventArgs e)
        {

        }

        #endregion ■■ Change >>> CellChange

        #region ■■ 5.2.3 Click >>> AfterCellActivate | AfterRowActivate | AfterSelectChange

        private void uniGrid1_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {

        }

        private void uniGrid1_AfterCellActivate(object sender, EventArgs e)
        {

        }

        private void uniGrid1_AfterRowActivate(object sender, EventArgs e)
        {

        }

        #endregion ■■ Click >>> AfterSelectChange

        #region ■■ 5.2.4 ComboSelChange >>> CellListSelect

        /// <summary>
        /// Cell 내의 콤보박스의 Item을 선택 변경했을때 이벤트가 발생합니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uniGrid1_CellListSelect(object sender, CellEventArgs e)
        {

        }

        #endregion ■■ ComboSelChange >>> CellListSelect

        #region ■■ 5.2.5 DblClick >>> DoubleClickCell

        /// <summary>
        /// fpSpread의 DblClick이벤트는 UltraGrid의 DoubleClickCell이벤트로 변경 하실 수 있습니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uniGrid1_DoubleClickCell(object sender, DoubleClickCellEventArgs e)
        {

        }

        #endregion ■■ DblClick >>> DoubleClickCell

        #region ■■ 5.2.6 MouseDown >>> MouseDown

        /// <summary>
        /// 마우스 우측 버튼 클릭시 Context메뉴를 보여주는 일련의 작업들을 이 이벤트에서 수행합니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uniGrid1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        #endregion ■■ MouseDown >>> MouseDown

        #region ■■ 5.2.7 ScriptLeaveCell >>> BeforeCellDeactivate

        /// <summary>
        /// fpSpread의 ScripLeaveCell 이벤트는 UltraGrid의 
        /// BeforeCellDeactivate 이벤트와 AfterCellActivate 이벤트를 겸해서 사용합니다.
        /// BeforeCellDeactivate    : 기존Cell에서 새로운 Cell로 이동하기 전에 기존Cell위치에서 처리 할 일련의 작업들을 기술합니다.
        /// AfterCellActivate       : 새로운 Cell로 이동해서 처리할 일련의 작업들을 기술합니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uniGrid1_BeforeCellDeactivate(object sender, CancelEventArgs e)
        {

        }

        #endregion ■■ ScriptLeaveCell >>> BeforeCellDeactivate

        private void uniGrid1_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
        {
            if (e.Cell.Row == null)
            {
                return;
            }

            if (e.Cell.Row.Cells["Status"].Value.ToString() != "" && Int32.Parse(e.Cell.Row.Cells["Status"].Value.ToString()) >= 0)
            {
                uniBase.UMessage.DisplayMessageBox("DT9999", MessageBoxButtons.OK, "확정, 작업중, 작업완료상태는 수정할 수 없습니다.");
                uniGrid1.editUndo();
                e.Cancel = true;
                //uniGrid2.ResetRowUpdateCancelAction();
                //uniGrid2.ActiveCell.CancelUpdate();
            }
            else if (e.Cell.Row.Cells["Status"].Value.Equals("-1"))
            {
            }

            if (e.Cell.Row.Cells["isClosed"].Value.ToString() == "1")
            {
                uniBase.UMessage.DisplayMessageBox("DT9999", MessageBoxButtons.OK, "마감된 오더는 수정할 수 없습니다.");
                uniGrid1.editUndo();
                e.Cancel = true;
                return;
            }
        }

        #endregion

        #region ■ 5.3 TAB    control event implementation group

        #endregion

        #endregion

        #region ▶ 6. Popup method part

        #region ■ 6.1 Common popup implementation group

        private void uniGrid1_BeforePopupOpen(object sender, AppFramework.UI.Controls.Popup.BeforePopupOpenEventArgs e)
        {
            if (uniGrid1.ActiveRow == null)
            {
                return;
            }
            if (uniGrid1.ActiveRow.Cells["Status"].Value.ToString() != "" && Int32.Parse(uniGrid1.ActiveRow.Cells["Status"].Value.ToString()) >= 0)
            {
                uniBase.UMessage.DisplayMessageBox("DT9999", MessageBoxButtons.OK, "확정, 작업중, 작업완료상태는 수정할 수 없습니다.");
                uniGrid1.editUndo();
                e.Cancel = true;
                //uniGrid2.ResetRowUpdateCancelAction();
                //uniGrid2.ActiveCell.CancelUpdate();
            }

            if (uniGrid1.ActiveRow.Cells["isClosed"].Value.ToString() == "1")
            {
                uniBase.UMessage.DisplayMessageBox("DT9999", MessageBoxButtons.OK, "마감된 오더는 수정할 수 없습니다.");
                uniGrid1.editUndo();
                e.Cancel = true;
                return;
            }

            switch (uniGrid1.ActiveCell.Column.Key)
            {
                case "ProductID":
                    e.PopupPassData.PopupWinTitle = "품목 PopUp";
                    e.PopupPassData.ConditionCaption = "품목";
                    e.PopupPassData.SQLFromStatements = @" Information.Product (nolock) A 
                                                           JOIN Information.ProductDetail (nolock) B ON B.DivisionID = '" + uniGrid1.ActiveRow.Cells["DivisionID"].Value.ToString() + @"' AND A.ProductID = B.ProductID AND B.ProductAccount IN (1, 2) 
                                                            JOIN Information.ProductAccount (nolock) C ON B.ProductAccount = C.ProductAccount
                                                            JOIN Information.ProductRouting (nolock) D ON B.DivisionID = D.DivisionID AND A.ProductID = D.ProductID AND D.isResultRouting = 1
                                                            LEFT OUTER JOIN Information.ProductRoutingEquipment (nolock) E ON B.DivisionID = E.DivisionID AND A.ProductID = E.ProductID AND D.RoutingSequence = E.RoutingSequence AND E.isDefault = 1
                                                            LEFT OUTER JOIN Information.CommonCodeDetail (nolock) H ON H.CodeTypeID = 'MODEL' AND A.ModelID = H.CodeID
                                                            OUTER APPLY ( Select Count(1) RoutingCount from Information.ProductRouting F where F.DivisionID = B.DivisionID and F.ProductID = A.ProductID) G";
                    e.PopupPassData.SQLWhereStatements = "A.isActive = 1";//"A.ITEM_CD LIKE '%-N%'";
                    e.PopupPassData.SQLWhereInputCodeValue = "";
                    e.PopupPassData.SQLWhereInputNameValue = "";
                    e.PopupPassData.DistinctOrNot = true;
                    e.PopupPassData.GridCellCode = new String[9];
                    e.PopupPassData.GridCellCaption = new String[9];
                    e.PopupPassData.GridCellType = new enumDef.GridCellType[9];
                    e.PopupPassData.GridCellLength = new int[9];
                    e.PopupPassData.GridCellCode[0] = "A.ProductID";
                    e.PopupPassData.GridCellCode[1] = "A.ProductName";
                    e.PopupPassData.GridCellCode[2] = "A.Spec";
                    e.PopupPassData.GridCellCode[3] = "C.ProductAccountName";
                    e.PopupPassData.GridCellCode[4] = "D.RoutingID";
                    e.PopupPassData.GridCellCode[5] = "D.RoutingDescription";
                    e.PopupPassData.GridCellCode[6] = "G.RoutingCount";
                    e.PopupPassData.GridCellCode[7] = "D.Cavity";
                    e.PopupPassData.GridCellCode[8] = "H.CodeDescription";
                    e.PopupPassData.GridCellCaption[0] = "품목코드";
                    e.PopupPassData.GridCellCaption[1] = "품목명";
                    e.PopupPassData.GridCellCaption[2] = "품목구분";
                    e.PopupPassData.GridCellCaption[3] = "규격";
                    e.PopupPassData.GridCellCaption[4] = "라우팅코드";
                    e.PopupPassData.GridCellCaption[5] = "라우팅설명";
                    e.PopupPassData.GridCellCaption[6] = "라우팅수";
                    e.PopupPassData.GridCellCaption[7] = "Cavity";
                    e.PopupPassData.GridCellCaption[8] = "품목유형상세";
                    e.PopupPassData.GridCellType[0] = enumDef.GridCellType.Edit;
                    e.PopupPassData.GridCellType[1] = enumDef.GridCellType.Edit;
                    e.PopupPassData.GridCellType[2] = enumDef.GridCellType.Edit;
                    e.PopupPassData.GridCellType[3] = enumDef.GridCellType.Edit;
                    e.PopupPassData.GridCellType[4] = enumDef.GridCellType.Edit;
                    e.PopupPassData.GridCellType[5] = enumDef.GridCellType.Hidden;
                    e.PopupPassData.GridCellType[6] = enumDef.GridCellType.Edit;
                    e.PopupPassData.GridCellType[7] = enumDef.GridCellType.Edit;
                    e.PopupPassData.GridCellType[8] = enumDef.GridCellType.Edit;
                    e.PopupPassData.PopupWinWidth = 800;
                    break;
                case "ShiftType":
                    e.PopupPassData.PopupWinTitle = "근무유형 PopUp";
                    e.PopupPassData.ConditionCaption = "근무유형";
                    e.PopupPassData.SQLFromStatements = " Information.ShiftType (nolock) A JOIN Information.ShiftTypeDetail (nolock) B ON A.DivisionID = B.DivisionID AND A.ShiftType = B.ShiftType";
                    e.PopupPassData.SQLWhereStatements = "A.DivisionID = '" + uniGrid1.ActiveRow.Cells["DivisionID"].Value.ToString() + "' and A.isActive = 1";//"A.ITEM_CD LIKE '%-N%'";
                    e.PopupPassData.SQLWhereInputCodeValue = "";
                    e.PopupPassData.SQLWhereInputNameValue = "";
                    e.PopupPassData.DistinctOrNot = true;
                    e.PopupPassData.GridCellCode = new String[4];
                    e.PopupPassData.GridCellCaption = new String[4];
                    e.PopupPassData.GridCellType = new enumDef.GridCellType[4];
                    e.PopupPassData.GridCellLength = new int[4];
                    e.PopupPassData.GridCellCode[0] = "A.ShiftType";
                    e.PopupPassData.GridCellCode[1] = "A.ShiftTypeName";
                    e.PopupPassData.GridCellCode[2] = "B.ShiftStartDateTime";
                    e.PopupPassData.GridCellCode[3] = "B.ShiftFinishDateTime";
                    e.PopupPassData.GridCellCaption[0] = "근무유형코드";
                    e.PopupPassData.GridCellCaption[1] = "근무유형명";
                    e.PopupPassData.GridCellCaption[2] = "근무시작시간";
                    e.PopupPassData.GridCellCaption[3] = "근무종료시간";
                    e.PopupPassData.GridCellType[0] = enumDef.GridCellType.Edit;
                    e.PopupPassData.GridCellType[1] = enumDef.GridCellType.Edit;
                    e.PopupPassData.GridCellType[2] = enumDef.GridCellType.Edit;
                    e.PopupPassData.GridCellType[3] = enumDef.GridCellType.Edit;
                    break;
                case "EquipmentName":
                    e.PopupPassData.PopupWinTitle = "설비 PopUp";
                    e.PopupPassData.ConditionCaption = "설비";
                    e.PopupPassData.SQLFromStatements = " Information.ProductRoutingEquipment (nolock) A JOIN Information.Equipment (nolock) E ON A.DivisionID = E.DivisionID AND A.EquipmentID = E.EquipmentID AND E.isActive = 1";
                    e.PopupPassData.SQLWhereStatements = "A.DivisionID = '" + uniGrid1.ActiveRow.Cells["DivisionID"].Value.ToString() + "' AND A.ProductID = '" + uniGrid1.ActiveRow.Cells["ProductID"].Value.ToString() + "' ";//"A.ITEM_CD LIKE '%-N%'";
                    e.PopupPassData.SQLWhereInputCodeValue = "";
                    e.PopupPassData.SQLWhereInputNameValue = "";
                    e.PopupPassData.DistinctOrNot = true;
                    e.PopupPassData.GridCellCode = new String[2];
                    e.PopupPassData.GridCellCaption = new String[2];
                    e.PopupPassData.GridCellType = new enumDef.GridCellType[2];
                    e.PopupPassData.GridCellLength = new int[2];
                    e.PopupPassData.GridCellCode[0] = "A.EquipmentID";
                    e.PopupPassData.GridCellCode[1] = "E.EquipmentName";
                    e.PopupPassData.GridCellCaption[0] = "설비코드";
                    e.PopupPassData.GridCellCaption[1] = "설비명";
                    e.PopupPassData.GridCellType[0] = enumDef.GridCellType.Edit;
                    e.PopupPassData.GridCellType[1] = enumDef.GridCellType.Edit;
                    break;
                case "MoldName":
                    e.PopupPassData.PopupWinTitle = "금형 PopUp";
                    e.PopupPassData.ConditionCaption = "금형";
                    e.PopupPassData.SQLFromStatements = " Information.EquipmentMold (nolock) A JOIN Information.Mold (nolock) E ON A.DivisionID = E.DivisionID AND A.MoldID = E.MoldID AND E.isActive = 1";
                    e.PopupPassData.SQLWhereStatements = "A.DivisionID = '" + uniGrid1.ActiveRow.Cells["DivisionID"].Value.ToString() + "' AND A.EquipmentID = '" + uniGrid1.ActiveRow.Cells["EquipmentID"].Value.ToString() + "'";//"A.ITEM_CD LIKE '%-N%'";
                    e.PopupPassData.SQLWhereInputCodeValue = "";
                    e.PopupPassData.SQLWhereInputNameValue = "";
                    e.PopupPassData.DistinctOrNot = true;
                    e.PopupPassData.GridCellCode = new String[2];
                    e.PopupPassData.GridCellCaption = new String[2];
                    e.PopupPassData.GridCellType = new enumDef.GridCellType[2];
                    e.PopupPassData.GridCellLength = new int[2];
                    e.PopupPassData.GridCellCode[0] = "A.MoldID";
                    e.PopupPassData.GridCellCode[1] = "E.MoldName";
                    e.PopupPassData.GridCellCaption[0] = "금형코드";
                    e.PopupPassData.GridCellCaption[1] = "금형명";
                    e.PopupPassData.GridCellType[0] = enumDef.GridCellType.Edit;
                    e.PopupPassData.GridCellType[1] = enumDef.GridCellType.Edit;
                    break;
                case "WarehouseName":
                    e.PopupPassData.PopupWinTitle = "창고 PopUp";
                    e.PopupPassData.ConditionCaption = "창고";
                    e.PopupPassData.SQLFromStatements = " Information.Warehouse (nolock)";
                    e.PopupPassData.SQLWhereStatements = "DivisionID = '" + uniGrid1.ActiveRow.Cells["DivisionID"].Value.ToString() + "'";//"A.ITEM_CD LIKE '%-N%'";
                    e.PopupPassData.SQLWhereInputCodeValue = "";
                    e.PopupPassData.SQLWhereInputNameValue = "";
                    e.PopupPassData.DistinctOrNot = true;
                    e.PopupPassData.GridCellCode = new String[2];
                    e.PopupPassData.GridCellCaption = new String[2];
                    e.PopupPassData.GridCellType = new enumDef.GridCellType[2];
                    e.PopupPassData.GridCellLength = new int[2];
                    e.PopupPassData.GridCellCode[0] = "WarehouseID";
                    e.PopupPassData.GridCellCode[1] = "WarehouseName";
                    e.PopupPassData.GridCellCaption[0] = "창고코드";
                    e.PopupPassData.GridCellCaption[1] = "창고명";
                    e.PopupPassData.GridCellType[0] = enumDef.GridCellType.Edit;
                    e.PopupPassData.GridCellType[1] = enumDef.GridCellType.Edit;
                    break;
                case "StoreLocationName":
                    e.PopupPassData.PopupWinTitle = "위치 PopUp";
                    e.PopupPassData.ConditionCaption = "위치";
                    e.PopupPassData.SQLFromStatements = " Information.StoreLocation (nolock)";
                    e.PopupPassData.SQLWhereStatements = "DivisionID = '" + uniGrid1.ActiveRow.Cells["DivisionID"].Value.ToString() + "' AND WarehouseID = '" + uniGrid1.ActiveRow.Cells["WarehouseID"].Value.ToString() + "' AND isActive = 1";//"A.ITEM_CD LIKE '%-N%'";
                    e.PopupPassData.SQLWhereInputCodeValue = "";
                    e.PopupPassData.SQLWhereInputNameValue = "";
                    e.PopupPassData.DistinctOrNot = true;
                    e.PopupPassData.GridCellCode = new String[2];
                    e.PopupPassData.GridCellCaption = new String[2];
                    e.PopupPassData.GridCellType = new enumDef.GridCellType[2];
                    e.PopupPassData.GridCellLength = new int[2];
                    e.PopupPassData.GridCellCode[0] = "StoreLocationID";
                    e.PopupPassData.GridCellCode[1] = "StoreLocationName";
                    e.PopupPassData.GridCellCaption[0] = "위치코드";
                    e.PopupPassData.GridCellCaption[1] = "위치명";
                    e.PopupPassData.GridCellType[0] = enumDef.GridCellType.Edit;
                    e.PopupPassData.GridCellType[1] = enumDef.GridCellType.Edit;
                    break;
                case "LineName":
                    e.PopupPassData.PopupWinTitle = "라인 PopUp";
                    e.PopupPassData.ConditionCaption = "라인";
                    e.PopupPassData.SQLFromStatements = " Information.ManufactureLine (nolock)";
                    e.PopupPassData.SQLWhereStatements = "DivisionID = '" + uniGrid1.ActiveRow.Cells["DivisionID"].Value.ToString() + "'";
                    e.PopupPassData.SQLWhereInputCodeValue = "";
                    e.PopupPassData.SQLWhereInputNameValue = "";
                    e.PopupPassData.DistinctOrNot = true;
                    e.PopupPassData.GridCellCode = new String[2];
                    e.PopupPassData.GridCellCaption = new String[2];
                    e.PopupPassData.GridCellType = new enumDef.GridCellType[2];
                    e.PopupPassData.GridCellLength = new int[2];
                    e.PopupPassData.GridCellCode[0] = "LineID";
                    e.PopupPassData.GridCellCode[1] = "LineName";
                    e.PopupPassData.GridCellCaption[0] = "라인코드";
                    e.PopupPassData.GridCellCaption[1] = "라인명";
                    e.PopupPassData.GridCellType[0] = enumDef.GridCellType.Edit;
                    e.PopupPassData.GridCellType[1] = enumDef.GridCellType.Edit;
                    break;
                case "WorkCenterName":
                    e.PopupPassData.PopupWinTitle = "작업장 PopUp";
                    e.PopupPassData.ConditionCaption = "작업장";
                    e.PopupPassData.SQLFromStatements = " Information.Workcenter (nolock)";
                    e.PopupPassData.SQLWhereStatements = "DivisionID = '" + uniGrid1.ActiveRow.Cells["DivisionID"].Value.ToString() + "'";
                    e.PopupPassData.SQLWhereInputCodeValue = "";
                    e.PopupPassData.SQLWhereInputNameValue = "";
                    e.PopupPassData.DistinctOrNot = true;
                    e.PopupPassData.GridCellCode = new String[2];
                    e.PopupPassData.GridCellCaption = new String[2];
                    e.PopupPassData.GridCellType = new enumDef.GridCellType[2];
                    e.PopupPassData.GridCellLength = new int[2];
                    e.PopupPassData.GridCellCode[0] = "WorkcenterID";
                    e.PopupPassData.GridCellCode[1] = "WorkcenterName";
                    e.PopupPassData.GridCellCaption[0] = "작업장코드";
                    e.PopupPassData.GridCellCaption[1] = "작업장명";
                    e.PopupPassData.GridCellType[0] = enumDef.GridCellType.Edit;
                    e.PopupPassData.GridCellType[1] = enumDef.GridCellType.Edit;
                    break;
                case "PartnerName":
                    e.PopupPassData.PopupWinTitle = "외주처 PopUp";
                    e.PopupPassData.ConditionCaption = "외주처";
                    e.PopupPassData.SQLFromStatements = " Information.ProductRoutingOutsourcing (nolock) A JOIN Information.OutsourcingPartner (nolock) B ON A.PartnerID = B.PartnerID";
                    e.PopupPassData.SQLWhereStatements = "A.DivisionID = '" + uniGrid1.ActiveRow.Cells["DivisionID"].Value.ToString() + "' AND A.ProductID = '" + uniGrid1.ActiveRow.Cells["ProductID"].Value.ToString() + "'";
                    e.PopupPassData.SQLWhereInputCodeValue = "";
                    e.PopupPassData.SQLWhereInputNameValue = "";
                    e.PopupPassData.DistinctOrNot = true;
                    e.PopupPassData.GridCellCode = new String[2];
                    e.PopupPassData.GridCellCaption = new String[2];
                    e.PopupPassData.GridCellType = new enumDef.GridCellType[2];
                    e.PopupPassData.GridCellLength = new int[2];
                    e.PopupPassData.GridCellCode[0] = "A.PartnerID";
                    e.PopupPassData.GridCellCode[1] = "B.PartnerName";
                    e.PopupPassData.GridCellCaption[0] = "외주처코드";
                    e.PopupPassData.GridCellCaption[1] = "외주처명";
                    e.PopupPassData.GridCellType[0] = enumDef.GridCellType.Edit;
                    e.PopupPassData.GridCellType[1] = enumDef.GridCellType.Edit;
                    break;
                case "MinimumLaborClassName":
                    e.PopupPassData.PopupWinTitle = "작업자등급 PopUp";
                    e.PopupPassData.ConditionCaption = "작업자등급";
                    e.PopupPassData.SQLFromStatements = " Information.CommonCodeDetail (nolock)";
                    e.PopupPassData.SQLWhereStatements = "CodeTypeID = 'LABOR_CLASS'";
                    e.PopupPassData.SQLWhereInputCodeValue = "";
                    e.PopupPassData.SQLWhereInputNameValue = "";
                    e.PopupPassData.DistinctOrNot = true;
                    e.PopupPassData.GridCellCode = new String[2];
                    e.PopupPassData.GridCellCaption = new String[2];
                    e.PopupPassData.GridCellType = new enumDef.GridCellType[2];
                    e.PopupPassData.GridCellLength = new int[2];
                    e.PopupPassData.GridCellCode[0] = "CodeID";
                    e.PopupPassData.GridCellCode[1] = "CodeName";
                    e.PopupPassData.GridCellCaption[0] = "외주처코드";
                    e.PopupPassData.GridCellCaption[1] = "외주처명";
                    e.PopupPassData.GridCellType[0] = enumDef.GridCellType.Edit;
                    e.PopupPassData.GridCellType[1] = enumDef.GridCellType.Edit;
                    break;
            }
        }

        private void uniGrid1_AfterPopupClosed(object sender, AppFramework.UI.Controls.Popup.AfterPopupCloseEventArgs e)
        {
            DataSet iDataSet = new DataSet();
            if (e.ResultData.Data == null)
                return;
            iDataSet = (DataSet)e.ResultData.Data;

            switch (uniGrid1.ActiveCell.Column.Key)
            {
                case "ProductID":
                    uniGrid1.ActiveCell.Value = iDataSet.Tables[0].Rows[0]["ProductID"].ToString();
                    uniGrid1.ActiveRow.Cells["ProductName"].Value = iDataSet.Tables[0].Rows[0]["ProductName"].ToString();
                    if (Int32.Parse(iDataSet.Tables[0].Rows[0]["RoutingCount"].ToString()) > 1)
                    {
                        uniGrid1.ActiveRow.Cells["isMultiRouting"].Value = "1";
                    }
                    uniGrid1.ActiveRow.Cells["Cavity"].Value = iDataSet.Tables[0].Rows[0]["Cavity"].ToString();
                    uniGrid1.ActiveRow.Cells["ModelDescription"].Value = iDataSet.Tables[0].Rows[0]["CodeDescription"].ToString();
                    uniGrid1.ActiveRow.Cells["Spec"].Value = iDataSet.Tables[0].Rows[0]["Spec"].ToString();
                    break;
                case "ShiftType":
                    uniGrid1.ActiveCell.Value = iDataSet.Tables[0].Rows[0]["ShiftType"].ToString();
                    uniGrid1.ActiveRow.Cells["ShiftTypeName"].Value = iDataSet.Tables[0].Rows[0]["ShiftTypeName"].ToString();
                    break;
                case "EquipmentName":
                    uniGrid1.ActiveCell.Value = iDataSet.Tables[0].Rows[0]["EquipmentName"].ToString();
                    uniGrid1.ActiveRow.Cells["EquipmentID"].Value = iDataSet.Tables[0].Rows[0]["EquipmentID"].ToString();
                    break;
                case "MoldName":
                    uniGrid1.ActiveCell.Value = iDataSet.Tables[0].Rows[0]["MoldName"].ToString();
                    uniGrid1.ActiveRow.Cells["MoldID"].Value = iDataSet.Tables[0].Rows[0]["MoldID"].ToString();
                    break;
                case "WarehouseName":
                    uniGrid1.ActiveCell.Value = iDataSet.Tables[0].Rows[0]["WarehouseName"].ToString();
                    uniGrid1.ActiveRow.Cells["WarehouseID"].Value = iDataSet.Tables[0].Rows[0]["WarehouseID"].ToString();
                    break;
                case "StoreLocationName":
                    uniGrid1.ActiveCell.Value = iDataSet.Tables[0].Rows[0]["StoreLocationName"].ToString();
                    uniGrid1.ActiveRow.Cells["StoreLocationID"].Value = iDataSet.Tables[0].Rows[0]["StoreLocationID"].ToString();
                    break;
                case "LineName":
                    uniGrid1.ActiveCell.Value = iDataSet.Tables[0].Rows[0]["LineName"].ToString();
                    uniGrid1.ActiveRow.Cells["LineID"].Value = iDataSet.Tables[0].Rows[0]["LineID"].ToString();
                    break;
                case "WorkCenterName":
                    uniGrid1.ActiveCell.Value = iDataSet.Tables[0].Rows[0]["WorkCenterName"].ToString();
                    uniGrid1.ActiveRow.Cells["WorkCenterID"].Value = iDataSet.Tables[0].Rows[0]["WorkCenterID"].ToString();
                    break;
                case "PartnerName":
                    uniGrid1.ActiveCell.Value = iDataSet.Tables[0].Rows[0]["PartnerName"].ToString();
                    uniGrid1.ActiveRow.Cells["PartnerID"].Value = iDataSet.Tables[0].Rows[0]["PartnerID"].ToString();
                    break;
                case "MinimumLaborClassName":
                    uniGrid1.ActiveCell.Value = iDataSet.Tables[0].Rows[0]["CodeID"].ToString();
                    uniGrid1.ActiveRow.Cells["MinimumLaborClassID"].Value = iDataSet.Tables[0].Rows[0]["CodeName"].ToString();
                    break;
            }
        }

        private void popProductID_BeforePopupOpen(object sender, AppFramework.UI.Controls.Popup.BeforePopupOpenEventArgs e)
        {
            if (cbDivisionID.SelectedItem == null)
            {
                uniBase.UMessage.DisplayMessageBox("970029", MessageBoxButtons.OK, new string[] { lblDivision.Name });
                e.Cancel = true;
                return;
            }

            e.PopupPassData.CalledPopupID = "Bizentro.App.UI.POPUP.MDM1004P1";
            e.PopupPassData.PopupWinTitle = "Product Popup";

            object[] passData = new object[2];
            passData[0] = popProductID.CodeValue;
            passData[1] = cbDivisionID.SelectedItem.DataValue.ToString();

            e.PopupPassData.Data = passData;
        }

        private void popProductID_AfterPopupClosed(object sender, AppFramework.UI.Controls.Popup.AfterPopupCloseEventArgs e)
        {
            DataSet iDataSet = new DataSet();
            // 팝업에서 선택한 데이터 가져오는부분
            if (e.ResultData.Data == null)
                return;
            iDataSet = (DataSet)e.ResultData.Data;

            popProductID.CodeValue = iDataSet.Tables[0].Rows[0]["ProductID"].ToString();
            popProductID.CodeName = iDataSet.Tables[0].Rows[0]["ProductName"].ToString();
        }

        private void popLine_BeforePopupOpen(object sender, AppFramework.UI.Controls.Popup.BeforePopupOpenEventArgs e)
        {
            if (cbDivisionID.SelectedItem == null)
            {
                uniBase.UMessage.DisplayMessageBox("970029", MessageBoxButtons.OK, new string[] { lblDivision.Name });
                e.Cancel = true;
                return;
            }

            e.PopupPassData.CalledPopupID = "Bizentro.App.UI.POPUP.MDM1011P1";
            e.PopupPassData.PopupWinTitle = "Line Popup";

            object[] passData = new object[2];
            passData[0] = cbDivisionID.SelectedItem.DataValue.ToString();
            passData[1] = popLine.CodeValue;

            e.PopupPassData.Data = passData;
        }

        private void popLine_AfterPopupClosed(object sender, AppFramework.UI.Controls.Popup.AfterPopupCloseEventArgs e)
        {
            DataSet iDataSet = new DataSet();
            // 팝업에서 선택한 데이터 가져오는부분
            if (e.ResultData.Data == null)
                return;
            iDataSet = (DataSet)e.ResultData.Data;

            popLine.CodeValue = iDataSet.Tables[0].Rows[0]["LineID"].ToString();
            popLine.CodeName = iDataSet.Tables[0].Rows[0]["LineName"].ToString();
        }

        private void popModel_BeforePopupOpen(object sender, AppFramework.UI.Controls.Popup.BeforePopupOpenEventArgs e)
        {
            if (cbDivisionID.SelectedItem == null)
            {
                uniBase.UMessage.DisplayMessageBox("970029", MessageBoxButtons.OK, new string[] { lblDivision.Name });
                e.Cancel = true;
                return;
            }

            e.PopupPassData.CalledPopupID = "Bizentro.App.UI.POPUP.MDM1005P1";
            e.PopupPassData.PopupWinTitle = "Model Popup";

            object[] passData = new object[1];
            passData[0] = popModel.CodeValue;

            e.PopupPassData.Data = passData;
        }

        private void popModel_AfterPopupClosed(object sender, AppFramework.UI.Controls.Popup.AfterPopupCloseEventArgs e)
        {
            DataSet iDataSet = new DataSet();
            // 팝업에서 선택한 데이터 가져오는부분
            if (e.ResultData.Data == null)
                return;
            iDataSet = (DataSet)e.ResultData.Data;

            popModel.CodeValue = iDataSet.Tables[0].Rows[0]["CodeID"].ToString();
            popModel.CodeName = iDataSet.Tables[0].Rows[0]["CodeName"].ToString();
        }

        private void uniGrid1_DetailBinding(object sender, uniGrid.DetailBindingEventArgs e)
        {
            if (e.DetailDataView.Count == 0 &&
              e.MasterKeyValues[0].ToString() != string.Empty) // && e.MasterKeyValues[1].ToString() != string.Empty)
            {
                cqtdsPIR101M1.B_WorkLot.Clear();
                //this.DBQuery2(e.MasterKeyValues[0].ToString(), (DateTime)e.MasterKeyValues[1], e.MasterKeyValues[2].ToString());
                this.DBQuery2(e.MasterKeyValues[0].ToString());
            }
        }

        #endregion

        #region ■ 6.2 User-defined popup implementation group

        private void OpenNumberingType(string iWhere)
        {
            #region ▶▶▶ 10.1.2.1 Popup Constructors
            //CommonPopup cp = new CommonUtil.CommonPopup(PopupType.AutoNumbering);

            //string[] arrRet = cp.showModalDialog(InputParam1);

            #endregion

            #region ▶▶▶ 10.1.2.2 Setting Returned Data

            //if (iWhere) 
            //{
            //    txtMinor.value = arrRet[0];
            //    txtMinorNm.value = arrRet[1];
            //}
            //else
            //{
            //    uniGrid1.Rows[uniGrid1.ActiveRow][gridCol.NumberingCd].value = arrRet[0];
            //    uniGrid1.Rows[uniGrid1.ActiveRow][gridCol.NumberingNm].value = arrRet[1];

            //    if (arrRet[2].Length > 0) 
            //        uniGrid1.Rows[uniGrid1.ActiveRow][gridCol.MaxLen].value = arrRet[2];
            //    else
            //        uniGrid1.Rows[uniGrid1.ActiveRow][gridCol.MaxLen].value = "18";

            //    uniGrid1.Rows[uniGrid1.ActiveRow][gridCol.PrefixCd].value = arrRet[0];

            //}

            #endregion

            //CommonVariable.lgBlnFlgChgValue = true;  // 사용자 액션 발생 알림
        }

        #endregion

        #endregion

        #region ▶ 7. User-defined method part

        #region ■ 7.1 User-defined function group

        #endregion

        #endregion

    }
}
