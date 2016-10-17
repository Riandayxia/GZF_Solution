/**-----------------------------------------------------------------
* @explanation:数据表编辑页面
* @created：rainday
* @create time：2016-08-09 10:11
* @modified history: //修改历史
/-------------------------------------------------------------------*/

/*
* 数据表编辑页面
*/
Ext.define("BPM.WFModel.Edit", {
    extend: 'Ext.user.NEdit',
    xtype: 'bpm_wfmodel_edit',
    layout: 'fit',
    formSubmit: false,
    modal: true,
    onlySave: true,
    layout: {
        type: 'table',
        columns: 2
    },
    closeAction: 'hide',
    defaults: {
        labelAlign: 'right',
        width: 300
    },
    items: [
        { xtype: 'hiddenfield', fieldLabel: '唯一标识', name: 'Id' },
        { xtype: 'textfield', fieldLabel: '流程名称', name: 'Name' },
        { xtype: 'textfield', fieldLabel: '完成标识', name: 'DBFieldName' },
        { xtype: 'textfield', fieldLabel: '对应表', name: 'DBTableName', colspan: 2, value: 'Test2' },
        { xtype: 'textarea', fieldLabel: '说明', name: 'Desc', colspan: 2, width: 600 },
        { xtype: 'hiddenfield', fieldLabel: '管理人员', name: 'Manager' },
        { xtype: 'hiddenfield', fieldLabel: '实例管理人员', name: 'InstanceManager' },
        { xtype: 'hiddenfield', fieldLabel: '创建人', name: 'CreateUserID', value: '00000000-0000-0000-0000-000000000001' },
        //{ xtype: 'hiddenfield', fieldLabel: '安装日期', name: 'InstallDate', dateFormat: 'Y/m/d ', value: new Date() },
        { xtype: 'hiddenfield', fieldLabel: '安装人员', name: 'InstallUserID' },
        { xtype: 'hiddenfield', fieldLabel: '设计Json', name: 'DesignJSON' },
        { xtype: 'hiddenfield', fieldLabel: '状态', name: 'Status', value: '1' },
        //{ xtype: 'hiddenfield', fieldLabel: '是否删除', name: 'IsDeleted', value: '1' },
        //{ xtype: 'hiddenfield', fieldLabel: '创建时间', name: 'CreatedTime', dateFormat: 'Y/m/d ', value: new Date() },
        //{ xtype: 'hiddenfield', fieldLabel: '修改时间', name: 'LastUpdatedTime', dateFormat: 'Y/m/d ', value: new Date() }
    ]
});
