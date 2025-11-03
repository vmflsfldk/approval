using Bizentro.AppFramework.UI.Module;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.ObjectBuilder;

namespace Bizentro.App.UI.PIM.PIR101M1_CKO134
{
    public class ModuleInitializer : Bizentro.AppFramework.UI.Module.Module
    {
        [InjectionConstructor]
        public ModuleInitializer([ServiceDependency] WorkItem rootWorkItem)
            : base(rootWorkItem) { }

        protected override void RegisterModureViewer()
        {
            base.AddModule<ModuleViewer>();
        }
    }

    partial class ModuleViewer
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance25 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance26 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance27 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance28 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance29 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance30 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance31 = new Infragistics.Win.Appearance();
            this.uniTBL_OuterMost = new Bizentro.AppFramework.UI.Controls.uniTableLayoutPanel(this.components);
            this.uniTableLayoutPanel2 = new Bizentro.AppFramework.UI.Controls.uniTableLayoutPanel(this.components);
            this.lblProductOrderNumber = new Bizentro.AppFramework.UI.Controls.uniLabel(this.components);
            this.lblStatus = new Bizentro.AppFramework.UI.Controls.uniLabel(this.components);
            this.lblProduct = new Bizentro.AppFramework.UI.Controls.uniLabel(this.components);
            this.lblModel = new Bizentro.AppFramework.UI.Controls.uniLabel(this.components);
            this.lblLine = new Bizentro.AppFramework.UI.Controls.uniLabel(this.components);
            this.popModel = new Bizentro.AppFramework.UI.Controls.uniOpenPopup();
            this.cbStatus = new Bizentro.AppFramework.UI.Controls.uniCombo(this.components);
            this.txtProductOrderNumber = new Bizentro.AppFramework.UI.Controls.uniTextBox(this.components);
            this.popLine = new Bizentro.AppFramework.UI.Controls.uniOpenPopup();
            this.popProductID = new Bizentro.AppFramework.UI.Controls.uniOpenPopup();
            this.lblOrderType = new Bizentro.AppFramework.UI.Controls.uniLabel(this.components);
            this.dtDateTerm = new Bizentro.AppFramework.UI.Controls.uniDateTerm();
            this.cbOrderType = new Bizentro.AppFramework.UI.Controls.uniCombo(this.components);
            this.lblOrderDate = new Bizentro.AppFramework.UI.Controls.uniLabel(this.components);
            this.lblDivision = new Bizentro.AppFramework.UI.Controls.uniLabel(this.components);
            this.cbDivisionID = new Bizentro.AppFramework.UI.Controls.uniCombo(this.components);
            this.uniTBL_MainData = new Bizentro.AppFramework.UI.Controls.uniTableLayoutPanel(this.components);
            this.uniGrid2 = new Bizentro.AppFramework.UI.Controls.uniGrid(this.components);
            this.uniGrid1 = new Bizentro.AppFramework.UI.Controls.uniGrid(this.components);
            this.uniTBL_MainReference = new Bizentro.AppFramework.UI.Controls.uniTableLayoutPanel(this.components);
            this.uniTBL_MainBatch = new Bizentro.AppFramework.UI.Controls.uniTableLayoutPanel(this.components);
            this.uniTBL_OuterMost.SuspendLayout();
            this.uniTableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductOrderNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbOrderType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbDivisionID)).BeginInit();
            this.uniTBL_MainData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uniGrid2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uniGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // uniLabel_Path
            // 
            this.uniLabel_Path.LabelType = Bizentro.AppFramework.UI.Variables.enumDef.LabelType.PathInfo;
            this.uniLabel_Path.Size = new System.Drawing.Size(500, 14);
            // 
            // uniTBL_OuterMost
            // 
            this.uniTBL_OuterMost.AutoFit = false;
            this.uniTBL_OuterMost.AutoFitColumnCount = 4;
            this.uniTBL_OuterMost.AutoFitRowCount = 4;
            this.uniTBL_OuterMost.BackColor = System.Drawing.Color.Transparent;
            this.uniTBL_OuterMost.BizentroTableLayout = Bizentro.AppFramework.UI.Controls.BizentroTableLayOutStyle.DefaultTableLayout;
            this.uniTBL_OuterMost.ColumnCount = 1;
            this.uniTBL_OuterMost.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.uniTBL_OuterMost.Controls.Add(this.uniTableLayoutPanel2, 0, 2);
            this.uniTBL_OuterMost.Controls.Add(this.uniTBL_MainData, 0, 4);
            this.uniTBL_OuterMost.Controls.Add(this.uniTBL_MainReference, 0, 0);
            this.uniTBL_OuterMost.Controls.Add(this.uniTBL_MainBatch, 0, 6);
            this.uniTBL_OuterMost.DefaultRowSize = 23;
            this.uniTBL_OuterMost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uniTBL_OuterMost.EasyBaseBatchType = Bizentro.AppFramework.UI.Controls.EasyBaseTBType.NONE;
            this.uniTBL_OuterMost.HEIGHT_TYPE_00_REFERENCE = 21F;
            this.uniTBL_OuterMost.HEIGHT_TYPE_01 = 6F;
            this.uniTBL_OuterMost.HEIGHT_TYPE_01_CONDITION = 38F;
            this.uniTBL_OuterMost.HEIGHT_TYPE_02 = 9F;
            this.uniTBL_OuterMost.HEIGHT_TYPE_02_DATA = 0F;
            this.uniTBL_OuterMost.HEIGHT_TYPE_03 = 3F;
            this.uniTBL_OuterMost.HEIGHT_TYPE_03_BOTTOM = 28F;
            this.uniTBL_OuterMost.HEIGHT_TYPE_04 = 1F;
            this.uniTBL_OuterMost.Location = new System.Drawing.Point(1, 15);
            this.uniTBL_OuterMost.Margin = new System.Windows.Forms.Padding(0);
            this.uniTBL_OuterMost.Name = "uniTBL_OuterMost";
            this.uniTBL_OuterMost.PanelType = Bizentro.AppFramework.UI.Variables.enumDef.PanelType.Default;
            this.uniTBL_OuterMost.RowCount = 8;
            this.uniTBL_OuterMost.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.uniTBL_OuterMost.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.uniTBL_OuterMost.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 107F));
            this.uniTBL_OuterMost.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 9F));
            this.uniTBL_OuterMost.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.uniTBL_OuterMost.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 3F));
            this.uniTBL_OuterMost.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.uniTBL_OuterMost.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.uniTBL_OuterMost.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.uniTBL_OuterMost.Size = new System.Drawing.Size(836, 579);
            this.uniTBL_OuterMost.SizeTD5 = 14F;
            this.uniTBL_OuterMost.SizeTD6 = 36F;
            this.uniTBL_OuterMost.TabIndex = 0;
            this.uniTBL_OuterMost.uniLR_SPACE_TYPE = Bizentro.AppFramework.UI.Controls.LR_SPACE_TYPE.LR_SPACE_TYPE_00;
            // 
            // uniTableLayoutPanel2
            // 
            this.uniTableLayoutPanel2.AutoFit = false;
            this.uniTableLayoutPanel2.AutoFitColumnCount = 4;
            this.uniTableLayoutPanel2.AutoFitRowCount = 4;
            this.uniTableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(245)))), ((int)(((byte)(247)))));
            this.uniTableLayoutPanel2.BizentroTableLayout = Bizentro.AppFramework.UI.Controls.BizentroTableLayOutStyle.DefaultTableLayout;
            this.uniTableLayoutPanel2.ColumnCount = 4;
            this.uniTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.uniTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36F));
            this.uniTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.uniTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36F));
            this.uniTableLayoutPanel2.Controls.Add(this.lblProductOrderNumber, 2, 3);
            this.uniTableLayoutPanel2.Controls.Add(this.lblStatus, 0, 3);
            this.uniTableLayoutPanel2.Controls.Add(this.lblProduct, 2, 2);
            this.uniTableLayoutPanel2.Controls.Add(this.lblModel, 0, 2);
            this.uniTableLayoutPanel2.Controls.Add(this.lblLine, 2, 1);
            this.uniTableLayoutPanel2.Controls.Add(this.popModel, 1, 2);
            this.uniTableLayoutPanel2.Controls.Add(this.cbStatus, 1, 3);
            this.uniTableLayoutPanel2.Controls.Add(this.txtProductOrderNumber, 3, 3);
            this.uniTableLayoutPanel2.Controls.Add(this.popLine, 3, 1);
            this.uniTableLayoutPanel2.Controls.Add(this.popProductID, 3, 2);
            this.uniTableLayoutPanel2.Controls.Add(this.lblOrderType, 0, 1);
            this.uniTableLayoutPanel2.Controls.Add(this.dtDateTerm, 3, 0);
            this.uniTableLayoutPanel2.Controls.Add(this.cbOrderType, 1, 1);
            this.uniTableLayoutPanel2.Controls.Add(this.lblOrderDate, 2, 0);
            this.uniTableLayoutPanel2.Controls.Add(this.lblDivision, 0, 0);
            this.uniTableLayoutPanel2.Controls.Add(this.cbDivisionID, 1, 0);
            this.uniTableLayoutPanel2.DefaultRowSize = 23;
            this.uniTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uniTableLayoutPanel2.EasyBaseBatchType = Bizentro.AppFramework.UI.Controls.EasyBaseTBType.NONE;
            this.uniTableLayoutPanel2.HEIGHT_TYPE_00_REFERENCE = 32F;
            this.uniTableLayoutPanel2.HEIGHT_TYPE_01 = 3F;
            this.uniTableLayoutPanel2.HEIGHT_TYPE_01_CONDITION = 29F;
            this.uniTableLayoutPanel2.HEIGHT_TYPE_02 = 5F;
            this.uniTableLayoutPanel2.HEIGHT_TYPE_02_DATA = 0F;
            this.uniTableLayoutPanel2.HEIGHT_TYPE_03 = 3F;
            this.uniTableLayoutPanel2.HEIGHT_TYPE_03_BOTTOM = 32F;
            this.uniTableLayoutPanel2.HEIGHT_TYPE_04 = 3F;
            this.uniTableLayoutPanel2.Location = new System.Drawing.Point(0, 27);
            this.uniTableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.uniTableLayoutPanel2.Name = "uniTableLayoutPanel2";
            this.uniTableLayoutPanel2.Padding = new System.Windows.Forms.Padding(0, 5, 0, 10);
            this.uniTableLayoutPanel2.PanelType = Bizentro.AppFramework.UI.Variables.enumDef.PanelType.Condition;
            this.uniTableLayoutPanel2.RowCount = 5;
            this.uniTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.uniTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.uniTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.uniTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.uniTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.uniTableLayoutPanel2.Size = new System.Drawing.Size(836, 107);
            this.uniTableLayoutPanel2.SizeTD5 = 14F;
            this.uniTableLayoutPanel2.SizeTD6 = 36F;
            this.uniTableLayoutPanel2.TabIndex = 5;
            this.uniTableLayoutPanel2.uniLR_SPACE_TYPE = Bizentro.AppFramework.UI.Controls.LR_SPACE_TYPE.LR_SPACE_TYPE_00;
            // 
            // lblProductOrderNumber
            // 
            appearance1.TextHAlignAsString = "Left";
            appearance1.TextVAlignAsString = "Middle";
            this.lblProductOrderNumber.Appearance = appearance1;
            this.lblProductOrderNumber.AutoPopupID = null;
            this.lblProductOrderNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProductOrderNumber.LabelType = Bizentro.AppFramework.UI.Variables.enumDef.LabelType.Title;
            this.lblProductOrderNumber.Location = new System.Drawing.Point(432, 75);
            this.lblProductOrderNumber.Margin = new System.Windows.Forms.Padding(15, 1, 0, 0);
            this.lblProductOrderNumber.Name = "lblProductOrderNumber";
            this.lblProductOrderNumber.Size = new System.Drawing.Size(102, 22);
            this.lblProductOrderNumber.StyleSetName = "Default";
            this.lblProductOrderNumber.TabIndex = 25;
            this.lblProductOrderNumber.Text = "ProductOrderNumber";
            this.lblProductOrderNumber.UseMnemonic = false;
            // 
            // lblStatus
            // 
            appearance2.TextHAlignAsString = "Left";
            appearance2.TextVAlignAsString = "Middle";
            this.lblStatus.Appearance = appearance2;
            this.lblStatus.AutoPopupID = null;
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatus.LabelType = Bizentro.AppFramework.UI.Variables.enumDef.LabelType.Title;
            this.lblStatus.Location = new System.Drawing.Point(15, 75);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(15, 1, 0, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(102, 22);
            this.lblStatus.StyleSetName = "Default";
            this.lblStatus.TabIndex = 24;
            this.lblStatus.Text = "Status";
            this.lblStatus.UseMnemonic = false;
            // 
            // lblProduct
            // 
            appearance3.TextHAlignAsString = "Left";
            appearance3.TextVAlignAsString = "Middle";
            this.lblProduct.Appearance = appearance3;
            this.lblProduct.AutoPopupID = null;
            this.lblProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProduct.LabelType = Bizentro.AppFramework.UI.Variables.enumDef.LabelType.Title;
            this.lblProduct.Location = new System.Drawing.Point(432, 52);
            this.lblProduct.Margin = new System.Windows.Forms.Padding(15, 1, 0, 0);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(102, 22);
            this.lblProduct.StyleSetName = "Default";
            this.lblProduct.TabIndex = 23;
            this.lblProduct.Text = "Product";
            this.lblProduct.UseMnemonic = false;
            // 
            // lblModel
            // 
            appearance4.TextHAlignAsString = "Left";
            appearance4.TextVAlignAsString = "Middle";
            this.lblModel.Appearance = appearance4;
            this.lblModel.AutoPopupID = null;
            this.lblModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblModel.LabelType = Bizentro.AppFramework.UI.Variables.enumDef.LabelType.Title;
            this.lblModel.Location = new System.Drawing.Point(15, 52);
            this.lblModel.Margin = new System.Windows.Forms.Padding(15, 1, 0, 0);
            this.lblModel.Name = "lblModel";
            this.lblModel.Size = new System.Drawing.Size(102, 22);
            this.lblModel.StyleSetName = "Default";
            this.lblModel.TabIndex = 22;
            this.lblModel.Text = "Model";
            this.lblModel.UseMnemonic = false;
            // 
            // lblLine
            // 
            appearance5.TextHAlignAsString = "Left";
            appearance5.TextVAlignAsString = "Middle";
            this.lblLine.Appearance = appearance5;
            this.lblLine.AutoPopupID = null;
            this.lblLine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLine.LabelType = Bizentro.AppFramework.UI.Variables.enumDef.LabelType.Title;
            this.lblLine.Location = new System.Drawing.Point(432, 29);
            this.lblLine.Margin = new System.Windows.Forms.Padding(15, 1, 0, 0);
            this.lblLine.Name = "lblLine";
            this.lblLine.Size = new System.Drawing.Size(102, 22);
            this.lblLine.StyleSetName = "Default";
            this.lblLine.TabIndex = 21;
            this.lblLine.Text = "Line";
            this.lblLine.UseMnemonic = false;
            // 
            // popModel
            // 
            this.popModel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.popModel.AutoPopupCodeParameter = null;
            this.popModel.AutoPopupID = null;
            this.popModel.AutoPopupNameParameter = null;
            this.popModel.CodeMaxLength = 255;
            this.popModel.CodeName = "";
            this.popModel.CodeSize = 100;
            this.popModel.CodeStyle = Bizentro.AppFramework.UI.Controls.TextBox_Style.Default;
            this.popModel.CodeTextBoxName = null;
            this.popModel.CodeValue = "";
            this.popModel.FieldType = Bizentro.AppFramework.UI.Variables.enumDef.FieldType.Default;
            this.popModel.Location = new System.Drawing.Point(117, 53);
            this.popModel.LockedField = false;
            this.popModel.Margin = new System.Windows.Forms.Padding(0);
            this.popModel.Name = "popModel";
            this.popModel.NameDisplay = true;
            this.popModel.NameId = null;
            this.popModel.NameMaxLength = 255;
            this.popModel.NamePopup = false;
            this.popModel.NameSize = 150;
            this.popModel.NameStyle = Bizentro.AppFramework.UI.Controls.TextBox_Style.Default;
            this.popModel.Parameter = null;
            this.popModel.PopupButtonEnable = Bizentro.AppFramework.UI.Variables.enumDef.uniOpenPopupButton.Enable;
            this.popModel.PopupId = null;
            this.popModel.PopupType = Bizentro.AppFramework.UI.Variables.enumDef.PopupType.CommonPopup;
            this.popModel.QueryIfEnterKeyPressed = true;
            this.popModel.RequiredField = false;
            this.popModel.Size = new System.Drawing.Size(271, 21);
            this.popModel.TabIndex = 20;
            this.popModel.uniALT = null;
            this.popModel.uniCharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.popModel.UseDynamicFormat = false;
            this.popModel.ValueTextBoxName = null;
            this.popModel.BeforePopupOpen += new Bizentro.AppFramework.UI.Controls.Popup.BeforePopupOpenEventHandler(this.popModel_BeforePopupOpen);
            this.popModel.AfterPopupClosed += new Bizentro.AppFramework.UI.Controls.Popup.AfterPopupCloseEventHandler(this.popModel_AfterPopupClosed);
            // 
            // cbStatus
            // 
            this.cbStatus.AddEmptyRow = false;
            this.cbStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbStatus.ComboFrom = "";
            this.cbStatus.ComboMajorCd = "";
            this.cbStatus.ComboSelect = "";
            this.cbStatus.ComboType = Bizentro.AppFramework.UI.Variables.enumDef.ComboType.MajorCode;
            this.cbStatus.ComboWhere = "";
            this.cbStatus.DropDownListWidth = -1;
            this.cbStatus.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
            this.cbStatus.FieldType = Bizentro.AppFramework.UI.Variables.enumDef.FieldType.Default;
            this.cbStatus.Location = new System.Drawing.Point(117, 76);
            this.cbStatus.LockedField = false;
            this.cbStatus.Margin = new System.Windows.Forms.Padding(0);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.RequiredField = false;
            this.cbStatus.Size = new System.Drawing.Size(144, 28);
            this.cbStatus.Style = Bizentro.AppFramework.UI.Controls.Combo_Style.Default;
            this.cbStatus.StyleSetName = "Default";
            this.cbStatus.TabIndex = 16;
            this.cbStatus.uniALT = null;
            // 
            // txtProductOrderNumber
            // 
            this.txtProductOrderNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            appearance6.TextVAlignAsString = "Bottom";
            this.txtProductOrderNumber.Appearance = appearance6;
            this.txtProductOrderNumber.FieldType = Bizentro.AppFramework.UI.Variables.enumDef.FieldType.Default;
            this.txtProductOrderNumber.Location = new System.Drawing.Point(534, 74);
            this.txtProductOrderNumber.LockedField = false;
            this.txtProductOrderNumber.Margin = new System.Windows.Forms.Padding(0);
            this.txtProductOrderNumber.Name = "txtProductOrderNumber";
            this.txtProductOrderNumber.QueryIfEnterKeyPressed = true;
            this.txtProductOrderNumber.RequiredField = false;
            this.txtProductOrderNumber.Size = new System.Drawing.Size(194, 28);
            this.txtProductOrderNumber.Style = Bizentro.AppFramework.UI.Controls.TextBox_Style.Default;
            this.txtProductOrderNumber.StyleSetName = "Default";
            this.txtProductOrderNumber.TabIndex = 18;
            this.txtProductOrderNumber.uniALT = null;
            this.txtProductOrderNumber.uniCharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtProductOrderNumber.UseDynamicFormat = false;
            // 
            // popLine
            // 
            this.popLine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.popLine.AutoPopupCodeParameter = null;
            this.popLine.AutoPopupID = null;
            this.popLine.AutoPopupNameParameter = null;
            this.popLine.CodeMaxLength = 255;
            this.popLine.CodeName = "";
            this.popLine.CodeSize = 100;
            this.popLine.CodeStyle = Bizentro.AppFramework.UI.Controls.TextBox_Style.Default;
            this.popLine.CodeTextBoxName = null;
            this.popLine.CodeValue = "";
            this.popLine.FieldType = Bizentro.AppFramework.UI.Variables.enumDef.FieldType.Default;
            this.popLine.Location = new System.Drawing.Point(534, 30);
            this.popLine.LockedField = false;
            this.popLine.Margin = new System.Windows.Forms.Padding(0);
            this.popLine.Name = "popLine";
            this.popLine.NameDisplay = true;
            this.popLine.NameId = null;
            this.popLine.NameMaxLength = 255;
            this.popLine.NamePopup = false;
            this.popLine.NameSize = 150;
            this.popLine.NameStyle = Bizentro.AppFramework.UI.Controls.TextBox_Style.Default;
            this.popLine.Parameter = null;
            this.popLine.PopupButtonEnable = Bizentro.AppFramework.UI.Variables.enumDef.uniOpenPopupButton.Enable;
            this.popLine.PopupId = null;
            this.popLine.PopupType = Bizentro.AppFramework.UI.Variables.enumDef.PopupType.CommonPopup;
            this.popLine.QueryIfEnterKeyPressed = true;
            this.popLine.RequiredField = false;
            this.popLine.Size = new System.Drawing.Size(271, 21);
            this.popLine.TabIndex = 19;
            this.popLine.uniALT = null;
            this.popLine.uniCharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.popLine.UseDynamicFormat = false;
            this.popLine.ValueTextBoxName = null;
            this.popLine.BeforePopupOpen += new Bizentro.AppFramework.UI.Controls.Popup.BeforePopupOpenEventHandler(this.popLine_BeforePopupOpen);
            this.popLine.AfterPopupClosed += new Bizentro.AppFramework.UI.Controls.Popup.AfterPopupCloseEventHandler(this.popLine_AfterPopupClosed);
            // 
            // popProductID
            // 
            this.popProductID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.popProductID.AutoPopupCodeParameter = null;
            this.popProductID.AutoPopupID = null;
            this.popProductID.AutoPopupNameParameter = null;
            this.popProductID.CodeMaxLength = 255;
            this.popProductID.CodeName = "";
            this.popProductID.CodeSize = 100;
            this.popProductID.CodeStyle = Bizentro.AppFramework.UI.Controls.TextBox_Style.Default;
            this.popProductID.CodeTextBoxName = null;
            this.popProductID.CodeValue = "";
            this.popProductID.FieldType = Bizentro.AppFramework.UI.Variables.enumDef.FieldType.Default;
            this.popProductID.Location = new System.Drawing.Point(534, 53);
            this.popProductID.LockedField = false;
            this.popProductID.Margin = new System.Windows.Forms.Padding(0);
            this.popProductID.Name = "popProductID";
            this.popProductID.NameDisplay = true;
            this.popProductID.NameId = null;
            this.popProductID.NameMaxLength = 255;
            this.popProductID.NamePopup = false;
            this.popProductID.NameSize = 150;
            this.popProductID.NameStyle = Bizentro.AppFramework.UI.Controls.TextBox_Style.Default;
            this.popProductID.Parameter = null;
            this.popProductID.PopupButtonEnable = Bizentro.AppFramework.UI.Variables.enumDef.uniOpenPopupButton.Enable;
            this.popProductID.PopupId = null;
            this.popProductID.PopupType = Bizentro.AppFramework.UI.Variables.enumDef.PopupType.CommonPopup;
            this.popProductID.QueryIfEnterKeyPressed = true;
            this.popProductID.RequiredField = false;
            this.popProductID.Size = new System.Drawing.Size(271, 21);
            this.popProductID.TabIndex = 18;
            this.popProductID.uniALT = null;
            this.popProductID.uniCharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.popProductID.UseDynamicFormat = false;
            this.popProductID.ValueTextBoxName = null;
            this.popProductID.BeforePopupOpen += new Bizentro.AppFramework.UI.Controls.Popup.BeforePopupOpenEventHandler(this.popProductID_BeforePopupOpen);
            this.popProductID.AfterPopupClosed += new Bizentro.AppFramework.UI.Controls.Popup.AfterPopupCloseEventHandler(this.popProductID_AfterPopupClosed);
            // 
            // lblOrderType
            // 
            appearance7.TextHAlignAsString = "Left";
            appearance7.TextVAlignAsString = "Middle";
            this.lblOrderType.Appearance = appearance7;
            this.lblOrderType.AutoPopupID = null;
            this.lblOrderType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOrderType.LabelType = Bizentro.AppFramework.UI.Variables.enumDef.LabelType.Title;
            this.lblOrderType.Location = new System.Drawing.Point(15, 29);
            this.lblOrderType.Margin = new System.Windows.Forms.Padding(15, 1, 0, 0);
            this.lblOrderType.Name = "lblOrderType";
            this.lblOrderType.Size = new System.Drawing.Size(102, 22);
            this.lblOrderType.StyleSetName = "Default";
            this.lblOrderType.TabIndex = 13;
            this.lblOrderType.Text = "OrderType";
            this.lblOrderType.UseMnemonic = false;
            // 
            // dtDateTerm
            // 
            this.dtDateTerm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dtDateTerm.DateType = Bizentro.AppFramework.UI.Variables.enumDef.DateType.Default;
            this.dtDateTerm.FieldTypeFrom = Bizentro.AppFramework.UI.Variables.enumDef.FieldType.Default;
            this.dtDateTerm.FieldTypeTo = Bizentro.AppFramework.UI.Variables.enumDef.FieldType.Default;
            this.dtDateTerm.Location = new System.Drawing.Point(534, 7);
            this.dtDateTerm.Margin = new System.Windows.Forms.Padding(0);
            this.dtDateTerm.Name = "dtDateTerm";
            this.dtDateTerm.Size = new System.Drawing.Size(225, 21);
            this.dtDateTerm.Style = Bizentro.AppFramework.UI.Controls.DateTime_Style.Default;
            this.dtDateTerm.TabIndex = 12;
            this.dtDateTerm.uniFromALT = null;
            this.dtDateTerm.uniFromValue = new System.DateTime(2007, 8, 18, 0, 0, 0, 0);
            this.dtDateTerm.uniTabSameValue = false;
            this.dtDateTerm.uniToALT = null;
            this.dtDateTerm.uniToValue = new System.DateTime(2007, 8, 18, 0, 0, 0, 0);
            // 
            // cbOrderType
            // 
            this.cbOrderType.AddEmptyRow = false;
            this.cbOrderType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbOrderType.ComboFrom = "";
            this.cbOrderType.ComboMajorCd = "";
            this.cbOrderType.ComboSelect = "";
            this.cbOrderType.ComboType = Bizentro.AppFramework.UI.Variables.enumDef.ComboType.MajorCode;
            this.cbOrderType.ComboWhere = "";
            this.cbOrderType.DropDownListWidth = -1;
            this.cbOrderType.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
            this.cbOrderType.FieldType = Bizentro.AppFramework.UI.Variables.enumDef.FieldType.Default;
            this.cbOrderType.Location = new System.Drawing.Point(117, 30);
            this.cbOrderType.LockedField = false;
            this.cbOrderType.Margin = new System.Windows.Forms.Padding(0);
            this.cbOrderType.Name = "cbOrderType";
            this.cbOrderType.RequiredField = false;
            this.cbOrderType.Size = new System.Drawing.Size(144, 28);
            this.cbOrderType.Style = Bizentro.AppFramework.UI.Controls.Combo_Style.Default;
            this.cbOrderType.StyleSetName = "Default";
            this.cbOrderType.TabIndex = 10;
            this.cbOrderType.uniALT = null;
            // 
            // lblOrderDate
            // 
            appearance8.TextHAlignAsString = "Left";
            appearance8.TextVAlignAsString = "Middle";
            this.lblOrderDate.Appearance = appearance8;
            this.lblOrderDate.AutoPopupID = null;
            this.lblOrderDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOrderDate.LabelType = Bizentro.AppFramework.UI.Variables.enumDef.LabelType.Title;
            this.lblOrderDate.Location = new System.Drawing.Point(432, 6);
            this.lblOrderDate.Margin = new System.Windows.Forms.Padding(15, 1, 0, 0);
            this.lblOrderDate.Name = "lblOrderDate";
            this.lblOrderDate.Size = new System.Drawing.Size(102, 22);
            this.lblOrderDate.StyleSetName = "Default";
            this.lblOrderDate.TabIndex = 11;
            this.lblOrderDate.Text = "StartDate";
            this.lblOrderDate.UseMnemonic = false;
            // 
            // lblDivision
            // 
            appearance9.TextHAlignAsString = "Left";
            appearance9.TextVAlignAsString = "Middle";
            this.lblDivision.Appearance = appearance9;
            this.lblDivision.AutoPopupID = null;
            this.lblDivision.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDivision.LabelType = Bizentro.AppFramework.UI.Variables.enumDef.LabelType.Title;
            this.lblDivision.Location = new System.Drawing.Point(15, 6);
            this.lblDivision.Margin = new System.Windows.Forms.Padding(15, 1, 0, 0);
            this.lblDivision.Name = "lblDivision";
            this.lblDivision.Size = new System.Drawing.Size(102, 22);
            this.lblDivision.StyleSetName = "Default";
            this.lblDivision.TabIndex = 0;
            this.lblDivision.Text = "DivisionID";
            this.lblDivision.UseMnemonic = false;
            // 
            // cbDivisionID
            // 
            this.cbDivisionID.AddEmptyRow = false;
            this.cbDivisionID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbDivisionID.ComboFrom = "";
            this.cbDivisionID.ComboMajorCd = "";
            this.cbDivisionID.ComboSelect = "";
            this.cbDivisionID.ComboType = Bizentro.AppFramework.UI.Variables.enumDef.ComboType.MajorCode;
            this.cbDivisionID.ComboWhere = "";
            this.cbDivisionID.DropDownListWidth = -1;
            this.cbDivisionID.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
            this.cbDivisionID.FieldType = Bizentro.AppFramework.UI.Variables.enumDef.FieldType.Default;
            this.cbDivisionID.Location = new System.Drawing.Point(117, 5);
            this.cbDivisionID.LockedField = false;
            this.cbDivisionID.Margin = new System.Windows.Forms.Padding(0);
            this.cbDivisionID.Name = "cbDivisionID";
            this.cbDivisionID.RequiredField = false;
            this.cbDivisionID.Size = new System.Drawing.Size(180, 28);
            this.cbDivisionID.Style = Bizentro.AppFramework.UI.Controls.Combo_Style.Default;
            this.cbDivisionID.StyleSetName = "Default";
            this.cbDivisionID.TabIndex = 10;
            this.cbDivisionID.uniALT = null;
            // 
            // uniTBL_MainData
            // 
            this.uniTBL_MainData.AutoFit = false;
            this.uniTBL_MainData.AutoFitColumnCount = 4;
            this.uniTBL_MainData.AutoFitRowCount = 4;
            this.uniTBL_MainData.BackColor = System.Drawing.Color.Transparent;
            this.uniTBL_MainData.BizentroTableLayout = Bizentro.AppFramework.UI.Controls.BizentroTableLayOutStyle.DefaultTableLayout;
            this.uniTBL_MainData.ColumnCount = 1;
            this.uniTBL_MainData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.uniTBL_MainData.Controls.Add(this.uniGrid2, 0, 2);
            this.uniTBL_MainData.Controls.Add(this.uniGrid1, 0, 0);
            this.uniTBL_MainData.DefaultRowSize = 23;
            this.uniTBL_MainData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uniTBL_MainData.EasyBaseBatchType = Bizentro.AppFramework.UI.Controls.EasyBaseTBType.NONE;
            this.uniTBL_MainData.HEIGHT_TYPE_00_REFERENCE = 32F;
            this.uniTBL_MainData.HEIGHT_TYPE_01 = 3F;
            this.uniTBL_MainData.HEIGHT_TYPE_01_CONDITION = 29F;
            this.uniTBL_MainData.HEIGHT_TYPE_02 = 5F;
            this.uniTBL_MainData.HEIGHT_TYPE_02_DATA = 0F;
            this.uniTBL_MainData.HEIGHT_TYPE_03 = 3F;
            this.uniTBL_MainData.HEIGHT_TYPE_03_BOTTOM = 32F;
            this.uniTBL_MainData.HEIGHT_TYPE_04 = 3F;
            this.uniTBL_MainData.Location = new System.Drawing.Point(0, 143);
            this.uniTBL_MainData.Margin = new System.Windows.Forms.Padding(0);
            this.uniTBL_MainData.Name = "uniTBL_MainData";
            this.uniTBL_MainData.PanelType = Bizentro.AppFramework.UI.Variables.enumDef.PanelType.Data;
            this.uniTBL_MainData.RowCount = 3;
            this.uniTBL_MainData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.uniTBL_MainData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.uniTBL_MainData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.uniTBL_MainData.Size = new System.Drawing.Size(836, 404);
            this.uniTBL_MainData.SizeTD5 = 14F;
            this.uniTBL_MainData.SizeTD6 = 36F;
            this.uniTBL_MainData.TabIndex = 0;
            this.uniTBL_MainData.uniLR_SPACE_TYPE = Bizentro.AppFramework.UI.Controls.LR_SPACE_TYPE.LR_SPACE_TYPE_00;
            // 
            // uniGrid2
            // 
            this.uniGrid2.AddEmptyRow = false;
            this.uniGrid2.DirectPaste = false;
            appearance10.BackColor = System.Drawing.SystemColors.Window;
            appearance10.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.uniGrid2.DisplayLayout.Appearance = appearance10;
            this.uniGrid2.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.uniGrid2.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance11.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance11.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance11.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance11.BorderColor = System.Drawing.SystemColors.Window;
            this.uniGrid2.DisplayLayout.GroupByBox.Appearance = appearance11;
            appearance12.ForeColor = System.Drawing.SystemColors.GrayText;
            this.uniGrid2.DisplayLayout.GroupByBox.BandLabelAppearance = appearance12;
            this.uniGrid2.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance13.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance13.BackColor2 = System.Drawing.SystemColors.Control;
            appearance13.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance13.ForeColor = System.Drawing.SystemColors.GrayText;
            this.uniGrid2.DisplayLayout.GroupByBox.PromptAppearance = appearance13;
            this.uniGrid2.DisplayLayout.MaxColScrollRegions = 1;
            this.uniGrid2.DisplayLayout.MaxRowScrollRegions = 1;
            appearance14.BackColor = System.Drawing.SystemColors.Window;
            appearance14.ForeColor = System.Drawing.SystemColors.ControlText;
            this.uniGrid2.DisplayLayout.Override.ActiveCellAppearance = appearance14;
            this.uniGrid2.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.uniGrid2.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.uniGrid2.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance15.BackColor = System.Drawing.SystemColors.Window;
            this.uniGrid2.DisplayLayout.Override.CardAreaAppearance = appearance15;
            appearance16.BorderColor = System.Drawing.Color.Silver;
            appearance16.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.uniGrid2.DisplayLayout.Override.CellAppearance = appearance16;
            this.uniGrid2.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect;
            this.uniGrid2.DisplayLayout.Override.CellPadding = 0;
            appearance17.BackColor = System.Drawing.SystemColors.Control;
            appearance17.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance17.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance17.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance17.BorderColor = System.Drawing.SystemColors.Window;
            this.uniGrid2.DisplayLayout.Override.GroupByRowAppearance = appearance17;
            appearance18.TextHAlignAsString = "Left";
            this.uniGrid2.DisplayLayout.Override.HeaderAppearance = appearance18;
            this.uniGrid2.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.uniGrid2.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance19.BackColor = System.Drawing.SystemColors.Window;
            appearance19.BorderColor = System.Drawing.Color.Silver;
            this.uniGrid2.DisplayLayout.Override.RowAppearance = appearance19;
            this.uniGrid2.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance20.BackColor = System.Drawing.SystemColors.ControlLight;
            this.uniGrid2.DisplayLayout.Override.TemplateAddRowAppearance = appearance20;
            this.uniGrid2.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.uniGrid2.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.uniGrid2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uniGrid2.EnableContextMenu = true;
            this.uniGrid2.EnableGridFilterMenu = false;
            this.uniGrid2.EnableGridInfoContextMenu = true;
            this.uniGrid2.ExceptInExcel = false;
            this.uniGrid2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uniGrid2.gComNumDec = Bizentro.AppFramework.UI.Variables.enumDef.ComDec.Decimal;
            this.uniGrid2.GridStyle = Bizentro.AppFramework.UI.Variables.enumDef.GridStyle.Master;
            this.uniGrid2.Location = new System.Drawing.Point(0, 204);
            this.uniGrid2.Margin = new System.Windows.Forms.Padding(0);
            this.uniGrid2.Name = "uniGrid2";
            this.uniGrid2.OutlookGroupBy = Bizentro.AppFramework.UI.Variables.enumDef.IsOutlookGroupBy.No;
            this.uniGrid2.PopupDeleteMenuVisible = true;
            this.uniGrid2.PopupInsertMenuVisible = true;
            this.uniGrid2.PopupUndoMenuVisible = true;
            this.uniGrid2.Search = Bizentro.AppFramework.UI.Variables.enumDef.IsSearch.Yes;
            this.uniGrid2.ShowHeaderCheck = true;
            this.uniGrid2.Size = new System.Drawing.Size(836, 200);
            this.uniGrid2.StyleSetName = "uniGrid_Query";
            this.uniGrid2.TabIndex = 1;
            this.uniGrid2.Text = "uniGrid2";
            this.uniGrid2.UseDynamicFormat = false;
            // 
            // uniGrid1
            // 
            this.uniGrid1.AddEmptyRow = false;
            this.uniGrid1.DirectPaste = false;
            appearance21.BackColor = System.Drawing.SystemColors.Window;
            appearance21.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.uniGrid1.DisplayLayout.Appearance = appearance21;
            this.uniGrid1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.uniGrid1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance22.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance22.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance22.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance22.BorderColor = System.Drawing.SystemColors.Window;
            this.uniGrid1.DisplayLayout.GroupByBox.Appearance = appearance22;
            appearance23.ForeColor = System.Drawing.SystemColors.GrayText;
            this.uniGrid1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance23;
            this.uniGrid1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance24.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance24.BackColor2 = System.Drawing.SystemColors.Control;
            appearance24.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance24.ForeColor = System.Drawing.SystemColors.GrayText;
            this.uniGrid1.DisplayLayout.GroupByBox.PromptAppearance = appearance24;
            this.uniGrid1.DisplayLayout.MaxColScrollRegions = 1;
            this.uniGrid1.DisplayLayout.MaxRowScrollRegions = 1;
            appearance25.BackColor = System.Drawing.SystemColors.Window;
            appearance25.ForeColor = System.Drawing.SystemColors.ControlText;
            this.uniGrid1.DisplayLayout.Override.ActiveCellAppearance = appearance25;
            this.uniGrid1.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.uniGrid1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.uniGrid1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance26.BackColor = System.Drawing.SystemColors.Window;
            this.uniGrid1.DisplayLayout.Override.CardAreaAppearance = appearance26;
            appearance27.BorderColor = System.Drawing.Color.Silver;
            appearance27.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.uniGrid1.DisplayLayout.Override.CellAppearance = appearance27;
            this.uniGrid1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect;
            this.uniGrid1.DisplayLayout.Override.CellPadding = 0;
            appearance28.BackColor = System.Drawing.SystemColors.Control;
            appearance28.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance28.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance28.BorderColor = System.Drawing.SystemColors.Window;
            this.uniGrid1.DisplayLayout.Override.GroupByRowAppearance = appearance28;
            appearance29.TextHAlignAsString = "Left";
            this.uniGrid1.DisplayLayout.Override.HeaderAppearance = appearance29;
            this.uniGrid1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.uniGrid1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance30.BackColor = System.Drawing.SystemColors.Window;
            appearance30.BorderColor = System.Drawing.Color.Silver;
            this.uniGrid1.DisplayLayout.Override.RowAppearance = appearance30;
            this.uniGrid1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance31.BackColor = System.Drawing.SystemColors.ControlLight;
            this.uniGrid1.DisplayLayout.Override.TemplateAddRowAppearance = appearance31;
            this.uniGrid1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.uniGrid1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.uniGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uniGrid1.EnableContextMenu = true;
            this.uniGrid1.EnableGridFilterMenu = false;
            this.uniGrid1.EnableGridInfoContextMenu = true;
            this.uniGrid1.ExceptInExcel = false;
            this.uniGrid1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uniGrid1.gComNumDec = Bizentro.AppFramework.UI.Variables.enumDef.ComDec.Decimal;
            this.uniGrid1.GridStyle = Bizentro.AppFramework.UI.Variables.enumDef.GridStyle.Master;
            this.uniGrid1.Location = new System.Drawing.Point(0, 0);
            this.uniGrid1.Margin = new System.Windows.Forms.Padding(0);
            this.uniGrid1.Name = "uniGrid1";
            this.uniGrid1.OutlookGroupBy = Bizentro.AppFramework.UI.Variables.enumDef.IsOutlookGroupBy.No;
            this.uniGrid1.PopupDeleteMenuVisible = true;
            this.uniGrid1.PopupInsertMenuVisible = true;
            this.uniGrid1.PopupUndoMenuVisible = true;
            this.uniGrid1.Search = Bizentro.AppFramework.UI.Variables.enumDef.IsSearch.Yes;
            this.uniGrid1.ShowHeaderCheck = true;
            this.uniGrid1.Size = new System.Drawing.Size(836, 199);
            this.uniGrid1.StyleSetName = "uniGrid_Query";
            this.uniGrid1.TabIndex = 0;
            this.uniGrid1.Text = "uniGrid1";
            this.uniGrid1.UseDynamicFormat = false;
            this.uniGrid1.BeforePopupOpen += new Bizentro.AppFramework.UI.Controls.Popup.BeforePopupOpenEventHandler(this.uniGrid1_BeforePopupOpen);
            this.uniGrid1.AfterPopupClosed += new Bizentro.AppFramework.UI.Controls.Popup.AfterPopupCloseEventHandler(this.uniGrid1_AfterPopupClosed);
            this.uniGrid1.DetailBinding += new Bizentro.AppFramework.UI.Controls.uniGrid.DetailBindingEventHandler(this.uniGrid1_DetailBinding);
            this.uniGrid1.BeforeCellUpdate += new Infragistics.Win.UltraWinGrid.BeforeCellUpdateEventHandler(this.uniGrid1_BeforeCellUpdate);
            // 
            // uniTBL_MainReference
            // 
            this.uniTBL_MainReference.AutoFit = false;
            this.uniTBL_MainReference.AutoFitColumnCount = 4;
            this.uniTBL_MainReference.AutoFitRowCount = 4;
            this.uniTBL_MainReference.BackColor = System.Drawing.Color.Transparent;
            this.uniTBL_MainReference.BizentroTableLayout = Bizentro.AppFramework.UI.Controls.BizentroTableLayOutStyle.DefaultTableLayout;
            this.uniTBL_MainReference.ColumnCount = 3;
            this.uniTBL_MainReference.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.uniTBL_MainReference.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.uniTBL_MainReference.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.uniTBL_MainReference.DefaultRowSize = 23;
            this.uniTBL_MainReference.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uniTBL_MainReference.EasyBaseBatchType = Bizentro.AppFramework.UI.Controls.EasyBaseTBType.NONE;
            this.uniTBL_MainReference.HEIGHT_TYPE_00_REFERENCE = 32F;
            this.uniTBL_MainReference.HEIGHT_TYPE_01 = 3F;
            this.uniTBL_MainReference.HEIGHT_TYPE_01_CONDITION = 29F;
            this.uniTBL_MainReference.HEIGHT_TYPE_02 = 5F;
            this.uniTBL_MainReference.HEIGHT_TYPE_02_DATA = 0F;
            this.uniTBL_MainReference.HEIGHT_TYPE_03 = 3F;
            this.uniTBL_MainReference.HEIGHT_TYPE_03_BOTTOM = 32F;
            this.uniTBL_MainReference.HEIGHT_TYPE_04 = 3F;
            this.uniTBL_MainReference.Location = new System.Drawing.Point(0, 0);
            this.uniTBL_MainReference.Margin = new System.Windows.Forms.Padding(0);
            this.uniTBL_MainReference.Name = "uniTBL_MainReference";
            this.uniTBL_MainReference.PanelType = Bizentro.AppFramework.UI.Variables.enumDef.PanelType.Default;
            this.uniTBL_MainReference.RowCount = 1;
            this.uniTBL_MainReference.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.uniTBL_MainReference.Size = new System.Drawing.Size(836, 21);
            this.uniTBL_MainReference.SizeTD5 = 14F;
            this.uniTBL_MainReference.SizeTD6 = 36F;
            this.uniTBL_MainReference.TabIndex = 2;
            this.uniTBL_MainReference.uniLR_SPACE_TYPE = Bizentro.AppFramework.UI.Controls.LR_SPACE_TYPE.LR_SPACE_TYPE_00;
            // 
            // uniTBL_MainBatch
            // 
            this.uniTBL_MainBatch.AutoFit = false;
            this.uniTBL_MainBatch.AutoFitColumnCount = 4;
            this.uniTBL_MainBatch.AutoFitRowCount = 4;
            this.uniTBL_MainBatch.BackColor = System.Drawing.Color.Transparent;
            this.uniTBL_MainBatch.BizentroTableLayout = Bizentro.AppFramework.UI.Controls.BizentroTableLayOutStyle.DefaultTableLayout;
            this.uniTBL_MainBatch.ColumnCount = 5;
            this.uniTBL_MainBatch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.uniTBL_MainBatch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.uniTBL_MainBatch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.uniTBL_MainBatch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.uniTBL_MainBatch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.uniTBL_MainBatch.DefaultRowSize = 23;
            this.uniTBL_MainBatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uniTBL_MainBatch.EasyBaseBatchType = Bizentro.AppFramework.UI.Controls.EasyBaseTBType.NONE;
            this.uniTBL_MainBatch.HEIGHT_TYPE_00_REFERENCE = 32F;
            this.uniTBL_MainBatch.HEIGHT_TYPE_01 = 3F;
            this.uniTBL_MainBatch.HEIGHT_TYPE_01_CONDITION = 29F;
            this.uniTBL_MainBatch.HEIGHT_TYPE_02 = 5F;
            this.uniTBL_MainBatch.HEIGHT_TYPE_02_DATA = 0F;
            this.uniTBL_MainBatch.HEIGHT_TYPE_03 = 3F;
            this.uniTBL_MainBatch.HEIGHT_TYPE_03_BOTTOM = 32F;
            this.uniTBL_MainBatch.HEIGHT_TYPE_04 = 3F;
            this.uniTBL_MainBatch.Location = new System.Drawing.Point(0, 550);
            this.uniTBL_MainBatch.Margin = new System.Windows.Forms.Padding(0);
            this.uniTBL_MainBatch.Name = "uniTBL_MainBatch";
            this.uniTBL_MainBatch.PanelType = Bizentro.AppFramework.UI.Variables.enumDef.PanelType.Default;
            this.uniTBL_MainBatch.RowCount = 1;
            this.uniTBL_MainBatch.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.uniTBL_MainBatch.Size = new System.Drawing.Size(836, 28);
            this.uniTBL_MainBatch.SizeTD5 = 14F;
            this.uniTBL_MainBatch.SizeTD6 = 36F;
            this.uniTBL_MainBatch.TabIndex = 3;
            this.uniTBL_MainBatch.uniLR_SPACE_TYPE = Bizentro.AppFramework.UI.Controls.LR_SPACE_TYPE.LR_SPACE_TYPE_00;
            // 
            // ModuleViewer
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.uniTBL_OuterMost);
            this.MinimumSize = new System.Drawing.Size(0, 0);
            this.Name = "ModuleViewer";
            this.Size = new System.Drawing.Size(851, 609);
            this.Controls.SetChildIndex(this.uniTBL_OuterMost, 0);
            this.Controls.SetChildIndex(this.uniLabel_Path, 0);
            this.uniTBL_OuterMost.ResumeLayout(false);
            this.uniTableLayoutPanel2.ResumeLayout(false);
            this.uniTableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductOrderNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbOrderType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbDivisionID)).EndInit();
            this.uniTBL_MainData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uniGrid2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uniGrid1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Bizentro.AppFramework.UI.Controls.uniTableLayoutPanel uniTBL_OuterMost;
        private Bizentro.AppFramework.UI.Controls.uniTableLayoutPanel uniTBL_MainData;
        private Bizentro.AppFramework.UI.Controls.uniTableLayoutPanel uniTBL_MainReference;
        private Bizentro.AppFramework.UI.Controls.uniTableLayoutPanel uniTBL_MainBatch;
        private Bizentro.AppFramework.UI.Controls.uniGrid uniGrid1;
        private AppFramework.UI.Controls.uniCombo cbOrderType;
        private AppFramework.UI.Controls.uniTableLayoutPanel uniTableLayoutPanel2;
        private AppFramework.UI.Controls.uniDateTerm dtDateTerm;
        private AppFramework.UI.Controls.uniLabel lblOrderDate;
        private AppFramework.UI.Controls.uniLabel lblDivision;
        private AppFramework.UI.Controls.uniCombo cbDivisionID;
        private AppFramework.UI.Controls.uniGrid uniGrid2;
        private AppFramework.UI.Controls.uniCombo cbStatus;
        private AppFramework.UI.Controls.uniOpenPopup popProductID;
        private AppFramework.UI.Controls.uniOpenPopup popLine;
        private AppFramework.UI.Controls.uniTextBox txtProductOrderNumber;
        private AppFramework.UI.Controls.uniOpenPopup popModel;
        private AppFramework.UI.Controls.uniLabel lblProductOrderNumber;
        private AppFramework.UI.Controls.uniLabel lblStatus;
        private AppFramework.UI.Controls.uniLabel lblProduct;
        private AppFramework.UI.Controls.uniLabel lblModel;
        private AppFramework.UI.Controls.uniLabel lblLine;
        private AppFramework.UI.Controls.uniLabel lblOrderType;

    }
}
